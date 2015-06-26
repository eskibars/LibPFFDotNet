using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Mail;

namespace LibPFFDotNet
{
    public class PFFFolderReadError : Exception
    {
        public PFFFolderReadError(string Message)
            : base(Message)
        {
        }
    }

    public class PFFFolder : PFFItem
    {
        public PFFFolder(IntPtr FolderPtr)
            : base(FolderPtr)
        {
        }

        public enum FolderType { Generic = 1, Root = 2, Search = 3 };

        public List<PFFFolder> GetSubfolders()
        {
            List<PFFFolder> Folders = new List<PFFFolder>();
            IntPtr ErrorMessage;
            int FolderCount = 0;
            int r = libpff.GetNumberOfSubfolders(this._ItemHandler, out FolderCount, out ErrorMessage);
            if (r == -1)
                throw new PFFFolderReadError("Error reading submessages: " + libpff.GetErrorsFromIntPtr(ErrorMessage)[0]);
            for (int i = 0; i < FolderCount; i++)
            {
                IntPtr s = new IntPtr();
                r = libpff.GetSubfolder(this._ItemHandler, i, out s, out ErrorMessage);
                if (r == -1)
                    throw new PFFFolderReadError("Error reading subfolder " + i + ": " + libpff.GetErrorsFromIntPtr(ErrorMessage)[0]);
                Folders.Add(new PFFFolder(s));
            }
            return Folders;
        }

        /// <summary>
        /// Exports the current PFFFolder object to a given directory (as well as all folders and objects underneath it, recursively)
        /// </summary>
        /// <param name="BaseDirectory">The base directory to dump files to.  The directory must already exist.</param>
        public void Export(string BaseDirectory)
        {
            if (Directory.Exists(BaseDirectory))
            {
                SmtpClient Client = new SmtpClient("empty");
                Client.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                Client.PickupDirectoryLocation = BaseDirectory;
                foreach (PFFMessage m in GetSubmessages())
                {
                    switch (m.GetItemType())
                    {
                        case ItemTypes.Email:
                            MailMessage msg = m.GetAsMailMessage();
                            Client.Send(msg);
                            break;
                    }
                }

                foreach (PFFFolder f in GetSubfolders())
                {
                    string NewDirectory = Path.Combine(BaseDirectory, f.GetName());
                    Directory.CreateDirectory(NewDirectory);
                    f.Export(NewDirectory);
                }
            }
            else
            {
                throw new DirectoryNotFoundException(BaseDirectory + " not found");
            }
        }

        public FolderType GetFolderType()
        {
            return (FolderType)(GetMapiIntegerValue(mapi.EntryTypes.FolderType));
        }

        /// <summary>
        /// Gets a list of messages contained within this PFFFolder object
        /// </summary>
        /// <returns>A list of PFFMessage objects beneath the current PFFFolder object.  Note that this list is NOT a recursive list, but only the ones contained directly in the given folder</returns>
        public List<PFFMessage> GetSubmessages()
        {
            List<PFFMessage> Messages = new List<PFFMessage>();
            IntPtr ErrorMessage;
            int MessageCount = 0;
            int r = libpff.GetNumberOfSubmessages(this._ItemHandler, out MessageCount, out ErrorMessage);
            if (r == -1)
                throw new PFFFolderReadError("Error reading submessages: " + libpff.GetErrorsFromIntPtr(ErrorMessage)[0]);
            for (int i = 0; i < MessageCount; i++)
            {
                IntPtr s = new IntPtr();
                r = libpff.GetSubmessage(this._ItemHandler, i, out s, out ErrorMessage);
                if (r == -1)
                    throw new PFFFolderReadError("Error reading submessage " + i + ": " + libpff.GetErrorsFromIntPtr(ErrorMessage)[0]);
                Messages.Add(new PFFMessage(s));
            }
            return Messages;
        }
    }
}
