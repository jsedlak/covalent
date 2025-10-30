using Covalent.Agents.Model;
using Covalent.Agents.Providers;
using Covalent.Plugins.Storyblok.Services;

namespace Covalent.Plugins.Storyblok.Tools;

public class ManagementToolProvider : IToolProvider
{
    public IEnumerable<Tool> GetTools()
    {
        return new Tool[] {
            new Tool {
                Id = "query_components",
                Name = "Query Components",
                Description = "Get all components from Storyblok",
                TypeName = nameof(IStoryblokManagementService),
                MethodName = nameof(IStoryblokManagementService.GetComponents)
            },
            new Tool {
                Id = "get_component",
                Name = "Get Component",
                Description = "Get a component from Storyblok",
                TypeName = nameof(IStoryblokManagementService),
                MethodName = nameof(IStoryblokManagementService.GetComponent)
            },
            new Tool {
                Id = "create_component",
                Name = "Create Component",
                Description = "Create a new component in Storyblok",
                TypeName = nameof(IStoryblokManagementService),
                MethodName = nameof(IStoryblokManagementService.CreateComponent)
            }
        };
    }

    public string Id => "storyblok-management";

    public string Name => "Storyblok Management";

    public string Description => "Manage Storyblok components and content";

    public string? Icon => null;
}
