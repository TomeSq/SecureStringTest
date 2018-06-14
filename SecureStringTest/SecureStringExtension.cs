using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;

namespace SecureStringTest
{
    public static class SecureStringExtension
    {
        /// <summary>
        /// SecureStringの比較を行う
        /// </summary>
        /// <param name="strA">比較対象1</param>
        /// <param name="strB">比較対象2</param>
        /// <returns>同じ場合はtrue/違う場合はfalse</returns>
        public static bool Equals(this SecureString strA, SecureString strB)
        {
            if (strA == null && strB == null)
            { return true; }

            if (strA == null || strB == null)
            { return false; }

            if (strA.Length != strB.Length)
            { return false; }

            var aPtr = Marshal.SecureStringToBSTR(strA);
            var bPtr = Marshal.SecureStringToBSTR(strB);
            try
            {
                return Enumerable.Range(0, strA.Length)
                  .All(i => Marshal.ReadInt16(aPtr, i) == Marshal.ReadInt16(bPtr, i));
            }
            finally
            {
                Marshal.ZeroFreeBSTR(aPtr);
                Marshal.ZeroFreeBSTR(bPtr);
            }
        }

        /// <summary>
        /// BSTRの文字列をSecureStringにコピー
        /// </summary>
        /// <param name="self">SecureString</param>
        /// <param name="bstr">コピー先</param>
        /// <param name="count">文字数</param>
        /// <remarks>
        /// bstrは用が済んだらMarshal.ZeroFreeBSTR()して中身を削除すること
        /// </remarks>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "BSTR")]
        public static void CopyFromBSTR(this SecureString self, IntPtr bstr, int count)
        {
            if(self == null)
            {
                throw new ArgumentNullException(nameof(self), "self cannot be null");
            }

            self.Clear();
            var chars = Enumerable.Range(0, count)
                .Select(i => (char)Marshal.ReadInt16(bstr, i * 2));

            foreach (var c in chars)
            {
                self.AppendChar(c);
            }
        }

        /// <summary>
        /// セキュリティ文字列を平文にする
        /// </summary>
        /// <param name="secureString">SecureString</param>
        /// <returns>平文テキスト</returns>
        public static string SecureStringToText(SecureString secureString)
        {
            if (secureString != null)
            {
                return Marshal.PtrToStringUni(Marshal.SecureStringToGlobalAllocUnicode(secureString));
            }

            return string.Empty;
        }
    }
}
