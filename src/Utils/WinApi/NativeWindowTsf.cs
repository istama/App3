using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace IsTama.Utils
{
    /// <summary>
    /// COMコンポーネントを利用したIMEのAPI。
    /// しかし、インターフェイスへのキャストで失敗する。
    /// </summary>
    public static class NativeWindowTsf
    {
        public static readonly string AlphaNumericGuid = "B2F9C502-1742-11D4-9790-0080C882687E";
        public static readonly string HiraganaGuid     = "FEE9896C-2D75-4738-BDE5-F0472C17ED05";
        public static readonly string KatakanaGuid     = "1B7C2B79-BB8C-4AA6-96F1-36EA6A6D032D";
        public static readonly string EisujiGuid       = "037B2C25-480C-4D7F-B027-D6CA6B69788A";

        [DllImport("msctf.dll")]
        public static extern int TF_CreateInputProcessorProfiles(out ITfInputProcessorProfiles ppipr);
        [DllImport("msctf.dll")]
        public static extern int TF_CreateInputProcessorProfiles(out IntPtr ptr);
    }


    [ComImport]
    [Guid("31EFACF3-5A0C-11D2-83C3-00C04F8EE6C0")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface ITfInputProcessorProfiles
    {
        void ActivateLanguageProfile(ref Guid rclsid, short langid, ref Guid guidProfile);
        void SetLanguageProfile(ref Guid rclsid, short langid, ref Guid guidProfile);
        void GetLanguageProfile(ref Guid rclsid, short langid, ref Guid guidProfile);
        void SetDefaultLanguageProfile(ref Guid rclsid, short langid, ref Guid guidProfile);
    }

    //[ComImport]
    //[Guid("31EFACF3-5A0C-11D2-83C3-00C04F8EE6C0")]
    //[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    //public interface ITfInputProcessorProfiles
    //{
    //    void Stub1();
    //    void Stub2();
    //    void Stub3();
    //    void Stub4();
    //    void Stub5();
    //    void Stub6();
    //    void Stub7();
    //    void Stub8();
    //    void Stub9();
    //    void Stub10();
    //    void Stub11();
    //    void Stub12();
    //    void ActivateLanguageProfile(ref Guid rclsid, short langid, ref Guid guidProfile);
    //    //void GetActiveLanguageProfile(ref Guid rclsid, out Guid guidProfile);
    //}
}
