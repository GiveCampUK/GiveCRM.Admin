using System;
using System.IO;

namespace GiveCRM.Admin.Web.Interfaces
{
    /// <summary>
    /// Exposes methods for importing a list of member data from Excel
    /// </summary>
    public interface IExcelImportService
    {
        /// <summary>
        /// Fires when the import completes
        /// </summary>
        event Action<object, ImportDataCompletedEventArgs> ImportCompleted;

        /// <summary>
        /// Imports the list of member data from the provided stream on a background thread.
        /// </summary>
        /// <param name="file">The stream from which the member data should be imported.</param>
        void ImportAsync(Stream file);
    }

    public class ImportDataCompletedEventArgs
    {
        public object ImportedData
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
    }
}