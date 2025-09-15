using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using Covalent.Aspire.Hosting.Resources;

namespace Covalent.Aspire.Hosting.HealthChecks;

/// <summary>
/// Health check implementation for Azure Foundry resources.
/// </summary>
public class AzureFoundryHealthCheck : IHealthCheck
{
    private readonly AzureFoundryResource _resource;
    private readonly HttpClient _httpClient;

    public AzureFoundryHealthCheck(AzureFoundryResource resource, HttpClient httpClient)
    {
        _resource = resource;
        _httpClient = httpClient;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        Console.WriteLine("CheckHealthAsync invoked for Azure Foundry resource: " + _resource.Name);
        try
        {
            var uri = _resource.Uri;
            if (string.IsNullOrEmpty(uri))
            {
                return HealthCheckResult.Unhealthy(
                    "Azure Foundry URI is not configured",
                    data: new Dictionary<string, object>
                    {
                        ["ResourceName"] = _resource.Name,
                        ["Error"] = "Missing URI configuration"
                    });
            }

            // Add authorization header if token is available
            if (!string.IsNullOrEmpty(_resource.Token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = 
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _resource.Token);
            }

            // Perform a simple health check by making a request to the management endpoint
            var response = await _httpClient.GetAsync($"{uri.TrimEnd('/')}/health", cancellationToken);
            
            _resource.Status = response.IsSuccessStatusCode ? HealthStatus.Healthy : HealthStatus.Unhealthy;
            _resource.LastStatusCheck = DateTime.UtcNow;
            _resource.StatusData = new Dictionary<string, object>
            {
                ["StatusCode"] = (int)response.StatusCode,
                ["ResponseTime"] = DateTime.UtcNow,
                ["Uri"] = uri
            };

            if (response.IsSuccessStatusCode)
            {
                return HealthCheckResult.Healthy(
                    "Azure Foundry resource is healthy",
                    data: _resource.StatusData);
            }
            else
            {
                return HealthCheckResult.Unhealthy(
                    $"Azure Foundry resource returned status {response.StatusCode}",
                    data: _resource.StatusData);
            }
        }
        catch (HttpRequestException ex)
        {
            _resource.Status = HealthStatus.Unhealthy;
            _resource.LastStatusCheck = DateTime.UtcNow;
            _resource.StatusData = new Dictionary<string, object>
            {
                ["Error"] = ex.Message,
                ["ErrorType"] = "HttpRequestException"
            };

            return HealthCheckResult.Unhealthy(
                $"Failed to connect to Azure Foundry: {ex.Message}",
                ex,
                _resource.StatusData);
        }
        catch (TaskCanceledException ex)
        {
            _resource.Status = HealthStatus.Unhealthy;
            _resource.LastStatusCheck = DateTime.UtcNow;
            _resource.StatusData = new Dictionary<string, object>
            {
                ["Error"] = "Request timeout",
                ["ErrorType"] = "TaskCanceledException"
            };

            return HealthCheckResult.Unhealthy(
                "Azure Foundry health check timed out",
                ex,
                _resource.StatusData);
        }
        catch (Exception ex)
        {
            _resource.Status = HealthStatus.Unhealthy;
            _resource.LastStatusCheck = DateTime.UtcNow;
            _resource.StatusData = new Dictionary<string, object>
            {
                ["Error"] = ex.Message,
                ["ErrorType"] = ex.GetType().Name
            };

            return HealthCheckResult.Unhealthy(
                $"Unexpected error during Azure Foundry health check: {ex.Message}",
                ex,
                _resource.StatusData);
        }
    }
}