using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Npam
{
    internal class MarshalUtils
    {
        private static readonly int PtrSize = Marshal.SizeOf<IntPtr>();

        ///<summary>
        /// Marshal an incoming pointer to array of pointers to structs into an enumerable of struct.
        ///</summary>
        internal static IEnumerable<T> MarshalPtrPtrStructIn<T>(int ptrArraySize, IntPtr ptrToArray) where T : new()
        {
            for (var index = 0; index < ptrArraySize; index++)
            {
                var structPtr = Marshal.ReadIntPtr(ptrToArray, index * PtrSize);
                yield return Marshal.PtrToStructure<T>(structPtr);
            }
        }


        ///<summary>
        /// Marshal an outgoing list of structs to a pointer to array of pointers to structs.
        ///</summary>
        internal static IntPtr MarshalPtrPtrStructOut<T>(List<T> structList)
        {
            var ptrArray = Marshal.AllocHGlobal(PtrSize * structList.Count);
            for (var i = 0; i < structList.Count; i++)
            {
                var nativeResponse = Marshal.AllocHGlobal(Marshal.SizeOf<T>());
                Marshal.StructureToPtr(structList[i], nativeResponse, false);
                Marshal.WriteIntPtr(ptrArray, i * PtrSize, nativeResponse);
            }

            return ptrArray;
        }

        ///<summary>
        /// Marshal an outgoing struct to a pointer to that struct.
        ///</summary>
        internal static IntPtr MarshalPtrStructOut<T>(T myVal)
        {
            var nativeResponse = Marshal.AllocHGlobal(Marshal.SizeOf<T>());
            Marshal.StructureToPtr(myVal, nativeResponse, false);
            return nativeResponse;
        }
    }
}