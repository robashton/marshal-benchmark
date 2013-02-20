using System;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;

namespace Benchmark {
  public static class Native {

    // Tear up/down
    [DllImport("native", CallingConvention = CallingConvention.Cdecl)]
    public static extern void init(IntPtr size);
    [DllImport("native", CallingConvention = CallingConvention.Cdecl)]
    public static extern void shutdown();

    // Common
    [DllImport("native", CallingConvention = CallingConvention.Cdecl)]
    public static extern void remove(IntPtr index);
    [DllImport("native", CallingConvention = CallingConvention.Cdecl)]
    public static extern int get_size(IntPtr index);

    // Synchronous marshal-all-the-bytes-methods
    [DllImport("native", CallingConvention = CallingConvention.Cdecl)]
    public static extern void put(IntPtr index, byte[] data, IntPtr size);
    [DllImport("native", CallingConvention = CallingConvention.Cdecl)]
    public static extern void get(IntPtr index, byte[] output, int offset, int amount);
  }
}
