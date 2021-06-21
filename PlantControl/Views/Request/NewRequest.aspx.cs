using System;

namespace PlantControl.Views
{
    public partial class NewRequest : System.Web.UI.Page
    {
        SQLQuery sQLQuery = new SQLQuery();
        protected void Page_Load(object sender, EventArgs e)
        {            
            if (!IsPostBack)
            {
                txtDateOS.Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");              
                txtOSNum.Value = DateTime.Now.ToString("ddMMyyHHmmss");

                SelectionCell.DataSource = sQLQuery.GetCellName();
                SelectionCell.DataTextField = "CELL";
                SelectionCell.DataValueField = "CELL";
                SelectionCell.DataBind();

                SelectionBT.DataSource = sQLQuery.GetBT(SelectionCell.SelectedValue, null);
                SelectionBT.DataTextField = "BT";
                SelectionBT.DataValueField = "BT";
                SelectionBT.DataBind();

                txtMachine.Value = sQLQuery.GetBTDesc(SelectionBT.SelectedValue, null).Rows[0].ItemArray[0].ToString();
            }            
        }

        protected void btSend_ServerClick(object sender, EventArgs e)
        {
            SQLQuery sQLQuery = new SQLQuery();

            string reqName = txtReqName.Value.ToUpper();
            string summary = txtSummary.Value.ToUpper();
            string description = txtDesc.Value.ToUpper();

            if (sQLQuery.CheckOrderExists(txtOSNum.Value) != 1)
            {
                int res = sQLQuery.RecordOSNumber(txtOSNum.Value,
                reqName,               
                SelReqDep.Value,
                txtDateOS.Value,
                SelectionCell.SelectedValue,
                txtMachine.Value,
                SelectionBT.SelectedValue,
                summary,
                description);

                if (res == 1)
                {
                    Response.Write("<script>alert(' OK - Requisition Sent !')</script>");
                }
                else
                {
                    Response.Write("<script>alert(' NOK - Fail to Send Requisition !')</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('!!! REQUISITION ALREADY SENT !!!  OPEN A NEW REQUISITION ')</script>");
            }
        }
        protected void btnNew_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("/Views/Request/NewRequest.aspx");
        }
        protected void SelectionCell_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectionBT.DataSource = sQLQuery.GetBT(SelectionCell.SelectedValue, null);
            SelectionBT.DataTextField = "BT";
            SelectionBT.DataValueField = "BT";
            SelectionBT.DataBind();

            txtMachine.Value = sQLQuery.GetBTDesc(SelectionBT.SelectedValue, null).Rows[0].ItemArray[0].ToString();
        }

        protected void SelectionBT_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtMachine.Value = sQLQuery.GetBTDesc(SelectionBT.SelectedValue, null).Rows[0].ItemArray[0].ToString();
        }
    }
}