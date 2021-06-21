using Ionic.Zip;
using System;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PlantControl.Views
{
    public partial class ControlsAB : System.Web.UI.Page
    {
        private static string pathServer = @"\\xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx\";
        protected void Page_Load(object sender, EventArgs e)
        {
            Encryption userAuthorization = new Encryption();

            Web masterPage = (Web)Page.Master;
            int role = userAuthorization.ReadCookieRole();
            bool authorizated = masterPage.DefineUserRights(role, "ControlsAB");
            if (!authorizated)
            {
                Response.Redirect("/Views/Index.aspx");                
            }
        }

        private void BindDataGridView(string subType)
        {
            string[] folder = null;
            string pathDir = pathServer + subType;

            folder = Directory.GetFileSystemEntries(pathDir, "*.*", SearchOption.TopDirectoryOnly);

            var FolderSorted = folder.OrderBy(fn => Path.GetExtension(fn));
            //Array.Sort(folder, (s1, s2) => Path.GetExtension(s1).CompareTo(Path.GetExtension(s2)));

            StandardFiles.DataSource = from f in FolderSorted

                                       select new
                                       {
                                           FileName = Path.GetFileName(f.ToString()).ToUpper(),
                                           FilePath = f,
                                           FileFolder = IsFolder(f),
                                           FileSize = FileSize(f),
                                           LinkbtnVisible = IsFolder(f),
                                           chkVisible = !IsShorctut(f)
                                       };

            StandardFiles.DataBind();
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
                return (fs.Length / 1024).ToString() + "kB";
            }
            else
            {
                return null;
            }
        }

        protected void btnESM_ServerClick(object sender, EventArgs e)
        {
            BindDataGridView("ESM");
            id05.Style.Add("display", "block");
            labelType.InnerText = "ESM AB";
        }

        protected void btnCancel_ServerClick(object sender, EventArgs e)
        {
            id05.Style.Add("display", "none");
        }

        protected void btnSave_ServerClick(object sender, EventArgs e)
        {
            ZipFiles();           
        }

        private void ZipFiles()
        {
            string filePath, fileName = "";
            int checkedCount = StandardFiles.Rows.Count;

            using (ZipFile zip = new ZipFile())
            {
                zip.UseZip64WhenSaving = Zip64Option.AsNecessary;
                zip.AlternateEncodingUsage = ZipOption.AsNecessary;
                foreach (GridViewRow row in StandardFiles.Rows)
                {
                    if ((row.FindControl("chkSelect") as CheckBox).Checked)
                    {
                        filePath = (row.FindControl("lblFilePath") as Label).Text;
                        fileName = (row.FindControl("lblFileName") as Label).Text;

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
                        else if (Directory.Exists(filePath))
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
                    string zipName = String.Format("{0}.zip", "AAM Standard for" + " " + labelType.InnerText + " " + DateTime.Now.ToString("ddMMyyyy"));
                    Response.Clear();
                    Response.BufferOutput = false;
                    Response.ContentType = "application/zip";
                    Response.AddHeader("content-disposition", "attachment; filename=" + zipName);
                    zip.Save(Response.OutputStream);
                    Response.End();                   
                }

            }
        }

    }
}