using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net.Mime;

namespace LibPFFDotNet
{
    public class PFFMessageDataError : Exception
    {
        public PFFMessageDataError(string Message)
            : base(Message)
        {
        }
    }

    public class PFFMessage : PFFItem
    {
        public enum Importance : int { Low = 0, Normal = 1, High = 2 };
        public enum BodyType : int { PlainText = 0, RTF = 1, HTML = 2 };
        public enum Priority : int { Low = -1, Normal = 0, High = 1 };

        public PFFMessage(IntPtr MessagePtr)
            : base(MessagePtr)
        {

        }

        public Priority GetPriority()
        {
            uint v = GetMapiIntegerValue(mapi.EntryTypes.MessagePriority);
            return (Priority)(unchecked((int)v));
        }

        public List<MailAddress> GetRecipients()
        {
            IntPtr RecipientsPtr = new IntPtr();
            IntPtr ErrorMessage = new IntPtr();
            int r = libpff.GetRecipients(_ItemHandler, out RecipientsPtr, out ErrorMessage);
            if (r == -1)
                throw new PFFMessageDataError("Error reading recipients: " + libpff.GetErrorsFromIntPtr(ErrorMessage)[0]);
            PFFRecipients Recipients = new PFFRecipients(RecipientsPtr);
            List<MailAddress> RecipientAddresses = Recipients.GetRecipients();
            return RecipientAddresses;
        }

        public List<MailAddress> GetRecipients(PFFRecipients.RecipientType Allowed)
        {
            IntPtr RecipientsPtr = new IntPtr();
            IntPtr ErrorMessage = new IntPtr();
            int r = libpff.GetRecipients(_ItemHandler, out RecipientsPtr, out ErrorMessage);
            if (r == -1)
                throw new PFFMessageDataError("Error reading recipients: " + libpff.GetErrorsFromIntPtr(ErrorMessage)[0]);
            PFFRecipients Recipients = new PFFRecipients(RecipientsPtr);
            List<MailAddress> RecipientAddresses = Recipients.GetRecipients(Allowed);
            return RecipientAddresses;
        }

        private KeyValuePair<string, string> GetHeader(string HeaderText)
        {
            KeyValuePair<string, string> de = new KeyValuePair<string, string>();
            Regex r = new Regex(@"^([a-z0-9\-]+):\s*(.*)", RegexOptions.IgnoreCase);
            Match m = r.Match(HeaderText);
            if (m.Groups.Count == 3)
            {
                de = new KeyValuePair<string, string>(m.Groups[1].ToString(), m.Groups[2].ToString());
            }
            return de;
        }

        public List<KeyValuePair<string, string>> GetHeaders()
        {
            List<KeyValuePair<string, string>> Headers = new List<KeyValuePair<string, string>>();
            string HeadersString = GetMapiStringValue(mapi.EntryTypes.MessageHeaders);
            string[] lines = HeadersString.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            string HeaderText = lines[0];
            KeyValuePair<string, string> de;
            for (int i = 1; i < lines.Length; i++)
            {
                string line = lines[i];
                if (Regex.IsMatch(line, @"^\s"))
                    HeaderText += line;
                else
                {
                    de = GetHeader(HeaderText);
                    if (de.Key != null)
                        Headers.Add(de);
                    HeaderText = line;
                }
            }
            de = GetHeader(HeaderText);
            if (de.Key != null)
                Headers.Add(de);
            return Headers;
        }

        public MailMessage GetAsMailMessage()
        {
            MailMessage m = new MailMessage();

            try
            {
                string SenderDisplayName = GetMapiStringValue(mapi.EntryTypes.MessageSenderName);
                string SenderEmailAddress = GetMapiStringValue(mapi.EntryTypes.MessageSenderEmailAddress);
                MailAddress Sender = new MailAddress(SenderEmailAddress, SenderDisplayName);
                m.Sender = Sender;
            }
            catch (FormatException e)
            {
                /*
                 * This happens when the e-mail address is an directory-name-style entry
                 * We'll ignore this for now and pick it up in the headers
                 */
            }

            try
            {
                string OriginatorDisplayName = GetMapiStringValue(mapi.EntryTypes.MessageOriginatorName);
                string OriginatorEmailAddress = GetMapiStringValue(mapi.EntryTypes.MessageOriginatorEmailAddress);
                MailAddress Originator = new MailAddress(OriginatorEmailAddress, OriginatorDisplayName);
                m.From = Originator;
            }
            catch (FormatException e)
            {
                /*
                 * This happens when the e-mail address is an directory-name-style entry
                 * We'll ignore this for now and pick it up in the headers
                 */
            }

            
            foreach (MailAddress a in GetRecipients(PFFRecipients.RecipientType.To))
                try
                {
                    m.To.Add(a);
                }
                catch (FormatException e) { /* we'll ignore this entirely for now */ }
            foreach (MailAddress a in GetRecipients(PFFRecipients.RecipientType.CC))
                try
                {
                    m.CC.Add(a);
                }
                catch (FormatException e) { /* we'll ignore this entirely for now */ }
            foreach (MailAddress a in GetRecipients(PFFRecipients.RecipientType.BCC))
                try
                {
                    m.Bcc.Add(a);
                }
                catch (FormatException e) { /* we'll ignore this entirely for now */ }

            string textbody = GetBody(BodyType.PlainText);
            string htmlbody = GetBody(BodyType.HTML);
            m.IsBodyHtml = true;
            if (textbody != null && textbody.Length > 0)
            {
                m.Body = textbody;
                m.IsBodyHtml = false;
            }
            else if (htmlbody != null && htmlbody.Length > 0)
            {
                m.Body = htmlbody;
            }
            if (! m.IsBodyHtml && htmlbody != null)
            {
                AlternateView alternate = AlternateView.CreateAlternateViewFromString(htmlbody, new ContentType("text/html"));
                m.AlternateViews.Add(alternate);
            }

            m.Subject = GetName();

            Priority p = GetPriority();
            switch (p)
            {
                case Priority.High: m.Priority = MailPriority.High; break;
                case Priority.Low: m.Priority = MailPriority.Low; break;
                case Priority.Normal: m.Priority = MailPriority.Normal; break;
            }

            string[] NoAddHeaders = { "to", "from", "bcc", "cc", "subject", "x-priority", "importance", "mime-version", "sender" };
            foreach (KeyValuePair<string,string> de in GetHeaders())
            {
                if (! NoAddHeaders.Contains<string>(de.Key.ToLower()) && de.Value.Length > 0)
                {
                    if (de.Key.ToLower().Equals("x-rcpt-to") && m.Bcc.Count == 0)
                    {
                        m.Bcc.Add(de.Value);
                    }
                    m.Headers.Add(de.Key, de.Value);
                } else if (de.Key.ToLower().Equals("from") && m.From == null)
                {
                    m.From = new MailAddress(de.Value);
                }
                else if (de.Key.ToLower().Equals("sender") && m.Sender == null)
                {
                    m.Sender = new MailAddress(de.Value);
                }
                else if (de.Key.ToLower().Equals("to") && m.To.Count == 0)
                {
                    try
                    {
                        m.To.Add(de.Value);
                    }
                    catch (FormatException e)
                    {
                        /* Generally, this will happen if "to" is "undisclosed recipients", etc.  We'll try to catch it in x-rcpt-to */
                    }
                }
                else if (de.Key.ToLower().Equals("cc") && m.CC.Count == 0)
                {
                    m.CC.Add(de.Value);
                }
            }

            if (m.To.Count == 0)
            {
                string displayto = GetMapiStringValue(mapi.EntryTypes.MessageReceivedByEmailAddress);
                m.To.Add(displayto);
            }

            foreach (PFFAttachment pa in GetAttachments())
            {
                Attachment a = new Attachment(new PFFAttachmentStream(pa.Handler), pa.GetName(), pa.GetMimeType());
                m.Attachments.Add(a);
            }
            
            return m;
        }

        public override string GetName()
        {
            string r = GetMapiStringValue(mapi.EntryTypes.MessageSubject);
            if (r != null)
                r = Regex.Replace(r, "^[\\x00-\\x32]*", "");
            else
                r = "";
            return r;
        }

        unsafe virtual public string GetBody(BodyType PreferredType)
        {
            UIntPtr StringSize = new UIntPtr();
            IntPtr ErrorMessage = new IntPtr();
            string value = null;
            int r = 0;
            switch (PreferredType)
            {
                case BodyType.HTML:
                    r = libpff.GetBodyHTMLSize(_ItemHandler, out StringSize, out ErrorMessage);
                    break;
                case BodyType.RTF:
                    r = libpff.GetBodyRTFSize(_ItemHandler, out StringSize, out ErrorMessage);
                    break;
                case BodyType.PlainText:
                    r = libpff.GetBodyPlainTextSize(_ItemHandler, out StringSize, out ErrorMessage);
                    break;
            }
            
            if (r == -1)
                throw new PFFMessageDataError("Error reading body string size: " + libpff.GetErrorsFromIntPtr(ErrorMessage)[0]);
            int size = (int)StringSize.ToUInt32();
            if (size > 0)
            {
                byte[] buffer = new byte[size];
                fixed (byte* p = buffer)
                {
                    IntPtr StringValue = (IntPtr)p;
                    switch (PreferredType)
                    {
                        case BodyType.HTML:
                            r = libpff.GetBodyHTML(_ItemHandler, StringValue, StringSize, out ErrorMessage);
                            break;
                        case BodyType.RTF:
                            r = libpff.GetBodyRTF(_ItemHandler, StringValue, StringSize, out ErrorMessage);
                            break;
                        case BodyType.PlainText:
                            r = libpff.GetBodyPlainText(_ItemHandler, StringValue, StringSize, out ErrorMessage);
                            break;
                    }
                    if (r == -1)
                        throw new PFFItemDataError("Error reading body value: " + libpff.GetErrorsFromIntPtr(ErrorMessage)[0]);
                    value = Encoding.UTF8.GetString(buffer);
                }
                value = value.TrimEnd(new char[] { '\0' });
            }
            return value;
        }

        public List<PFFAttachment> GetAttachments()
        {
            List<PFFAttachment> Attachments = new List<PFFAttachment>();
            IntPtr ErrorMessage = new IntPtr();
            IntPtr AttachmentRef = new IntPtr();
            int AttachmentCount = 0;
            int r = libpff.GetMessageAttachmentCount(_ItemHandler, out AttachmentCount, out ErrorMessage);
            if (r == -1)
                throw new PFFMessageDataError("Error reading attachment count: " + libpff.GetErrorsFromIntPtr(ErrorMessage)[0]);
            if (AttachmentCount > 0)
            {
                r = libpff.GetMessageAttachments(_ItemHandler, out AttachmentRef, out ErrorMessage);
                if (r == -1)
                    throw new PFFMessageDataError("Error reading attachments: " + libpff.GetErrorsFromIntPtr(ErrorMessage)[0]);
                if (r == 1)
                {
                    for (int i = 0; i < AttachmentCount; i++)
                    {
                        IntPtr a = new IntPtr();
                        r = libpff.GetMessageAttachment(_ItemHandler, i, out a, out ErrorMessage);
                        if (r == -1)
                            throw new PFFMessageDataError("Error reading attachment " + i + ": " + libpff.GetErrorsFromIntPtr(ErrorMessage)[0]);
                        Attachments.Add(new PFFAttachment(a));
                    }
                }
            }
            return Attachments;
        }

        public Importance GetImportance()
        {
            return (Importance)(GetMapiIntegerValue(mapi.EntryTypes.MessageImportance));
        }
    }
}
