using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.IO;

namespace CRMWebService
{
    public class DefaultStaff
    {
        DataTable staff;
        string[] headers;

        string TableName = "Personal WS";
        public DefaultStaff(string filename)
        {
            // read csv-file and create an according data table. 
            // very very basic, just get the types for very known fields
            // consumed like
            //semi.ProductUid = (Guid)drSOD["ProductId"];
            //baseamount = (decimal)drSOD["ExtendedAmount"];
            // the rest is string. 

            string[] lines = File.ReadAllLines(filename);
            headers = lines[0].Split(';');
            staff = GetEmptyTable();
            for (int line = 1; line < lines.Length; line++)
            {
                string[] parts = lines[line].Split(';');
                DataRow dr = staff.Rows.Add(parts);
            }

        }
        private DataTable GetEmptyTable()
        {
            DataTable result = new DataTable(TableName);
            foreach (string c in headers)
            {
                Type t = typeof(string);
                if (c == "Trainer") t = typeof(byte);
                if (c == "sex") t = typeof(int);
                if (c == "StatusActiveInactive") t = typeof(int);
                if (c == "AMS") t = typeof(byte);
                if (c == "BusinessBereich") t = typeof(byte);
                if (c == "Extern") t = typeof(byte);
                if (c == "Admin") t = typeof(byte);
                if (c == "Superadmin") t = typeof(byte);
                if (c == "BirthDate") t = typeof(DateTime);


                result.Columns.Add(c, t);
            }
            return result;
        }
        public DataSet GetAllStaff()
        {
            DataSet result = new DataSet();
            result.Tables.Add(staff);
            return result;
        }
    }
}