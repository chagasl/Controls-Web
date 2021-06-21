using System;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;

namespace PlantControl.Views
{
    public partial class ViewRequests : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Encryption userAuthorization = new Encryption();

            Web masterPage = (Web)Page.Master;
            int role = userAuthorization.ReadCookieRole();
            bool authorizated = masterPage.DefineUserRights(role, "ViewRequests");
            if (authorizated)
            {
                if (!IsPostBack)
                {
                    SQLQuery sQLQuery = new SQLQuery();
                    Viewresults.DataSource = sQLQuery.ViewAllOrders(Convert.ToInt16(txtBox.Value), "ISNULL (tB.PRIORITY, 999)", "ASC");
                    Viewresults.DataBind();
                }
            }
            else
            {
                Response.Redirect("/Views/Index.aspx");
            }
        }

        protected void btnLink_Click(object sender, EventArgs e)
        {
            HttpCookie cookie = new HttpCookie("ControlsWorkNum");
            cookie.Value = (sender as LinkButton).CommandArgument;
            cookie.Expires = DateTime.Now.AddDays(360);
            HttpContext.Current.Response.Cookies.Add(cookie);
            Response.Redirect("EditRequests.aspx");
        }

        protected void Viewresults_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string status;
            string priority;
            string controlsName;

            for (int i = 0; i < Viewresults.Rows.Count; i++)
            {
                status = Viewresults.Rows[i].Cells[0].Text;
                priority = Viewresults.Rows[i].Cells[1].Text;
                controlsName = Viewresults.Rows[i].Cells[12].Text;

                if (status != "100%")
                {
                    if (controlsName == "&nbsp;" || controlsName == "")
                    {
                        Viewresults.Rows[i].BackColor = Color.BurlyWood;
                    }
                    else
                    {
                        Viewresults.Rows[i].BackColor = Color.LightSkyBlue;
                    }
                    if (priority == "0")
                    {
                        Viewresults.Rows[i].BackColor = Color.Red;
                    }
                }
                else
                {
                    Viewresults.Rows[i].BackColor = Color.LightGreen;
                }
            }
        }

        protected void btnCompleted_ServerClick(object sender, EventArgs e)
        {
            if (txtBox.Value == "")
            {
                txtBox.Value = "1";
            }
            SQLQuery sQLQuery = new SQLQuery();
            Viewresults.DataSource = sQLQuery.ViewOrderCompleted(Convert.ToInt16(txtBox.Value));
            Viewresults.DataBind();
        }

        protected void btnOpened_ServerClick(object sender, EventArgs e)
        {
            if (txtBox.Value == "")
            {
                txtBox.Value = "1";
            }
            SQLQuery sQLQuery = new SQLQuery();
            Viewresults.DataSource = sQLQuery.ViewOrderOpened(Convert.ToInt16(txtBox.Value));
            Viewresults.DataBind();
        }

        protected void btnOnGoing_ServerClick(object sender, EventArgs e)
        {
            if (txtBox.Value == "")
            {
                txtBox.Value = "1";
            }
            SQLQuery sQLQuery = new SQLQuery();
            Viewresults.DataSource = sQLQuery.ViewOrderOnGoing(Convert.ToInt16(txtBox.Value));
            Viewresults.DataBind();
        }

        protected void btnAllOrders_ServerClick(object sender, EventArgs e)
        {
            if (txtBox.Value == "")
            {
                txtBox.Value = "1";
            }
            SQLQuery sQLQuery = new SQLQuery();
            Viewresults.DataSource = sQLQuery.ViewAllOrders(Convert.ToInt16(txtBox.Value), "ISNULL (tB.PRIORITY, 999)", "ASC");
            Viewresults.DataBind();
        }

        protected void btnFind_ServerClick(object sender, EventArgs e)
        {
            SQLQuery sQLQuery = new SQLQuery();
            Viewresults.DataSource = sQLQuery.FindOrderFilter(Convert.ToInt16(txtBox.Value), ControlsName.Value, RequesterName.Value, OSNum.Value, Bt.Value);
            Viewresults.DataBind();
        }

        protected void Viewresults_Sorting(object sender, GridViewSortEventArgs e)
        {
            SQLQuery sQLQuery = new SQLQuery();

            string sortExpression = e.SortExpression;
      
            switch (e.SortExpression)
            {
                case "OS_STATUS":
                    if (SortDirection == SortDirection.Ascending)
                    {
                        Viewresults.DataSource = sQLQuery.ViewAllOrders(Convert.ToInt16(txtBox.Value), "tB.OS_STATUS", "ASC");
                        Viewresults.DataBind();
                        SortDirection = SortDirection.Descending;
                    }
                    else
                    {
                        Viewresults.DataSource = sQLQuery.ViewAllOrders(Convert.ToInt16(txtBox.Value), "tB.OS_STATUS", "DESC");
                        Viewresults.DataBind();
                        SortDirection = SortDirection.Ascending;
                    }
                    break;

                case "PRIORITY":
                    if (SortDirection == SortDirection.Ascending)
                    {
                        Viewresults.DataSource = sQLQuery.ViewAllOrders(Convert.ToInt16(txtBox.Value), "tB.PRIORITY", "ASC");
                        Viewresults.DataBind();
                        SortDirection = SortDirection.Descending;
                    }
                    else
                    {
                        Viewresults.DataSource = sQLQuery.ViewAllOrders(Convert.ToInt16(txtBox.Value), "tB.PRIORITY", "DESC");
                        Viewresults.DataBind();
                        SortDirection = SortDirection.Ascending;
                    }
                    break;

                case "REQ_NAME":
                    if (SortDirection == SortDirection.Ascending)
                    {
                        Viewresults.DataSource = sQLQuery.ViewAllOrders(Convert.ToInt16(txtBox.Value), "tD.REQ_NAME", "ASC");
                        Viewresults.DataBind();
                        SortDirection = SortDirection.Descending;
                    }
                    else
                    {
                        Viewresults.DataSource = sQLQuery.ViewAllOrders(Convert.ToInt16(txtBox.Value), "tD.REQ_NAME", "DESC");
                        Viewresults.DataBind();
                        SortDirection = SortDirection.Ascending;
                    }
                    break;

                case "REQ_DEPARTMENT":
                    if (SortDirection == SortDirection.Ascending)
                    {
                        Viewresults.DataSource = sQLQuery.ViewAllOrders(Convert.ToInt16(txtBox.Value), "tD.REQ_DEPARTMENT", "ASC");
                        Viewresults.DataBind();
                        SortDirection = SortDirection.Descending;
                    }
                    else
                    {
                        Viewresults.DataSource = sQLQuery.ViewAllOrders(Convert.ToInt16(txtBox.Value), "tD.REQ_DEPARTMENT", "DESC");
                        Viewresults.DataBind();
                        SortDirection = SortDirection.Ascending;
                    }
                    break;

                case "CELL":
                    if (SortDirection == SortDirection.Ascending)
                    {
                        Viewresults.DataSource = sQLQuery.ViewAllOrders(Convert.ToInt16(txtBox.Value), "tE.CELL", "ASC");
                        Viewresults.DataBind();
                        SortDirection = SortDirection.Descending;
                    }
                    else
                    {
                        Viewresults.DataSource = sQLQuery.ViewAllOrders(Convert.ToInt16(txtBox.Value), "tE.CELL", "DESC");
                        Viewresults.DataBind();
                        SortDirection = SortDirection.Ascending;
                    }
                    break;

                case "MACHINE":
                    if (SortDirection == SortDirection.Ascending)
                    {
                        Viewresults.DataSource = sQLQuery.ViewAllOrders(Convert.ToInt16(txtBox.Value), "tE.MACHINE", "ASC");
                        Viewresults.DataBind();
                        SortDirection = SortDirection.Descending;
                    }
                    else
                    {
                        Viewresults.DataSource = sQLQuery.ViewAllOrders(Convert.ToInt16(txtBox.Value), "tE.MACHINE", "DESC");
                        Viewresults.DataBind();
                        SortDirection = SortDirection.Ascending;
                    }
                    break;

                case "ID":
                    if (SortDirection == SortDirection.Ascending)
                    {
                        Viewresults.DataSource = sQLQuery.ViewAllOrders(Convert.ToInt16(txtBox.Value), "tA.ID", "ASC");
                        Viewresults.DataBind();
                        SortDirection = SortDirection.Descending;
                    }
                    else
                    {
                        Viewresults.DataSource = sQLQuery.ViewAllOrders(Convert.ToInt16(txtBox.Value), "tA.ID", "DESC");
                        Viewresults.DataBind();
                        SortDirection = SortDirection.Ascending;
                    }
                    break;
               
                case "PLANNED_DATE":
                    if (SortDirection == SortDirection.Ascending)
                    {
                        Viewresults.DataSource = sQLQuery.ViewAllOrders(Convert.ToInt16(txtBox.Value), "tB.PLANNED_DATE", "ASC");
                        Viewresults.DataBind();
                        SortDirection = SortDirection.Descending;
                    }
                    else
                    {
                        Viewresults.DataSource = sQLQuery.ViewAllOrders(Convert.ToInt16(txtBox.Value), "tB.PLANNED_DATE", "DESC");
                        Viewresults.DataBind();
                        SortDirection = SortDirection.Ascending;
                    }
                    break;

                case "END_DATE":
                    if (SortDirection == SortDirection.Ascending)
                    {
                        Viewresults.DataSource = sQLQuery.ViewAllOrders(Convert.ToInt16(txtBox.Value), "tB.END_DATE", "ASC");
                        Viewresults.DataBind();
                        SortDirection = SortDirection.Descending;
                    }
                    else
                    {
                        Viewresults.DataSource = sQLQuery.ViewAllOrders(Convert.ToInt16(txtBox.Value), "tB.END_DATE", "DESC");
                        Viewresults.DataBind();
                        SortDirection = SortDirection.Ascending;
                    }
                    break;

                case "CONTROLS_NAME":
                    if (SortDirection == SortDirection.Ascending)
                    {
                        Viewresults.DataSource = sQLQuery.ViewAllOrders(Convert.ToInt16(txtBox.Value), "tC.CONTROLS_NAME", "ASC");
                        Viewresults.DataBind();
                        SortDirection = SortDirection.Descending;
                    }
                    else
                    {
                        Viewresults.DataSource = sQLQuery.ViewAllOrders(Convert.ToInt16(txtBox.Value), "tC.CONTROLS_NAME", "DESC");
                        Viewresults.DataBind();
                        SortDirection = SortDirection.Ascending;
                    }
                    break;                    
            }
        }
        public SortDirection SortDirection
        {
            get
            {
                if (ViewState["SortDirection"] == null)
                {
                    ViewState["SortDirection"] = SortDirection.Ascending;
                }
                return (SortDirection)ViewState["SortDirection"];
            }
            set
            {
                ViewState["SortDirection"] = value;
            }
        }
    }
}