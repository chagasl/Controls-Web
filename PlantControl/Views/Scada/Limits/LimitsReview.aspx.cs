using System;
using System.Data;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PlantControl.Views
{
    public partial class LimitsReview : System.Web.UI.Page
    {
        Encryption userAuthorization = new Encryption();

        static DataTable dataTable = null;
        protected void Page_Load(object sender, EventArgs e)
        {           
            Web masterPage = (Web)Page.Master;
            int role = userAuthorization.ReadCookieRole();
            bool authorizated = masterPage.DefineUserRights(role, "LimitsReview");
            if (authorizated)
            {
                if (!IsPostBack)
                {
                                   
                }
            }
            else
            {
                Response.Redirect("/Views/Index.aspx");
            }

            lblResults.InnerText = "";
        }

        protected void BtnViewLimits_ServerClick(object sender, EventArgs e)
        {
            if (!serverSelected.Value.Equals(""))
            {              
                SQLQuery sQLQuery = new SQLQuery();
                
                dataTable = sQLQuery.ViewLimitsReview(serverSelected.Value);
                            
                fillSelectFilter(dataTable);

                limitsReviewFilter.Visible = true;
              
                LimitsResults.DataSource = dataTable;
                LimitsResults.DataBind();

                lblResults.InnerText = dataTable.Rows.Count.ToString() + " " + "LIMITS";

                sQLQuery.StoreUserHistory(userAuthorization.ReadCookieUser(), serverSelected.Value + " Limits Viewed");
            }
            else
            {
                Response.Write("<script>alert('SELECT AN ASSEMBLY LINE FIRST')</script>");
            }
        }

        protected void fillSelectFilter(DataTable dataTable)
        {
            SelReviewed.Items.Clear();
            SelPartID.Items.Clear();
            SelParamID.Items.Clear();
            SelParamDesc.Items.Clear();
            SelLimType.Items.Clear();
            SelHiLim.Items.Clear();
            SelLoLim.Items.Clear();
            SelTarg.Items.Clear();
            SelTol.Items.Clear();
            SelText.Items.Clear();
            SelStation.Items.Clear();
            SelPartNum.Items.Clear();

            DataView view = new DataView(dataTable);

            SelReviewed.Items.Add("STATUS");
            SelReviewed.Items.Add(new ListItem("OK", "true"));
            SelReviewed.Items.Add(new ListItem("NOK", "false"));
            SelParamDesc.DataBind();

            DataTable partID = view.ToTable(true, "PART_ID");
            partID.DefaultView.Sort = "PART_ID ASC";
            SelPartID.AppendDataBoundItems = true;
            SelPartID.Items.Add("PART ID");
            SelPartID.DataSource = partID;
            SelPartID.DataTextField = "PART_ID";
            SelPartID.DataValueField = "PART_ID";
            SelPartID.DataBind();

            DataTable paramID = view.ToTable(true, "PARAM_ID");
            paramID.DefaultView.Sort = "PARAM_ID ASC";
            SelParamID.AppendDataBoundItems = true;
            SelParamID.Items.Add("PARAM ID");
            SelParamID.DataSource = paramID;           
            SelParamID.DataTextField = "PARAM_ID";
            SelParamID.DataValueField = "PARAM_ID";
            SelParamID.DataBind();

            DataTable paramDesc = view.ToTable(true, "PARAM_DESC");
            paramDesc.DefaultView.Sort = "PARAM_DESC ASC";
            SelParamDesc.AppendDataBoundItems = true;
            SelParamDesc.Items.Add("PARAM DESC");
            SelParamDesc.DataSource = paramDesc;
            SelParamDesc.DataTextField = "PARAM_DESC";
            SelParamDesc.DataValueField = "PARAM_DESC";
            SelParamDesc.DataBind();

            DataTable LimType = view.ToTable(true, "LIMIT_TYPE");
            LimType.DefaultView.Sort = "LIMIT_TYPE ASC";
            SelLimType.AppendDataBoundItems = true;
            SelLimType.Items.Add("TYPE");
            SelLimType.DataSource = LimType;
            SelLimType.DataTextField = "LIMIT_TYPE";
            SelLimType.DataValueField = "LIMIT_TYPE";
            SelLimType.DataBind();

            DataTable hiLim = view.ToTable(true, "HI_LIM");
            hiLim.DefaultView.Sort = "HI_LIM ASC";
            SelHiLim.AppendDataBoundItems = true;
            SelHiLim.Items.Add("HI LIM");
            SelHiLim.DataSource = hiLim;
            SelHiLim.DataTextField = "HI_LIM";
            SelHiLim.DataValueField = "HI_LIM";
            SelHiLim.DataBind();

            DataTable loLim = view.ToTable(true, "LO_LIM");
            loLim.DefaultView.Sort = "LO_LIM ASC";
            SelLoLim.AppendDataBoundItems = true;
            SelLoLim.Items.Add("LO LIM");
            SelLoLim.DataSource = loLim;
            SelLoLim.DataTextField = "LO_LIM";
            SelLoLim.DataValueField = "LO_LIM";
            SelLoLim.DataBind();

            DataTable targ = view.ToTable(true,"TARG");
            targ.DefaultView.Sort = "TARG ASC";
            SelTarg.AppendDataBoundItems = true;
            SelTarg.Items.Add("TARG");
            SelTarg.DataSource = targ;
            SelTarg.DataTextField = "TARG";
            SelTarg.DataValueField = "TARG";
            SelTarg.DataBind();

            DataTable tol = view.ToTable(true, "TOL");
            tol.DefaultView.Sort = "TOL ASC";
            SelTol.AppendDataBoundItems = true;
            SelTol.Items.Add("TOL");
            SelTol.DataSource = tol;
            SelTol.DataTextField = "TOL";
            SelTol.DataValueField = "TOL";
            SelTol.DataBind();

            DataTable text = view.ToTable(true, "TEXT");
            text.DefaultView.Sort = "TEXT ASC";
            SelText.AppendDataBoundItems = true;
            SelText.Items.Add("TEXT");
            SelText.DataSource = text;
            SelText.DataTextField = "TEXT";
            SelText.DataValueField = "TEXT";
            SelText.DataBind();

            DataTable staName = view.ToTable(true, "STN_NAME");
            staName.DefaultView.Sort = "STN_NAME ASC";
            SelStation.AppendDataBoundItems = true;
            SelStation.Items.Add("STATION");
            SelStation.DataSource = staName;
            SelStation.DataTextField = "STN_NAME";
            SelStation.DataValueField = "STN_NAME";
            SelStation.DataBind();

            DataTable partNum = view.ToTable(true, "PART_NUM");
            partNum.DefaultView.Sort = "PART_NUM ASC";
            SelPartNum.AppendDataBoundItems = true;
            SelPartNum.Items.Add("PART NUM");
            SelPartNum.DataSource = partNum;
            SelPartNum.DataTextField = "PART_NUM";
            SelPartNum.DataValueField = "PART_NUM";
            SelPartNum.DataBind();
        }

        protected void btnSaveStatus_ServerClick(object sender, EventArgs e)
        {
            CheckBox chkRow;

            SQLQuery sQLQuery = new SQLQuery();

            foreach (GridViewRow row in LimitsResults.Rows) //Running all lines of grid
            {              
                if (row.RowType == DataControlRowType.DataRow)
                {                      
                   chkRow = (row.Cells[0].FindControl("checkBoxReview") as CheckBox);

                   if (chkRow.Checked)
                   {
                      LimitsResults.Rows[row.RowIndex].Cells[0].BackColor = Color.Green;
                      sQLQuery.UpdateLimitsReview(serverSelected.Value, Convert.ToInt16(row.Cells[2].Text), Convert.ToInt16(row.Cells[1].Text), true);
                   }
                   else
                   {
                      LimitsResults.Rows[row.RowIndex].Cells[0].BackColor = Color.Red;
                      sQLQuery.UpdateLimitsReview(serverSelected.Value, Convert.ToInt16(row.Cells[2].Text), Convert.ToInt16(row.Cells[1].Text), false);
                   }                     
                }               
            }

            lblResults.InnerText = dataTable.Rows.Count.ToString() + " " + "LIMITS";

            lblResults.InnerText = "LIMITS SAVED";
        }

        protected void MarkGreenRed()
        {
            CheckBox chkRow;

            foreach (GridViewRow row in LimitsResults.Rows) //Running all lines of grid
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    chkRow = (row.Cells[0].FindControl("checkBoxReview") as CheckBox);

                    if (chkRow.Checked)
                    {
                        LimitsResults.Rows[row.RowIndex].Cells[0].BackColor = Color.Green;
                    }
                    else
                    {
                        LimitsResults.Rows[row.RowIndex].Cells[0].BackColor = Color.Red;
                    }
                }
            }
        }

        protected void LimitsResults_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            LimitsResults.PageIndex = e.NewPageIndex;

            lblResults.InnerText = dataTable.Rows.Count.ToString() + " " + "LIMITS";

            LimitsResults.DataSource = dataTable;
            LimitsResults.DataBind();
        }

        protected void LimitsResults_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            MarkGreenRed();
        }

        protected void chkHeader_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkRow, chckheader;

            chckheader = (CheckBox)LimitsResults.HeaderRow.FindControl("chkHeader");

            foreach (GridViewRow row in LimitsResults.Rows) //Running all lines of grid
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    chkRow = (row.Cells[0].FindControl("checkBoxReview") as CheckBox);

                    if (chckheader.Checked)
                    {
                        chkRow.Checked = true;
                    }
                    else
                    {
                        chkRow.Checked = false;
                    }
                }
            }
        }

        protected void SelPartID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelPartID.Text!= "PART ID" & SelPartID.Text != "")
            {
                DataView view = new DataView(dataTable);
                view.RowFilter = "PART_ID =  '" + SelPartID.Text + "'";
                string selValue = SelPartID.Text;

                dataTable = view.ToTable();

                fillSelectFilter(dataTable);
                SelPartID.SelectedValue = selValue;

                LimitsResults.DataSource = dataTable;
                LimitsResults.DataBind();
            }           
        }

        protected void SelParamID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelParamID.Text!= "PARAM ID" & SelParamID.Text != "")
            {            
                DataView view = new DataView(dataTable);
                view.RowFilter = "PARAM_ID = '" + SelParamID.Text + "'";
                string selValue = SelParamID.Text;

                dataTable = view.ToTable();

                fillSelectFilter(dataTable);
                SelParamID.SelectedValue = selValue;

                LimitsResults.DataSource = dataTable;
                LimitsResults.DataBind();
            }
        }

        protected void SelParamDesc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelParamDesc.Text!= "PARAM DESC" & SelParamDesc.Text != "")
            {
                DataView view = new DataView(dataTable);
                view.RowFilter = "PARAM_DESC = '" + SelParamDesc.Text + "'";
                string selValue = SelParamDesc.Text;

                dataTable = view.ToTable();

                fillSelectFilter(dataTable);
                SelParamDesc.SelectedValue = selValue;

                LimitsResults.DataSource = dataTable;
                LimitsResults.DataBind();
            }            
        }

        protected void SelLimType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelLimType.Text!= "LIMIT_TYPE" & SelLimType.Text != "")
            {
                DataView view = new DataView(dataTable);
                view.RowFilter = "LIMIT_TYPE = '" + SelLimType.Text + "'";
                string selValue = SelLimType.Text;

                dataTable = view.ToTable();

                fillSelectFilter(dataTable);
                SelLimType.SelectedValue = selValue;

                LimitsResults.DataSource = dataTable;
                LimitsResults.DataBind();
            }          
        }

        protected void SelHiLim_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelHiLim.Text!= "HI LIM" & SelHiLim.Text != "")
            {
                DataView view = new DataView(dataTable);
                view.RowFilter = "HI_LIM = '" + SelHiLim.Text + "'";
                string selValue = SelHiLim.Text;

                dataTable = view.ToTable();

                fillSelectFilter(dataTable);
                SelHiLim.SelectedValue = selValue;

                LimitsResults.DataSource = dataTable;
                LimitsResults.DataBind();
            }            
        }

        protected void SelLoLim_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelLoLim.Text!= "LO LIM" & SelLoLim.Text != "")
            {
                DataView view = new DataView(dataTable);
                view.RowFilter = "LO_LIM = '" + SelLoLim.Text + "'";
                string selValue = SelLoLim.Text;

                dataTable = view.ToTable();

                fillSelectFilter(dataTable);
                SelLoLim.SelectedValue = selValue;

                LimitsResults.DataSource = dataTable;
                LimitsResults.DataBind();
            }            
        }

        protected void SelTarg_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelTarg.Text!= "TARG" & SelTarg.Text != "")
            {
                DataView view = new DataView(dataTable);
                view.RowFilter = "TARG = '" + SelTarg.Text + "'";
                string selValue = SelTarg.Text;

                dataTable = view.ToTable();

                fillSelectFilter(dataTable);
                SelTarg.SelectedValue = selValue;

                LimitsResults.DataSource = dataTable;
                LimitsResults.DataBind();
            }          
        }

        protected void SelTol_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelTol.Text!= "TOL" & SelTol.Text != "")
            {
                DataView view = new DataView(dataTable);
                view.RowFilter = "TOL = '" + SelTol.Text + "'";
                string selValue = SelTol.Text;

                dataTable = view.ToTable();

                fillSelectFilter(dataTable);
                SelTol.SelectedValue = selValue;

                LimitsResults.DataSource = dataTable;
                LimitsResults.DataBind();
            }         
        }

        protected void SelText_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelText.Text!= "TEXT" & SelText.Text != "")
            {
                DataView view = new DataView(dataTable);
                view.RowFilter = "TEXT = '" + SelText.Text + "'";
                string selValue = SelText.Text;

                dataTable = view.ToTable();

                fillSelectFilter(dataTable);
                SelText.SelectedValue = selValue;

                LimitsResults.DataSource = dataTable;
                LimitsResults.DataBind();
            }           
        }

        protected void SelStation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelStation.Text!= "STN NAME" & SelStation.Text != "")
            {
                DataView view = new DataView(dataTable);
                view.RowFilter = "STN_NAME = '" + SelStation.Text + "'";
                string selValue = SelStation.Text;

                dataTable = view.ToTable();

                fillSelectFilter(dataTable);
                SelStation.SelectedValue = selValue;

                LimitsResults.DataSource = dataTable;
                LimitsResults.DataBind();
            }           
        }

        protected void SelPartNum_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelPartNum.Text!= "PART NUM" & SelPartNum.Text != "")
            {
                DataView view = new DataView(dataTable);
                view.RowFilter = "PART_NUM = '" + SelPartNum.Text + "'";
                string selValue = SelPartNum.Text;

                dataTable = view.ToTable();

                fillSelectFilter(dataTable);
                SelPartNum.SelectedValue = selValue;

                LimitsResults.DataSource = dataTable;
                LimitsResults.DataBind();
            }           
        }

        protected void SelReviewed_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelReviewed.Text!="STATUS" & SelReviewed.Text != "")
            {
                DataView view = new DataView(dataTable);
                view.RowFilter = "REVIEWED = '" + SelReviewed.Text + "'";
                string selValue = SelReviewed.SelectedItem.ToString();

                dataTable = view.ToTable();

                fillSelectFilter(dataTable);
                SelReviewed.Items.Clear();
                SelReviewed.Items.Add(selValue);
                SelReviewed.SelectedValue = selValue;

                LimitsResults.DataSource = dataTable;
                LimitsResults.DataBind();
            }           
        }

        protected void btnRstFilter_ServerClick(object sender, EventArgs e)
        {
            SQLQuery sQLQuery = new SQLQuery();

            dataTable = sQLQuery.ViewLimitsReview(serverSelected.Value);

            fillSelectFilter(dataTable);

            LimitsResults.DataSource = dataTable;
            LimitsResults.DataBind();

            lblResults.InnerText = dataTable.Rows.Count.ToString() + " " + "LIMITS";
        }
    }
}