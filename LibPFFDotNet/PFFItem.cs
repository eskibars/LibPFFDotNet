using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPFFDotNet
{
    public class PFFItemDataError : Exception
    {
        public PFFItemDataError(string Message)
            : base(Message)
        {
        }
    }

    public class PFFItem
    {
        protected IntPtr _ItemHandler;

        public enum ItemTypes : uint
        {
            Undefined,
            Activity, // IPM.Activity
            Appointment, // IPM.Appointment
            Attachment,
            Attachments,
            Common, // IPM
            Configuration, // IPM.Configuration.*
            ConflictMessage, // IPM.Conflict.Message
            Contact, // IPM.Contact
            DistributionList, // IPM.DistList
            Document, // IPM.Document.*
            Email, // IPM.Note, REPORT.IPM.Note
            EmailSMIME, // IPM.Note.SMIME
            Fax, // IPM.FAX, IPM.Note.Fax
            Folder,
            Meeting, // IPM.Schedule.Meeting
            MMS, // IPM.Note.Mobile.MMS
            StickyNote, // IPM.StickyNote
            PostingNote, // IPM.Post
            Recipients,
            RSSFeed, // IPM.Post.RSS
            Sharing, // IPM.Sharing.*
            SMS, // IPM.SMS
            AssociatedContents,
            Subfolders,
            Submessages,
            Task, // IPM.Task
            TaskRequest, // IPM.TaskRequest.* (including IPM.TaskRequest.Accept and IPM.TaskRequest.Decline)
            Voicemail, // IPM.Note.Voicemail
            Unknown
        };

        public PFFItem(IntPtr ItemPtr)
        {
            _ItemHandler = ItemPtr;
        }

        public IntPtr Handler { get { return _ItemHandler; } }

        ~PFFItem()
        {
            /*IntPtr ErrorMessage;
            int r = libpff.FreeItem(ItemHandler, out ErrorMessage);*/
        }

        protected bool GetMapiBooleanValue(mapi.EntryTypes MapiAttribute)
        {
            IntPtr ErrorMessage = new IntPtr();
            uint value = 0;
            int r = libpff.GetBooleanEntry(_ItemHandler, 0, (UInt32)MapiAttribute, out value, 0, out ErrorMessage);
            if (r == -1)
                throw new PFFItemDataError("Error reading MAPI boolean value: " + libpff.GetErrorsFromIntPtr(ErrorMessage)[0]);
            return (value == 1);
        }

        protected bool GetMapiBooleanValue(mapi.EntryTypes MapiAttribute, int index)
        {
            IntPtr ErrorMessage = new IntPtr();
            uint value = 0;
            int r = libpff.GetBooleanEntry(_ItemHandler, index, (UInt32)MapiAttribute, out value, 0, out ErrorMessage);
            if (r == -1)
                throw new PFFItemDataError("Error reading MAPI boolean value: " + libpff.GetErrorsFromIntPtr(ErrorMessage)[0]);
            return (value == 1);
        }

        protected double GetMapiFloatingPointValue(mapi.EntryTypes MapiAttribute)
        {
            IntPtr ErrorMessage = new IntPtr();
            double value = 0;
            int r = libpff.GetFloatingPointEntry(_ItemHandler, 0, (UInt32)MapiAttribute, out value, 0, out ErrorMessage);
            if (r == -1)
                throw new PFFItemDataError("Error reading MAPI floating point value: " + libpff.GetErrorsFromIntPtr(ErrorMessage)[0]);
            return value;
        }

        protected double GetMapiFloatingPointValue(mapi.EntryTypes MapiAttribute, int index)
        {
            IntPtr ErrorMessage = new IntPtr();
            double value = 0;
            int r = libpff.GetFloatingPointEntry(_ItemHandler, index, (UInt32)MapiAttribute, out value, 0, out ErrorMessage);
            if (r == -1)
                throw new PFFItemDataError("Error reading MAPI floating point value: " + libpff.GetErrorsFromIntPtr(ErrorMessage)[0]);
            return value;
        }

        protected UInt32 GetMapiIntegerValue(mapi.EntryTypes MapiAttribute)
        {
            IntPtr ErrorMessage = new IntPtr();
            UInt32 value = 0;
            int r = libpff.GetIntegerEntry(_ItemHandler, 0, (UInt32)MapiAttribute, out value, 0, out ErrorMessage);
            if (r == -1)
                throw new PFFItemDataError("Error reading MAPI integer value: " + libpff.GetErrorsFromIntPtr(ErrorMessage)[0]);
            return value;
        }

        protected UInt32 GetMapiIntegerValue(mapi.EntryTypes MapiAttribute, int index)
        {
            IntPtr ErrorMessage = new IntPtr();
            UInt32 value = 0;
            int r = libpff.GetIntegerEntry(_ItemHandler, index, (UInt32)MapiAttribute, out value, 0, out ErrorMessage);
            if (r == -1)
                throw new PFFItemDataError("Error reading MAPI integer value: " + libpff.GetErrorsFromIntPtr(ErrorMessage)[0]);
            return value;
        }

        unsafe protected string GetMapiStringValue(mapi.EntryTypes MapiAttribute)
        {
            UIntPtr StringSize = new UIntPtr();
            IntPtr ErrorMessage = new IntPtr();
            string value = null;
            int r = libpff.GetStringEntrySize(_ItemHandler, 0, (UInt32) MapiAttribute, out StringSize, 0, out ErrorMessage);
            if (r == -1)
                throw new PFFItemDataError("Error reading MAPI string size: " + libpff.GetErrorsFromIntPtr(ErrorMessage)[0]);
            int size = (int)StringSize.ToUInt32();
            if (size > 0)
            {
                byte[] buffer = new byte[size];
                fixed (byte* p = buffer)
                {
                    IntPtr StringValue = (IntPtr)p;
                    r = libpff.GetStringEntry(_ItemHandler, 0, (UInt32)MapiAttribute, StringValue, StringSize, 0, out ErrorMessage);
                    if (r == -1)
                        throw new PFFItemDataError("Error reading MAPI string value: " + libpff.GetErrorsFromIntPtr(ErrorMessage)[0]);
                    value = Encoding.Default.GetString(buffer);
                }
                value = value.TrimEnd(new char[] { '\0' });
            }
            return value;
        }

        unsafe protected string GetMapiStringValue(mapi.EntryTypes MapiAttribute, int index)
        {
            string res = null;
            IntPtr ErrorMessage = new IntPtr();
            UIntPtr StringSize = new UIntPtr();
            int r = libpff.GetStringEntrySize(_ItemHandler, index, (UInt32)MapiAttribute, out StringSize, 0, out ErrorMessage);
            if (r == -1)
                throw new PFFRecipientsDataError("Error reading MAPI string size: " + libpff.GetErrorsFromIntPtr(ErrorMessage)[0]);
            int size = (int)StringSize.ToUInt32();
            if (size > 0)
            {
                byte[] buffer = new byte[size];
                fixed (byte* p = buffer)
                {
                    IntPtr StringValue = (IntPtr)p;
                    r = libpff.GetStringEntry(_ItemHandler, index, (UInt32)MapiAttribute, StringValue, StringSize, 0, out ErrorMessage);
                    if (r == -1)
                        throw new PFFItemDataError("Error reading MAPI string value: " + libpff.GetErrorsFromIntPtr(ErrorMessage)[0]);
                    res = Encoding.ASCII.GetString(buffer);
                }
                res = res.TrimEnd(new char[] { '\0' });
            }
            return res;
        }

        public virtual string GetName()
        {
            return GetMapiStringValue(mapi.EntryTypes.DisplayName);
        }

        public override string ToString()
        {
            return GetName();
        }

        public uint GetIdentifier()
        {
            uint Identifier;
            IntPtr ErrorMessage;
            int r = libpff.GetIdentifier(_ItemHandler, out Identifier, out ErrorMessage);
            if (r == -1)
                throw new PFFItemDataError("Error reading identifier: " + libpff.GetErrorsFromIntPtr(ErrorMessage)[0]);
            return Identifier;
        }

        public ItemTypes GetItemType()
        {
            uint ItemType;
            IntPtr ErrorMessage;
            int r = libpff.GetItemType(_ItemHandler, out ItemType, out ErrorMessage);
            if (r == -1)
                throw new PFFItemDataError("Error reading item type: " + libpff.GetErrorsFromIntPtr(ErrorMessage)[0]);
            return (ItemTypes)ItemType;
        }
    }
}
