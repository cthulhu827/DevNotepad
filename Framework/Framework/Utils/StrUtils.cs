using System.IO;

namespace Framework.Utils
{
    public static class StrUtils
    {
        public static string CombineUrl(params string[] segments)
        {
            var result = string.Empty;

            for (int i = 0; i < segments.Length; i++)
            {
                if (string.IsNullOrEmpty(segments[i]))
                {
                    continue;
                }

                if (i != 0)
                {
                    result += '/';
                }

                result += segments[i].Trim('/');
            }

            return result;
        }

        public static string ParentFolder(
#if NET_20
#else
            this 
#endif
            string path, int levels = 1)
        {
            var result = path;
            for (int i = 0; i < levels; i++)
            {
                result = Directory.GetParent(result).FullName;
            }
            return result;
        }
    }
}