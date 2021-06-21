using System;

namespace PlantControl.Views
{
    public partial class FindRequest : System.Web.UI.Page
    {
        protected void btSend_ServerClick(object sender, EventArgs e)
        {
            SQLQuery sQLQuery = new SQLQuery();
            Findresults.DataSource = sQLQuery.FindOrder(OSnumber.Value, RequestName.Value, BT.Value);
            Findresults.DataBind();      
        }
    }
}