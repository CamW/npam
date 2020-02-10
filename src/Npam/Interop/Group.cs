using System;
using System.Runtime.InteropServices;

namespace Npam.Interop
{
    [StructLayout(LayoutKind.Sequential)]
    public class Group
    {
        public string GroupName;
        public string GroupPassword;
        public int GroupID;
        public IntPtr GroupMembers;

        public override string ToString()
        {
            return $"{GroupName} ({GroupID})";
        }
    }
}