using Ionic.Zip;
using IWshRuntimeLibrary;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.UI.WebControls;
using File = System.IO.File;

namespace PlantControl.Views
{
    public partial class DownloadsBackup : System.Web.UI.Page
    {
        private static string pathFull, pathRoot, pathShortcut;
        private static string pathServer = @"\\xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx\";

        protected void Page_Load(object sender, EventArgs e)
        {
            SQLQuery sQLQuery = new SQLQuery();

            if (!IsPostBack)
            {                           
                btnSaveFiles.Visible = false;
                btnBack.Visible = false;
            }
        }

        protected void SelectionCellType_SelectedIndexChanged(object sender, EventArgs e)
        {
            SQLQuery sQLQuery = new SQLQuery();

            SelectionCell.Items.Clear();

            SelectionBTDesc.Items.Clear();

            txtBT.Text = null;

            btnSaveFiles.Visible = false;

            AssemblyFiles.DataSource = null;
            AssemblyFiles.DataBind();

            SelectionCell.AppendDataBoundItems = true;
            SelectionCell.Items.Add("SELECT");
            SelectionCell.DataSource = sQLQuery.GetCellName(SelectionCellType.SelectedValue);
            SelectionCell.DataTextField = "CELL";
            SelectionCell.DataValueField = "CELL";
            SelectionCell.DataBind();       
        }

        protected void SelectionCell_SelectedIndexChanged(object sender, EventArgs e)
        {
            SQLQuery sQLQuery = new SQLQuery();

            SelectionBTDesc.Items.Clear();

            txtBT.Text = null;

            btnSaveFiles.Visible = false;

            AssemblyFiles.DataSource = null;
            AssemblyFiles.DataBind();

            SelectionBTDesc.AppendDataBoundItems = true;
            SelectionBTDesc.Items.Add("SELECT");
            SelectionBTDesc.DataSource = sQLQuery.GetBTDesc(null, SelectionCell.SelectedValue);
            SelectionBTDesc.DataTextField = "MACHINE_NAME";
            SelectionBTDesc.DataValueField = "MACHINE_NAME";
            SelectionBTDesc.DataBind();                     
        }

        protected void SelectionBTDesc_SelectedIndexChanged(object sender, EventArgs e)
        {
            SQLQuery sQLQuery = new SQLQuery();
            
            if (SelectionBTDesc.SelectedValue != "")
            {
                txtBT.Text = sQLQuery.GetBT(null, SelectionBTDesc.SelectedValue).Rows[0].ItemArray[0].ToString();
            }

            AssemblyFiles.DataSource = null;
            AssemblyFiles.DataBind();

            btnSaveFiles.Visible = false;

            ViewFiles();
        }

        protected void btnSaveFiles_ServerClick(object sender, EventArgs e)
        {
            ZipFiles();
        }

        protected void ViewFiles()
        {
            string pathDir = pathServer + SelectionCellType.SelectedValue + "\\" + SelectionCell.SelectedValue + "\\";
            string pathBT = null;        

            var dir = Directory.GetDirectories(pathDir);

            foreach (var item in dir)
            {
                if (item.Contains(txtBT.Text))
                {
                    pathBT = Path.GetFileName(item);
                    break;
                }
            }

            pathFull = pathDir + pathBT;

            pathRoot = pathFull;

            if (pathBT != null)
            {
                BindDataGridView(pathFull);

                if (AssemblyFiles.Rows.Count != 0)
                {
                    btnSaveFiles.Visible = true;
                }
            }
            else
            {
                AssemblyFiles.DataSource = null;
                AssemblyFiles.DataBind();

                btnSaveFiles.Visible = false;
            }
        }

        private bool IsFolder(string path)
        {
            if (path.Contains(".lnk"))
            {
                return true;
            }
            else if (File.Exists(path))
            {
                return false;
            }
            else
            {
                return true;
            }            
        }

        private bool IsShorctut(string path)
        {
            if (path.Contains(".lnk"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private string FileSize(string path)
        {
            if (!IsFolder(path))
            {
                FileInfo fs = new FileInfo(path);
                return (fs.Length/1024).ToString() + "kB";
            }
            else
            {
                return null;
            }            
        }

        private void ZipFiles()
        {
            string filePath;
            int checkedCount = AssemblyFiles.Rows.Count;

            using (ZipFile zip = new ZipFile())
            {
                zip.UseZip64WhenSaving = Zip64Option.AsNecessary;
                zip.AlternateEncodingUsage = ZipOption.AsNecessary;
                foreach (GridViewRow row in AssemblyFiles.Rows)
                {
                    if ((row.FindControl("chkSelect") as CheckBox).Checked)
                    {
                        filePath = (row.FindControl("lblFilePath") as Label).Text;
                        //fileName = (row.FindControl("lblFileName") as Label).Text;

                        if (File.Exists(filePath))
                        {
                            try
                            {
                                zip.AddFile(filePath, "");
                            }
                            catch (Exception)
                            {
                                throw;
                            }                                                     
                        }
                        else if(Directory.Exists(filePath))
                        {
                            try
                            {
                                zip.AddDirectory(filePath, new DirectoryInfo(filePath).Name);
                            }
                            catch (Exception)
                            {
                                throw;
                            }                           
                        }
                    }
                    else
                    {
                        checkedCount = checkedCount - 1;

                        if (checkedCount == 0)
                        {
                            Response.Write("<script>alert(' SELECT A FILE TO COPY !')</script>");
                        }
                    }
                }

                if (checkedCount != 0)
                {
                    string zipName = String.Format("{0}.zip", "BT" + txtBT.Text + " " + DateTime.Now.ToString("ddMMyyyy"));
                    Response.Clear();
                    Response.BufferOutput = false;
                    Response.ContentType = "application/zip";
                    Response.AddHeader("content-disposition", "attachment; filename=" + zipName);
                    zip.Save(Response.OutputStream);
                    Response.End();
                }
               
            }
        }

        protected void btnLink_Click(object sender, EventArgs e)
        {
            btnBack.Visible = true;

            pathFull = (sender as LinkButton).CommandArgument;

            if (pathFull.Contains(".lnk"))
            {
                pathShortcut = GetShortcutTargetFile(pathFull);
                BindDataGridView(pathShortcut);
            }
            else
            {
                BindDataGridView(pathFull);
            }
         
            if (AssemblyFiles.Rows.Count != 0)
            {
                btnSaveFiles.Visible = true;
            }
            else
            {
                AssemblyFiles.DataSource = null;
                AssemblyFiles.DataBind();

                btnSaveFiles.Visible = false;
            }
        }

        protected void btnBack_ServerClick(object sender, EventArgs e)
        {
            if (pathRoot != pathFull)
            {
                if (pathShortcut != pathFull)
                {
                    int index = pathFull.LastIndexOf(@"\");

                    pathFull = pathFull.Remove(index, pathFull.Length - index);

                    BindDataGridView(pathFull);
                }
                else
                {
                    BindDataGridView(pathRoot);
                    btnBack.Visible = false;
                }
                                             
                if (AssemblyFiles.Rows.Count != 0)
                {
                    btnSaveFiles.Visible = true;
                }
                else
                {
                    AssemblyFiles.DataSource = null;
                    AssemblyFiles.DataBind();

                    btnSaveFiles.Visible = false;
                }

                if (pathRoot == pathFull)
                {
                    btnBack.Visible = false;
                }
            }
        }

        private static string GetShortcutTargetFile(string shortcutFilename )
        {
            WshShell shell = new WshShell();
            IWshShortcut link = (IWshShortcut)shell.CreateShortcut(shortcutFilename);
            string targetpath = link.TargetPath;
            return targetpath;
        }

        private void BindDataGridView(string path)
        {
            string[] folder = null;

            folder = Directory.GetFileSystemEntries(path, "*.*", SearchOption.TopDirectoryOnly);

            var FolderSorted = folder.OrderBy(fn => Path.GetExtension(fn));
            //Array.Sort(folder, (s1, s2) => Path.GetExtension(s1).CompareTo(Path.GetExtension(s2)));

            AssemblyFiles.DataSource = from f in FolderSorted

                                       select new
                                       {
                                           FileName = Path.GetFileName(f.ToString()).ToUpper(),
                                           FilePath = f,
                                           FileFolder = IsFolder(f),
                                           FileSize = FileSize(f),
                                           LinkbtnVisible = IsFolder(f),
                                           chkVisible = !IsShorctut(f)
                                       };

            AssemblyFiles.DataBind();
        }
    }
}