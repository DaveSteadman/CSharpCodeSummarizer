// dotnet new console -n CodeSummarizer
// cd CodeSummarizer
// dotnet add package Microsoft.CodeAnalysis
// dotnet add package Microsoft.CodeAnalysis.CSharp
// dotnet build

using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CodeSummarizer
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Usage: CodeSummarizer <directory_path>");
                return;
            }

            string directoryPath = args[0];
            var files = Directory.GetFiles(directoryPath, "*.cs", SearchOption.AllDirectories);
            var summaryLines = new List<string>();

            foreach (var file in files)
            {
                var code = File.ReadAllText(file);
                var tree = CSharpSyntaxTree.ParseText(code);
                var root = tree.GetCompilationUnitRoot();

                foreach (var classDeclaration in root.DescendantNodes().OfType<ClassDeclarationSyntax>())
                {
                    // Add class name
                    summaryLines.Add($"Class: {classDeclaration.Identifier.Text}");

                    // Add attributes
                    foreach (var property in classDeclaration.DescendantNodes().OfType<PropertyDeclarationSyntax>())
                    {
                        summaryLines.Add($"  Attribute: {property.Type} {property.Identifier.Text}");
                    }

                    // Add methods
                    foreach (var method in classDeclaration.DescendantNodes().OfType<MethodDeclarationSyntax>())
                    {
                        var parameters = string.Join(", ", method.ParameterList.Parameters.Select(p => $"{p.Type} {p.Identifier.Text}"));
                        summaryLines.Add($"  Method: {method.ReturnType} {method.Identifier.Text}({parameters})");
                    }
                }
            }

            // Write the summary to a single file
            string outputPath = Path.Combine(directoryPath, "CodeSummary.txt");
            File.WriteAllLines(outputPath, summaryLines);

            Console.WriteLine($"Code summary written to: {outputPath}");
        }
    }
}
