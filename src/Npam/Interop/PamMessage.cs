using System.Runtime.InteropServices;

namespace Npam.Interop
{
    ///<summary>
    /// Used to pass prompting text, error messages, or other informatory
    /// text to the user.  This structure is allocated and freed by the PAM
    /// library (or loaded module).
    ///</summary>
    [StructLayout(LayoutKind.Sequential)]
    public class PamMessage
    {
        public MessageStyle MsgStyle;
        public string Message;
    }
}