using System.Runtime.InteropServices;

namespace Npam.Interop
{

    ///<summary>
    /// Renamed as "AccountInfo", this structure is actually the linux passwd struct as defined in pwd.h.
    /// http://linux.die.net/man/3/getpwnam
    ///</summary>
    [StructLayout(LayoutKind.Sequential)]
    public class AccountInfo {          
            public string Username;
            public string Password;
            public int UserID;
            public int GroupID;
            public string RealName;
            public string HomeDir;
            public string ShellProgram;

            public override string ToString() {
                return string.Format("Username=[{0}] UserID=[{1}] GroupID=[{2}] RealName=[{3}] HomeDir=[{4}] ShellProgram=[{5}]", Username, UserID, GroupID, RealName, HomeDir, ShellProgram);
            }
    }
}