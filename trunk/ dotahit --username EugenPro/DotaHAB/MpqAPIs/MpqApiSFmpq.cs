using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace DotaHIT
{
#pragma warning disable 649
    class MpqApi
    {        
        #region dll_import
        [DllImport("SFmpq")]
        extern public static bool MpqInitialize();
        [DllImport("SFmpq")]
        extern public static string MpqGetVersionString();
        [DllImport("SFmpq")]
        extern public static float MpqGetVersion();        

        // Staredit Function Prototypes

        [DllImport("SFmpq")]
        extern public static IntPtr MpqOpenArchiveForUpdate(string lpFileName, UInt32 dwFlags, UInt32 dwMaximumFilesInArchive);
        [DllImport("SFmpq")]
        extern public static UInt32 MpqCloseUpdatedArchive(IntPtr hMPQ, UInt32 dwUnknown2);
        [DllImport("SFmpq",SetLastError=true)]
        extern public static bool MpqAddFileToArchive(IntPtr hMPQ, string lpSourceFileName, string lpDestFileName, UInt32 dwFlags);
        [DllImport("SFmpq")]
        extern public static bool MpqAddWaveToArchive(IntPtr hMPQ, string lpSourceFileName, string lpDestFileName, UInt32 dwFlags, UInt32 dwQuality);
        [DllImport("SFmpq")]
        extern public static bool MpqRenameFile(IntPtr hMPQ, string lpcOldFileName, string lpcNewFileName);
        [DllImport("SFmpq")]
        extern public static bool MpqDeleteFile(IntPtr hMPQ, string lpFileName);
        [DllImport("SFmpq")]
        extern public static bool MpqCompactArchive(IntPtr hMPQ);

        // Storm Function Prototypes

        [DllImport("SFmpq")]
        extern public static bool SFileOpenArchive(string lpFilename, UInt32 dwMPQID, UInt32 p3, ref IntPtr hMPQ);
        [DllImport("SFmpq")]
        extern public static bool SFileCloseArchive(IntPtr hMPQ);
        [DllImport("SFmpq")]
        extern public static bool SFileOpenFileEx(IntPtr hMPQ, string lpFileName, UInt32 dwFlags, out IntPtr hFile);
        [DllImport("SFmpq")]
        extern public static bool SFileCloseFile(IntPtr hFile);
        [DllImport("SFmpq")]
        extern public static UInt32 SFileGetFileSize(IntPtr hFile, UInt32 dwFlags);
        [DllImport("SFmpq")]
        extern public static UInt32 SFileGetFileInfo(IntPtr hFile, UInt32 dwInfoType);
        [DllImport("SFmpq")]
        extern public static UInt32 SFileSetFilePointer(IntPtr hFile, long lDistanceToMove, ref Int32 lplDistanceToMoveHigh, UInt32 dwMoveMethod);
        [DllImport("SFmpq")]
        extern public static bool SFileReadFile(IntPtr fileHandle, byte[] buf, uint numberOfBytesToRead, out uint numberOfBytesRead, IntPtr unused);
        [DllImport("SFmpq")]
        extern public static int SFileSetLocale(int nNewLocale);

        // Extra storm-related functions
        
        [DllImport("SFmpq")]
        extern public static bool SFileSetArchivePriority(IntPtr hMPQ, UInt32 dwPriority);
        [DllImport("SFmpq")]
        extern public static UInt32 SFileFindMpqHeader(int hFile);
        [DllImport("SFmpq")]
        extern public static bool SFileListFiles(IntPtr hMPQ, string lpFileLists, FILELISTENTRY[] lpListBuffer, UInt32 dwFlags);

        // Additional functions

        [DllImport("Advapi32")]
        extern public static bool ImpersonateSelf(SECURITY_IMPERSONATION_LEVEL ImpersonationLevel);

        #endregion

        #region constants
        //General error codes
        public const UInt32 MPQ_ERROR_INIT_FAILED     = 0x85000001; //Unspecified error
        public const UInt32 MPQ_ERROR_NO_STAREDIT = 0x85000002; //Can't find StarEdit.exe
        public const UInt32 MPQ_ERROR_BAD_STAREDIT = 0x85000003; //Bad version of StarEdit.exe. Need SC/BW 1.07
        public const UInt32 MPQ_ERROR_STAREDIT_RUNNING = 0x85000004; //StarEdit.exe is running. Must be closed

        public const UInt32 MPQ_ERROR_MPQ_INVALID = 0x85200065;
        public const UInt32 MPQ_ERROR_FILE_NOT_FOUND = 0x85200066;
        public const UInt32 MPQ_ERROR_DISK_FULL = 0x85200068; //Physical write file to MPQ failed. Not sure of exact meaning
        public const UInt32 MPQ_ERROR_HASH_TABLE_FULL = 0x85200069;
        public const UInt32 MPQ_ERROR_ALREADY_EXISTS = 0x8520006A;
        public const UInt32 MPQ_ERROR_BAD_OPEN_MODE = 0x8520006C; //When MOAU_READ_ONLY is used without MOAU_OPEN_EXISTING

        public const UInt32 MPQ_ERROR_COMPACT_ERROR = 0x85300001;

        //MpqOpenArchiveForUpdate flags
        public const UInt32 MOAU_CREATE_NEW       = 0x00;
        public const UInt32 MOAU_CREATE_ALWAYS    = 0x08; //Was wrongly named MOAU_CREATE_NEW
        public const UInt32 MOAU_OPEN_EXISTING    = 0x04;
        public const UInt32 MOAU_OPEN_ALWAYS      = 0x20;
        public const UInt32 MOAU_READ_ONLY        = 0x10; //Must be used with MOAU_OPEN_EXISTING
        public const UInt32 MOAU_MAINTAIN_LISTFILE= 0x01;

        // AddFileToArchive flags
        public const UInt32 MAFA_EXISTS           = 0x80000000; //Will be added if not present
        public const UInt32 MAFA_UNKNOWN40000000  = 0x40000000;
        public const UInt32 MAFA_MODCRYPTKEY      = 0x00020000;
        public const UInt32 MAFA_ENCRYPT          = 0x00010000;
        public const UInt32 MAFA_COMPRESS         = 0x00000200;
        public const UInt32 MAFA_COMPRESS2        = 0x00000100;
        public const UInt32 MAFA_REPLACE_EXISTING = 0x00000001;

        // AddWAVToArchive flags
        public const UInt32 MAWA_QUALITY_HIGH     = 1;
        public const UInt32 MAWA_QUALITY_MEDIUM   = 0;
        public const UInt32 MAWA_QUALITY_LOW      = 2;

        // SFileGetFileInfo flags
        public const UInt32 SFILE_INFO_BLOCK_SIZE     = 0x01; //Block size in MPQ
        public const UInt32 SFILE_INFO_HASH_TABLE_SIZE= 0x02; //Hash table size in MPQ
        public const UInt32 SFILE_INFO_NUM_FILES      = 0x03; //Number of files in MPQ
        public const UInt32 SFILE_INFO_TYPE           = 0x04; //Is MPQHANDLE a file or an MPQ?
        public const UInt32 SFILE_INFO_SIZE           = 0x05; //Size of MPQ or uncompressed file
        public const UInt32 SFILE_INFO_COMPRESSED_SIZE= 0x06; //Size of compressed file
        public const UInt32 SFILE_INFO_FLAGS          = 0x07; //File flags (compressed, etc.)
        public const UInt32 SFILE_INFO_PARENT         = 0x08; //Handle of MPQ that file is in
        public const UInt32 SFILE_INFO_POSITION       = 0x09; //Position of file pointer in files

        // SFileListFiles flags
        public const UInt32 SFILE_LIST_MEMORY_LIST  = 0x01; // Specifies that lpFilelists is a file list from memory, rather than being a list of file lists
        public const UInt32 SFILE_LIST_ONLY_KNOWN   = 0x02; // Only list files that the function finds a name for
        public const UInt32 SFILE_LIST_ONLY_UNKNOWN = 0x04; // Only list files that the function does not find a name for

        public const UInt32 SFILE_TYPE_MPQ = 0x01;
        public const UInt32 SFILE_TYPE_FILE= 0x02;

        public const UInt32  SFILE_OPEN_HARD_DISK_FILE= 0x0000; //Open archive without regard to the drive type it resides on
        public const UInt32  SFILE_OPEN_CD_ROM_FILE   = 0x0001; //Open the archive only if it is on a CD-ROM
        public const UInt32  SFILE_OPEN_ALLOW_WRITE   = 0x8000; //Open file with write access

        public const UInt32  SFILE_SEARCH_CURRENT_ONLY= 0x00; //Used with SFileOpenFileEx; only the archive with the handle specified will be searched for the file
        public const UInt32  SFILE_SEARCH_ALL_OPEN    = 0x01; //SFileOpenFileEx will look through all open archives for the file
        #endregion

        #region structs
        public struct SFMPQVERSION
        {
	        public UInt16 Major;
            public UInt16 Minor;
            public UInt16 Revision;
            public UInt16 Subrevision;
        };
        public struct FILELISTENTRY 
        {
            public UInt32 dwFileExists; // Nonzero if this entry is used
            public int lcLocale; // Locale ID of file
            public UInt32 dwCompressedSize; // Compressed size of file
            public UInt32 dwFullSize; // Uncompressed size of file
            public UInt32 dwFlags; // Flags for file

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 260)]
            public char[] szFileName;            
        };
        public struct MPQHEADER
        {
            public UInt32 dwMPQID; //"MPQ\x1A" for mpq's, "BN3\x1A" for bncache.dat
            public UInt32 dwHeaderSize; // Size of this header
            public UInt32 dwMPQSize; //The size of the mpq archive
            public UInt16 wUnused0C; // Seems to always be 0
            public UInt16 wBlockSize; // Size of blocks in files equals 512 << wBlockSize
            public UInt32 dwHashTableOffset; // Offset to hash table
            public UInt32 dwBlockTableOffset; // Offset to block table
            public UInt32 dwHashTableSize; // Number of entries in hash table
            public UInt32 dwBlockTableSize; // Number of entries in block table
        };

        /// <summary>
        /// Archive handles may be typecasted to this struct so you can access
        /// some of the archive's properties and the decrypted hash table and
        /// block table directly.
        /// </summary>
        public struct MPQARCHIVE
        {
            // Arranged according to priority with lowest priority first
            public MPQARCHIVE[] lpNextArc; // Pointer to the next ARCHIVEREC struct. Pointer to addresses of first and last archives if last archive
            public MPQARCHIVE[] lpPrevArc; // Pointer to the previous ARCHIVEREC struct. 0xEAFC5E23 if first archive

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 260)]
            public char[] szFileName; // Filename of the archive

            public int hFile; // The archive's file handle
            public UInt32 dwFlags1; // Some flags, bit 1 (0 based) seems to be set when opening an archive from a CD
            public UInt32 dwPriority; // Priority of the archive set when calling SFileOpenArchive
            public MPQFILE[] lpLastReadFile; // Pointer to the last read file's FILEREC struct. Only used for incomplete reads of blocks
            public UInt32 dwUnk; // Seems to always be 0
            public UInt32 dwBlockSize; // Size of file blocks in bytes
            public byte[] lpLastReadBlock; // Pointer to the read buffer for archive. Only used for incomplete reads of blocks
            public UInt32 dwBufferSize; // Size of the read buffer for archive. Only used for incomplete reads of blocks
            public UInt32 dwMPQStart; // The starting offset of the archive
            public MPQHEADER[] lpMPQHeader; // Pointer to the archive header
            public BLOCKTABLEENTRY[] lpBlockTable; // Pointer to the start of the block table
            public HASHTABLEENTRY[] lpHashTable; // Pointer to the start of the hash table
            public UInt32 dwFileSize; // The size of the file in which the archive is contained
            public UInt32 dwOpenFiles; // Count of files open in archive + 1
            public MPQHEADER MpqHeader;
            public UInt32 dwFlags; //The only flag that should be changed is MOAU_MAINTAIN_LISTFILE
            public string lpFileName;
        };
        
        /// <summary>
        /// Handles to files in the archive may be typecasted to this struct
        /// so you can access some of the file's properties directly.
        /// </summary>
        public struct MPQFILE
        {
            public MPQFILE[] lpNextFile; // Pointer to the next FILEREC struct. Pointer to addresses of first and last files if last file

            public MPQFILE[] lpPrevFile; // Pointer to the previous FILEREC struct. 0xEAFC5E13 if first file

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 260)]
            public char[] szFileName; // Filename of the file

            public int hPlaceHolder; // Always 0xFFFFFFFF
            public MPQARCHIVE[] lpParentArc; // Pointer to the ARCHIVEREC struct of the archive in which the file is contained
            public BLOCKTABLEENTRY[] lpBlockEntry; // Pointer to the file's block table entry
            public UInt32 dwCryptKey; // Decryption key for the file
            public UInt32 dwFilePointer; // Position of file pointer in the file
            public UInt32 dwUnk1; // Seems to always be 0
            public UInt32 dwBlockCount; // Number of blocks in file
            public UInt32[] lpdwBlockOffsets; // Offsets to blocks in file. There are 1 more of these than the number of blocks
            public UInt32 dwReadStarted; // Set to 1 after first read
            public UInt32 dwUnk2; // Seems to always be 0
            public byte[] lpLastReadBlock; // Pointer to the read buffer for file. Only used for incomplete reads of blocks
            public UInt32 dwBytesRead; // Total bytes read from open file
            public UInt32 dwBufferSize; // Size of the read buffer for file. Only used for incomplete reads of blocks
            public UInt32 dwConstant; // Seems to always be 1
            public HASHTABLEENTRY[] lpHashEntry;
            public string lpFileName;
        };

        public struct BLOCKTABLEENTRY
        {
            public UInt32 dwFileOffset; // Offset to file
            public UInt32 dwCompressedSize; // Compressed size of file
            public UInt32 dwFullSize; // Uncompressed size of file
            public UInt32 dwFlags; // Flags for file
        };
        public struct HASHTABLEENTRY
        {
            public UInt32 dwNameHashA; // First name hash of file
            public UInt32 dwNameHashB; // Second name hash of file
            public int lcLocale; // Locale ID of file
            public UInt32 dwBlockTableIndex; // Index to the block table entry for the file
        };
        #endregion

        #region enums
        public enum SECURITY_IMPERSONATION_LEVEL : int
        {
            SecurityAnonymous = 0,
            SecurityIdentification = 1,
            SecurityImpersonation = 2,
            SecurityDelegation = 3
        }
        #endregion
    }
#pragma warning restore 649
}
