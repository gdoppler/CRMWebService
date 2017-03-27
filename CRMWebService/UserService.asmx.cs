using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.Services;

namespace CRMWebService
{
    /// <summary>
    /// Zusammenfassungsbeschreibung für UserService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]

    public class UserService : System.Web.Services.WebService, IService1Soap
    {
        public DataSet Feiertage()
        {
            throw new NotImplementedException();
        }

        public DataSet getSollZeiten()
        {
            throw new NotImplementedException();
        }

        public DataSet getSollZeitenFeiertage()
        {
            throw new NotImplementedException();
        }

        public DataSet getSollZeitenFeiertagePerPersIDAndDatum(string[] persid, DateTime von, DateTime bis)
        {
            throw new NotImplementedException();
        }

        public DataSet getSollZeitenPerPersIDAndDatum(string[] persid, DateTime von, DateTime bis)
        {
            throw new NotImplementedException();
        }

        public DataSet getTrainerFromNavision()
        {
            /* this method should be filled with some demo-data
             Here follows the method how this data is consumed = how the returned DataSet should look like: 
             public List<object> InitializeUsersAndTrainersFromNav(DataSet dsBitNav) {
                
            if (!dsBitNav.Tables.Contains("Personal WS"))
                throw new Exception("Datatable Personal WS not found!");
            DataTable dtBitTrainer = dsBitNav.Tables["Personal WS"];
            trainersFromNav=new List<Trainer>();
            usersFromNav=new List<User>();
            
            foreach (DataRow dr in dtBitTrainer.Rows) {
                bool isTrainer = ((byte)dr["Trainer"])>0;
                string PersId = dr["No"].ToString();
                string FirstName = dr["FirstName"].ToString();
                string LastName = dr["LastName"].ToString();
                string Title = dr["Title"].ToString();
                string phone = dr["PhoneNo"].ToString();
                string mobile = dr["Mobile"].ToString();
                string email = dr["Email"].ToString();
                int salutationid = (int)dr["sex"]; // 2 ... Herr, 1 .. Frau, fits nicely to SalutationId
                int status = (int)dr["StatusActiveInactive"]; //0 .. active, 1 .. inactive, 2 .. Austritt (left company) 
                string companyemail = dr["CompanyEmail"].ToString();
                bool AMS = ((byte)dr["AMS"]) > 0;
                bool Business = ((byte)dr["BusinessBereich"]) > 0;
                bool isExtern = ((byte)dr["Extern"]) > 0;
                string winlogin = dr["BitLogin"].ToString();
                bool isAdmin = ((byte)dr["Admin"]) > 0;
                bool isSuperAdmin = ((byte)dr["Superadmin"]) > 0;
                string SupervisorPersId = dr["Supervisior"].ToString();
                DateTime birthday = dr["BirthDate"]!=DBNull.Value? (DateTime)dr["BirthDate"]:new DateTime(2000,1,1);
                if (birthday.Year < 1900) {
                    birthday = new DateTime(2000, 1, 1); // for those with birthday 1.1.1753 :-)
                }

                if (isTrainer && status == 0) {
                    Trainer t = new Trainer();
                    t.PersId = PersId;
                    t.FirstName = FirstName;
                    t.LastName = LastName;
                    t.Title = Title;
                    t.Telefon = phone;
                    t.eMail = companyemail;
                    t.SalutationId = salutationid;
                    t.IsExternal = isExtern;
                    t.WinLogin=winlogin;
                    t.isactive = true;
                    t.SupervisorPersId = SupervisorPersId;
                    t.LastConfirmedWorkingHourSaldoDate = new DateTime(2016, 1, 1);
                    t.Birthday = birthday;
                    trainersFromNav.Add(t);
                }
                if (status == 0 && (AMS || Business)) {
                    User u = new User();
                    u.active = true; // from status==0
                    u.PersId = PersId;
                    u.FirstName = FirstName;
                    u.LastName = LastName;
                    u.Title = Title;
                    u.Telefon = phone;
                    u.eMail = email;
                    u.SalutationId = salutationid;
                    u.AMS = AMS;
                    u.Business = Business;
                    u.WinLogin = winlogin;
                    u.isAdmin = isAdmin;
                    u.isSuperAdmin = isSuperAdmin;
                    u.SupervisorPersId = SupervisorPersId;
                    usersFromNav.Add(u);
                }
                        


                
            }
            then the lists are further processed. 
            List<object> result = new List<object>();
            result.Add(trainersFromNav);
            result.Add(usersFromNav);
            result.Add(dtBitTrainer);
            return result;



              
             */
            throw new NotImplementedException();
        }

        public DataSet getTrainerTable()
        {
            throw new NotImplementedException();
        }

        public DataSet getUrlaubssaldo()
        {
            throw new NotImplementedException();
        }

        public DataSet getZeitdaten()
        {
            throw new NotImplementedException();
        }

        public DataSet getZeitdatenPerMAString(string[] persids)
        {
            throw new NotImplementedException();
        }

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        public DataSet Planzeiten()
        {
            throw new NotImplementedException();
        }

        public DataSet PlanzeitenPerPersId(string[] Persids)
        {
            throw new NotImplementedException();
        }

        public DataSet PlanzeitenPerPersIdUndZeitraum(string[] PersIds, DateTime von, DateTime bis)
        {
            throw new NotImplementedException();
        }

        public DataSet Stempeldaten(string[] persids, DateTime von, DateTime bis)
        {
            throw new NotImplementedException();
        }

        public DataSet StempeldatenPerZeitraum(DateTime von, DateTime bis)
        {
            throw new NotImplementedException();
        }

        public DataSet Verrechnungszeiten()
        {
            throw new NotImplementedException();
        }

        public DataSet VerrechnungszeitenPerPersIds(string[] PersIds)
        {
            throw new NotImplementedException();
        }

        public DataSet VerrechnungszeitenPerPersidsUndZeitraum(string[] persIds, DateTime von, DateTime bis)
        {
            throw new NotImplementedException();
        }

        public DataSet ZeitschemaMitWochentagen()
        {
            throw new NotImplementedException();
        }
    }
}
