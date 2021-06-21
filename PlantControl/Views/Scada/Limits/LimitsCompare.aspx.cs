using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PlantControl.Views
{
    public partial class LimitsCompare : System.Web.UI.Page
    {
        Encryption userAuthorization = new Encryption();
        protected void Page_Load(object sender, EventArgs e)
        {           
            Web masterPage = (Web)Page.Master;
            int role = userAuthorization.ReadCookieRole();
            bool authorizated = masterPage.DefineUserRights(role, "LimitsCompare");
            if (authorizated)
            {
                if (!IsPostBack)
                {
                    if (role != 1)
                    {
                        btnUpdate.Disabled = true;
                        btnUpdate.Style.Add("background-color", "lightgray");
                        btnUpdate.Attributes.Add("onmouseover", "javascript:this.style.backgroundColor='lightgray'");

                        btnCompareAll.Disabled = true;
                        btnCompareAll.Style.Add("background-color", "lightgray");
                        btnCompareAll.Attributes.Add("onmouseover", "javascript:this.style.backgroundColor='lightgray'");
                    }
                }
            }
            else
            {
                Response.Redirect("/Views/Index.aspx");
            }
        }

        protected void btnUpdate_ServerClick(object sender, EventArgs e)
        {
            lblResults.InnerText = "";

            if (!serverSelected.Value.Equals(""))
            {
                UpdateLimits(serverSelected.Value, userAuthorization.ReadCookieUser());
                lblResults.InnerText = serverSelected.Value + " Local database was updated";
            }
            else
            {
                Response.Write("<script>alert('SELECT AN ASSEMBLY LINE FIRST')</script>");
            }
        }

        protected void btnCompare_ServerClick(object sender, EventArgs e)
        {
            CompareResultsAll.DataSource = null;
            CompareResultsAll.DataBind();

            LimitsResults.DataSource = null;
            LimitsResults.DataBind();

            lblResults.InnerText = "";

            if (!serverSelected.Value.Equals(""))
            {
                SQLQuery sQLQuery = new SQLQuery();
                CompareResults.DataSource = sQLQuery.SCADACompareTables(serverSelected.Value);
                CompareResults.DataBind();
                lblResults.InnerText = CompareResults.Rows.Count.ToString() + " Differences found on Database - " + serverSelected.Value;                
                sQLQuery.StoreUserHistory(userAuthorization.ReadCookieUser(), lblResults.InnerText);
            }
            else
            {
                Response.Write("<script>alert('SELECT AN ASSEMBLY LINE FIRST')</script>");
            }
        }

        protected void BtnViewLimits_ServerClick(object sender, EventArgs e)
        {
            CompareResultsAll.DataSource = null;
            CompareResultsAll.DataBind();

            CompareResults.DataSource = null;
            CompareResults.DataBind();

            lblResults.InnerText = "";

            if (!serverSelected.Value.Equals(""))
            {
                SQLQuery sQLQuery = new SQLQuery();

                LimitsResults.DataSource = sQLQuery.ViewLimits(serverSelected.Value);
                LimitsResults.DataBind();
                sQLQuery.StoreUserHistory(userAuthorization.ReadCookieUser(), serverSelected.Value + " Limits Viewed");
            }
            else
            {
                Response.Write("<script>alert('SELECT AN ASSEMBLY LINE FIRST')</script>");
            }
        }

        protected void CompareResults_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {
            CompareResults.PageIndex = e.NewPageIndex;
            SQLQuery sQLQuery = new SQLQuery();

            CompareResults.DataSource = sQLQuery.ViewLimits(serverSelected.Value);
            CompareResults.DataBind();
        }

        protected void CompareResults_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            for (int i = 0; i < CompareResults.Rows.Count; i++)
            {
                CompareResults.Rows[i].Cells[6].BackColor = Color.LightBlue;
                CompareResults.Rows[i].Cells[7].BackColor = Color.LightBlue;
                CompareResults.Rows[i].Cells[8].BackColor = Color.LightBlue;
                CompareResults.Rows[i].Cells[9].BackColor = Color.LightBlue;
                CompareResults.Rows[i].Cells[10].BackColor = Color.LightBlue;

                CompareResults.Rows[i].Cells[11].BackColor = Color.LightSteelBlue;
                CompareResults.Rows[i].Cells[12].BackColor = Color.LightSteelBlue;
                CompareResults.Rows[i].Cells[13].BackColor = Color.LightSteelBlue;
                CompareResults.Rows[i].Cells[14].BackColor = Color.LightSteelBlue;
                CompareResults.Rows[i].Cells[15].BackColor = Color.LightSteelBlue;
            }
        }

        protected void LimitsResults_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {
            LimitsResults.PageIndex = e.NewPageIndex;
            SQLQuery sQLQuery = new SQLQuery();

            LimitsResults.DataSource = sQLQuery.ViewLimits(serverSelected.Value);
            LimitsResults.DataBind();
        }

        protected void btnCompareAll_ServerClick(object sender, EventArgs e)
        {
            CompareResults.DataSource = null;
            CompareResults.DataBind();

            LimitsResults.DataSource = null;
            LimitsResults.DataBind();

            lblResults.InnerText = "";

            SQLQuery sQLQuery = new SQLQuery();

            DataTable DtCellName = sQLQuery.GetCellName("ASSEMBLY");
            string[] CellName = new string[DtCellName.Rows.Count];

            for (int i = 0; i < DtCellName.Rows.Count; i++)
            {
                CellName[i] = DtCellName.Rows[i].ItemArray[0].ToString();
            }

            DataTable[] compareResults = new DataTable[10];
            DataTable gridView = new DataTable();
            DataRow dataRow = null;

            int[] lines = new int[10];

            gridView.Columns.Add("ASSY_LINE", System.Type.GetType("System.String"));
            gridView.Columns.Add("DIFFERENCES", System.Type.GetType("System.String"));

            for (int i = 0; i < CellName.Length; i++)
            {
                compareResults[i] = sQLQuery.SCADACompareTables(CellName[i]);
                lines[i] = compareResults[i].Rows.Count;

                if (lines[i] != 0)
                {
                    dataRow = gridView.NewRow();
                    dataRow["ASSY_LINE"] = CellName[i];
                    dataRow["DIFFERENCES"] = lines[i];
                    gridView.Rows.Add(dataRow);
                }
            }

            if (lines.Sum() == 0)
            {
                lblResults.InnerText = " No Differences found on Database ";
            }

            gridView.AcceptChanges();
            CompareResultsAll.DataSource = gridView;
            CompareResultsAll.DataBind();
            sQLQuery.StoreUserHistory(userAuthorization.ReadCookieUser(), "All lines - " + lines.Sum() + " differences found");
        }

        public void AutoCompare()
        {
            SQLQuery sQLQuery = new SQLQuery();
            SendEmail sendEmail = new SendEmail();
            DataTable compareResults, mailCC, mailTO;
            string mailBodyHTML, mailSubject;

            DataTable DtCellName = sQLQuery.GetCellName("ASSEMBLY");

            string[] CellName = new string[DtCellName.Rows.Count];

            for (int i = 0; i < DtCellName.Rows.Count; i++)
            {
                CellName[i] = DtCellName.Rows[i].ItemArray[0].ToString();
            }
                                       
            for (int i = 0; i < CellName.Length; i++)
            {
                compareResults = sQLQuery.SCADACompareTables(CellName[i]);

                if (compareResults.Rows.Count > 0)
                {
                    mailBodyHTML = ConvertDataTableToHTML(compareResults);
                    mailSubject = compareResults.Rows.Count.ToString() + " Differences found on Database - " + CellName[i];
                    mailTO = sQLQuery.GetProcessEngMail(CellName[i]);
                    mailCC = sQLQuery.GetCopyEngMail();

                    sendEmail.SendMail(mailSubject, mailBodyHTML, mailCC, mailTO);

                    UpdateLimits(CellName[i],"SYSTEM");
                }
                sQLQuery.StoreUserHistory("SYSTEM", "Auto Compare found " + compareResults.Rows.Count.ToString() + " Differences on Database - " + CellName[i]);

            }
        }

        protected void UpdateLimits(string server, string user)
        {
            SQLQuery sQLQuery = new SQLQuery();
            sQLQuery.UpdateSCADALimTable(server);
            sQLQuery.StoreUserHistory(user, server + " Local Limits Updated");
        }

        private string ConvertDataTableToHTML(DataTable dataTable)
        {
            string html = "<table>";

            //add header row
            html += "<tr>";
            html += "<table style=\"text-align:center;\" >";
            html += "<tr style =\"background-color:#050c8d; color:white;\">";

            for (int i = 0; i < dataTable.Columns.Count; i++)
                html += "<td>" + dataTable.Columns[i].ColumnName+ "</td>";
            html += "</tr>";

            //add rows
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                html += "<tr>";
                html += "<tr style =\"background-color:#e2dfdf; color:black;\">";
                for (int j = 0; j < dataTable.Columns.Count; j++)
                    html += "<td>" + dataTable.Rows[i][j].ToString() + "</td>";
                html += "</tr>";
            }
            html += "</table>";
            return html;
        }

        protected void btnLink_Click(object sender, EventArgs e)
        {
            CompareResultsAll.DataSource = null;
            CompareResultsAll.DataBind();

            LimitsResults.DataSource = null;
            LimitsResults.DataBind();

            lblResults.InnerText = "";

            SQLQuery sQLQuery = new SQLQuery();

            CompareResults.DataSource = sQLQuery.SCADACompareTables((sender as LinkButton).CommandArgument);
            CompareResults.DataBind();
        }
    }
}