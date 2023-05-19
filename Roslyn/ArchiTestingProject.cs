using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editing;
using Microsoft.CodeAnalysis.MSBuild;

namespace Hx.ArchTests.Roslyn;

public class ArchiTestingProject
{
    [Test]
    public async Task The_Parameters_Of_Service_Methods_Should_End_With_DTO()
    {
        // Arrange
        string projectPath = @"C:\projects\hat\ArchTestWithRoslyn\test\test.csproj";
        MSBuildLocator.RegisterDefaults();
        using (var workspace = MSBuildWorkspace.Create())
        {
            var project = await workspace.OpenProjectAsync(projectPath);
            var compilation = await project.GetCompilationAsync();
            foreach (var syntaxTree in compilation.SyntaxTrees)
            {
                var nodeRoot = syntaxTree.GetRoot();
                var fields = nodeRoot.DescendantNodes()
                    .OfType<FieldDeclarationSyntax>()
                    .Where(field => field.Modifiers.Any(modify => modify.Kind().Equals(SyntaxKind.PrivateKeyword))
                    && field.Declaration.Variables.Any(aVar => !aVar.Identifier.ValueText.StartsWith("_"))
                    );
        
                // Assert
                Assert.IsTrue(fields.Any());
            }

        }
        
    }
    
    
}