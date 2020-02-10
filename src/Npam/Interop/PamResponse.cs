using System.Runtime.InteropServices;

namespace Npam.Interop
{
    ///<summary>
    /// Used to return the user's response to the PAM library.  This
    /// structure is allocated by the application program, and free()'d by
    /// the Linux-PAM library (or calling module).
    ///</summary>
    [StructLayout(LayoutKind.Sequential)]
    public class PamResponse
    {
        public PamResponse()
        {
        }

        public PamResponse(string response)
        {
            this.Response = response;
        }

        public string Response;
        public int ReturnCode = 0; //currently un-used, zero expected
    }
}