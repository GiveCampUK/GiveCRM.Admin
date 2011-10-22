using System;
using System.IO;

namespace GiveCRM.Admin.BusinessLogic
{
    public interface IExcelImportService
    {
        event Action<object, ImportDataCompletedEventArgs> ImportCompleted;
        void ImportAsync(Stream file);
    }
}