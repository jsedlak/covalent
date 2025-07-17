using Microsoft.VisualStudio.TestTools.UnitTesting;
using Covalent.Agents.Services;
using Covalent.Agents.Model;

namespace Covalent.Tests.Agents.Services;

[TestClass]
public class AgentFileAgentImporterTests
{
    private AgentFileAgentImporter _importer = null!;
    private string _testFilePath = null!;

    [TestInitialize]
    public void Setup()
    {
        _importer = new AgentFileAgentImporter();
        
        // Find the test file in the output directory
        var baseDir = AppDomain.CurrentDomain.BaseDirectory;
        _testFilePath = Path.Combine(baseDir, "Samples", "customer_service.af");
        
        // If not found in base directory, try to find it relative to the project
        if (!File.Exists(_testFilePath))
        {
            var projectDir = FindProjectDirectory();
            if (projectDir != null)
            {
                _testFilePath = Path.Combine(projectDir, "Samples", "customer_service.af");
            }
        }
    }
    
    private string? FindProjectDirectory()
    {
        var currentDir = Directory.GetCurrentDirectory();
        
        // Walk up the directory tree to find the project directory
        while (currentDir != null)
        {
            var testFile = Path.Combine(currentDir, "Samples", "customer_service.af");
            if (File.Exists(testFile))
            {
                return currentDir;
            }
            
            currentDir = Directory.GetParent(currentDir)?.FullName;
        }
        
        return null;
    }

    [TestMethod]
    public async Task ImportAgent_WithValidFile_ShouldReturnAgentDefinition()
    {
        // Arrange
        var agentName = "customer_service";
        var properties = new Dictionary<string, string>
        {
            ["file"] = _testFilePath
        };

        // Act
        var result = await _importer.ImportAgent(agentName, properties);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual("customer_service", result.Name);
        Assert.AreEqual("An agent that always searches the conversation history before responding", result.Description);
        Assert.AreEqual("memgpt_agent", result.AgentType);
        Assert.IsNotNull(result.CoreMemory);
        Assert.AreEqual(2, result.CoreMemory.Count);
        Assert.IsNotNull(result.EmbeddingConfig);
        Assert.IsNotNull(result.LlmConfig);
        Assert.IsNotNull(result.Messages);
        Assert.IsTrue(result.Messages.Count > 0);
        Assert.IsNotNull(result.Tools);
        Assert.IsTrue(result.Tools.Count > 0);
    }

    [TestMethod]
    public async Task ImportAgent_WithMissingFileProperty_ShouldThrowArgumentException()
    {
        // Arrange
        var agentName = "test_agent";
        var properties = new Dictionary<string, string>();

        // Act & Assert
        await Assert.ThrowsExceptionAsync<ArgumentException>(
            async () => await _importer.ImportAgent(agentName, properties));
    }

    [TestMethod]
    public async Task ImportAgent_WithEmptyFileProperty_ShouldThrowArgumentException()
    {
        // Arrange
        var agentName = "test_agent";
        var properties = new Dictionary<string, string>
        {
            ["file"] = ""
        };

        // Act & Assert
        await Assert.ThrowsExceptionAsync<ArgumentException>(
            async () => await _importer.ImportAgent(agentName, properties));
    }

    [TestMethod]
    public async Task ImportAgent_WithNonExistentFile_ShouldThrowFileNotFoundException()
    {
        // Arrange
        var agentName = "test_agent";
        var properties = new Dictionary<string, string>
        {
            ["file"] = "non_existent_file.af"
        };

        // Act & Assert
        await Assert.ThrowsExceptionAsync<FileNotFoundException>(
            async () => await _importer.ImportAgent(agentName, properties));
    }

    [TestMethod]
    public async Task ImportAgent_VerifyDetailedProperties()
    {
        // Arrange
        var agentName = "customer_service";
        var properties = new Dictionary<string, string>
        {
            ["file"] = _testFilePath
        };

        // Act
        var result = await _importer.ImportAgent(agentName, properties);

        // Assert - Verify core memory
        Assert.AreEqual("human", result.CoreMemory[0].Label);
        Assert.AreEqual("persona", result.CoreMemory[1].Label);
        Assert.IsTrue(result.CoreMemory[0].Value.Contains("customer support issue"));
        Assert.IsTrue(result.CoreMemory[1].Value.Contains("ANNA"));

        // Assert - Verify embedding config
        Assert.AreEqual("openai", result.EmbeddingConfig.EmbeddingEndpointType);
        Assert.AreEqual("text-embedding-ada-002", result.EmbeddingConfig.EmbeddingModel);
        Assert.AreEqual(1536, result.EmbeddingConfig.EmbeddingDim);

        // Assert - Verify LLM config
        Assert.AreEqual("gpt-4o-mini", result.LlmConfig.Model);
        Assert.AreEqual("openai", result.LlmConfig.ModelEndpointType);
        Assert.AreEqual(32000, result.LlmConfig.ContextWindow);
        Assert.AreEqual(0.7, result.LlmConfig.Temperature);

        // Assert - Verify tools
        var toolNames = result.Tools.Select(t => t.Name).ToList();
        Assert.IsTrue(toolNames.Contains("conversation_search"));
        Assert.IsTrue(toolNames.Contains("send_message"));
        Assert.IsTrue(toolNames.Contains("check_order_status"));
        Assert.IsTrue(toolNames.Contains("cancel_order"));
    }

    public TestContext? TestContext { get; set; }
} 