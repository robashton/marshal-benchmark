using System;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;

namespace Benchmark {
  public class Db : IDisposable {

    public Db(int size) {
      Native.init(new IntPtr(size));
    }

    public void Put(int index, Byte[] data) {
      Native.put(new IntPtr(index), data, new IntPtr(data.Length));
    }

    public void Get(int index, Byte[] buffer, int offset, int amount) {
      Native.get(new IntPtr(index), buffer, new IntPtr(offset), new IntPtr(amount));
    }
    
    public int GetSize(int index) {
      return Native.get_size(new IntPtr(index)).ToInt32();
    }

    public void Dispose() {
      Native.shutdown();
    }
  }
}
