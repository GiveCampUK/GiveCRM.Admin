using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using GiveCRM.ImportExport;
using Simple.Data;

namespace GiveCRM.Admin.BusinessLogic
{
    public class ExcelImportService : IExcelImportService
    {
        private readonly IExcelImport importer;

        public ExcelImportService(IExcelImport importer)
        {
            if (importer == null)
            {
                throw new ArgumentNullException("importer");
            }

            this.importer = importer;
        }

        #region IExcelImportService

        public event Action<object, ImportDataCompletedEventArgs> ImportCompleted;
        public event Action<object, ImportDataFailedEventArgs> ImportFailed;

        public void ImportAsync(Stream file, Action callback = null)
        {
            var importTask = Task.Factory.StartNew(() => Import(file, callback));

            importTask.ContinueWith(InvokeImportDataCompleted, TaskContinuationOptions.OnlyOnRanToCompletion);
            importTask.ContinueWith(InvokeImportErrorFailed, TaskContinuationOptions.OnlyOnFaulted);
        }

        #endregion


        public IEnumerable<IDictionary<string, object>> Import(Stream file, Action callback = null)
        {
            importer.Open(file, ExcelFileType.XLS, true); // Hard-coded for now - FIX THIS!!!

            // Hard-coded for now
            IEnumerable<IDictionary<string, object>> rowsAsKeyValuePairs = importer.GetRowsAsKeyValuePairs(0);

            var db = Database.OpenConnection(""); // TODO

            db.Members.Insert(rowsAsKeyValuePairs);

            if (callback != null)
            {
                callback();
            }

            return rowsAsKeyValuePairs;
        }

        #region Invoke Events

        private void InvokeImportDataCompleted(Task<IEnumerable<IDictionary<string, object>>> task)
        {
            var handler = ImportCompleted;
            if (handler != null)
            {
                handler(this, new ImportDataCompletedEventArgs
                               {
                                   ImportedData = task.Result
                               });
            }
        }

        private void InvokeImportErrorFailed(Task<IEnumerable<IDictionary<string, object>>> task)
        {
            var handler = ImportFailed;
            if (handler != null)
            {
                handler(this, new ImportDataFailedEventArgs(task.Exception.InnerExceptions));
            }
        }

        #endregion

    }
}
