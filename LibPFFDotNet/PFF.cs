using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPFFDotNet
{
    public class PFFFileOpenError : Exception
    {
        public PFFFileOpenError(string Message)
            : base(Message)
        {
        }
    }

    public class PFFFileDataError : Exception
    {
        public PFFFileDataError(string Message)
            : base(Message)
        {
        }
    }

    public class PFFFileCloseError : Exception
    {
        public PFFFileCloseError(string Message)
            : base(Message)
        {
        }
    }

    public class PFFFileDetailsError : Exception
    {
        public PFFFileDetailsError(string Message)
            : base(Message)
        {
        }
    }

    /// <summary>
    /// Class is used to interact with PFF files and should be the entry point for any downstream PFF classes
    /// </summary>
    public class PFF
    {
        public enum AccessFlags : int { Read = 1, Write = 2 };
        public enum ExportModes : int { All = 'a', Debug = 'd', Items = 'i', NoAttachments = 'n', Recovered = 'r' };
        public enum RecoveryFlags : uint { IgnoreAllocationData = 1, ScanForFragments = 2 };
        public enum FileType : uint { PAB = 'a', PST = 'p', OST = 'o' };
        public enum EncryptionType : uint { None = 0, Compressible = 1, High = 2 };

        bool _Opened = false, _DetailsLoaded = false;
        IntPtr _PFFHandler;
        uint _FileType = 0, _Architecture = 0, _EncryptionType = 0;
        int _Codepage = -1, _NumberOfOrphanItems = 0, _NumberOfRecoveredItems = 0;
        string _FileName;

        /// <summary>
        /// Loads the details of the PFF file into this PFF object
        /// </summary>
        public void LoadFileDetails()
        {
            IntPtr ErrorMessage = new IntPtr();
            if (_Opened)
            {
                int r = libpff.GetFileType(_PFFHandler, out _FileType, out ErrorMessage); // this is in enum pff.FileType
                if (r == -1)
                    throw new PFFFileDetailsError("Error reading file type: " + libpff.GetErrorsFromIntPtr(ErrorMessage)[0]);

                r = libpff.GetArchitecture(_PFFHandler, out _Architecture, out ErrorMessage); // 32, 64, or 65 (for 64 bit + 4k page)
                if (r == -1)
                    throw new PFFFileDetailsError("Error reading file architecture: " + libpff.GetErrorsFromIntPtr(ErrorMessage)[0]);

                r = libpff.GetEncryptionType(_PFFHandler, out _EncryptionType, out ErrorMessage); // this is in enum pff.EncryptionType
                if (r == -1)
                    throw new PFFFileDetailsError("Error reading encryption details: " + libpff.GetErrorsFromIntPtr(ErrorMessage)[0]);

                r = libpff.GetCodepage(_PFFHandler, out _Codepage, out ErrorMessage); // this is in enum pff.EncryptionType
                if (r == -1)
                    throw new PFFFileDetailsError("Error reading codepage: " + libpff.GetErrorsFromIntPtr(ErrorMessage)[0]);

                /*
                r = libpff.GetNumberOfOrphanItems(PFFHandler, out NumberOfOrphanItems, out ErrorMessage); // this is in enum pff.EncryptionType
                if (r == -1)
                    throw new PFFFileDetailsError("Error reading number of orphan items: " + libpff.GetErrorsFromIntPtr(ErrorMessage)[0]);

                r = libpff.GetNumberOfRecoveredItems(PFFHandler, out NumberOfRecoveredItems, out ErrorMessage); // this is in enum pff.EncryptionType
                if (r == -1)
                    throw new PFFFileDetailsError("Error reading number of recovered items: " + libpff.GetErrorsFromIntPtr(ErrorMessage)[0]);
                */

                _DetailsLoaded = true;
            }
        }

        /// <summary>
        /// Converts the opened PFF file to a string representation
        /// </summary>
        /// <returns>A string containing the details of the PFF file</returns>
        public override string ToString()
        {
            if (_Opened)
            {
                if (! _DetailsLoaded)
                    LoadFileDetails();
                string res = _FileName + ": ";
                switch (_FileType)
                {
                    case (int)FileType.OST: res += "OST File; "; break;
                    case (int)FileType.PST: res += "PST File; "; break;
                    case (int)FileType.PAB: res += "PAB File; "; break;
                }
                switch (_EncryptionType)
                {
                    case (int)EncryptionType.None: res += "No encryption; "; break;
                    case (int)EncryptionType.Compressible: res += "Compressible encryption; "; break;
                    case (int)EncryptionType.High: res += "High encryption; "; break;
                }
                switch (_Architecture)
                {
                    case 32: res += "32bit"; break;
                    case 64: res += "64bit"; break;
                    case 65: res += "64bit+4k page"; break;
                }
                return res;
            }
            else return "";
        }

        /// <summary>
        /// Gets the root item of the opened PFF file
        /// </summary>
        /// <returns>>A PFFItem object pointing to the root item</returns>
        public PFFItem GetRootItem()
        {
            if (_Opened)
            {
                IntPtr RootItem, ErrorMessage;
                int r = libpff.GetRootItem(_PFFHandler, out RootItem, out ErrorMessage);
                if (r == -1)
                    throw new PFFFileDataError("Error getting root item: " + libpff.GetErrorsFromIntPtr(ErrorMessage)[0]);

                return new PFFItem(RootItem);
            }
            else
                throw new PFFFileDataError("File is not opened");
        }

        /// <summary>
        /// Gets the root folder of the opened PFF file
        /// </summary>
        /// <returns>A PFFFolder object pointing to the root folder</returns>
        public PFFFolder GetRootFolder()
        {
            if (_Opened)
            {
                IntPtr RootFolder, ErrorMessage;
                int r = libpff.GetRootFolder(_PFFHandler, out RootFolder, out ErrorMessage);
                if (r == -1)
                    throw new PFFFileDataError("Error getting root folder: " + libpff.GetErrorsFromIntPtr(ErrorMessage)[0]);

                return new PFFFolder(RootFolder);
            }
            else
                throw new PFFFileDataError("File is not opened");
        }

        /// <summary>
        /// Opens a PFF file at a given location and loads it into this object
        /// </summary>
        /// <param name="FilePath">The location of the PFF (.ost/.pst) file to open</param>
        public void Open(string FilePath)
        {
            IntPtr ErrorMessage;
            _FileName = FilePath;
            int r = libpff.CheckFileSignature(FilePath, out ErrorMessage);
            if (r == 0)
                throw new PFFFileOpenError("Not a valid personal file folder: " + libpff.GetErrorsFromIntPtr(ErrorMessage)[0]);
            else if (r == -1)
                throw new PFFFileOpenError("Error validating personal file folder signature: " + libpff.GetErrorsFromIntPtr(ErrorMessage)[0]);

            r = libpff.Initialize(out _PFFHandler, out ErrorMessage);
            if (r != 1)
                throw new PFFFileOpenError("Error initializing file: " + libpff.GetErrorsFromIntPtr(ErrorMessage)[0]);

            r = libpff.OpenFile(_PFFHandler, FilePath, (int)AccessFlags.Read, out ErrorMessage);
            if (r != 1)
                throw new PFFFileOpenError("Error opening file: " + libpff.GetErrorsFromIntPtr(ErrorMessage)[0]);

            r = libpff.IsCorrupted(_PFFHandler, out ErrorMessage);
            if (r == 1)
                throw new PFFFileOpenError("File is corrupted: " + libpff.GetErrorsFromIntPtr(ErrorMessage)[0]);
            else if (r == -1)
                throw new PFFFileOpenError("Error checking for file corruption: " + libpff.GetErrorsFromIntPtr(ErrorMessage)[0]);

            _Opened = true;
        }

        ~PFF()
        {
            /*IntPtr ErrorMessage;
            int r = libpff.FreeFile(PFFHandler, out ErrorMessage);*/
        }

        /// <summary>
        /// Closes the PFF file and frees memory.
        /// </summary>
        public void Close()
        {
            IntPtr ErrorMessage;
            if (_Opened)
            {
                int r = libpff.CloseFile(_PFFHandler, out ErrorMessage);
                if (r != 0)
                    throw new PFFFileCloseError("Error closing file: " + libpff.GetErrorsFromIntPtr(ErrorMessage)[0]);

                r = libpff.FreeFile(_PFFHandler, out ErrorMessage);
                if (r != 1)
                    throw new PFFFileCloseError("Error freeing file: " + libpff.GetErrorsFromIntPtr(ErrorMessage)[0]);

                _Opened = false;
            }
            else
                throw new PFFFileCloseError("No currently open file");
        }
    }
}
