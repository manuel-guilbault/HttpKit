using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpKit.Mvc
{
    public class RangeStreamDecorator : Stream, IDisposable
    {
        private readonly Stream stream;

        public RangeStreamDecorator(long startAt, long endAt, Stream stream)
        {
            if (startAt < 0) throw new ArgumentException("startAt must be equal to or greater than zero", "startAt");
            if (startAt >= stream.Length) throw new IndexOutOfRangeException("range is outside out stream bounds");
            if (endAt < startAt) throw new ArgumentException("endAt must be equal to or greater than startAt", "endAt");
            if (stream == null) throw new ArgumentNullException("stream");

            this.StartAt = startAt;
            this.EndAt = endAt;
            this.stream = stream;

            Prepare();
        }

        private void Prepare()
        {
            EndAt = Math.Min(EndAt, stream.Length - 1);
            stream.Position = StartAt;
        }

        public long StartAt { get; private set; }

        public long EndAt { get; private set; }

        public long TotalLength
        {
            get { return stream.Length; }
        }

        public override bool CanRead
        {
            get { return stream.CanRead; }
        }

        public override bool CanSeek
        {
            get { return stream.CanSeek; }
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override bool CanTimeout
        {
            get { return base.CanTimeout; }
        }

        public override int ReadTimeout
        {
            get { return stream.ReadTimeout; }
            set { stream.ReadTimeout = value; }
        }

        public override int WriteTimeout
        {
            get { throw new NotSupportedException(); }
            set { throw new NotSupportedException(); }
        }

        public override void Flush()
        {
            throw new NotSupportedException();
        }

        public override long Length
        {
            get { return EndAt - StartAt + 1; }
        }

        public override long Position
        {
            get { return stream.Position - StartAt; }
            set { throw new NotSupportedException(); }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            count = (int)Math.Min(count, EndAt - stream.Position + 1);
            stream.Read(buffer, offset, count);
            return count;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            switch (origin)
            {
                case SeekOrigin.Begin:
                    if (offset > EndAt - StartAt + 1) throw new IOException();
                    stream.Position = StartAt + offset;
                    break;

                case SeekOrigin.Current:
                    if (stream.Position + offset < StartAt) throw new IOException();
                    if (stream.Position + offset > EndAt) throw new IOException();
                    stream.Position += offset;
                    break;

                case SeekOrigin.End:
                    if (EndAt - StartAt + 1 + offset > EndAt) throw new IOException();
                    stream.Position = EndAt + offset;
                    break;

                default:
                    throw new InvalidProgramException("Unknown SeekOrigin." + origin);
            }

            return stream.Position;
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException();
        }

        protected override void Dispose(bool disposing)
        {
            stream.Dispose();
        }
    }
}
