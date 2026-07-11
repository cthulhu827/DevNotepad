using DevNotepad.Core;

namespace DevNotepad.Infrastructure
{
    public class KeysShortcutConverter : IShortcutConverter
    {
        public int Convert(string shortcut)
        {
            if (string.IsNullOrWhiteSpace(shortcut)) return 0;

            try
            {
                return (int)new KeysConverter().ConvertFromString(shortcut);
            }
            catch
            {
                return 0;
            }
        }
    }
}