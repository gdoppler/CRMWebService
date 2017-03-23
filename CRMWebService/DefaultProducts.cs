using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.IO;

namespace CRMWebService {
    public class DefaultProducts {
        DataTable products;
        Dictionary<string, string[]> linesByProductName;
        Dictionary<Guid, string[]> linesByProductId;
        string[] headers;
        Dictionary<string, int> headerPositions;

        string ProductTableName = "WSProduct";
        public DefaultProducts(string filename) {
            // read csv-file and create an according data table. 
            // very very basic, just get the types for very known fields
            // consumed like
            //semi.ProductUid = (Guid)drSOD["ProductId"];
            //baseamount = (decimal)drSOD["ExtendedAmount"];
            // the rest is string. 
            
            string[] lines = File.ReadAllLines(filename);
            headers = lines[0].Split(';');
            headerPositions = new Dictionary<string, int>();
            for (int x = 0; x < headers.Length; x++) {
                headerPositions.Add(headers[x], x);
            }

            int indexOfProductName = 0;
            int indexOfProductId = 2;
            products = GetEmptyTable();
            linesByProductId = new Dictionary<Guid,string[]>();
            linesByProductName = new Dictionary<string, string[]>();
            for (int line = 1; line < lines.Length; line++) {
                string[] parts = lines[line].Split(';');
                DataRow dr=products.Rows.Add(parts);
                string productName = parts[indexOfProductName];
                Guid productId = new Guid(parts[indexOfProductId]);
                if (!linesByProductName.ContainsKey(productName)) linesByProductName.Add(productName.ToLower(), parts);
                if (!linesByProductId.ContainsKey(productId)) linesByProductId.Add(productId, parts);
            }

        }
        private DataTable GetEmptyTable() {
            DataTable result= new DataTable(ProductTableName);
            foreach (string c in headers) {
                Type t = typeof(string);
                if (c == "ProductId") t = typeof(Guid);
                if (c == "ExtendedAmount") t = typeof(decimal);
                result.Columns.Add(c, t);
            }
            return result;
        }
        public DataSet GetAllProducts() {
            DataSet result = new DataSet();
            result.Tables.Add(products);
            return result;
        }
        public DataSet GetProductsByName(string name) {
            DataSet result = new DataSet();
            if (!string.IsNullOrEmpty(name)) {
                DataTable table = GetEmptyTable();
                foreach (string s in linesByProductName.Keys) {
                    if (s.ToLower().Contains(name)) table.Rows.Add(linesByProductName[s]);
                }
                result.Tables.Add(table);
            }
            else {
                result.Tables.Add(products);
            }
            return result;
        }
        public DataSet GetProductByProductId(Guid productId) {
            DataTable table = GetEmptyTable();
            table.Rows.Add(linesByProductId[productId]);
            DataSet result = new DataSet();
            result.Tables.Add(table);
            return result;
        }
        public string[] GetEnrichmentPartsForProductId(Guid productId) {
            string[] productParts = linesByProductId[productId];
            string[] result = new string[4];
            result[0] = productParts[headerPositions["ProductNumber"]];
            result[1] = productParts[headerPositions["new_InterneInfo"]];
            result[2] = productParts[headerPositions["new_ZusatzinformationfrKunden"]];
            result[3] = productParts[headerPositions["new_Produkttext"]];
            return result;
        }
    }
}