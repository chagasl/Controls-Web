using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;

namespace PlantControl.Views
{
    public partial class AssyMachines : System.Web.UI.Page
    {
        DataTable dataTableStatus, dataTableConfig;
        protected void Page_Load(object sender, EventArgs e)
        {
            Encryption userAuthorization = new Encryption();

            SQLQuery sQLQuery = new SQLQuery();

            Web masterPage = (Web)Page.Master;
            int role = userAuthorization.ReadCookieRole();
            bool authorizated = masterPage.DefineUserRights(role, "AssyMachines");
            if (authorizated)
            {
                if (!IsPostBack)
                {                 
                    string[] chkboxNames = new string[10];
                    chkboxNames[0] = "checkBoxPLC";
                    chkboxNames[1] = "checkBoxIHM";
                    chkboxNames[2] = "checkBoxGAGE";
                    chkboxNames[3] = "checkBoxPRESS";
                    chkboxNames[4] = "checkBoxCAMERA";
                    chkboxNames[5] = "checkBoxROBOT";
                    chkboxNames[6] = "checkBoxDRIVE";
                    chkboxNames[7] = "checkBoxNUTRUNNER";
                    chkboxNames[8] = "checkBoxCNC";
                    chkboxNames[9] = "checkBoxHD";

                    HttpCookie cookieMachCell = HttpContext.Current.Request.Cookies["MachBackCell"];

                    dataTableConfig = sQLQuery.GetBackupMachineConfig(cookieMachCell.Value);

                    dataTableStatus = sQLQuery.GetBackupMachineStatus(cookieMachCell.Value);
                    
                    AssyIDResults.DataSource = dataTableStatus;
                    AssyIDResults.DataBind();

                    int Backupchecked,Configchecked = 0;

                    CheckBox chkRow;

                    foreach (GridViewRow row in AssyIDResults.Rows) //Running all lines of grid
                    {
                        for (int i = 4; i < 14; i++)
                        {
                            if (row.RowType == DataControlRowType.DataRow)
                            {
                                Backupchecked = Convert.ToInt16(dataTableStatus.Rows[row.RowIndex].ItemArray[i]);
                                Configchecked = Convert.ToInt16(dataTableConfig.Rows[row.RowIndex].ItemArray[i-3]);

                                chkRow = (row.Cells[i].FindControl(chkboxNames[i - 4]) as CheckBox);

                                if (Backupchecked == 1)
                                {
                                    chkRow.Checked = true;
                                }
                                if (Configchecked == 1 && Backupchecked == 0)
                                {
                                    chkRow.BackColor = Color.Red;
                                }
                                if(Configchecked == 0)
                                {
                                    chkRow.Enabled= false;
                                }
                            }
                        }
                    }

                    SelectionCell.DataSource = sQLQuery.GetCellName("ASSEMBLY");
                    SelectionCell.DataTextField = "CELL";
                    SelectionCell.DataValueField = "CELL";
                    SelectionCell.DataBind();

                    SelectionCell.SelectedValue = cookieMachCell.Value;
                }
            }
            else
            {
                Response.Redirect("/Views/Index.aspx");
            }
        }

        protected void btnSaveStatus_ServerClick(object sender, EventArgs e)
        {
            SQLQuery sQLQuery = new SQLQuery();
            Encryption userAuthorization = new Encryption();

            CheckBox chkRow;

            int Backupchecked = 0;

            string[] chkboxNames = new string[10];
            chkboxNames[0] = "checkBoxPLC";
            chkboxNames[1] = "checkBoxIHM";
            chkboxNames[2] = "checkBoxGAGE";
            chkboxNames[3] = "checkBoxPRESS";
            chkboxNames[4] = "checkBoxCAMERA";
            chkboxNames[5] = "checkBoxROBOT";
            chkboxNames[6] = "checkBoxDRIVE";
            chkboxNames[7] = "checkBoxNUTRUNNER";
            chkboxNames[8] = "checkBoxCNC";
            chkboxNames[9] = "checkBoxHD";

            string[] device = new string[10];
            device[0] = "PLC";
            device[1] = "IHM";
            device[2] = "GAGE";
            device[3] = "PRESS";
            device[4] = "CAMERA";
            device[5] = "ROBOT";
            device[6] = "DRIVE";
            device[7] = "NUTRUNNER";
            device[8] = "CNC";
            device[9] = "HD";

            dataTableStatus = sQLQuery.GetBackupMachineStatus(SelectionCell.SelectedValue);

            foreach (GridViewRow row in AssyIDResults.Rows) //Running all lines of grid
            {
                for (int i = 4; i < 14; i++)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        Backupchecked = Convert.ToInt16(dataTableStatus.Rows[row.RowIndex].ItemArray[i]);

                        chkRow = (row.Cells[i].FindControl(chkboxNames[i - 4]) as CheckBox);

                        if (chkRow.Checked && Backupchecked == 0)
                        {
                            sQLQuery.UpdateBackupDevice(dataTableStatus.Rows[row.RowIndex].ItemArray[0].ToString(), device[i-4], 1);
                            chkRow.BackColor = Color.Green;
                        }
                        if (!chkRow.Checked && Backupchecked == 1)
                        {
                            sQLQuery.UpdateBackupDevice(dataTableStatus.Rows[row.RowIndex].ItemArray[0].ToString(), device[i - 4], 0);
                            chkRow.BackColor = Color.Red;
                        }
                    }
                }
            }

            dataTableConfig = sQLQuery.GetBackupMachineConfig(SelectionCell.SelectedValue);
            dataTableStatus = sQLQuery.GetBackupMachineStatus(SelectionCell.SelectedValue);

            int Configchecked;
            float BackupSum, ConfigSum, Sumfinal;

            for (int i = 0; i < dataTableConfig.Rows.Count; i++)
            {
                ConfigSum = 0;
                BackupSum = 0;
                Sumfinal = 0;

                for (int j = 1; j < 11; j++)
                {
                    Configchecked = Convert.ToInt16(dataTableConfig.Rows[i].ItemArray[j]);
                    Backupchecked = Convert.ToInt16(dataTableStatus.Rows[i].ItemArray[j + 3]);

                    if (Configchecked == 1)
                    {
                        ConfigSum = ConfigSum + 1;
                    }
                    if (Backupchecked == 1)
                    {
                        BackupSum = BackupSum + 1;
                    }
                }

                Sumfinal = (BackupSum / ConfigSum) * 100;

                sQLQuery.UpdateBackupStatus(dataTableConfig.Rows[i].ItemArray[0].ToString(), Sumfinal);
            }


            dataTableStatus = sQLQuery.GetBackupMachineStatus(SelectionCell.SelectedValue);

            foreach (GridViewRow row in AssyIDResults.Rows)
            {
                row.Cells[2].Text = dataTableStatus.Rows[row.RowIndex].ItemArray[2].ToString();
            }

            int SumBTs_Status = 0;
            SumBTs_Status = sQLQuery.GetBackupCellStatus(SelectionCell.SelectedValue);
            sQLQuery.UpdateBackupCellStatus(SelectionCell.SelectedValue, SumBTs_Status);

            sQLQuery.StoreUserHistory(userAuthorization.ReadCookieUser(), "Backups " + SelectionCell.SelectedValue + " Saved");
        }

        protected void btnBack_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("/Views/Backups/AssemblyStatus.aspx");
        }

        protected void SelectionCell_SelectedIndexChanged(object sender, EventArgs e)
        {
            SQLQuery sQLQuery = new SQLQuery();

            string[] chkboxNames = new string[10];
            chkboxNames[0] = "checkBoxPLC";
            chkboxNames[1] = "checkBoxIHM";
            chkboxNames[2] = "checkBoxGAGE";
            chkboxNames[3] = "checkBoxPRESS";
            chkboxNames[4] = "checkBoxCAMERA";
            chkboxNames[5] = "checkBoxROBOT";
            chkboxNames[6] = "checkBoxDRIVE";
            chkboxNames[7] = "checkBoxNUTRUNNER";
            chkboxNames[8] = "checkBoxCNC";
            chkboxNames[9] = "checkBoxHD";

            dataTableConfig = sQLQuery.GetBackupMachineConfig(SelectionCell.SelectedValue);

            dataTableStatus = sQLQuery.GetBackupMachineStatus(SelectionCell.SelectedValue);
            
            AssyIDResults.DataSource = dataTableStatus;
            AssyIDResults.DataBind();

            int Backupchecked, Configchecked = 0;

            CheckBox chkRow;

            foreach (GridViewRow row in AssyIDResults.Rows) //Running all lines of grid
            {
                for (int i = 4; i < 14; i++)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        Backupchecked = Convert.ToInt16(dataTableStatus.Rows[row.RowIndex].ItemArray[i]);
                        Configchecked = Convert.ToInt16(dataTableConfig.Rows[row.RowIndex].ItemArray[i - 3]);

                        chkRow = (row.Cells[i].FindControl(chkboxNames[i - 4]) as CheckBox);

                        if (Backupchecked == 1)
                        {
                            chkRow.Checked = true;
                        }
                        if (Configchecked == 1 && Backupchecked == 0)
                        {
                            chkRow.BackColor = Color.Red;
                        }
                        if (Configchecked == 0)
                        {
                            chkRow.Enabled = false;
                        }
                    }
                }
            }
        }
    }
}