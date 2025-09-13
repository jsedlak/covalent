using System;

namespace Covalent.Runtime;

[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
public class ServiceBuilderAttribute : Attribute
{
    public ServiceBuilderAttribute(string category, string name, Type builderType)
    {
        Category = category;
        Name = name;
        BuilderType = builderType;
    }

    /// <summary>
    /// The category of the service builder (e.g. "Provider", "Importer", "Exporter")
    /// </summary>
    public string Category { get; set; } = null!;

    /// <summary>
    /// The name of the service builder (e.g. "AzureFoundry", "OpenAI")
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// The type that provides service registration and dependency injection
    /// </summary>
    public Type BuilderType { get; set; } = null!;
}
