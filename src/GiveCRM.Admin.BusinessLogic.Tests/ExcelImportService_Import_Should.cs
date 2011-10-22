using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using GiveCRM.ImportExport;
using Moq;
using NUnit.Framework;

namespace GiveCRM.Admin.BusinessLogic.Tests
{
    [TestFixture]
    public class ExcelImportService_Import_Should : AsyncTest
    {
        [Test]
        public void ReturnAnEnumerableListOfResults()
        {
            var dataToImport = new List<IDictionary<string, object>>
                           {
                               new Dictionary<string, object>
                                   {
                                       {"FirstName", "Joe"},
                                       {"LastName", "Bloggs"}
                                   }
                           };
            ExcelImportService importer = SetupImportService(dataToImport);
            var inputStream = new Mock<Stream>();
            
            var importedData = importer.Import(inputStream.Object);

            CollectionAssert.AreEqual(dataToImport, importedData);
        }

        [Test]
        public void FiresImportCompletedWhenEverythingsFine()
        {
            var dataToImport = new List<IDictionary<string, object>>
                                   {
                                       new Dictionary<string, object>
                                           {
                                               {"TwitterHandle", "@joebloggs"},
                                               {"FavouriteComputer", "MacBook Air"}
                                           }
                                   };
            bool eventFired = false;
            ExcelImportService importer = SetupImportService(dataToImport, (s,e) => eventFired = true);
            var inputStream = new Mock<Stream>();

            WaitFor(complete => importer.ImportAsync(inputStream.Object, complete));

            Assert.IsTrue(eventFired);
        }

        [Test]
        public void FiresImportFailedWhenSomethingGoesWrong()
        {
            bool eventFired = false;
            ExcelImportService importer = SetupImportService(new DataFormatException(), (s,e) => eventFired = true);
            var inputStream = new Mock<Stream>();

            WaitFor(complete => importer.ImportAsync(inputStream.Object, complete));

            Assert.IsTrue(eventFired);
        }

        private ExcelImportService SetupImportService(IEnumerable<IDictionary<string, object>> dataToImport, Action<object, ImportDataCompletedEventArgs> eventHandler = null)
        {
            var excelImporter = new Mock<IExcelImport>();
            excelImporter.Setup(i => i.GetRowsAsKeyValuePairs(0)).Returns(dataToImport);

            var importService = new ExcelImportService(excelImporter.Object);
            importService.ImportCompleted += eventHandler;

            return importService;
        }

        private ExcelImportService SetupImportService(Exception exception, Action<object, ImportDataFailedEventArgs> eventHandler)
        {
            var excelImporter = new Mock<IExcelImport>();
            excelImporter.Setup(i => i.GetRowsAsKeyValuePairs(0)).Throws(exception);

            var importService = new ExcelImportService(excelImporter.Object);
            importService.ImportFailed += eventHandler;

            return importService;
        }
    }

    public class AsyncTest
    {
        /// <summary>
        /// Wait for an async call to complete before returning control.
        /// </summary>
        /// <param name="action">The action to perform.</param>
        public static void WaitFor(Action<Action> action)
        {
            WaitFor(action, TimeSpan.FromSeconds(15));
        }
    
        /// <summary>
        /// Wait for an async call to complete before returning control.
        /// </summary>
        /// <param name="action">The action to perform.</param>
        /// <param name="waitDuration">The duration to wait before failing.</param>
        public static void WaitFor(Action<Action> action, TimeSpan waitDuration)
        {
            var mre = new ManualResetEvent(false);
            Action callback = () => mre.Set();
            action(callback);
    
            if (!mre.WaitOne(waitDuration))
            {
                Assert.Fail("Timed out waiting for callback");
            }
        }
    }
}
