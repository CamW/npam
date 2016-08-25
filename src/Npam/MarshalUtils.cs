using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Npam {
    internal class MarshalUtils {

        private readonly static int PTR_SIZE = Marshal.SizeOf<IntPtr>();

        ///<summary>
        /// Marshal an incomming pointer to array of pointers to structs into an enumerable of struct.
        ///</summary>
        internal static IEnumerable<T> MarshalPtrPtrStructIn<T>(int ptrArraySize, IntPtr ptrToArray) where T : new() {
            for (int index = 0; index < ptrArraySize; index++) {
                IntPtr structPtr = Marshal.ReadIntPtr(ptrToArray, index * PTR_SIZE);
                yield return Marshal.PtrToStructure<T>(structPtr);
            }
        }


        ///<summary>
        /// Marshal an outgoing list of structs to a pointer to array of pointers to structs.
        ///</summary>
        internal static IntPtr MarshalPtrPtrStructOut<T>(List<T> structList) {
            IntPtr ptrArray = Marshal.AllocHGlobal(PTR_SIZE * structList.Count);
            IntPtr nativeResponse = IntPtr.Zero;
            for (int i = 0; i < structList.Count; i++)
            {
                nativeResponse = Marshal.AllocHGlobal(Marshal.SizeOf<T>());
                Marshal.StructureToPtr(structList[i], nativeResponse, false);
                Marshal.WriteIntPtr(ptrArray, i * PTR_SIZE, nativeResponse);
            }
            return ptrArray;
        }

        ///<summary>
        /// Marshal an outgoing struct to a pointer to that struct.
        ///</summary>
        internal static IntPtr MarshalPtrStructOut<T>(T myVal) {
            IntPtr nativeResponse = Marshal.AllocHGlobal(Marshal.SizeOf<T>());
            Marshal.StructureToPtr(myVal, nativeResponse, false);
            return nativeResponse;
        }
    }
}