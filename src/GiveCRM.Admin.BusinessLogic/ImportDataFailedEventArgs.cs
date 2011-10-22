using System;
using System.Collections.Generic;

namespace GiveCRM.Admin.BusinessLogic
{
    public class ImportDataFailedEventArgs : EventArgs
    {
        public ImportDataFailedEventArgs(IEnumerable<Exception> exceptions)
        {
            Exceptions = exceptions;
        }

        public IEnumerable<Exception> Exceptions { get; private set; }
    }
}