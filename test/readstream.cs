using System;
using System.IO;

namespace Benchmark {
    public class ReadStream : Stream {
        int current = 0;
        int index = 0;
        int max = 0;
        Db db = null;

        public ReadStream(Db db, int index) {
            this.index = index;
            this.db = db;
            this.current = 0;
            this.max = db.GetSize(index);
        }

        public override int Read (byte[] buffer, int offset, int count) {
            int end = current + count;
            if(end >= max) {
                end = max;
            }
            count = end - current;

            if(count > 0) {
              this.db.Get(this.index, buffer, current, count);
            }
            current = end;
            return count;
        }

        public override void Flush () {
            throw new System.NotSupportedException ();
        }

        public override long Seek (long offset, SeekOrigin origin) {
            throw new System.NotSupportedException ();
        }

        public override void SetLength (long value) {
            throw new System.NotSupportedException ();
        }

        public override void Write (byte[] buffer, int offset, int count) {
            throw new System.NotSupportedException ();
        }

        public override bool CanRead { get {
                return true; 
            }
        }

        public override bool CanSeek { get {
                return false;
            }
        }

        public override bool CanWrite { get {
               return false;
            }
        }

        public override long Length {
            get {
                return this.max;
            }
        }

        public override long Position { get {
                return this.current;
            }
            set {
                this.current = (int)value;
            }
        }

    }
}

