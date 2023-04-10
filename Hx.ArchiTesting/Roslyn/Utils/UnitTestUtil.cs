using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using SyntaxTree = Microsoft.CodeAnalysis.SyntaxTree;

namespace Hx.ArchTests.Roslyn.Utils
{
    public class UnitTestUtil
    {
        public static readonly CSharpCompilationOptions DefaultCompilationOptions =
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary);
        
        public static CSharpCompilation getCompilation(SyntaxNode node)
        {
            var directory = System.Runtime.InteropServices.RuntimeEnvironment.GetRuntimeDirectory();
            var defaultReferences = Directory.GetFiles(directory).Where(s => s.EndsWith(".dll"))
                .Select(s => MetadataReference.CreateFromFile(s));

            var compilation = CSharpCompilation.Create("Test.dll", new SyntaxTree[] { node.SyntaxTree }, defaultReferences,
                DefaultCompilationOptions);
            return compilation;
        }

        public static SyntaxNode createNodeRoot(string content)
        {
            SyntaxTree tree = CSharpSyntaxTree.ParseText(content);
            var nodeRoot = tree.GetRoot();
            return nodeRoot;
        }
        
    }
}

