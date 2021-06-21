using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PlantControl.Views
{
    public partial class LimitsConfig : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Encryption userAuthorization = new Encryption();

            Web masterPage = (Web)Page.Master;
            int role = userAuthorization.ReadCookieRole();
            bool authorizated = masterPage.DefineUserRights(role, "LimitsConfig");
            if (authorizated)
            {
                if (!IsPostBack)
                {                   
                    if (role != 1)
                    {
                       
                    }
                }
            }
            else
            {
                Response.Redirect("/Views/Index.aspx");
            }             
        }      
        protected void serverSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblResults.InnerText = "";
            SQLQuery sQLQuery = new SQLQuery();

            stationSelect.DataSource = sQLQuery.GetStationName(serverSelect.Text);
            stationSelect.DataTextField = "STN_NAME";
            stationSelect.DataValueField = "STN_NAME";
            stationSelect.DataBind();
        }
      
        protected void btnShowCharIDAvailable_ServerClick(object sender, EventArgs e)
        {
            lblResults.InnerText = "";

            SQLQuery sQLQuery = new SQLQuery();            
            charIDResults.DataSource = sQLQuery.GetCharIDAvailable(serverSelect.Text, stationSelect.Text);
            charIDResults.DataBind();
          
            bool exists;
            CheckBox chkRow;

            foreach (GridViewRow row in charIDResults.Rows) //Running all lines of grid
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    exists = sQLQuery.CheckCharIDExist(Convert.ToInt16(row.Cells[0].Text), stationSelect.Text);
                    chkRow = (row.Cells[2].FindControl("checkBoxAdd") as CheckBox);

                    if (exists)
                    {
                        chkRow.Checked = true;
                    }
                }
            }
        }

        protected void btnRegisterCharID_ServerClick(object sender, EventArgs e)
        {
            lblResults.InnerText = "";

            SQLQuery sQLQuery = new SQLQuery();

            foreach (GridViewRow row in charIDResults.Rows) //Running all lines of grid
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[2].FindControl("checkBoxAdd") as CheckBox);                   

                    if (chkRow.Checked)
                    {
                        sQLQuery.InsertCharID(serverSelect.Text, stationSelect.Text, Convert.ToInt16(row.Cells[0].Text));
                    }
                    else
                    {
                        sQLQuery.DeleteCharID(serverSelect.Text, stationSelect.Text, Convert.ToInt16(row.Cells[0].Text));
                    }
                }
            }
            lblResults.InnerText = "Char ID Registered";
        }       
    }
}