using ResourceProvisioner.Application.ResourceRun.Commands.CreateResourceRun;
using ResourceProvisioner.Domain.Entities;
using FluentAssertions;
using FluentValidation;

namespace ResourceProvisioner.Application.IntegrationTests.ResourceRun.Commands;

using static Testing;
public class CreateResourceRunTests
{

    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        var command = new CreateResourceRunCommand();
        
        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }
    
    [Test]
    [Ignore("Incomplete functionality")]
    public async Task ShouldCreateResourceRun()
    {
        await RunAsDefaultUserAsync();
        
        var command = new CreateResourceRunCommand
        {
            Templates = new List<DataHubTemplate>
            {
                new()
                {
                    Name = "azure-storage-blob",
                    Version = "latest"
                }
            },
            Workspace = new Workspace
            {
                Acronym = "TEST",
                Name = "Test Project",
                Organization = new Organization
                {
                    Name = "SBDA Number 42",
                    Code = "SBDA-42"
                }
            }
        };
        
        var id = await SendAsync(command);
        Assert.That(id, Is.Not.Null);
    }
}