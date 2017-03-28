using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.IO;


namespace CRMWebService
{
    public class DefaultActualHolidaySaldo
    {
        DataTable actualHolidaySaldo;
        string[] headers;

        string TableName = "Holidays";
        public DefaultActualHolidaySaldo(string filename)
        {
            // read csv-file and create an according data table. 
            // very very basic, just get the types for very known fields
            // consumed like
            //semi.ProductUid = (Guid)drSOD["ProductId"];
            //baseamount = (decimal)drSOD["ExtendedAmount"];
            // the rest is string. 

            string[] lines = File.ReadAllLines(filename);
            headers = lines[0].Split(';');
            actualHolidaySaldo = GetEmptyTable();
            for (int line = 1; line < lines.Length; line++)
            {
                string[] parts = lines[line].Split(';');
                DataRow dr = actualHolidaySaldo.Rows.Add(parts);
            }

        }
        private DataTable GetEmptyTable()
        {
            DataTable result = new DataTable(TableName);
            foreach (string c in headers)
            {
                Type t = typeof(string);
                if (c == "Urlaubssaldo") t = typeof(decimal);


                result.Columns.Add(c, t);
            }
            return result;
        }
        public DataSet GetAllActualHolidays()
        {
            DataSet result = new DataSet();
            result.Tables.Add(actualHolidaySaldo);
            return result;
        }
    }
}