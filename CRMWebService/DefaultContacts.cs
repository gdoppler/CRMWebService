using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Data;

namespace CRMWebService {
    public class DefaultContacts {
        Dictionary<string, string[]> linesByCombinedName;
        Dictionary<Guid, string[]> linesById;
        DataTable allContacts;
        string[] headers;

        string TableName = "WSContact";
        public DefaultContacts(string filename) {
            // read csv-file and create an according data table. 
            
            
            string[] lines = File.ReadAllLines(filename);
            headers = lines[0].Split(';');
            int indexOfFirstName = 1;
            int indexOfLastName = 2;
            int indexOfCompanyName = 3;
            int indexOfContactId = 6;
            allContacts = GetEmptyTable();
            linesById = new Dictionary<Guid, string[]>();
            linesByCombinedName = new Dictionary<string, string[]>();
            for (int line = 1; line < lines.Length; line++) {
                string[] parts = lines[line].Split(';');
                allContacts.Rows.Add(parts);
                string combinedname = (parts[indexOfFirstName] + parts[indexOfLastName] + parts[indexOfCompanyName]);
                Guid contactId = new Guid(parts[indexOfContactId]);
                if (!linesByCombinedName.ContainsKey(combinedname)) linesByCombinedName.Add(combinedname.ToLower(), parts);
                if (!linesById.ContainsKey(contactId)) linesById.Add(contactId, parts);
            }

        }
        private DataTable GetEmptyTable() {
            DataTable result= new DataTable(TableName);
            foreach (string c in headers) {
                Type t = typeof(string);
                if (c == "KontaktId") t = typeof(Guid);
                result.Columns.Add(c, t);
            }
            return result;
        }
        public DataSet GetContactsByName(string name) {
            DataSet result = new DataSet();
            if (!string.IsNullOrEmpty(name)) {
                name = name.ToLower();
            
                DataTable table = GetEmptyTable();
                foreach (string s in linesByCombinedName.Keys) {
                    if (s.ToLower().Contains(name)) table.Rows.Add(linesByCombinedName[s]);
                }
                result.Tables.Add(table);
            }
            else {
                result.Tables.Add(allContacts);
            }
            return result;
        }
        public DataSet GetContactByContactId(Guid contactId) {
            DataTable table = GetEmptyTable();
            table.Rows.Add(linesById[contactId]);
            DataSet result = new DataSet();
            result.Tables.Add(table);
            return result;
        }
    }
}