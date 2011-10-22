using System;
using System.Collections.Generic;

namespace GiveCRM.Admin.BusinessLogic
{
    public class ImportDataCompletedEventArgs : EventArgs
    {
        public IEnumerable<IEnumerable<string>> ImportedData { get; set; }
    }
}