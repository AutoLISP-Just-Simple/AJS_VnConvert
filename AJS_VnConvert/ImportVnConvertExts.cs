using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace AJS_VnConvert
{
    internal static class ImportVnConvertExts
    {
        public const string vnconv = "vnconv.dll";

        [DllImport(vnconv, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.BStr)]
        private static extern string VnConvertString(int inCharset, int outCharset, byte[] input, int inlen);

        public static string ConvertString(this string input, int inCharset, int outCharset)
        {
            byte[] bytes = (inCharset <= 1 ? Encoding.UTF8 : Encoding.Default).GetBytes(input);
            var ret = VnConvertString(inCharset, outCharset, bytes, bytes.Length);
            return ret;
        }

        [DllImport(vnconv, CallingConvention = CallingConvention.Cdecl)]
        public static extern int VnFileConvert(int inCharset, int outCharset, string input, string output);

        public static string ConvertStringUsingFile(this string clipboard, int inCharset, int outCharset)
        {
            var inf = Path.GetTempFileName();
            var outf = Path.GetTempFileName();

            if (inCharset > 1)
                File.WriteAllText(inf, clipboard, Encoding.Default);
            else
                File.WriteAllText(inf, clipboard);

            string str = clipboard;

            VnFileConvert(inCharset, outCharset, inf, outf);

            File.Delete(inf);
            if (File.Exists(outf))
            {
                str = File.ReadAllText(outf, outCharset <= 1 ? Encoding.UTF8 : System.Text.Encoding.Default);
                File.Delete(outf);
            }

            return str;
        }
    }
}