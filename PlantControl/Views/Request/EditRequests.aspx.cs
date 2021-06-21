using System;
using System.Data;
using System.Linq;
using System.Web;

namespace PlantControl.Views
{
    public partial class EditRequests : System.Web.UI.Page
    {
        Encryption userAuthorization = new Encryption();
        SQLQuery sQLQuery = new SQLQuery();

        static string cookieWorkNum;
        public static string CookieWorkNum { get => cookieWorkNum;}

        protected void Page_Load(object sender, EventArgs e)
        {
            Web masterPage = (Web)Page.Master;
            int role = userAuthorization.ReadCookieRole();
            bool authorizated = masterPage.DefineUserRights(role, "EditRequests");
            if (authorizated)
            {
                if (!IsPostBack)
                {
                    statusLbl.Text = "";                   
                    if (role != 1)
                    {
                        priorityNum.Disabled = true;
                    }

                    try
                    {
                        HttpCookie cookieWorkNum = HttpContext.Current.Request.Cookies["ControlsWorkNum"];
                        EditRequests.cookieWorkNum = cookieWorkNum.Value;

                        DataTable dataSource = sQLQuery.RetrieveOrder();
           
                        txtOSNum.Value = dataSource.Rows[0].Field<string>("ID");
                        txtDateOS.Value = dataSource.Rows[0].Field<DateTime>("OS_DATE").ToString("dd-MM-yyyy HH:mm:ss");
                        txtSummary.Value = dataSource.Rows[0].Field<string>("SUMMARY");
                        txtDesc.Value = dataSource.Rows[0].Field<string>("DESCRIPTION");
                        txtReqName.Value = dataSource.Rows[0].Field<string>("REQ_NAME");
                        txtReqDep.Value = dataSource.Rows[0].Field<string>("REQ_DEPARTMENT");
                        txtCell.Value = dataSource.Rows[0].Field<string>("CELL");
                        txtMachine.Value = dataSource.Rows[0].Field<string>("MACHINE_NAME");
                        txtBT.Value = dataSource.Rows[0].Field<string>("BT");
                        txtStatus.Text = dataSource.Rows[0].Field<string>("OS_STATUS");
                        txtWorkDone.Value = dataSource.Rows[0].Field<string>("WORK_DESC");
                        SelectControlsName.Value = dataSource.Rows[0].Field<string>("CONTROLS_NAME");

                        try
                        {
                            priorityNum.Value = dataSource.Rows[0].Field<int>("PRIORITY").ToString();
                        }
                        catch (Exception)
                        {
                        }
                        try
                        {
                            txtPlanningDateOS.Value = dataSource.Rows[0].Field<DateTime>("PLANNED_DATE").ToString("yyyy-MM-dd");
                        }
                        catch (Exception)
                        {
                        }
                        try
                        {
                            txtStartDateOS.Value = dataSource.Rows[0].Field<DateTime>("START_DATE").ToString("yyyy-MM-dd");
                        }
                        catch (Exception)
                        {
                        }
                        try
                        {
                            txtStartTimeOS.Value = dataSource.Rows[0].Field<DateTime>("START_DATE").ToString("HH:mm");
                        }
                        catch (Exception)
                        {
                        }
                        try
                        {
                            txtEndDate.Value = dataSource.Rows[0].Field<DateTime>("END_DATE").ToString("yyyy-MM-dd");
                        }
                        catch (Exception)
                        {
                        }
                        try
                        {
                            txtEndTime.Value = dataSource.Rows[0].Field<DateTime>("END_DATE").ToString("HH:mm");
                        }
                        catch (Exception)
                        {
                        }
                        try
                        {
                            chboxIT.Checked = dataSource.Rows[0].Field<bool>("WORK_IT");
                        }
                        catch (Exception)
                        {
                        }
                        try
                        {
                            chboxManut.Checked = dataSource.Rows[0].Field<bool>("WORK_MAINT");
                        }
                        catch (Exception)
                        {
                        }
                        try
                        {
                            chboxProd.Checked = dataSource.Rows[0].Field<bool>("WORK_PROD");
                        }
                        catch (Exception)
                        {
                        }
                        try
                        {
                            chboxProj.Checked = dataSource.Rows[0].Field<bool>("WORK_PROJ");
                        }
                        catch (Exception)
                        {
                        }
                        try
                        {
                            chboxEng.Checked = dataSource.Rows[0].Field<bool>("WORK_ENG");
                        }
                        catch (Exception)
                        {
                        }                                                                                            
                    }
                    catch (Exception ex)
                    {
                        string test = ex.ToString();
                    }
                }
            }
            else
            {
                Response.Redirect("/Views/Index.aspx");
            }            
        }

        protected void btSave_ServerClick(object sender, EventArgs e)
        {
           bool success = sQLQuery.UpdateOrder(txtOSNum.Value, txtWorkDone.Value, txtStatus.Text, txtPlanningDateOS.Value, txtStartDateOS.Value,
                                                txtStartTimeOS.Value, txtEndDate.Value, txtEndTime.Value, SelectControlsName.Value, priorityNum.Value,
                                                chboxIT.Checked, chboxManut.Checked, chboxProd.Checked, chboxProj.Checked, chboxEng.Checked);
            
           //check if was successfully stored
           if (success)
           {                  
               statusLbl.Text = "Order Saved!";
               sQLQuery.StoreUserHistory(userAuthorization.ReadCookieUser(), "Order " +txtOSNum.Value+ " Saved");
           }
           else
           {
                statusLbl.Text = "Fail to Save!";
            }           
        }
        protected void status_TextChanged(object sender, EventArgs e)
        {
            string statusString = txtStatus.Text;
            statusLbl.Text = "";

            if (statusString.Any(x => !char.IsDigit(x)))
            {
                statusString = "0";
            }
            else
            {
                int statusInt = Convert.ToInt16(statusString);
                if (statusInt > 100)
                {
                    statusString = "0";
                }
            }

            txtStatus.Text = statusString + "%";
        }

        protected void btnBack_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("/Views/Request/ViewRequests.aspx");
        }
    }
}