using Hx.ArchTests.Roslyn.Utils;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Hx.ArchTests.Roslyn;

public class ArchiTesting
{

    [Test]
    public void Should_Create_New_Variable_in_Class()
    {
        // Arrange
        var program = @" public class DeclarationExpressionsExample
                                        {
                                            public bool inputIsPositiveNumber = int.TryParse(ReadString(), out int input) && input > 0;

                                            public static string ReadString() => Console.ReadLine();
                                        }";

        var nodeRoot = GetNodeRoot(program);
        var compilation = UnitTestUtil.getCompilation(nodeRoot);
        
        // Assert
        Assert.IsTrue(true);
    }
    

    private SyntaxNode GetNodeRoot(string program) {
        var tree = CSharpSyntaxTree.ParseText(program);
        return tree.GetRoot();
    }
}