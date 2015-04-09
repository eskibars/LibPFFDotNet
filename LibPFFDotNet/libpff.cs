using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace LibPFFDotNet
{
    public class mapi
    {
        public enum EntryTypes : uint
        {
            MessageImportance = 0x0017,

            MessageClass = 0x001a,

            MessagePriority = 0x0026,

            MessageSensitivity = 0x0036,
            MessageSubject = 0x0037,

            MessageClientSubmitTime = 0x0039,

            LIBPFF_ENTRY_TYPE_MESSAGE_SENT_REPRESENTING_SEARCH_KEY = 0x003b,

            LIBPFF_ENTRY_TYPE_MESSAGE_RECEIVED_BY_ENTRY_IDENTIFIER = 0x003f,
            LIBPFF_ENTRY_TYPE_MESSAGE_RECEIVED_BY_NAME = 0x0040,
            LIBPFF_ENTRY_TYPE_MESSAGE_SENT_REPRESENTING_ENTRY_IDENTIFIER = 0x0041,
            LIBPFF_ENTRY_TYPE_MESSAGE_RECEIVED_REPRESENTING_ENTRY_IDENTIFIER = 0x0043,
            LIBPFF_ENTRY_TYPE_MESSAGE_RECEIVED_REPRESENTING_NAME = 0x0044,

            LIBPFF_ENTRY_TYPE_MESSAGE_REPLY_RECIPIENT_ENTRIES = 0x004f,
            LIBPFF_ENTRY_TYPE_MESSAGE_REPLY_RECIPIENT_NAMES = 0x0050,
            LIBPFF_ENTRY_TYPE_MESSAGE_RECEIVED_BY_SEARCH_KEY = 0x0051,
            LIBPFF_ENTRY_TYPE_MESSAGE_RECEIVED_REPRESENTING_SEARCH_KEY = 0x0052,

            MessageConversationTopic = 0x0070,
            MessageConversationIndex = 0x0071,

            LIBPFF_ENTRY_TYPE_MESSAGE_RECEIVED_REPRESENTING_ADDRESS_TYPE = 0x0077,
            LIBPFF_ENTRY_TYPE_MESSAGE_RECEIVED_REPRESENTING_EMAIL_ADDRESS = 0x0078,

            MessageHeaders = 0x007d,

            RecipientType = 0x0c15,

            LIBPFF_ENTRY_TYPE_MESSAGE_SENDER_ENTRY_IDENTIFIER = 0x0c19,

            LIBPFF_ENTRY_TYPE_MESSAGE_SENDER_SEARCH_KEY = 0x0c1d,
            
            MessageSenderName = 0x0c1a,
            MessageSenderAddressType = 0x0c1e,
            MessageSenderEmailAddress = 0x0c1f,
            MessageReceivedByAddressType = 0x0075,
            MessageReceivedByEmailAddress = 0x0076,
            MessageOriginatorName = 0x0042,
            MessageOriginatorAddressType = 0x0064,
            MessageOriginatorEmailAddress = 0x0065,

            LIBPFF_ENTRY_TYPE_MESSAGE_DISPLAY_TO = 0x0e04,

            MessageDeliveryTime = 0x0e06,
            MessageFlags = 0x0e07,
            MessageSize = 0x0e08,

            MessageStatus = 0x0e17,

            AttachmentSize = 0x0e20,

            LIBPFF_ENTRY_TYPE_MESSAGE_INTERNET_ARTICLE_NUMBER = 0x0e23,

            MessagePermission = 0x0e27,

            LIBPFF_ENTRY_TYPE_MESSAGE_URL_COMPUTER_NAME_SET = 0x0e62,

            MessageTrustSender = 0x0e79,

            MessageBodyText = 0x1000,
            MessageBodyRTF = 0x1009,
            MessageBodyHTML = 0x1013,

            LIBPFF_ENTRY_TYPE_EMAIL_EML_FILENAME = 0x10f3,

            DisplayName = 0x3001,
            AddressType = 0x3002,
            EmailAddress = 0x3003,

            MessageCreationTime = 0x3007,
            MessageModificationTime = 0x3008,

            LIBPFF_ENTRY_TYPE_MESSAGE_STORE_VALID_FOLDER_MASK = 0x35df,

            FolderType = 0x3601,
            NumberOfContentItems = 0x3602,
            NumberOfUnreadContentItems = 0x3603,

            HasSubfolders = 0x360a,

            ContainerClass = 0x3613,

            LIBPFF_ENTRY_TYPE_NUMBER_OF_ASSOCIATED_CONTENT = 0x3617,

            AttachmentDataObject = 0x3701,
            AttachmentFilenameShort = 0x3704,
            AttachmentFilenameLong = 0x3707,
            AttachmentMimeType = 0x370e,

            AttachmentConentID = 0x3712,
            AttachmentContentLocation = 0x3713,
            AttachmentContentDisposition = 0x3716,

            AttachmentMethod = 0x3705,

            LIBPFF_ENTRY_TYPE_ATTACHMENT_RENDERING_POSITION = 0x370b,

            LIBPFF_ENTRY_TYPE_CONTACT_GENERATIONAL_ABBREVIATION = 0x3a05,

            ContactInitials = 0x3a0a,
            ContactSurname = 0x3a11,
            ContactGivenName = 0x3a06,

            ContactPrimaryPhoneNumber = 0x3a1a,
            ContactBusinessPhoneNumber1 = 0x3a08,
            ContactBusinessPhoneNumber2 = 0x3a1b,
            ContactHomePhoneNumber = 0x3a09,
            ContactMobilePhoneNumber = 0x3a1c,
            ContactBusinessFaxNumber = 0x3a24,
            ContactCallbackPhoneNumber = 0x3a02,

            ContactEmailAddress1 = 0x8083,
            ContactEmailAddress2 = 0x8093,
            ContactEmailAddress3 = 0x80a3,

            ContactPostalAddress = 0x3a15,
            ContactCompanyName = 0x3a16,
            ContactJobTitle = 0x3a17,
            ContactDepartmentName = 0x3a18,
            LIBPFF_ENTRY_TYPE_CONTACT_OFFICE_LOCATION = 0x3a19,

            ContactCountry = 0x3a26,
            LIBPFF_ENTRY_TYPE_CONTACT_LOCALITY = 0x3a27,

            ContactTitle = 0x3a45,

            MessageBodyCodepage = 0x3fde,

            MessageCodepage = 0x3ffd,

            RecipientDisplayName = 0x5ff6,

            FolderChildCount = 0x6638,

            LIBPFF_ENTRY_TYPE_SUB_ITEM_IDENTIFIER = 0x67f2,

            LIBPFF_ENTRY_TYPE_MESSAGE_STORE_PASSWORD_CHECKSUM = 0x67ff,

            LIBPFF_ENTRY_TYPE_ADDRESS_FILE_UNDER = 0x8005,

            DistributionListName = 0x8053,
            LIBPFF_ENTRY_TYPE_DISTRIBUTION_LIST_MEMBER_ONE_OFF_ENTRY_IDENTIFIERS = 0x8054,
            LIBPFF_ENTRY_TYPE_DISTRIBUTION_LIST_MEMBER_ENTRY_IDENTIFIERS = 0x8055,

            TaskStatus = 0x8101,
            TaskPercentageCommplete = 0x8102,
            TaskStartDate = 0x8104,
            TaskDueDate = 0x8105,
            TaskActualEffort = 0x8110,
            TaskTotalEffort = 0x8111,
            TaskVersion = 0x8112,
            TaskIsComplete = 0x811c,
            TaskIsRecurring = 0x8126,

            AppointmentBusyStatus = 0x8205,

            AppointmentLocation = 0x8208,
            AppointmentStartTime = 0x820d,
            AppointmentEndTime = 0x820e,
            AppointmentDuration = 0x8213,

            AppointmentIsRecurring = 0x8223,
            AppointmentRecurrencePattern = 0x8232,

            AppointmentTimezone = 0x8234,
            AppointmentFirstEffectiveTime = 0x8235,
            AppointmentLastEffectiveTime = 0x8236,

            MessageReminderTime = 0x8502,
            MessageIsReminder = 0x8503,

            MessageIsPrivate = 0x8506,

            LIBPFF_ENTRY_TYPE_MESSAGE_REMINDER_SIGNAL_TIME = 0x8550
        };
    }

    public class codepage
    {
        public enum Codepages : int
        {
            ASCII = 20127,

            ISO_8859_1 = 28591,
            ISO_8859_2 = 28592,
            ISO_8859_3 = 28593,
            ISO_8859_4 = 28594,
            ISO_8859_5 = 28595,
            ISO_8859_6 = 28596,
            ISO_8859_7 = 28597,
            ISO_8859_8 = 28598,
            ISO_8859_9 = 28599,
            ISO_8859_10 = 28600,
            ISO_8859_11 = 28601,
            ISO_8859_13 = 28603,
            ISO_8859_14 = 28604,
            ISO_8859_15 = 28605,
            ISO_8859_16 = 28606,

            KOI8_R = 20866,
            KOI8_U = 21866,

            WINDOWS_874 = 874,
            WINDOWS_932 = 932,
            WINDOWS_936 = 936,
            WINDOWS_949 = 949,
            WINDOWS_950 = 950,
            WINDOWS_1250 = 1250,
            WINDOWS_1251 = 1251,
            WINDOWS_1252 = 1252,
            WINDOWS_1253 = 1253,
            WINDOWS_1254 = 1254,
            WINDOWS_1255 = 1255,
            WINDOWS_1256 = 1256,
            WINDOWS_1257 = 1257,
            WINDOWS_1258 = 1258
        };

        public static string CodepageToString(int Codepage)
        {
            switch (Codepage)
            {
                case (int)Codepages.ASCII:
                    return ("ascii");

                case (int)Codepages.ISO_8859_1:
                    return ("iso-8859-1");

                case (int)Codepages.ISO_8859_2:
                    return ("iso-8859-2");

                case (int)Codepages.ISO_8859_3:
                    return ("iso-8859-3");

                case (int)Codepages.ISO_8859_4:
                    return ("iso-8859-4");

                case (int)Codepages.ISO_8859_5:
                    return ("iso-8859-5");

                case (int)Codepages.ISO_8859_6:
                    return ("iso-8859-6");

                case (int)Codepages.ISO_8859_7:
                    return ("iso-8859-7");

                case (int)Codepages.ISO_8859_8:
                    return ("iso-8859-8");

                case (int)Codepages.ISO_8859_9:
                    return ("iso-8859-9");

                case (int)Codepages.ISO_8859_10:
                    return ("iso-8859-10");

                case (int)Codepages.ISO_8859_11:
                    return ("iso-8859-11");

                case (int)Codepages.ISO_8859_13:
                    return ("iso-8859-13");

                case (int)Codepages.ISO_8859_14:
                    return ("iso-8859-14");

                case (int)Codepages.ISO_8859_15:
                    return ("iso-8859-15");

                case (int)Codepages.ISO_8859_16:
                    return ("iso-8859-16");

                case (int)Codepages.KOI8_R:
                    return ("koi8_r");

                case (int)Codepages.KOI8_U:
                    return ("koi8_u");

                case (int)Codepages.WINDOWS_874:
                    return ("cp874");

                case (int)Codepages.WINDOWS_932:
                    return ("cp932");

                case (int)Codepages.WINDOWS_936:
                    return ("cp936");

                case (int)Codepages.WINDOWS_949:
                    return ("cp949");

                case (int)Codepages.WINDOWS_950:
                    return ("cp950");

                case (int)Codepages.WINDOWS_1250:
                    return ("cp1250");

                case (int)Codepages.WINDOWS_1251:
                    return ("cp1251");

                case (int)Codepages.WINDOWS_1252:
                    return ("cp1252");

                case (int)Codepages.WINDOWS_1253:
                    return ("cp1253");

                case (int)Codepages.WINDOWS_1254:
                    return ("cp1254");

                case (int)Codepages.WINDOWS_1255:
                    return ("cp1255");

                case (int)Codepages.WINDOWS_1256:
                    return ("cp1256");

                case (int)Codepages.WINDOWS_1257:
                    return ("cp1257");

                case (int)Codepages.WINDOWS_1258:
                    return ("cp1258");

                default:
                    break;
            }
            return (null);
        }
    }

    public class libpff
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct ErrorStruct
        {
            public int domain;
            public int code;
            public int number_of_messages;
            public IntPtr messages_string_array;
            public IntPtr sizes_uint_array;
        }

        public static List<string> GetErrorsFromIntPtr(IntPtr P)
        {
            ErrorStruct r = (ErrorStruct)Marshal.PtrToStructure(P, typeof(ErrorStruct));
            List<string> Errors = new List<string>();
            for (int i = 0; i < r.number_of_messages; i++)
            {
                int ErrorLength = Marshal.ReadInt32(r.sizes_uint_array, i);
                if (ErrorLength > 0)
                {
                    IntPtr s = Marshal.ReadIntPtr(r.messages_string_array, i);
                    string Error = Marshal.PtrToStringAuto(s);
                    Errors.Add(Error);
                }
            }
            return Errors;
        }

        // dll functions
        [DllImport("libpff.dll", EntryPoint = "libpff_get_version")]
        public static extern string GetVersion();

        // file functions
        // "F" will be a reference for the file
        [DllImport("libpff.dll", EntryPoint = "libpff_check_file_signature")]
        public static extern int CheckFileSignature(string FilePath, out IntPtr ErrorMessage);
        [DllImport("libpff.dll", EntryPoint = "libpff_file_initialize")]
        public static extern int Initialize(out IntPtr F, out IntPtr ErrorMessage);
        [DllImport("libpff.dll", EntryPoint = "libpff_file_open")]
        public static extern int OpenFile(IntPtr F, string FilePath, int AccessFlags, out IntPtr ErrorMessage);
        [DllImport("libpff.dll", EntryPoint = "libpff_file_close")]
        public static extern int CloseFile(IntPtr F, out IntPtr ErrorMessage);
        [DllImport("libpff.dll", EntryPoint = "libpff_file_free")]
        public static extern int FreeFile(IntPtr F, out IntPtr ErrorMessage);
        [DllImport("libpff.dll", EntryPoint = "libpff_file_is_corrupted")]
        public static extern int IsCorrupted(IntPtr F, out IntPtr ErrorMessage);
        [DllImport("libpff.dll", EntryPoint = "libpff_file_recover_items")]
        public static extern int RecoverDeletedItems(IntPtr F, uint RecoveryFlags, out IntPtr ErrorMessage);
        [DllImport("libpff.dll", EntryPoint = "libpff_file_get_content_type")]
        public static extern int GetFileType(IntPtr F, out uint FileType, out IntPtr ErrorMessage);
        [DllImport("libpff.dll", EntryPoint = "libpff_file_get_type")]
        public static extern int GetArchitecture(IntPtr F, out uint Width, out IntPtr ErrorMessage);
        [DllImport("libpff.dll", EntryPoint = "libpff_file_get_encryption_type")]
        public static extern int GetEncryptionType(IntPtr F, out uint EncryptionType, out IntPtr ErrorMessage);
        [DllImport("libpff.dll", EntryPoint = "libpff_file_get_ascii_codepage")]
        public static extern int GetCodepage(IntPtr F, out int Codepage, out IntPtr ErrorMessage);
        [DllImport("libpff.dll", EntryPoint = "libpff_file_get_number_of_orphan_items")]
        public static extern int GetNumberOfOrphanItems(IntPtr F, out int NumberOfOrphanItems, out IntPtr ErrorMessage);
        [DllImport("libpff.dll", EntryPoint = "libpff_file_get_number_of_recovered_items")]
        public static extern int GetNumberOfRecoveredItems(IntPtr F, out int NumberOfRecoveredItems, out IntPtr ErrorMessage);
        
        // folders from file
        [DllImport("libpff.dll", EntryPoint = "libpff_file_get_root_folder")]
        public static extern int GetRootFolder(IntPtr F, out IntPtr RootFolder, out IntPtr ErrorMessage);

        // message store from file
        [DllImport("libpff.dll", EntryPoint = "libpff_file_get_message_store")]
        public static extern int GetMessageStore(IntPtr F, out IntPtr MessageStore, out IntPtr ErrorMessage);

        // items from file
        [DllImport("libpff.dll", EntryPoint = "libpff_file_get_root_item")]
        public static extern int GetRootItem(IntPtr F, out IntPtr RootItem, out IntPtr ErrorMessage);
        [DllImport("libpff.dll", EntryPoint = "libpff_file_get_item_by_identifier")]
        public static extern int GetItemByIdentifier(IntPtr F, UInt32 Identifier, out IntPtr Item, out IntPtr ErrorMessage);
        [DllImport("libpff.dll", EntryPoint = "libpff_file_get_orphan_item")]
        public static extern int GetOrphanItem(IntPtr F, int OrphanItemIndex, out IntPtr Item, out IntPtr ErrorMessage);
        [DllImport("libpff.dll", EntryPoint = "libpff_file_get_recovered_item")]
        public static extern int GetRecoveredItem(IntPtr F, int RecoveredItemIndex, out IntPtr Item, out IntPtr ErrorMessage);

        // folder functions
        [DllImport("libpff.dll", EntryPoint = "libpff_folder_get_number_of_sub_folders")]
        public static extern int GetNumberOfSubfolders(IntPtr F, out int NumberOfSubfolders, out IntPtr ErrorMessage);
        [DllImport("libpff.dll", EntryPoint = "libpff_folder_get_sub_folder")]
        public static extern int GetSubfolder(IntPtr F, int SubfolderIndex, out IntPtr Subfolder, out IntPtr ErrorMessage);
        [DllImport("libpff.dll", EntryPoint = "libpff_folder_get_number_of_sub_messages")]
        public static extern int GetNumberOfSubmessages(IntPtr F, out int NumberOfSubmessages, out IntPtr ErrorMessage);
        [DllImport("libpff.dll", EntryPoint = "libpff_folder_get_sub_message")]
        public static extern int GetSubmessage(IntPtr F, int SubmessageIndex, out IntPtr Submessage, out IntPtr ErrorMessage);
        [DllImport("libpff.dll", EntryPoint = "libpff_folder_get_type")]
        public static extern int GetFolderType(IntPtr F, out uint FolderType, out IntPtr ErrorMessage);


        // item functions
        // "I" will be a reference for the item
        [DllImport("libpff.dll", EntryPoint = "libpff_item_get_type")]
        public static extern int GetItemType(IntPtr I, out uint ItemType, out IntPtr ErrorMessage);
        [DllImport("libpff.dll", EntryPoint = "libpff_item_free")]
        public static extern int FreeItem(IntPtr I, out IntPtr ErrorMessage);
        [DllImport("libpff.dll", EntryPoint = "libpff_item_get_identifier")]
        public static extern int GetIdentifier(IntPtr I, out UInt32 Identifier, out IntPtr ErrorMessage);
        [DllImport("libpff.dll", EntryPoint = "libpff_item_get_number_of_entries")]
        public static extern int GetNumberOfEntries(IntPtr I, out UInt32 NumberOfEntries, out IntPtr ErrorMessage);
        [DllImport("libpff.dll", EntryPoint = "libpff_item_get_number_of_sets")]
        public static extern int GetNumberOfSets(IntPtr I, out UInt32 NumberOfSets, out IntPtr ErrorMessage);
        [DllImport("libpff.dll", EntryPoint = "libpff_item_get_number_of_sub_items")]
        public static extern int GetNumberOfSubitems(IntPtr I, out UInt32 NumberOfSubitems, out IntPtr ErrorMessage);
        [DllImport("libpff.dll", EntryPoint = "libpff_item_get_sub_item")]
        public static extern int GetSubitem(IntPtr I, int SubitemIndex, out IntPtr Subitem, out IntPtr ErrorMessage);

        // entries and subitems on the item
        [DllImport("libpff.dll", EntryPoint = "libpff_item_get_value_type")]
        public static extern int GetItemValueType(IntPtr I, int ItemIndex, UInt32 EntryType, out IntPtr ValueType, uint Flags, out IntPtr ErrorMessage);
        [DllImport("libpff.dll", EntryPoint = "libpff_item_get_sub_item")]
        public static extern int GetItemValueType(IntPtr I, int ItemIndex, int SubitemIndex, out IntPtr Subitem, out IntPtr ErrorMessage);

        [DllImport("libpff.dll", EntryPoint = "libpff_item_get_entry_value_utf8_string_size")]
        public static extern int GetStringEntrySize(IntPtr I, int SetIndex, UInt32 EntryType, out UIntPtr StringSize, uint Flags, out IntPtr ErrorMessage);
        [DllImport("libpff.dll", EntryPoint = "libpff_item_get_entry_value_utf8_string")]
        public static extern int GetStringEntry(IntPtr I, int SetIndex, UInt32 EntryType, IntPtr Value, UIntPtr StringSize, uint Flags, out IntPtr ErrorMessage);

        [DllImport("libpff.dll", EntryPoint = "libpff_item_get_entry_value_boolean")]
        public static extern int GetBooleanEntry(IntPtr I, int SetIndex, UInt32 EntryType, out uint Value, uint Flags, out IntPtr ErrorMessage);
        [DllImport("libpff.dll", EntryPoint = "libpff_item_get_entry_value_floating_point")]
        public static extern int GetFloatingPointEntry(IntPtr I, int SetIndex, UInt32 EntryType, out double Value, uint Flags, out IntPtr ErrorMessage);
        [DllImport("libpff.dll", EntryPoint = "libpff_item_get_entry_value_32bit")]
        public static extern int GetIntegerEntry(IntPtr I, int SetIndex, UInt32 EntryType, out UInt32 Value, uint Flags, out IntPtr ErrorMessage);
        [DllImport("libpff.dll", EntryPoint = "libpff_item_get_entry_value_filetime")]
        public static extern int GetFloatingPointEntry(IntPtr I, int SetIndex, UInt32 EntryType, out UInt64 Value, uint Flags, out IntPtr ErrorMessage);


        // message functions
        // "M" will be a reference for the item
        [DllImport("libpff.dll", EntryPoint = "libpff_message_get_number_of_attachments")]
        public static extern int GetMessageAttachmentCount(IntPtr M, out int AttachmentCount, out IntPtr ErrorMessage);
        [DllImport("libpff.dll", EntryPoint = "libpff_message_get_attachments")]
        public static extern int GetMessageAttachments(IntPtr M, out IntPtr Attachments, out IntPtr ErrorMessage);
        [DllImport("libpff.dll", EntryPoint = "libpff_message_get_attachment")]
        public static extern int GetMessageAttachment(IntPtr M, int AttachmentIndex, out IntPtr Attachment, out IntPtr ErrorMessage);
        [DllImport("libpff.dll", EntryPoint = "libpff_message_get_plain_text_body_size")]
        public static extern int GetBodyPlainTextSize(IntPtr M, out UIntPtr BodySize, out IntPtr ErrorMessage);
        [DllImport("libpff.dll", EntryPoint = "libpff_message_get_rtf_body_size")]
        public static extern int GetBodyRTFSize(IntPtr M, out UIntPtr BodySize, out IntPtr ErrorMessage);
        [DllImport("libpff.dll", EntryPoint = "libpff_message_get_html_body_size")]
        public static extern int GetBodyHTMLSize(IntPtr M, out UIntPtr BodySize, out IntPtr ErrorMessage);
        [DllImport("libpff.dll", EntryPoint = "libpff_message_get_plain_text_body")]
        public static extern int GetBodyPlainText(IntPtr M, IntPtr Value, UIntPtr StringSize, out IntPtr ErrorMessage);
        [DllImport("libpff.dll", EntryPoint = "libpff_message_get_rtf_body")]
        public static extern int GetBodyRTF(IntPtr M, IntPtr Value, UIntPtr StringSize, out IntPtr ErrorMessage);
        [DllImport("libpff.dll", EntryPoint = "libpff_message_get_html_body")]
        public static extern int GetBodyHTML(IntPtr M, IntPtr Value, UIntPtr StringSize, out IntPtr ErrorMessage);
        [DllImport("libpff.dll", EntryPoint = "libpff_message_get_recipients")]
        public static extern int GetRecipients(IntPtr M, out IntPtr Recipients, out IntPtr ErrorMessage);

        // attachment functions
        // "A" will be a reference for the attachment
        [DllImport("libpff.dll", EntryPoint = "libpff_attachment_get_type")]
        public static extern int GetAttachmentType(IntPtr A, out int AttachmentType, out IntPtr ErrorMessage);
        [DllImport("libpff.dll", EntryPoint = "libpff_attachment_get_data_size")]
        public static extern int GetAttachmentSize(IntPtr A, out UIntPtr AttachmentSize, out IntPtr ErrorMessage);
        [DllImport("libpff.dll", EntryPoint = "libpff_attachment_data_read_buffer")]
        public static extern IntPtr GetAttachmentData(IntPtr A, IntPtr Buffer, UIntPtr BufferSize, out IntPtr ErrorMessage);
        [DllImport("libpff.dll", EntryPoint = "libpff_attachment_data_seek_offset")]
        public static extern IntPtr AttachmentSeekOffset(IntPtr A, Int64 Offset, int Whence, out IntPtr ErrorMessage);
    }
}
