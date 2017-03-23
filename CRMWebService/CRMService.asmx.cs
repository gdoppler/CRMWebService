using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Data;
using System.IO;

namespace CRMWebService {
    /// <summary>
    /// Zusammenfassungsbeschreibung für CRMService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]

    public class CRMService : System.Web.Services.WebService, IServiceSoap {
        string pathOfAppData = HttpContext.Current.ApplicationInstance.Server.MapPath("~/App_Data");
        
        DefaultProducts _products;
        DefaultProducts products {
            get {
                if (_products == null) {
                    string filename = Path.Combine(pathOfAppData, "products.csv");
                    _products = new DefaultProducts(filename);
                }
                return _products;
            }
        }
        DefaultContacts _contacts;
        DefaultContacts contacts {
            get {
                if(_contacts==null){
                    string filename = Path.Combine(pathOfAppData, "contacts.csv");
                    _contacts = new DefaultContacts(filename);
                }
                return _contacts;
            }
        }

        DefaultSalesOrderDetails _sodes;
        DefaultSalesOrderDetails sodes {
            get {
                if (_sodes == null) {
                    string filename = Path.Combine(pathOfAppData, "salesorderdetails.csv");
                    _sodes = new DefaultSalesOrderDetails(filename,products);
                }
                return _sodes;
            }
        }

        
        [WebMethod]
        public string HelloWorld() {
            return "Hello World";
        }


        public System.Data.DataSet getProduct() {
            return products.GetAllProducts();
        }

        public System.Data.DataSet getProductByName(string name) {
            return products.GetProductsByName(name);

        }

        public System.Data.DataSet getProductById(Guid Id) {
            return products.GetProductByProductId(Id);
        }


        public System.Data.DataSet getProductIdByAuftragId(Guid soId, string ptyp, int ou) {
            return sodes.GetSalesOrderDetailById(soId);
        }

        public System.Data.DataSet getAuftrag() {
            return sodes.GetAllRecords();
        }

        public System.Data.DataSet getAuftragBySalesorderDetailId(Guid sodetId) {
            return sodes.GetSalesOrderDetailById(sodetId);
        }

        public System.Data.DataSet getAuftragByDate(string _start, string _end, string produkttyp, int orgUnit) {
            return sodes.GetSalesOrderDetailsInDateRange(DateTime.Parse(_start), DateTime.Parse(_end));
        }

        
        public System.Data.DataSet getContactDatas() {
            return contacts.GetContactsByName(null);
        }

        public System.Data.DataSet getContactDatasByName(string name) {
            return contacts.GetContactsByName(name);
        }

        public System.Data.DataSet getContactDatasByFirsAndLastName(string firstname, string lastname) {
            return contacts.GetContactsByName(firstname+lastname);
        }

        public System.Data.DataSet getContactDataByUid(Guid Uid) {
            return contacts.GetContactByContactId(Uid);
        }


        public bool setEventnrToOrderline(Guid _SalesOrderDetailId, string _Eventnr) {
            return true;
        }

        public bool deleteEvenetnrFromOrderline(Guid _salesOrderDetailId) {
            return true;
        }

        
        
    }
}
