using System;
using System.Runtime.InteropServices;

namespace Npam.Interop
{
    ///<summary>
    ///
    ///</summary>
    public delegate PamStatus ConvCallback(int messageCount, IntPtr messageArrayPtr, ref IntPtr responseArrayPtr,
        IntPtr appDataPtr);

    ///<summary>
    /// The actual conversation structure itself.
    ///</summary>
    [StructLayout(LayoutKind.Sequential)]
    public class PamConv
    {
        public ConvCallback ConversationCallback;
        public IntPtr AppData;
    }
}