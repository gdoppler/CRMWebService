using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Data;


namespace CRMWebService {
    public class DefaultSalesOrderDetails {
        class SoDeLine {
            public DateTime RequestDeliveryBy { get; set; }
            public string[] parts { get; set; }
        }
        List<SoDeLine> linesByDate;
        Dictionary<Guid, string[]> linesById;
        DataTable allRecords;
        string[] headers;
        int indexOfSalesOrderDetailsId = 3;
        int indexOfRequestDeliveredBy = 2;
        int indexOfProductId = 8;
        DefaultProducts products;
            
        string TableName = "WSAuftrag";
        public DefaultSalesOrderDetails(string filename,DefaultProducts products) {
            // read csv-file and create an according data table. 
            // very very basic, just get the types for very known fields
            // consumed like
            this.products = products;
            string[] lines = File.ReadAllLines(filename);
            headers = lines[0].Split(';');
            allRecords = GetEmptyTable();
            linesById = new Dictionary<Guid, string[]>();
            linesByDate = new List<SoDeLine>();
            for (int line = 1; line < lines.Length; line++) {
                string[] parts = lines[line].Split(';');
                Guid sodeId = new Guid(parts[indexOfSalesOrderDetailsId]);
                Guid productId = new Guid(parts[indexOfProductId]);

                string[] partsIncludingProductFields = EnrichWithProductInformation(parts, productId);
                allRecords.Rows.Add(partsIncludingProductFields);
                DateTime RequestDeliveryBy = DateTime.Parse(parts[indexOfRequestDeliveredBy]);
                linesByDate.Add(new SoDeLine() {
                    RequestDeliveryBy = RequestDeliveryBy,
                    parts = partsIncludingProductFields
                });


                if (!linesById.ContainsKey(sodeId)) linesById.Add(sodeId, partsIncludingProductFields);
            }

        }
        private string[] EnrichWithProductInformation(string[] parts, Guid productId) {
            List<string> result=new List<string>(parts);
            result.AddRange(products.GetEnrichmentPartsForProductId(productId));
            return result.ToArray();

        }
        private DataTable GetEmptyTable() {
            DataTable result = new DataTable(TableName);
            foreach (string c in headers) {
                Type t = typeof(string);
                if (c == "SalesOrderDetailId") t = typeof(Guid);
                if (c == "ProductId") t = typeof(Guid);

                if (c == "RequestDeliveryBy") t = typeof(DateTime);
                if (c == "new_Enddatum") t = typeof(DateTime);
                if (c == "ExtendedAmount") t = typeof(decimal);
                if (c == "BaseAmount") t = typeof(decimal);
                result.Columns.Add(c, t);
            }
            // add columns which are derived from Products - we simply assume that nobody changed these suggestions in the sales order details :-)
            result.Columns.Add("ProductNumber", typeof(string));
            result.Columns.Add("new_InterneInfo", typeof(string));
            result.Columns.Add("new_ZusatzinformationfrKunden", typeof(string));
            result.Columns.Add("new_Produkttext", typeof(string));

            return result;
        }
        public DataSet GetSalesOrderDetailsInDateRange(DateTime fromDate, DateTime toDate) {
            DataSet result = new DataSet();
            
            List<SoDeLine> linesInDateRange=new List<SoDeLine>();
            
            foreach (SoDeLine sl in linesByDate) {
                if (sl.RequestDeliveryBy >= fromDate && sl.RequestDeliveryBy <= toDate) {
                    linesInDateRange.Add(sl);
                }
            }
            
            DataTable table = GetEmptyTable();
            foreach (SoDeLine sl in linesInDateRange) {
                table.Rows.Add(sl.parts);
            }
            result.Tables.Add(table);
            
            return result;
        }

        public DataSet GetSalesOrderDetailById(Guid sodeId) {
            string[] parts=linesById[sodeId];
            Guid productId=new Guid(parts[indexOfProductId]);
            DataSet result=products.GetProductByProductId(productId);

            DataTable table = GetEmptyTable();
            table.Rows.Add(parts);
            result.Tables.Add(table);
            return result;
        }
        public DataSet GetAllRecords() {
            return GetSalesOrderDetailsInDateRange(DateTime.MinValue, DateTime.MaxValue);

        }
    }
}