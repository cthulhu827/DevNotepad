using DevNotepad.Core.TextTransformers;
using NUnit.Framework;

namespace DevNotepad.Core.Tests.TextTransformers
{
    [TestFixture]
    public class GrepTransformerTests
    {
        #region Edge cases: empty input / empty search text

        [Test]
        public void Transform_EmptyLines_ReturnsSameEmptyArray()
        {
            var transformer = new GrepTransformer("test");
            var result = transformer.Transform(new string[0]);
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void Transform_NullSearchText_ReturnsLinesAsIs()
        {
            var transformer = new GrepTransformer(null!);
            var lines = new[] { "line1", "line2" };
            var result = transformer.Transform(lines);
            Assert.That(result, Is.EqualTo(lines));
        }

        [Test]
        public void Transform_WhiteSpaceSearchText_ReturnsLinesAsIs()
        {
            var transformer = new GrepTransformer("   ");
            var lines = new[] { "line1", "line2" };
            var result = transformer.Transform(lines);
            Assert.That(result, Is.EqualTo(lines));
        }

        [Test]
        public void Transform_DefaultConstructor_EmptySearchText_ReturnsLinesAsIs()
        {
            var transformer = new GrepTransformer();
            var lines = new[] { "line1", "line2" };
            var result = transformer.Transform(lines);
            Assert.That(result, Is.EqualTo(lines));
        }

        #endregion

        #region Plain text search

        [Test]
        public void Transform_PlainSearch_NoMatches_ReturnsEmptyArray()
        {
            var transformer = new GrepTransformer("zzz");
            var lines = new[] { "aaa", "bbb", "ccc" };
            var result = transformer.Transform(lines);
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void Transform_PlainSearch_SingleMatch_ReturnsThatLine()
        {
            var transformer = new GrepTransformer("bbb");
            var lines = new[] { "aaa", "bbb", "ccc" };
            var result = transformer.Transform(lines);
            Assert.That(result, Is.EqualTo(new[] { "bbb" }));
        }

        [Test]
        public void Transform_PlainSearch_MultipleMatches_ReturnsAllMatchingLines()
        {
            var transformer = new GrepTransformer("a");
            var lines = new[] { "abc", "def", "agh", "xyz", "a" };
            var result = transformer.Transform(lines);
            Assert.That(result, Is.EqualTo(new[] { "abc", "agh", "a" }));
        }

        [Test]
        public void Transform_PlainSearch_ByDefault_CaseInsensitive()
        {
            var transformer = new GrepTransformer("HELLO");
            var lines = new[] { "hello world", "HELLO WORLD", "Hello World" };
            var result = transformer.Transform(lines);
            Assert.That(result, Is.EqualTo(new[] { "hello world", "HELLO WORLD", "Hello World" }));
        }

        [Test]
        public void Transform_PlainSearch_CaseSensitive_MatchesOnlyExactCase()
        {
            var transformer = new GrepTransformer("HELLO") { CaseSensitive = true };
            var lines = new[] { "hello world", "HELLO WORLD", "Hello World" };
            var result = transformer.Transform(lines);
            Assert.That(result, Is.EqualTo(new[] { "HELLO WORLD" }));
        }

        [Test]
        public void Transform_PlainSearch_SubstringMatch()
        {
            var transformer = new GrepTransformer("lo");
            var lines = new[] { "hello", "world", "alone" };
            var result = transformer.Transform(lines);
            Assert.That(result, Is.EqualTo(new[] { "hello", "alone" }));
        }

        #endregion

        #region RegEx search

        [Test]
        public void Transform_RegEx_BasicMatch()
        {
            var transformer = new GrepTransformer(@"\d+") { RegEx = true };
            var lines = new[] { "abc", "123", "def", "456" };
            var result = transformer.Transform(lines);
            Assert.That(result, Is.EqualTo(new[] { "123", "456" }));
        }

        [Test]
        public void Transform_RegEx_NoMatches_ReturnsEmptyArray()
        {
            var transformer = new GrepTransformer(@"\d+") { RegEx = true };
            var lines = new[] { "abc", "def" };
            var result = transformer.Transform(lines);
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void Transform_RegEx_WithGroup_ReturnsGroupValue()
        {
            var transformer = new GrepTransformer(@"id=(\d+)") { RegEx = true };
            var lines = new[] { "user id=42 name=John", "no match", "item id=99" };
            var result = transformer.Transform(lines);
            Assert.That(result, Is.EqualTo(new[] { "42", "99" }));
        }

        [Test]
        public void Transform_RegEx_WithMultipleGroups_ReturnsFirstGroupValue()
        {
            var transformer = new GrepTransformer(@"(\d+)-(\w+)") { RegEx = true };
            var lines = new[] { "123-abc", "no match" };
            var result = transformer.Transform(lines);
            Assert.That(result, Is.EqualTo(new[] { "123" }));
        }

        [Test]
        public void Transform_InvalidRegEx_ReturnsErrorMessage()
        {
            var transformer = new GrepTransformer(@"[invalid") { RegEx = true };
            var lines = new[] { "any", "lines" };
            var result = transformer.Transform(lines);
            Assert.That(result, Is.EqualTo(new[] { "Invalid RegEx: [invalid" }));
        }

        [Test]
        public void Transform_RegEx_DotStar_MatchesAll()
        {
            var transformer = new GrepTransformer(@".*") { RegEx = true };
            var lines = new[] { "abc", "def", "ghi" };
            var result = transformer.Transform(lines);
            Assert.That(result, Is.EqualTo(new[] { "abc", "def", "ghi" }));
        }

        #endregion

        #region Exclude mode

        [Test]
        public void Transform_Exclude_ExcludesMatchingLines()
        {
            var transformer = new GrepTransformer("bbb") { Exclude = true };
            var lines = new[] { "aaa", "bbb", "ccc", "bbb", "ddd" };
            var result = transformer.Transform(lines);
            Assert.That(result, Is.EqualTo(new[] { "aaa", "ccc", "ddd" }));
        }

        [Test]
        public void Transform_Exclude_NoMatches_ReturnsAllLines()
        {
            var transformer = new GrepTransformer("zzz") { Exclude = true };
            var lines = new[] { "aaa", "bbb", "ccc" };
            var result = transformer.Transform(lines);
            Assert.That(result, Is.EqualTo(new[] { "aaa", "bbb", "ccc" }));
        }

        [Test]
        public void Transform_Exclude_AllMatch_ReturnsEmptyArray()
        {
            var transformer = new GrepTransformer("a") { Exclude = true };
            var lines = new[] { "abc", "defa", "a" };
            var result = transformer.Transform(lines);
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void Transform_Exclude_WithRegEx()
        {
            var transformer = new GrepTransformer(@"\d+") { Exclude = true, RegEx = true };
            var lines = new[] { "abc", "123", "def", "456" };
            var result = transformer.Transform(lines);
            Assert.That(result, Is.EqualTo(new[] { "abc", "def" }));
        }

        [Test]
        public void Transform_Exclude_IgnoresLinesBeforeAndAfter()
        {
            var transformer = new GrepTransformer("bbb") { Exclude = true, LinesBefore = 2, LinesAfter = 2 };
            var lines = new[] { "aaa", "before1", "bbb", "after1", "ccc" };
            var result = transformer.Transform(lines);
            // Exclude mode ignores LinesBefore/LinesAfter, so only non-matching lines are returned
            Assert.That(result, Is.EqualTo(new[] { "aaa", "before1", "after1", "ccc" }));
        }

        #endregion

        #region LinesBefore / LinesAfter

        [Test]
        public void Transform_LinesBefore_IncludesSpecifiedNumberOfPreviousLines()
        {
            var transformer = new GrepTransformer("match") { LinesBefore = 2 };
            var lines = new[] { "a", "b", "c", "match", "d", "e" };
            var result = transformer.Transform(lines);
            Assert.That(result, Is.EqualTo(new[] { "b", "c", "match", "" }));
        }

        [Test]
        public void Transform_LinesAfter_IncludesSpecifiedNumberOfFollowingLines()
        {
            var transformer = new GrepTransformer("match") { LinesAfter = 2 };
            var lines = new[] { "a", "b", "match", "c", "d", "e" };
            var result = transformer.Transform(lines);
            Assert.That(result, Is.EqualTo(new[] { "match", "c", "d", "" }));
        }

        [Test]
        public void Transform_LinesBeforeAndAfter_IncludesBothSides()
        {
            var transformer = new GrepTransformer("match") { LinesBefore = 1, LinesAfter = 1 };
            var lines = new[] { "a", "before", "match", "after", "b" };
            var result = transformer.Transform(lines);
            Assert.That(result, Is.EqualTo(new[] { "before", "match", "after", "" }));
        }

        [Test]
        public void Transform_LinesBefore_OutOfBounds_ClampsToZero()
        {
            var transformer = new GrepTransformer("match") { LinesBefore = 10 };
            var lines = new[] { "a", "match", "b" };
            var result = transformer.Transform(lines);
            Assert.That(result, Is.EqualTo(new[] { "a", "match", "" }));
        }

        [Test]
        public void Transform_LinesAfter_OutOfBounds_ClampsToEnd()
        {
            var transformer = new GrepTransformer("match") { LinesAfter = 10 };
            var lines = new[] { "a", "match", "b" };
            var result = transformer.Transform(lines);
            Assert.That(result, Is.EqualTo(new[] { "match", "b" }));
        }

        [Test]
        public void Transform_MultipleMatchesWithLinesAround_SeparatesGroupsWithEmptyLine()
        {
            var transformer = new GrepTransformer("match") { LinesBefore = 1, LinesAfter = 1 };
            var lines = new[] { "a", "x1", "match", "y1", "b", "x2", "match", "y2", "c" };
            var result = transformer.Transform(lines);
            Assert.That(result, Is.EqualTo(new[] { "x1", "match", "y1", "", "x2", "match", "y2", "" }));
        }

        [Test]
        public void Transform_OverlappingGroups_NoSeparatorAdded()
        {
            var transformer = new GrepTransformer("match") { LinesBefore = 3, LinesAfter = 3 };
            var lines = new[] { "a", "b", "match1", "c", "match2", "d", "e" };
            var result = transformer.Transform(lines);
            // Groups overlap, so no empty line separator between them
            Assert.That(result, Is.EqualTo(new[] { "a", "b", "match1", "c", "match2", "d", "e" }));
        }

        [Test]
        public void Transform_AdjacentGroups_NoSeparatorAdded()
        {
            var transformer = new GrepTransformer("match") { LinesAfter = 1 };
            var lines = new[] { "match1", "a", "match2", "b" };
            var result = transformer.Transform(lines);
            // match1 => match1,a  and match2 => match2,b are adjacent, so no separator
            Assert.That(result, Is.EqualTo(new[] { "match1", "a", "match2", "b" }));
        }

        [Test]
        public void Transform_LinesBefore_Zero_DoesNotAddPreviousLines()
        {
            var transformer = new GrepTransformer("match") { LinesBefore = 0, LinesAfter = 1 };
            var lines = new[] { "a", "match", "b", "c" };
            var result = transformer.Transform(lines);
            Assert.That(result, Is.EqualTo(new[] { "match", "b", "" }));
        }

        [Test]
        public void Transform_LinesAfter_Zero_DoesNotAddFollowingLines()
        {
            var transformer = new GrepTransformer("match") { LinesBefore = 1, LinesAfter = 0 };
            var lines = new[] { "a", "match", "b", "c" };
            var result = transformer.Transform(lines);
            Assert.That(result, Is.EqualTo(new[] { "a", "match", "" }));
        }

        #endregion

        #region Combined scenarios

        [Test]
        public void Transform_RegExWithLinesAround()
        {
            var transformer = new GrepTransformer(@"\d+") { RegEx = true, LinesBefore = 1, LinesAfter = 1 };
            var lines = new[] { "a", "b", "123", "c", "d" };
            var result = transformer.Transform(lines);
            Assert.That(result, Is.EqualTo(new[] { "b", "123", "c", "" }));
        }

        [Test]
        public void Transform_RegExWithGroupAndLinesAround()
        {
            var transformer = new GrepTransformer(@"id=(\d+)") { RegEx = true, LinesBefore = 1, LinesAfter = 1 };
            var lines = new[] { "before", "id=42 name=John", "after", "gap", "before2", "id=99", "after2" };
            var result = transformer.Transform(lines);
            // Group 1 values are returned for the matched lines, surrounding lines are returned as-is
            Assert.That(result, Is.EqualTo(new[] { "before", "42", "after", "", "before2", "99", "after2" }));
        }

        [Test]
        public void Transform_FirstLineMatch_NoLinesBefore()
        {
            var transformer = new GrepTransformer("match") { LinesBefore = 2 };
            var lines = new[] { "match", "a", "b" };
            var result = transformer.Transform(lines);
            Assert.That(result, Is.EqualTo(new[] { "match", "" }));
        }

        [Test]
        public void Transform_LastLineMatch_NoLinesAfter()
        {
            var transformer = new GrepTransformer("match") { LinesAfter = 2 };
            var lines = new[] { "a", "b", "match" };
            var result = transformer.Transform(lines);
            Assert.That(result, Is.EqualTo(new[] { "match" }));
        }

        #endregion
    }
}
