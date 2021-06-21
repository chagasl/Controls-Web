using System;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;

namespace PlantControl.Views
{
    public partial class AssemblyStatus : System.Web.UI.Page
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
            Encryption userAuthorization = new Encryption();

            SQLQuery sQLQuery = new SQLQuery();

            Web masterPage = (Web)Page.Master;
            int role = userAuthorization.ReadCookieRole();
            bool authorizated = masterPage.DefineUserRights(role, "AssemblyStatus");
            if (authorizated)
            {
                if (!IsPostBack)
                {                                      
                    AssemblyResults.DataSource = sQLQuery.GetBackupAssyStatus("ASSEMBLY");
                    AssemblyResults.DataBind();
                }
            }
            else
            {
                Response.Redirect("/Views/Index.aspx");
            }
        }

        protected void btnLink_Click(object sender, EventArgs e)
        {
            HttpCookie cookie = new HttpCookie("MachBackCell");
            cookie.Value = (sender as LinkButton).CommandArgument;
            cookie.Expires = DateTime.Now.AddDays(360);
            HttpContext.Current.Response.Cookies.Add(cookie);
            
            Response.Redirect("/Views/Backups/AssyMachines.aspx");
        }
    }
}