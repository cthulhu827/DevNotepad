using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Framework.Utils
{
    [Obsolete]
    public static class StrUtilsOld
    {
        public static string AppFileName()
        {
            return System.Reflection.Assembly.GetEntryAssembly().Location;
        }

        public static string AppPath()
        {
            return CheckSlash(Path.GetDirectoryName(AppFileName()));
        }

        public static string CheckSlash(string s)
        {
            return s.EndsWith(@"\") ? s : s + @"\";
        }

        /// <param name="number">Число</param>
        /// <param name="nominative">Страница (им.п., ед.ч.)</param>
        /// <param name="genitiveSingular">Страницы (род.п., ед.ч.)</param>
        /// <param name="genitivePlural">Страниц (род.п., мн.ч.)</param>
        public static string GetCase(int number, string nominative, string genitiveSingular, string genitivePlural)
        {
            int lastDigit = number % 10;
            int lastTwoDigits = number % 100;

            if ((lastDigit == 1) && (lastTwoDigits != 11))
            {
                return nominative;
            }

            if (((lastDigit == 2) && (lastTwoDigits != 12)) ||
                ((lastDigit == 3) && (lastTwoDigits != 13)) ||
                ((lastDigit == 4) && (lastTwoDigits != 14)))
            {
                return genitiveSingular;
            }
            else
            {
                return genitivePlural;
            }
        }

        public static float ToFloat(string s)
        {
            return float.Parse(s.Replace('.', ','));
        }

        public static float ToFloat(string s, float defaultValue)
        {
            try
            {
                return float.Parse(s.Replace('.', ','));
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        public static string TempFileName(string path, string extension)
        {
            int i = 1;
            while (true)
            {
                var fileName = Path.Combine(path, string.Format("{0}{1}", i.ToString(), extension));
                if (!File.Exists(fileName))
                {
                    return fileName;
                }

                i++;
            }
        }
    }
}
