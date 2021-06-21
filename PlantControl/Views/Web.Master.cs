using System;

namespace PlantControl
{
    public partial class Web : System.Web.UI.MasterPage
    {
        Encryption userAuthorization = new Encryption();
        SQLQuery sQLQuery = new SQLQuery();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int role = userAuthorization.ReadCookieRole();
                DefineUserRights(role, null);
            }

            Response.Cache.SetNoStore();
            Response.Cache.AppendCacheExtension("no-cache");
            Response.Expires = 0;
        }
        protected void btnLogin_ServerClick(object sender, EventArgs e)
        {           
            if (txtUser.Value != "" && txtPass.Value != "")
            {                                              
                int role = sQLQuery.GetUserRole(txtUser.Value, txtPass.Value);
                userAuthorization.AddCookieUser(txtUser.Value);
                userAuthorization.AddCookieRole(role.ToString());                

                DefineUserRights(role, null);
                if (role > 0 & role!= 99 )
                {
                    sQLQuery.StoreUserHistory(txtUser.Value, "LOGIN");
                }               
            }
            else
            {
                Response.Write("<script>alert('Please input your username and password!')</script>");
            }
        }    
        public bool DefineUserRights(int role, string page)
        {
            switch (role)
            {
                case 0:
                    scadaMenu.Visible = false;
                    backupsMenu.Visible = false;
                    facilityMenu.Visible = false;
                    standardMenu.Visible = false;
                    workReqMenu.Visible = true;
                    downloadMenu.Visible = true;
                    btnLogoff.Visible = false;
                    btnChangePass.Visible = false;
                    viewRequestPageMenu.Visible = false;
                    ConfigMenu.Visible = false;
                    LblName.Visible = false;
                    loginBtnVis.Visible = true;

                    switch (page)
                    {
                        case "AssemblyStatus":
                        case "AssyMachines":
                        case "MachiningStatus":
                        case "MachMachines":
                        case "ViewStatus":
                        case "ConfigBackup":
                        case "ConfigMachine":
                        case "ControlsHistory":
                        case "UserHistory":
                        case "AirConditioner":
                        case "Lights":
                        case "LightsHistory":
                        case "EditRequests":
                        case "ViewRequests":
                        case "LimitsChart":
                        case "LimitsCompare":
                        case "LimitsConfig":
                        case "LimitsOverview":
                        case "LimitsPizza":                                                                                                                                     
                        case "ControlsC":
                        case "ControlsAB":
                        case "LimitsReview":
                        case "LimitsReviewOverall":
                            return false;
                        default:
                            return false;                           
                    }

                case 1:                                                                            
                    scadaMenu.Visible = true;
                    backupsMenu.Visible = true;
                    facilityMenu.Visible = true;
                    standardMenu.Visible = true;
                    workReqMenu.Visible = true;
                    downloadMenu.Visible = true;
                    btnLogoff.Visible = true;
                    btnChangePass.Visible = true;
                    viewRequestPageMenu.Visible = true;
                    ConfigMenu.Visible = true;
                    LblName.Visible = true;
                    loginBtnVis.Visible = false;
                    LblName.Text = "- " + userAuthorization.ReadCookieUser();

                    menuReviewOverall.Visible = true;
                    menuReviewLimits.Visible = true;
                    menuLimitsByLine.Visible = true;
                    menuLimitsCompare.Visible = true;
                    menuLimitsConfig.Visible = true;
                    menuLimitsChart.Visible = true;
                    solidLine.Visible = true;

                    switch (page)
                    {
                        case "AssemblyStatus":
                        case "AssyMachines":
                        case "MachiningStatus":
                        case "MachMachines":
                        case "ViewStatus":
                            return true;
                        case "ConfigBackup":
                            return true;
                        case "ConfigMachine":
                            return true;
                        case "ControlsHistory":
                            return true;
                        case "UserHistory":
                            return true;
                        case "Downloads":
                            return true;
                        case "AirConditioner":
                            return true;
                        case "Lights":
                            return true;
                        case "LightsHistory":
                            return true;
                        case "EditRequests":
                            return true;
                        case "ViewRequests":
                            return true;
                        case "LimitsChart":
                            return true;
                        case "LimitsCompare":
                            return true;
                        case "LimitsConfig":
                            return true;
                        case "LimitsOverview":
                            return true;
                        case "LimitsPizza":
                            return true;
                        case "ControlsC":
                            return true;
                        case "ControlsAB":
                            return true;
                        case "LimitsReview":
                            return true;
                        case "LimitsReviewOverall":
                            return true;
                        default:
                            return false;
                    }

                case 2:                                      
                    scadaMenu.Visible = true;
                    backupsMenu.Visible = true;
                    facilityMenu.Visible = false;
                    standardMenu.Visible = true;
                    workReqMenu.Visible = true;
                    downloadMenu.Visible = true;
                    btnLogoff.Visible = true;
                    btnChangePass.Visible = true;
                    viewRequestPageMenu.Visible = true;
                    ConfigMenu.Visible = false;
                    LblName.Visible = true;
                    loginBtnVis.Visible = false;
                    LblName.Text = "- " + userAuthorization.ReadCookieUser();

                    menuReviewOverall.Visible = false;
                    menuReviewLimits.Visible = false;
                    menuLimitsByLine.Visible = true;
                    menuLimitsCompare.Visible = true;
                    menuLimitsConfig.Visible = false;
                    menuLimitsChart.Visible = false;
                    solidLine.Visible = false;

                    switch (page)
                    {
                        case "AssemblyStatus":
                        case "AssyMachines":
                        case "MachiningStatus":
                        case "MachMachines":
                        case "ViewStatus":
                            return true;                      
                        case "ConfigBackup":
                            return false;
                        case "ConfigMachine":
                            return false;
                        case "ControlsHistory":
                            return false;
                        case "UserHistory":
                            return false;
                        case "Downloads":
                            return true;
                        case "AirConditioner":
                            return false;
                        case "Lights":
                            return false;
                        case "LightsHistory":
                            return false;
                        case "EditRequests":
                            return true;
                        case "ViewRequests":
                            return true;
                        case "LimitsChart":
                            return false;
                        case "LimitsCompare":
                            return true;
                        case "LimitsConfig":
                            return false;
                        case "LimitsOverview":
                            return true;
                        case "LimitsPizza":
                            return true;
                        case "ControlsC":
                            return true;
                        case "ControlsAB":
                            return true;
                        case "LimitsReview":
                            return false;
                        case "LimitsReviewOverall":
                            return false;
                        default:
                            return false;
                    }
 
                case 3:                                      
                    scadaMenu.Visible = true;
                    backupsMenu.Visible = false;
                    facilityMenu.Visible = false;
                    standardMenu.Visible = true;
                    workReqMenu.Visible = true;
                    downloadMenu.Visible = true;
                    btnLogoff.Visible = true;
                    btnChangePass.Visible = true;
                    viewRequestPageMenu.Visible = false;
                    ConfigMenu.Visible = false;
                    LblName.Visible = true;
                    loginBtnVis.Visible = false;
                    LblName.Text = "- " + userAuthorization.ReadCookieUser();

                    menuReviewOverall.Visible = true;
                    menuReviewLimits.Visible = true;
                    menuLimitsByLine.Visible = false;
                    menuLimitsCompare.Visible = true;
                    menuLimitsConfig.Visible = false;
                    menuLimitsChart.Visible = false;
                    solidLine.Visible = true;

                    switch (page)
                    {
                        case "AssemblyStatus":
                        case "AssyMachines":
                        case "MachiningStatus":
                        case "MachMachines":
                        case "ViewStatus":
                            return false;                        
                        case "ConfigBackup":
                            return false;
                        case "ConfigMachine":
                            return false;
                        case "ControlsHistory":
                            return false;
                        case "UserHistory":
                            return false;
                        case "Downloads":
                            return true;
                        case "AirConditioner":
                            return false;
                        case "Lights":
                            return false;
                        case "LightsHistory":
                            return false;
                        case "EditRequests":
                            return false;
                        case "ViewRequests":
                            return false;
                        case "LimitsChart":
                            return false;
                        case "LimitsCompare":
                            return true;
                        case "LimitsConfig":
                            return false;
                        case "LimitsOverview":
                            return false;
                        case "LimitsPizza":
                            return false;
                        case "ControlsC":
                            return true;
                        case "ControlsAB":
                            return true;
                        case "LimitsReview":
                            return true;
                        case "LimitsReviewOverall":
                            return true;
                        default:
                            return false;
                    }

                case 4:
                    scadaMenu.Visible = true;
                    backupsMenu.Visible = true;
                    facilityMenu.Visible = true;
                    standardMenu.Visible = true;
                    workReqMenu.Visible = true;
                    downloadMenu.Visible = true;
                    btnLogoff.Visible = true;
                    btnChangePass.Visible = true;
                    viewRequestPageMenu.Visible = true;
                    ConfigMenu.Visible = false;
                    LblName.Visible = true;
                    loginBtnVis.Visible = false;
                    LblName.Text = "- " + userAuthorization.ReadCookieUser();

                    menuReviewOverall.Visible = true;
                    menuReviewLimits.Visible = true;
                    menuLimitsByLine.Visible = true;
                    menuLimitsCompare.Visible = true;
                    menuLimitsConfig.Visible = true;
                    menuLimitsChart.Visible = true;
                    solidLine.Visible = true;

                    switch (page)
                    {
                        case "AssemblyStatus":
                        case "AssyMachines":
                        case "MachiningStatus":
                        case "MachMachines":
                        case "ViewStatus":
                            return true;                       
                        case "ConfigBackup":
                            return false;
                        case "ConfigMachine":
                            return false;
                        case "ControlsHistory":
                            return false;
                        case "UserHistory":
                            return false;
                        case "Downloads":
                            return true;
                        case "AirConditioner":
                            return true;
                        case "Lights":
                            return true;
                        case "LightsHistory":
                            return true;
                        case "EditRequests":
                            return true;
                        case "ViewRequests":
                            return true;
                        case "LimitsChart":
                            return true;
                        case "LimitsCompare":
                            return true;
                        case "LimitsConfig":
                            return true;
                        case "LimitsOverview":
                            return true;
                        case "LimitsPizza":
                            return true;
                        case "ControlsC":
                            return true;
                        case "ControlsAB":
                            return true;
                        case "LimitsReview":
                            return true;
                        case "LimitsReviewOverall":
                            return true;
                        default:
                            return false;
                    }

                case 5:
                    scadaMenu.Visible = false;
                    backupsMenu.Visible = true;
                    facilityMenu.Visible = false;
                    standardMenu.Visible = false;
                    workReqMenu.Visible = true;
                    downloadMenu.Visible = true;
                    btnLogoff.Visible = true;
                    btnChangePass.Visible = true;
                    viewRequestPageMenu.Visible = false;
                    ConfigMenu.Visible = false;
                    LblName.Visible = true;
                    loginBtnVis.Visible = false;
                    LblName.Text = "- " + userAuthorization.ReadCookieUser();

                    menuReviewOverall.Visible = false;
                    menuReviewLimits.Visible = false;
                    menuLimitsByLine.Visible = false;
                    menuLimitsCompare.Visible = false;
                    menuLimitsConfig.Visible = false;
                    menuLimitsChart.Visible = false;
                    solidLine.Visible = false;

                    switch (page)
                    {
                        case "AssemblyStatus":
                        case "AssyMachines":
                        case "MachiningStatus":
                        case "MachMachines":
                        case "ViewStatus":
                            return true;
                        case "UserHistory":
                            return false;
                        case "ControlsHistory":
                            return false;
                        case "Downloads":
                            return true;
                        case "AirConditioner":
                            return false;
                        case "Lights":
                            return false;
                        case "LightsHistory":
                            return false;
                        case "EditRequests":
                            return false;
                        case "ViewRequests":
                            return false;
                        case "LimitsOverview":
                            return false;
                        case "LimitsPizza":
                            return false;
                        case "LimitsCompare":
                            return false;
                        case "LimitsChart":
                            return false;
                        case "LimitsConfig":
                            return false;
                        case "ControlsC":
                            return false;
                        case "ControlsAB":
                            return false;
                        case "LimitsReview":
                            return false;
                        case "LimitsReviewOverall":
                            return false;                      
                        default:
                            return false;
                    }
                
                default:                 
                    scadaMenu.Visible = false;
                    backupsMenu.Visible = false;
                    facilityMenu.Visible = false;
                    standardMenu.Visible = false;
                    workReqMenu.Visible = true;
                    downloadMenu.Visible = true;
                    btnLogoff.Visible = false;
                    btnChangePass.Visible = false;
                    viewRequestPageMenu.Visible = false;
                    ConfigMenu.Visible = false;
                    LblName.Visible = false;
                    loginBtnVis.Visible = true;

                    menuReviewOverall.Visible = false;
                    menuReviewLimits.Visible = false;
                    menuLimitsByLine.Visible = false;
                    menuLimitsCompare.Visible = false;
                    menuLimitsConfig.Visible = false;
                    menuLimitsChart.Visible = false;
                    solidLine.Visible = false;

                    Response.Write("<script>alert('Username or password Wrong!')</script>");
                    userAuthorization.DeleteCookie();

                    switch (page)
                    {
                        case "ConfigBackup":
                        case "ConfigMachine":
                        case "ControlsHistory":
                        case "UserHistory":                        
                        case "AirConditioner":
                        case "Lights":
                        case "LightsHistory":
                        case "EditRequests":
                        case "ViewRequests":
                        case "LimitsChart":
                        case "LimitsCompare":
                        case "LimitsConfig":
                        case "LimitsOverview":
                        case "LimitsPizza":
                        case "ControlsC":
                        case "ControlsAB":
                        case "LimitsReview":
                        case "LimitsReviewOverall":
                        case "AssemblyStatus":
                        case "AssyMachines":
                        case "MachiningStatus":
                        case "MachMachines":
                        case "ViewStatus":
                            return false;
                        case "Downloads":
                            return true;
                        default:
                            return false;
                    }
            }
        }
        protected void btnLogoff_ServerClick(object sender, EventArgs e)
        {
            userAuthorization.AddCookieRole("99");
            userAuthorization.DeleteCookie();
            sQLQuery.StoreUserHistory(userAuthorization.ReadCookieUser(), "LOGOFF");
            Response.Redirect("/Views/Index.aspx");

            scadaMenu.Visible = false;
            backupsMenu.Visible = false;
            facilityMenu.Visible = false;
            standardMenu.Visible = false;
            workReqMenu.Visible = true;
            downloadMenu.Visible = true;
            btnLogoff.Visible = false;
            ConfigMenu.Visible = false;
            btnChangePass.Visible = false;
            LblName.Visible = false;
            loginBtnVis.Visible = true;
        }
        protected void ChangePass_ServerClick(object sender, EventArgs e)
        {
            string user = userAuthorization.ReadCookieUser();
            if ((sQLQuery.GetUserPass(user) == userAuthorization.Encrypt(txtOldPass.Value)) && txtNewPass1.Value == txtNewPass2.Value)
            {
                int res = sQLQuery.ChangeUserPass(user, txtNewPass1.Value);
                if (res == 1)
                {
                    Response.Write("<script>alert('Password Changed')</script>");
                    sQLQuery.StoreUserHistory(user, "PASSWORD CHANGED");
                }
                else
                {
                    Response.Write("<script>alert('Failed to change password')</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('Password is Wrong')</script>");
            }
        }
    }
}