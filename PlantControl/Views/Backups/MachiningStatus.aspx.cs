using System;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;

namespace PlantControl.Views
{
    public partial class MachiningStatus : System.Web.UI.Page
    {
        SQLQuery sQLQuery = new SQLQuery();
        protected void Page_Load(object sender, EventArgs e)
        {
            Encryption userAuthorization = new Encryption();

            SQLQuery sQLQuery = new SQLQuery();

            Web masterPage = (Web)Page.Master;
            int role = userAuthorization.ReadCookieRole();
            bool authorizated = masterPage.DefineUserRights(role, "MachiningStatus");
            if (authorizated)
            {
                if (!IsPostBack)
                {                
                    MachiningResults.DataSource = sQLQuery.GetBackupAssyStatus("MACHINING");
                    MachiningResults.DataBind();
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

            Response.Redirect("/Views/Backups/MachMachines.aspx");
        }
    }
}