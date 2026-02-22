using System.Windows.Forms;
using Framework.UI.Properties;

namespace Framework.UI
{
    public static class SystemImages
    {
        public const int iiInfo = 0;
        public const int iiOk = 1;
        public const int iiWarning = 2;
        public const int iiError = 3;
        public const int iiCheckChecked = 4;
        public const int iiCheckUnchecked = 5;

        private static ImageList size16;

        public static ImageList Size16
        {
            get
            {
                if (size16 == null)
                {
                    size16 = new ImageList();
                    size16.Images.Add(Resource.info_16);
                    size16.Images.Add(Resource.ok_16);
                    size16.Images.Add(Resource.warning_16);
                    size16.Images.Add(Resource.error_16);
                    size16.Images.Add(Resource.check_checked);
                    size16.Images.Add(Resource.check_unchecked);
                }

                return size16;
            }
        }

        private static ImageList size32;

        public static ImageList Size32
        {
            get
            {
                if (size32 == null)
                {
                    size32 = new ImageList();
                    size32.Images.Add(Resource.info_32);
                    size32.Images.Add(Resource.ok_32);
                    size32.Images.Add(Resource.warning_32);
                    size32.Images.Add(Resource.error_32);
                }

                return size32;
            }
        }
    }
}