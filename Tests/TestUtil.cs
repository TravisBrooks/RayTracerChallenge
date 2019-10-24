using System;
using System.Linq;

namespace Tests
{
    public static class TestUtil
    {
        /// <summary>
        /// splits a given string into lines in array
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string[] AsLines(this string str)
        {
            var lines = str.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            return lines;
        }

        /// <summary>
        /// This goofy fn is just to match the syntax in the book's tests of saying a ppm file's lines from 1-3 or 4-7 should match expected
        /// </summary>
        /// <param name="str"></param>
        /// <param name="startLineIdx">1 based line index to start</param>
        /// <param name="endLineIdx">1 based line index to end</param>
        /// <returns></returns>
        public static string SliceLines(this string str, int startLineIdx, int endLineIdx)
        {
            var lines = str.AsLines();
            var sampleLines = lines.Skip(startLineIdx - 1).Take(endLineIdx - startLineIdx + 1);
            var sampleStr = string.Join(Environment.NewLine, sampleLines);
            return sampleStr;
        }
    }
}
