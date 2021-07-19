using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace ZkTest
{
    public static class Api
    {
        [DllImport("Sdk/Interop.zkemkeeper.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void zkemkeeper();
    }
}
