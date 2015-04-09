using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;

namespace LibPFFDotNet
{
    public class PFFRecipientsDataError : Exception
    {
        public PFFRecipientsDataError(string Message)
            : base(Message)
        {
        }
    }

    public class PFFRecipients : PFFItem
    {
        public enum RecipientType { Originator = 0, To = 1, CC = 2, BCC = 3 };

        public PFFRecipients(IntPtr RecipientsPtr)
            : base(RecipientsPtr)
        {

        }

        public uint GetCount()
        {
            uint RecipientCount = 0;
            IntPtr ErrorMessage = new IntPtr();
            int r = libpff.GetNumberOfSets(_ItemHandler, out RecipientCount, out ErrorMessage);
            if (r == -1)
                throw new PFFRecipientsDataError("Error getting recipients count: " + libpff.GetErrorsFromIntPtr(ErrorMessage)[0]);
            return RecipientCount;
        }

        public List<MailAddress> GetRecipients()
        {
            List<MailAddress> Recipients = new List<MailAddress>();
            for (int i = 0; i < GetCount(); i++)
            {
                string DisplayName = GetMapiStringValue(mapi.EntryTypes.RecipientDisplayName, i);
                string EmailAddress = GetMapiStringValue(mapi.EntryTypes.EmailAddress, i);
                MailAddress a = new MailAddress(EmailAddress, DisplayName);
                Recipients.Add(a);
            }
            return Recipients;
        }

        public List<MailAddress> GetRecipients(RecipientType Allowed)
        {
            List<MailAddress> Recipients = new List<MailAddress>();
            for (int i = 0; i < GetCount(); i++)
            {
                uint t = GetMapiIntegerValue(mapi.EntryTypes.RecipientType, i);
                if (t == (uint)Allowed)
                {
                    string DisplayName = GetMapiStringValue(mapi.EntryTypes.RecipientDisplayName, i);
                    string EmailAddress = GetMapiStringValue(mapi.EntryTypes.EmailAddress, i);
                    try
                    {
                        MailAddress a = new MailAddress(EmailAddress, DisplayName);
                        Recipients.Add(a);
                    }
                    catch (FormatException e)
                    {
                        /*
                         * For now, ignore this
                         */
                    }
                }
            }
            return Recipients;
        }
    }
}
