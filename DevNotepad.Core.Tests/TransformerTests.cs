using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using DevNotepad.Core.TextTransformers;
using NUnit.Framework;

namespace DevNotepad.Core.Tests.TextTransformers
{
    [TestFixture]
    public class TransformerTests
    {
        [TestCaseSource(nameof(ReadTestCases))]
        public void TestApiMethodUrlTransformer(string input, string expected)
        {
            var transformer = new ApiMethodUrlTransformer();
            var actual = transformer.Transform(new[] { input }).Single();
            Assert.AreEqual(expected, actual);
        }

        private static IEnumerable<TestCaseData> ReadTestCases()
        {
            var assembly = Assembly.GetExecutingAssembly();
            const string resourceName = "DevNotepad.Core.Tests.ApiMethodUrlTestCases.txt";
            using var stream = assembly.GetManifestResourceStream(resourceName)!;
            using var reader = new StreamReader(stream);

            string? input = null;
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                {
                    input = null;
                    continue;
                }

                if (input == null)
                {
                    input = line;
                }
                else
                {
                    yield return new TestCaseData(input, line).SetName(input);
                    input = null;
                }
            }
        }
    }
}