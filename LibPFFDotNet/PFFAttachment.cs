using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LibPFFDotNet
{
    public class PFFAttachmentDataError : Exception
    {
        public PFFAttachmentDataError(string Message)
            : base(Message)
        {
        }
    }

    public class PFFAttachmentStream : Stream
    {
        IntPtr _AttachmentHandler = new IntPtr();
        long position = 0;
        public enum SeekStyle : int { SetOffset = 0, FromCurrent = 1, FromEnd = 2 };

        public PFFAttachmentStream(IntPtr AttachmentHandler)
            : base()
        {
            _AttachmentHandler = AttachmentHandler;
            Seek(0, SeekOrigin.Begin);
        }

        public override bool CanWrite
        {
            get
            {
                return false;
            }
        }

        public override bool CanSeek
        {
            get { return true; }
        }

        public override bool CanRead
        {
            get { return true; }
        }

        public override long Length
        {
            get
            {
                UIntPtr size = new UIntPtr();
                IntPtr ErrorMessage = new IntPtr();
                int r = libpff.GetAttachmentSize(_AttachmentHandler, out size, out ErrorMessage);
                long AttachmentSize = 0;
                if (r == -1)
                    throw new PFFAttachmentDataError("Error getting attachment size: " + libpff.GetErrorsFromIntPtr(ErrorMessage)[0]);
                AttachmentSize = (long)size.ToUInt64();
                return AttachmentSize;
            }
        }

        public override long Position
        {
            get
            {
                return position;
            }
            set
            {
                Seek(value, SeekOrigin.Begin);
            }
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            IntPtr ErrorMessage = new IntPtr();
            Int64 or = position;
            switch (origin)
            {
                case SeekOrigin.Begin:
                    or = libpff.AttachmentSeekOffset(_AttachmentHandler, offset, (int)SeekStyle.SetOffset, out ErrorMessage).ToInt64();
                    break;
                case SeekOrigin.Current:
                    or = libpff.AttachmentSeekOffset(_AttachmentHandler, offset, (int)SeekStyle.FromCurrent, out ErrorMessage).ToInt64();
                    break;
                case SeekOrigin.End:
                    or = libpff.AttachmentSeekOffset(_AttachmentHandler, offset, (int)SeekStyle.FromEnd, out ErrorMessage).ToInt64();
                    break;
            }
            if (or == -1)
                throw new PFFAttachmentDataError("Error seeking to " + offset + ": " + libpff.GetErrorsFromIntPtr(ErrorMessage)[0]);
            else
                position = or;
            return or;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }

        public override void Flush()
        {
            throw new NotImplementedException();
        }

        unsafe public override int Read(byte[] buffer, int offset, int count)
        {
            byte[] InternalBuffer = new byte[count];
            UIntPtr s = new UIntPtr((uint)count);
            int BytesRead = 0;
            fixed (byte* p = InternalBuffer)
            {
                IntPtr ErrorMessage = new IntPtr();
                IntPtr DataValue = (IntPtr)p;
                BytesRead = libpff.GetAttachmentData(_AttachmentHandler, DataValue, s, out ErrorMessage).ToInt32();
                if (BytesRead == -1)
                    throw new PFFAttachmentDataError("Error getting attachment data: " + libpff.GetErrorsFromIntPtr(ErrorMessage)[0]);
                InternalBuffer.CopyTo(buffer, offset);
            }
            position += BytesRead;
            return BytesRead;
        }
    }

    public class PFFAttachment : PFFItem
    {
        public enum AttachmentMethod
        {
            None = 0,
            ByValue = 1,
            ByReference = 2,
            ByReferenceResolve = 3,
            ByReferenceOnly = 4,
            EmbeddedMessage = 5,
            OLE = 6
        };

        public enum SeekStyle : int { SetOffset = 0, FromCurrent = 1, FromEnd = 2 };

        public enum AttachmentType : int { Undefined = 0, Data = 'd', Item = 'i', Reference = 'r' };

        public PFFAttachment(IntPtr AttachmentPtr)
            : base(AttachmentPtr)
        {

        }

        public uint GetContentDisposition()
        {
            return GetMapiIntegerValue(mapi.EntryTypes.LIBPFF_ENTRY_TYPE_ATTACHMENT_RENDERING_POSITION);
        }

        public override string GetName()
        {
            return GetMapiStringValue(mapi.EntryTypes.AttachmentFilenameLong);
        }

        public string GetMimeType()
        {
            return GetMapiStringValue(mapi.EntryTypes.AttachmentMimeType);
        }

        public string GetContentID()
        {
            return GetMapiStringValue(mapi.EntryTypes.AttachmentConentID);
        }

        public Int64 Seek(Int64 Offset, SeekStyle Whence)
        {
            IntPtr ErrorMessage = new IntPtr();
            Int64 or = libpff.AttachmentSeekOffset(_ItemHandler, Offset, (int)Whence, out ErrorMessage).ToInt64();
            if (or == -1)
                throw new PFFAttachmentDataError("Error seeking to " + Offset + ": " + libpff.GetErrorsFromIntPtr(ErrorMessage)[0]);
            return or;
        }

        unsafe public byte[] GetContents()
        {
            MemoryStream fs = new MemoryStream();
            UInt64 buffersize = 1024;
            UInt64 filesize = GetSize();
            UInt64 offset = 0;
            Int64 or = Seek(0, SeekStyle.SetOffset);
            if (or != -1)
            {
                while (offset < filesize)
                {
                    UInt64 readsize = buffersize;
                    if (offset + buffersize >= filesize)
                        readsize = filesize - offset;
                    IntPtr ErrorMessage = new IntPtr();
                    UIntPtr s = new UIntPtr(readsize);
                    byte[] buffer = new byte[readsize];
                    fixed (byte* p = buffer)
                    {
                        IntPtr DataValue = (IntPtr)p;
                        int r = libpff.GetAttachmentData(_ItemHandler, DataValue, s, out ErrorMessage).ToInt32();
                        if (r == -1)
                            throw new PFFAttachmentDataError("Error getting attachment data: " + libpff.GetErrorsFromIntPtr(ErrorMessage)[0]);
                        fs.Write(buffer, 0, r);
                    }
                    offset += readsize;
                }
            }
            return fs.ToArray();
        }

        unsafe public void SaveContents(string FilePath)
        {
            FileStream fs = new FileStream(FilePath, FileMode.CreateNew);
            UInt64 buffersize = 1024;
            UInt64 filesize = GetSize();
            UInt64 offset = 0;
            Int64 or = Seek(0, SeekStyle.SetOffset);
            if (or != -1)
            {
                while (offset < filesize)
                {
                    UInt64 readsize = buffersize;
                    if (offset + buffersize >= filesize)
                        readsize = filesize - offset;
                    IntPtr ErrorMessage = new IntPtr();
                    UIntPtr s = new UIntPtr(readsize);
                    byte[] buffer = new byte[readsize];
                    fixed (byte* p = buffer)
                    {
                        IntPtr DataValue = (IntPtr)p;
                        int r = libpff.GetAttachmentData(_ItemHandler, DataValue, s, out ErrorMessage).ToInt32();
                        if (r == -1)
                            throw new PFFAttachmentDataError("Error getting attachment data: " + libpff.GetErrorsFromIntPtr(ErrorMessage)[0]);
                        fs.Write(buffer, 0, r);
                    }
                    offset += readsize;
                }
            }
            fs.Close();
        }

        public UInt64 GetSize()
        {
            UIntPtr size = new UIntPtr();
            IntPtr ErrorMessage = new IntPtr();
            int r = libpff.GetAttachmentSize(_ItemHandler, out size, out ErrorMessage);
            UInt64 AttachmentSize = 0;
            if (r == -1)
                throw new PFFAttachmentDataError("Error getting attachment size: " + libpff.GetErrorsFromIntPtr(ErrorMessage)[0]);
            AttachmentSize = (ulong)size.ToUInt64();
            return AttachmentSize;
        }

        public AttachmentType GetAttachmentType()
        {
            int type;
            IntPtr ErrorMessage = new IntPtr();
            int r = libpff.GetAttachmentType(_ItemHandler, out type, out ErrorMessage);
            if (r == -1)
                throw new PFFAttachmentDataError("Error getting attachment type: " + libpff.GetErrorsFromIntPtr(ErrorMessage)[0]);
            return (AttachmentType)type;
        }
    }
}
