using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using GiveCRM.Admin.Web.Interfaces;

namespace GiveCRM.Admin.Web.Controllers
{
    public class FileSystemWrapper : IFileSystemWrapper
    {
        public FileStream Open(string fileName, FileMode mode)
        {
            return File.Open(fileName, mode);
        }

        public FileStream Open(string fileName, FileMode mode, FileAccess access)
        {
            return File.Open(fileName, mode, access);
        }

        public FileStream Open(string fileName, FileMode mode, FileAccess access, FileShare share)
        {
            return File.Open(fileName, mode, access, share);
        }
    }
}