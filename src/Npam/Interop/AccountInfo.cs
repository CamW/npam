using System.Runtime.InteropServices;

namespace Npam.Interop
{
    ///<summary>
    /// Renamed as "AccountInfo", this structure is actually the linux passwd struct as defined in pwd.h.
    /// http://linux.die.net/man/3/getpwnam
    ///</summary>
    [StructLayout(LayoutKind.Sequential)]
    public class AccountInfo
    {
        public string Username;
        public string Password;
        public int UserID;
        public int GroupID;
        public string RealName;
        public string HomeDir;
        public string ShellProgram;

        public override string ToString()
        {
            return
                $"Username=[{Username}] UserID=[{UserID}] GroupID=[{GroupID}] RealName=[{RealName}] HomeDir=[{HomeDir}] ShellProgram=[{ShellProgram}]";
        }
    }
}