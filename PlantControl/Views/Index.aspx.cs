using System;
using System.Web.Configuration;

namespace PlantControl.Views
{
    public partial class Index : System.Web.UI.Page
    {
        static string connectionString;
        public static string ConnectionString { get => connectionString; }

        Encryption userAuthorization = new Encryption();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {               
                connectionString = userAuthorization.Decrypt(WebConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
                footerID.InnerText = "America Axle & Manufacturing " + "-" + " " + DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            }

        }
    }
}