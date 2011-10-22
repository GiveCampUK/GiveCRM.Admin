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

        //CREATE TABLE [dbo].[Member]
        //(
        //    ID int identity(1,1) NOT NULL PRIMARY KEY, 
        //    Reference nvarchar(20) NOT NULL,
        //    Title nvarchar(20) NULL,
        //    FirstName nvarchar(50) NOT NULL,
        //    LastName nvarchar(50) NOT NULL,
        //    Salutation nvarchar(50) NOT NULL,
        //    EmailAddress nvarchar(50) NULL,
        //    AddressLine1 nvarchar(50) NULL,
        //    AddressLine2 nvarchar(50) NULL,
        //    Town nvarchar(50) NULL,
        //    Region nvarchar(50) NULL,
        //    PostalCode nvarchar(50) NULL,
        //    Country nvarchar(50) NULL
        //)


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
            // Hard-coded for now - FIX THIS!!!
            importer.Open(file, ExcelFileType.XLS, hasHeaderRow:true);

            var db = Database.OpenConnection(""); // TODO

            //  FIELDS THAT CANNOT BE NULL!
            //    Reference
            //    FirstName
            //    LastName
            //    Salutation

            // Hard-coded for now
            const int sheetIndex = 0;
            IEnumerable<IDictionary<string, object>> rowsAsKeyValuePairs = importer.GetRowsAsKeyValuePairs(sheetIndex);

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
