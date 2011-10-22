using System.IO;

namespace GiveCRM.Admin.Web.Interfaces
{
    /// <summary>
    /// Thin wrapper around System.IO file system operations to make it easier to test.
    /// </summary>
    public interface IFileSystemWrapper
    {
        FileStream Open(string fileName, FileMode mode);
        FileStream Open(string fileName, FileMode mode, FileAccess access);
        FileStream Open(string fileName, FileMode mode, FileAccess access, FileShare share);
    }
}
