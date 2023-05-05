using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Hx.ArchTests.Roslyn;

public class ArchiTesting
{

    [Test]
    public void The_Parameters_Of_Service_Methods_Should_End_With_DTO()
    {
        // Arrange
        var program = @" public class ModelClassExample
                         {
                         } 

                         public class ExampleService
                         {
                             public bool Test1Method() => true;
                             public bool Test2Method(ModelClassExample example) => true;
                         }";

        var nodeRoot = GetNodeRoot(program);
        var classes = nodeRoot.DescendantNodes()
            .OfType<ClassDeclarationSyntax>()
            .Where(clazz => clazz.Identifier
                                .ToString().EndsWith("Service")
                            && clazz.DescendantNodes()
                                .OfType<MethodDeclarationSyntax>()
                                .Any(method => method.ParameterList.Parameters
                                    .Any(
                                    parameter => !parameter.Identifier.ToString().EndsWith("DTO")
                                )));
        
        // Assert
        Assert.IsTrue(classes.Any());
    }
    
    
    [Test]
    public void The_Parameters_Of_Service_Methods_Should_End_With_DTO2()
    {
        // Arrange
        var program = @" public class ModelClassExample
                         {
                         } 

                         public class ExampleService
                         {
                             public bool Test1Method() => true;
                             public bool Test2Method(ModelClassExample exampleDTO) => true;
                         }";

        var nodeRoot = GetNodeRoot(program);
        var classes = nodeRoot.DescendantNodes().OfType<ClassDeclarationSyntax>()
            .Where(clazz => clazz.Identifier.ToString().EndsWith("Service")
            && clazz.DescendantNodes()
                .OfType<MethodDeclarationSyntax>().Any(method => method.ParameterList.Parameters.Any(
                    parameter => parameter.Identifier.ToString().EndsWith("DTO")
                )));
        
        // Assert
        Assert.IsTrue(classes.Any());
    }

    private SyntaxNode GetNodeRoot(string program) {
        var tree = CSharpSyntaxTree.ParseText(program);
        return tree.GetRoot();
    }
}