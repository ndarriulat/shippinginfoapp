using System;
using System.Collections.Generic;
//using ShippingInfoApp.Models;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShippingInfoApp.Models;

namespace ShippingInfoAppTests.Integration_tests
{
    [TestClass]
    public class DatabaseTests
    {
        private const string CsvFilePath = @"C:\Users\nicol\Documents\Moteefe\products_db.csv";

        [TestMethod]
        public void FileIsValidTest()
        {
            throw new NotImplementedException();
            bool filePathIsNotEmpty = !string.IsNullOrEmpty(CsvFilePath);
            Assert.IsTrue(filePathIsNotEmpty);
            Assert.IsTrue(File.Exists(CsvFilePath));
        }

        [TestMethod]
        public void FileIsNotEmptyTest()
        {
            throw new NotImplementedException();
            using (TextFieldParser parser = new TextFieldParser(CsvFilePath))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                bool hasAtLeastOneLine = !parser.EndOfData;
                Assert.IsTrue(hasAtLeastOneLine);
            }
        }
        
        [TestMethod]
        public void DbToModelMappingTest()
        {
            throw new NotImplementedException();
            //IList<Product> products = new List<Product>();

            //using (TextFieldParser parser = new TextFieldParser(CsvFilePath))
            //{
            //    parser.TextFieldType = FieldType.Delimited;
            //    parser.SetDelimiters(",");
            //    bool hasAtLeastOneLine = !parser.EndOfData;
            //    bool headerWasProcessed = false;
            //    while (!parser.EndOfData)
            //    {
            //        //Process row
            //        string[] fields = parser.ReadFields();
            //        if (!headerWasProcessed)
            //        {
            //            //TODO: Process header

            //            headerWasProcessed = true;
            //        }
            //        else
            //        {
            //            //TODO: Process field
            //            products.Add(new Product()
            //            {
            //                Name = fields[0],
            //                Supplier = fields[1],
            //                //DeliveryTimes = ConvertToDictionary(fields[2]),
            //                ItemsInStock = int.Parse(fields[3])
            //            });
            //        }
            //    }
            //}

            //Assert.IsTrue(products.Count > 0);
        }

        //private Dictionary<string, int> ConvertToDictionary(string jsonObject)
        //{
        //    foreach
        //}
    }
}
