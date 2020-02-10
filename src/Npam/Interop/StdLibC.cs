using System;
using System.Runtime.InteropServices;

namespace Npam.Interop
{
    public class StdLibC
    {
        [DllImport("libc.so.6")]
        public static extern IntPtr getgrgid(int groupId);

        [DllImport("libc.so.6")]
        public static extern IntPtr getpwnam([MarshalAs(UnmanagedType.LPStr)] string user);

        [DllImport("libc.so.6")]
        public static extern int getgrouplist([MarshalAs(UnmanagedType.LPStr)] string user, int groupId,
            [Out] int[] groupIdArray, ref int numberOfGroups);

        public static AccountInfo GetPwNamAsAccountInfo(string user)
        {
            return Marshal.PtrToStructure<AccountInfo>(StdLibC.getpwnam(user));
        }

        public static Group GetGrGidAsGroup(int groupId)
        {
            return Marshal.PtrToStructure<Group>(StdLibC.getgrgid(groupId));
        }
    }
}