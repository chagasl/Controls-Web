using System;
using System.Data;
using System.Data.SqlClient;

namespace PlantControl
{
    public class SQLQuery
    {
        Encryption userAuthorization = new Encryption();

        public int GetUserRole(string user, string pass)
        {
            string EncrpytedPass = userAuthorization.Encrypt(pass);

            string connectionString = PlantControl.Views.Index.ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            try
            {
                sqlConnection.Open();
                string query = "select role from [PLANT_CONTROL].[USERS].[USER] where username = '" + user + "' and password = '" + EncrpytedPass + "'";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                return Convert.ToInt16(dataTable.Rows[0][0]);
            }
            catch (Exception ex)
            {
                string teste = ex.ToString();
                return 99;
            }
        }

        public string GetUserPass(string user)
        {
            string connectionString = PlantControl.Views.Index.ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            try
            {
                sqlConnection.Open();
                string query = "SELECT password FROM [PLANT_CONTROL].[USERS].[USER] where Username = '" + user + "' ";

                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                return dataTable.Rows[0][0].ToString();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public int ChangeUserPass(string user, string newPass)
        {
            int res = 99;
            string connectionString = PlantControl.Views.Index.ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            try
            {
                sqlConnection.Open();
                string query = "update [PLANT_CONTROL].[USERS].[USER] set Password = '" + userAuthorization.Encrypt(newPass) + "' where Username = '" + user + "' ";

                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                int statusQuery = sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();

                //check if was successfully recorded
                if (statusQuery > 0)
                {
                    res = 1;
                }
                return res;
            }
            catch (Exception)
            {
                return 99;
            }
        }

        public void StoreUserHistory(string Login, string Description)
        {
            string connectionString = PlantControl.Views.Index.ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            try
            {
                sqlConnection.Open();
                string query = "INSERT INTO [PLANT_CONTROL].[USERS].[HISTORY]"
                               + "(USERNAME, DESCRIPTION)"
                               + " VALUES("
                               + "   '" + Login.ToUpper() + "',"
                               + "   '" + Description + "')";

                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                int statusQuery = sqlCommand.ExecuteNonQuery();

                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                string test = ex.ToString();
            }
        }

        public DataTable GetUserHistory()
        {
            string connectionString = PlantControl.Views.Index.ConnectionString;
            string query = "SELECT * FROM [PLANT_CONTROL].[USERS].[HISTORY] order by TSTAMP DESC";

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                    DataTable dataTable = new DataTable();
                    sqlDataAdapter.Fill(dataTable);
                    sqlConnection.Close();
                    return dataTable;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public DataTable GetCellName()
        {
            string connectionString = PlantControl.Views.Index.ConnectionString;
            string query = "SELECT * FROM [PLANT_CONTROL].[MACHINE].[CELL] ORDER BY CELL ASC";

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                    DataTable dataTable = new DataTable();
                    sqlDataAdapter.Fill(dataTable);
                    sqlConnection.Close();
                    return dataTable;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public DataTable GetCellName(string cellType)
        {
            string connectionString = PlantControl.Views.Index.ConnectionString;
            string query = "SELECT CELL FROM [PLANT_CONTROL].[MACHINE].[CELL] "
                           + "WHERE DESCRIPTION = '" + cellType + "'  "
                           + "ORDER BY CELL ASC";

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                    DataTable dataTable = new DataTable();
                    sqlDataAdapter.Fill(dataTable);
                    sqlConnection.Close();
                    return dataTable;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public DataTable GetBT(string cell, string machineName)
        {
            string query = null;

            if (cell == null)
            {
                query = "SELECT BT FROM [PLANT_CONTROL].[MACHINE].[NAMES] WHERE MACHINE_NAME = '" + machineName + "' ORDER BY BT ASC";
            }
            if (machineName == null)
            {
                query = "SELECT BT FROM [PLANT_CONTROL].[MACHINE].[NAMES] WHERE CELL = '" + cell + "' ORDER BY BT ASC";
            }

            string connectionString = PlantControl.Views.Index.ConnectionString;

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                    DataTable dataTable = new DataTable();
                    sqlDataAdapter.Fill(dataTable);
                    sqlConnection.Close();
                    return dataTable;
                }
            }
            catch (Exception ex)
            {
                string test = ex.ToString();
                return null;
            }
        }

        public DataTable GetBTDesc(string bt, string cell)
        {
            string query = null;

            if (cell == null)
            {
                query = "SELECT MACHINE_NAME FROM [PLANT_CONTROL].[MACHINE].[NAMES] WHERE BT = '" + bt + "'";
            }
            if (bt == null)
            {
                query = "SELECT MACHINE_NAME FROM [PLANT_CONTROL].[MACHINE].[NAMES] WHERE CELL = '" + cell + "' ORDER BY MACHINE_NAME ASC";
            }

            string connectionString = PlantControl.Views.Index.ConnectionString;

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                    DataTable dataTable = new DataTable();
                    sqlDataAdapter.Fill(dataTable);
                    sqlConnection.Close();
                    return dataTable;
                }
            }
            catch (Exception ex)
            {
                string test = ex.ToString();
                return null;
            }
        }

        public DataTable GetBackupAssyStatus(string cellType)
        {
            string connectionString = PlantControl.Views.Index.ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            try
            {
                sqlConnection.Open();
                string query = "SELECT tA.CELL, tA.MACH_STATUS  FROM BACKUPS.STATUS_CELL tA, MACHINE.CELL tB "
                                + " WHERE tA.CELL = tB.CELL AND tB.DESCRIPTION = '" + cellType + "' ";

                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                return dataTable;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public int GetBackupCellStatus(string cell)
        {
            string connectionString = PlantControl.Views.Index.ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            try
            {
                sqlConnection.Open();
                string query = "DECLARE @ROW_COUNT INT "
                             + "SELECT @ROW_COUNT = COUNT(*) FROM [PLANT_CONTROL].[BACKUPS].[STATUS_MACHINE] tA, "
                             + "[PLANT_CONTROL].[MACHINE].[NAMES] tB "
                             + "WHERE tA.BT = tB.BT AND tB.CELL = '" + cell + "' "
                             + "SELECT SUM(tA.BT_STATUS)/@ROW_COUNT FROM [PLANT_CONTROL].[BACKUPS].[STATUS_MACHINE] tA, "
                             + "[PLANT_CONTROL].[MACHINE].[NAMES] tB "
                             + "        WHERE tA.BT = tB.BT AND tB.CELL = '" + cell + "'";

                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                return Convert.ToInt16(dataTable.Rows[0][0]);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int SumAssyLineBackup(string cellType)
        {
            string connectionString = PlantControl.Views.Index.ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            try
            {
                sqlConnection.Open();
                string query = "DECLARE @ROW_COUNT INT "
                                + "SELECT @ROW_COUNT = COUNT(*) FROM BACKUPS.STATUS_CELL tA, MACHINE.CELL tB "
                                + "WHERE tA.CELL = tB.CELL AND tB.DESCRIPTION = '" + cellType + "' "
                                + "SELECT SUM(tA.MACH_STATUS)/@ROW_COUNT FROM BACKUPS.STATUS_CELL tA, MACHINE.CELL tB "
                                + "WHERE tA.CELL = tB.CELL AND tB.DESCRIPTION = '" + cellType + "'";

                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                return Convert.ToInt16(dataTable.Rows[0][0]);

            }
            catch (Exception ex)
            {
                string test = ex.ToString();
                return 0;
            }
        }

        public void UpdateBackupStatus(string bt, double bt_status)
        {
            string connectionString = PlantControl.Views.Index.ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            try
            {
                sqlConnection.Open();
                string query = "UPDATE [PLANT_CONTROL].[BACKUPS].[STATUS_MACHINE] SET BT_STATUS = " + bt_status + " WHERE BT = '" + bt + "' ";

                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                int statusQuery = sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                string fault = ex.ToString();
            }
        }

        public int UpdateBackupCellStatus(string cell, double cell_status)
        {
            int res = 99;

            string connectionString = PlantControl.Views.Index.ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            try
            {
                sqlConnection.Open();
                string query = "UPDATE [PLANT_CONTROL].[BACKUPS].[STATUS_CELL] SET MACH_STATUS = " + cell_status + " WHERE CELL = '" + cell + "' ";


                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                int statusQuery = sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();

                //check if was successfully recorded
                if (statusQuery > 0)
                {
                    res = 1;
                }
                return res;
            }
            catch (Exception ex)
            {
                string fault = ex.ToString();
                return 99;
            }
        }

        public DataTable GetBackupMachineStatus()
        {
            string connectionString = PlantControl.Views.Index.ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            try
            {
                sqlConnection.Open();
                string query = "SELECT BT, PLC, IHM, GAGE, PRESS, CAMERA, ROBOT, DRIVE, NUTRUNNER, CNC, HD"
                                           + " FROM [PLANT_CONTROL].[BACKUPS].[STATUS_MACHINE] ORDER BY BT ASC ";

                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                return dataTable;
            }
            catch (Exception ex)
            {
                string test = ex.ToString();
                return null;
            }
        }

        public DataTable GetBackupMachineStatus(string cell)
        {
            string connectionString = PlantControl.Views.Index.ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            try
            {
                sqlConnection.Open();
                string query = "SELECT tA.BT, tB.MACHINE_NAME, tA.BT_STATUS, tA.LAST_DATE, PLC, IHM, GAGE, PRESS, CAMERA, ROBOT, DRIVE, NUTRUNNER, CNC, HD"
                                           + " FROM [PLANT_CONTROL].[BACKUPS].[STATUS_MACHINE] tA, [PLANT_CONTROL].[MACHINE].[NAMES] tB "
                                           + "      WHERE tA.BT = tB.BT AND tB.CELL = '" + cell + "'"
                                           + "          ORDER BY tA.BT ASC";

                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                return dataTable;
            }
            catch (Exception ex)
            {
                string test = ex.ToString();
                return null;
            }
        }

        public DataTable GetBackupMachineConfig()
        {
            string connectionString = PlantControl.Views.Index.ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            try
            {
                sqlConnection.Open();
                string query = "SELECT BT, PLC, IHM,GAGE, PRESS, CAMERA, ROBOT, DRIVE, NUTRUNNER, CNC, HD "
                                + "   FROM [PLANT_CONTROL].[BACKUPS].[CONFIG] ORDER BY BT ASC";

                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                return dataTable;
            }
            catch (Exception ex)
            {
                string test = ex.ToString();
                return null;
            }
        }

        public DataTable GetBackupMachineConfig(string cell)
        {
            string connectionString = PlantControl.Views.Index.ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            try
            {
                sqlConnection.Open();
                string query = "SELECT tA.BT, tA.PLC, tA.IHM,tA.GAGE, tA.PRESS, tA.CAMERA, tA.ROBOT, tA.DRIVE, tA.NUTRUNNER, tA.CNC, tA.HD, tC.OP"
                                + "  FROM [PLANT_CONTROL].[BACKUPS].[CONFIG] tA,"
                                + "      [PLANT_CONTROL].[BACKUPS].[STATUS_MACHINE] tB,"
                                + "      [PLANT_CONTROL].[MACHINE].[NAMES] tC "
                                + "  WHERE tA.BT = tB.BT AND "
                                + "        tA.BT = tC.BT AND "
                                + "        tC.CELL = '" + cell + "'"
                                + "  ORDER BY tA.BT ASC ";

                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                return dataTable;
            }
            catch (Exception ex)
            {
                string test = ex.ToString();
                return null;
            }
        }

        public int UpdateBackupDevice(string bt, string device, int value)
        {
            int res = 99;

            string dateNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            string connectionString = PlantControl.Views.Index.ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            try
            {
                sqlConnection.Open();
                string query = "UPDATE [PLANT_CONTROL].[BACKUPS].[STATUS_MACHINE] SET " + device + " = " + value + " WHERE BT = '" + bt + "' "
                             + " UPDATE [PLANT_CONTROL].[BACKUPS].[STATUS_MACHINE] SET LAST_DATE = '" + dateNow + "' WHERE BT = '" + bt + "' ";

                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                int statusQuery = sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();

                //check if was successfully recorded
                if (statusQuery > 0)
                {
                    res = 1;
                }
                return res;
            }
            catch (Exception ex)
            {
                string fault = ex.ToString();
                return 99;
            }
        }

        public int UpdateBackupConfig(string bt, string device, int value)
        {
            int res = 99;

            string connectionString = PlantControl.Views.Index.ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            try
            {
                sqlConnection.Open();
                string query = "UPDATE [PLANT_CONTROL].[BACKUPS].[CONFIG] SET " + device + " = " + value + " WHERE BT = '" + bt + "'";

                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                int statusQuery = sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();

                //check if was successfully recorded
                if (statusQuery > 0)
                {
                    res = 1;
                }
                return res;
            }
            catch (Exception ex)
            {
                string fault = ex.ToString();
                return 99;
            }
        }

        public bool CheckMachineExists(string BT)
        {
            bool res = false;
            string connectionString = PlantControl.Views.Index.ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(connectionString);

            try
            {
                sqlConnection.Open();
                string query = "SELECT BT FROM [PLANT_CONTROL].[MACHINE].[NAMES] WHERE BT = '" + BT + "'";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        res = true;
                    }
                }
                sqlConnection.Close();

                return res;
            }
            catch (Exception)
            {
                return res = false;
            }
        }

        public bool InsertMachine(string BT, string OP, string CELL, string MACHINE_NAME, int KCE)
        {
            string connectionString = PlantControl.Views.Index.ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(connectionString);

            try
            {
                string query = "UPDATE [PLANT_CONTROL].[MACHINE].[NAMES] SET  BT='" + BT + "', OP='" + OP + "', CELL='" + CELL + "', MACHINE_NAME='" + MACHINE_NAME + "', KCE=" + KCE + " "
                                       + " WHERE BT ='" + BT + "'"

                                       + "IF @@ROWCOUNT = 0 "
                                       + "INSERT INTO [PLANT_CONTROL].[MACHINE].[NAMES](BT, OP,CELL, MACHINE_NAME, KCE) "
                                               + " VALUES('" + BT + "', '" + OP + "', '" + CELL + "', '" + MACHINE_NAME + "', '" + KCE + "' ) "

                                       + "INSERT INTO [PLANT_CONTROL].[BACKUPS].[CONFIG](BT) "
                                               + " VALUES('" + BT + "')"

                                       + "INSERT INTO [PLANT_CONTROL].[BACKUPS].[STATUS_MACHINE](BT) "
                                               + " VALUES('" + BT + "')";

                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                int statusQuery = sqlCommand.ExecuteNonQuery();

                sqlConnection.Close();

                //check if was successfully recorded
                if (statusQuery > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                string test = ex.ToString();
                sqlConnection.Close();
                return false;
            }

        }

        public DataTable RetrieveOrder()
        {
            string connectionString = PlantControl.Views.Index.ConnectionString;
            string query = "SELECT * FROM [PLANT_CONTROL].[ORDERS].[REQUEST] tA"
                            + "  JOIN [PLANT_CONTROL].[ORDERS].[STATUS] tB ON tA.ID = tB.ID AND tA.ID = '" + PlantControl.Views.EditRequests.CookieWorkNum + "'"
                            + "  LEFT JOIN [PLANT_CONTROL].[ORDERS].[CONTROLS] tC ON tA.ID = tC.ID"
                            + "  LEFT JOIN [PLANT_CONTROL].[ORDERS].[REQUESTER] tD ON tA.ID = tD.ID"
                            + "  LEFT JOIN [PLANT_CONTROL].[ORDERS].[MACHINE] tE ON tA.ID = tE.ID"
                            + "  LEFT JOIN [PLANT_CONTROL].[MACHINE].[NAMES] tF ON tE.BT = tF.BT";

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                    DataTable dataTable = new DataTable();
                    sqlDataAdapter.Fill(dataTable);
                    sqlConnection.Close();
                    return dataTable;
                }
            }
            catch (Exception ex)
            {
                string test = ex.ToString();
                return null;
            }

        }

        public int RecordOSNumber(string OSNum, string name, string department, string data, string cell, string machine, string bt, string summary, string desc)
        {
            int res = 99;
            string connectionString = PlantControl.Views.Index.ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            try
            {
                sqlConnection.Open();
                string query = "INSERT INTO [PLANT_CONTROL].[ORDERS].[REQUEST]"
                                    + "(ID, OS_DATE, SUMMARY, DESCRIPTION)"
                                    + " VALUES("
                                    + "   '" + OSNum + "',"
                                    + "   '" + data + "',"
                                    + "   '" + summary + "',"
                                    + "   '" + desc + "')"

                                + "INSERT INTO [PLANT_CONTROL].[ORDERS].[REQUESTER]"
                                    + "(ID, REQ_NAME, REQ_DEPARTMENT)"
                                    + " VALUES("
                                    + "   '" + OSNum + "',"
                                    + "   '" + name + "',"
                                    + "   '" + department + "')"

                                + "INSERT INTO [PLANT_CONTROL].[ORDERS].[MACHINE]"
                                    + "(ID, BT)"
                                    + " VALUES("
                                    + "   '" + OSNum + "',"
                                    + "   '" + bt + "')"

                                + "INSERT INTO [PLANT_CONTROL].[ORDERS].[STATUS]"
                                    + "(ID)"
                                    + " VALUES("
                                    + "   '" + OSNum + "')";

                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                int statusQuery = sqlCommand.ExecuteNonQuery();

                sqlConnection.Close();

                //check if was successfully stored
                if (statusQuery > 0)
                {
                    res = 1;
                }
                return res;
            }
            catch (Exception ex)
            {
                string test = ex.ToString();
                return res = 99;
            }
        }

        public int CheckOrderExists(string OSNum)
        {
            int res = 99;
            string connectionString = PlantControl.Views.Index.ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(connectionString);

            try
            {
                sqlConnection.Open();
                string query = "SELECT ID FROM [PLANT_CONTROL].[ORDERS].[REQUEST] WHERE ID = '" + OSNum + "'";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        res = 1;
                    }
                }
                sqlConnection.Close();

                return res;
            }
            catch (Exception)
            {
                return res = 99;
            }
        }

        public DataTable FindOrder(string OSNum, string name, string bt)
        {
            if (bt == "")
            {
                bt = "1";
            }
            if (name == "")
            {
                name = "null";
            }
            string connectionString = PlantControl.Views.Index.ConnectionString;
            string query = "SELECT* FROM [PLANT_CONTROL].[ORDERS].[REQUEST] tA,"
                              + "   [PLANT_CONTROL].[ORDERS].[STATUS] tB,"
                              + "   [PLANT_CONTROL].[ORDERS].[REQUESTER] tC,"
                              + "   [PLANT_CONTROL].[ORDERS].[MACHINE]  tD,"
                              + "   [PLANT_CONTROL].[MACHINE].[NAMES] tE "
                              + "   WHERE tA.ID = tB.ID AND"
                              + "       tA.ID = tD.ID AND "
                              + "       tA.ID = tC.ID AND "
                              + "       tD.BT = tE.BT AND "
                              + "      (tC.REQ_NAME LIKE '%" + name + "%' OR "
                              + "       tA.ID = '" + OSNum + "' OR "
                              + "       tD.BT = '" + bt + "') ORDER BY tA.TSTAMP DESC ";

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                    DataTable dataTable = new DataTable();
                    sqlDataAdapter.Fill(dataTable);
                    sqlConnection.Close();
                    return dataTable;
                }
            }
            catch (Exception ex)
            {
                string test = ex.ToString();
                return null;
            }
        }

        public DataTable ViewAllOrders(int quantity, string OrderBy, string direction)
        {
            string connectionString = PlantControl.Views.Index.ConnectionString;
            string query = "SELECT TOP " + quantity + " * FROM [PLANT_CONTROL].[ORDERS].[REQUEST] tA"
                              + " LEFT JOIN [PLANT_CONTROL].[ORDERS].[STATUS] tB  ON tA.ID = tB.ID"
                              + " LEFT JOIN [PLANT_CONTROL].[ORDERS].[CONTROLS] tC ON tA.ID = tC.ID"
                              + " LEFT JOIN[PLANT_CONTROL].[ORDERS].[REQUESTER] tD ON tA.ID = tD.ID"
                              + " LEFT JOIN[PLANT_CONTROL].[ORDERS].[MACHINE] tE ON tA.ID = tE.ID"
                              + " LEFT JOIN[PLANT_CONTROL].[MACHINE].[NAMES] tF ON tE.BT = tF.BT"
                              + " ORDER BY " + OrderBy + " " + direction + ", tA.OS_DATE DESC ";

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                    DataTable dataTable = new DataTable();
                    sqlDataAdapter.Fill(dataTable);
                    sqlConnection.Close();
                    return dataTable;
                }
            }
            catch (Exception ex)
            {
                string test = ex.ToString();
                return null;
            }
        }

        public DataTable ViewOrderCompleted(int quantity)
        {
            string connectionString = PlantControl.Views.Index.ConnectionString;
            string query = "SELECT TOP " + quantity + " * FROM [PLANT_CONTROL].[ORDERS].[REQUEST] tA"
                              + "  JOIN [PLANT_CONTROL].[ORDERS].[STATUS] tB ON tA.ID = tB.ID"
                              + "  JOIN [PLANT_CONTROL].[ORDERS].[CONTROLS] tC ON tA.ID = tC.ID"
                              + "  JOIN [PLANT_CONTROL].[ORDERS].[REQUESTER] tD ON tA.ID = tD.ID"
                              + "  JOIN [PLANT_CONTROL].[ORDERS].[MACHINE] tE ON tA.ID = tE.ID"
                              + "  JOIN[PLANT_CONTROL].[MACHINE].[NAMES] tF ON tE.BT = tF.BT"
                              + "  AND tB.OS_STATUS = '100%' ORDER BY tB.PRIORITY ASC ";

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                    DataTable dataTable = new DataTable();
                    sqlDataAdapter.Fill(dataTable);
                    sqlConnection.Close();
                    return dataTable;
                }
            }
            catch (Exception ex)
            {
                string test = ex.ToString();
                return null;
            }
        }

        public DataTable ViewOrderOpened(int quantity)
        {
            string connectionString = PlantControl.Views.Index.ConnectionString;
            string query = "SELECT TOP " + quantity + " * FROM [PLANT_CONTROL].[ORDERS].[REQUEST] tA"
                              + "  LEFT JOIN [PLANT_CONTROL].[ORDERS].[STATUS] tB ON tA.ID = tB.ID"
                              + "  LEFT JOIN [PLANT_CONTROL].[ORDERS].[CONTROLS] tC ON tA.ID = tC.ID"
                              + "  LEFT JOIN [PLANT_CONTROL].[ORDERS].[REQUESTER] tD ON tA.ID = tD.ID"
                              + "  LEFT JOIN [PLANT_CONTROL].[ORDERS].[MACHINE] tE ON tA.ID = tE.ID"
                              + "  LEFT JOIN[PLANT_CONTROL].[MACHINE].[NAMES] tF ON tE.BT = tF.BT"
                              + "  WHERE (tC.CONTROLS_NAME = ' ' OR tC.CONTROLS_NAME IS NULL) "
                              + "  ORDER BY ISNULL (tB.PRIORITY, 999) ASC ";

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                    DataTable dataTable = new DataTable();
                    sqlDataAdapter.Fill(dataTable);
                    sqlConnection.Close();
                    return dataTable;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public DataTable ViewOrderOnGoing(int quantity)
        {
            string connectionString = PlantControl.Views.Index.ConnectionString;
            string query = "SELECT TOP " + quantity + " * FROM [PLANT_CONTROL].[ORDERS].[REQUEST] tA"
                              + "  JOIN [PLANT_CONTROL].[ORDERS].[STATUS] tB ON tA.ID = tB.ID"
                              + "  JOIN [PLANT_CONTROL].[ORDERS].[CONTROLS] tC ON tA.ID = tC.ID"
                              + "  JOIN [PLANT_CONTROL].[ORDERS].[REQUESTER] tD ON tA.ID = tD.ID"
                              + "  JOIN [PLANT_CONTROL].[ORDERS].[MACHINE] tE ON tA.ID = tE.ID"
                              + "  JOIN[PLANT_CONTROL].[MACHINE].[NAMES] tF ON tE.BT = tF.BT"
                              + "  AND (tC.CONTROLS_NAME IS NOT NULL AND tB.OS_STATUS NOT LIKE '100%')  "
                              + "  ORDER BY ISNULL (tB.PRIORITY, 999) ASC";

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                    DataTable dataTable = new DataTable();
                    sqlDataAdapter.Fill(dataTable);
                    sqlConnection.Close();
                    return dataTable;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public DataTable FindOrderFilter(int quantity, string ControlsName, string RequesterName, string OSNum, string Bt)
        {
            string connectionString = PlantControl.Views.Index.ConnectionString;
            string query = null;
            if (ControlsName != "")
            {
                query = "SELECT TOP " + quantity + " * FROM [PLANT_CONTROL].[ORDERS].[REQUEST] tA"
                              + "  JOIN [PLANT_CONTROL].[ORDERS].[STATUS] tB ON tA.ID = tB.ID"
                              + "  JOIN [PLANT_CONTROL].[ORDERS].[CONTROLS] tC ON tA.ID = tC.ID"
                              + "  JOIN [PLANT_CONTROL].[ORDERS].[REQUESTER] tD ON tA.ID = tD.ID"
                              + "  JOIN [PLANT_CONTROL].[ORDERS].[MACHINE] tE ON tA.ID = tE.ID"
                              + "  JOIN[PLANT_CONTROL].[MACHINE].[NAMES] tF ON tE.BT = tF.BT"
                              + "  AND (tC.CONTROLS_NAME LIKE '%" + ControlsName + "%') ORDER BY tB.PRIORITY ASC ";
            }
            else if (RequesterName != "")
            {
                query = "SELECT TOP " + quantity + " * FROM [PLANT_CONTROL].[ORDERS].[REQUEST] tA"
                              + "  JOIN [PLANT_CONTROL].[ORDERS].[STATUS] tB ON tA.ID = tB.ID"
                              + "  JOIN [PLANT_CONTROL].[ORDERS].[CONTROLS] tC ON tA.ID = tC.ID"
                              + "  JOIN [PLANT_CONTROL].[ORDERS].[REQUESTER] tD ON tA.ID = tD.ID"
                              + "  JOIN [PLANT_CONTROL].[ORDERS].[MACHINE] tE ON tA.ID = tE.ID"
                              + "  JOIN[PLANT_CONTROL].[MACHINE].[NAMES] tF ON tE.BT = tF.BT"
                              + "  AND (tD.REQ_NAME LIKE '%" + RequesterName + "%') ORDER BY tB.PRIORITY ASC ";
            }
            else if (OSNum != "")
            {
                query = "SELECT TOP " + quantity + " * FROM [PLANT_CONTROL].[ORDERS].[REQUEST] tA"
                             + "  JOIN [PLANT_CONTROL].[ORDERS].[STATUS] tB ON tA.ID = tB.ID"
                             + "  JOIN [PLANT_CONTROL].[ORDERS].[CONTROLS] tC ON tA.ID = tC.ID"
                             + "  JOIN [PLANT_CONTROL].[ORDERS].[REQUESTER] tD ON tA.ID = tD.ID"
                             + "  JOIN [PLANT_CONTROL].[ORDERS].[MACHINE] tE ON tA.ID = tE.ID"
                             + "  JOIN[PLANT_CONTROL].[MACHINE].[NAMES] tF ON tE.BT = tF.BT"
                             + "  AND (tA.ID = '" + OSNum + "') ORDER BY tB.PRIORITY ASC ";
            }
            else if (Bt != "")
            {
                query = "SELECT TOP " + quantity + " * FROM [PLANT_CONTROL].[ORDERS].[REQUEST] tA"
                            + "  JOIN [PLANT_CONTROL].[ORDERS].[STATUS] tB ON tA.ID = tB.ID"
                            + "  JOIN [PLANT_CONTROL].[ORDERS].[CONTROLS] tC ON tA.ID = tC.ID"
                            + "  JOIN [PLANT_CONTROL].[ORDERS].[REQUESTER] tD ON tA.ID = tD.ID"
                            + "  JOIN [PLANT_CONTROL].[ORDERS].[MACHINE] tE ON tA.ID = tE.ID"
                            + "  JOIN[PLANT_CONTROL].[MACHINE].[NAMES] tF ON tE.BT = tF.BT"
                            + "  AND (tE.BT = '" + Bt + "') ORDER BY tB.PRIORITY ASC ";
            }
            else
            {
                query = "SELECT TOP " + quantity + " * FROM [PLANT_CONTROL].[ORDERS].[REQUEST] tA"
                            + "  JOIN [PLANT_CONTROL].[ORDERS].[STATUS] tB ON tA.ID = tB.ID"
                            + "  JOIN [PLANT_CONTROL].[ORDERS].[CONTROLS] tC ON tA.ID = tC.ID"
                            + "  JOIN [PLANT_CONTROL].[ORDERS].[REQUESTER] tD ON tA.ID = tD.ID"
                            + "  JOIN [PLANT_CONTROL].[ORDERS].[MACHINE] tE ON tA.ID = tE.ID"
                            + "  JOIN[PLANT_CONTROL].[MACHINE].[NAMES] tF ON tE.BT = tF.BT"
                            + "  AND tA.ID = tB.ID ORDER BY tB.PRIORITY ASC ";
            }

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                    DataTable dataTable = new DataTable();
                    sqlDataAdapter.Fill(dataTable);
                    sqlConnection.Close();
                    return dataTable;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public DataTable FindOrderFilter(int quantity, string area)
        {
            area = area.ToUpper();
            string connectionString = PlantControl.Views.Index.ConnectionString;
            string query = null;

            query = "SELECT TOP " + quantity + " * FROM [PLANT_CONTROL].[ORDERS].[REQUEST] tA"
                        + "  JOIN [PLANT_CONTROL].[ORDERS].[STATUS] tB ON tA.ID = tB.ID"
                        + "  JOIN [PLANT_CONTROL].[ORDERS].[CONTROLS] tC ON tA.ID = tC.ID"
                        + "  JOIN [PLANT_CONTROL].[ORDERS].[REQUESTER] tD ON tA.ID = tD.ID"
                        + "  JOIN [PLANT_CONTROL].[ORDERS].[MACHINE] tE ON tA.ID = tE.ID"
                        + "  JOIN[PLANT_CONTROL].[MACHINE].[NAMES] tF ON tE.BT = tF.BT"
                        + "  AND tA.ID = tB.ID"
                        + "   AND tB." + area + " = 'true' ORDER BY tB.PRIORITY ASC ";
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                    DataTable dataTable = new DataTable();
                    sqlDataAdapter.Fill(dataTable);
                    sqlConnection.Close();
                    return dataTable;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool UpdateOrder(string ID, string WorkDesc, string OsStatus, string PlannedDate, string StartDate, string StartTime, string EndDate, string EndTime, string ControlsName, string Priority, bool WorkIT, bool WorkMaint, bool WorkProd, bool WorkProj, bool WorkEng)
        {
            string connectionString = PlantControl.Views.Index.ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(connectionString);

            try
            {
                string query1 = "UPDATE [PLANT_CONTROL].[ORDERS].[STATUS] SET "
                                   + "WORK_DESC = @workDesc,"
                                   + "OS_STATUS = @status,"
                                   + "PRIORITY = @priority,"
                                   + "PLANNED_DATE =  @planningDateOS,"
                                   + "START_DATE = @startDateOS,"
                                   + "END_DATE = @endDate, "
                                   + "WORK_IT = '" + WorkIT + "',"
                                   + "WORK_MAINT = '" + WorkMaint + "',"
                                   + "WORK_PROD = '" + WorkProd + "',"
                                   + "WORK_PROJ = '" + WorkProj + "',"
                                   + "WORK_ENG = '" + WorkEng + "'"
                                       + "WHERE ID = '" + ID + "' ";

                string query2 = "UPDATE [PLANT_CONTROL].[ORDERS].[CONTROLS] SET "
                                   + "CONTROLS_NAME = @controlsName "
                                       + "WHERE ID = '" + ID + "'"

                                       + "IF @@ROWCOUNT = 0 "
                                        + "INSERT INTO [PLANT_CONTROL].[ORDERS].[CONTROLS](ID, CONTROLS_NAME) "
                                               + " VALUES('" + ID + "', "
                                                             + " @controlsName) ";

                sqlConnection.Open();
                SqlCommand sqlCommand1 = new SqlCommand(query1, sqlConnection);
                sqlCommand1.Parameters.AddWithValue("@workDesc", String.IsNullOrWhiteSpace(WorkDesc) ? (object)DBNull.Value : (object)WorkDesc);
                sqlCommand1.Parameters.AddWithValue("@status", String.IsNullOrWhiteSpace(OsStatus) ? (object)DBNull.Value : (object)OsStatus);
                sqlCommand1.Parameters.AddWithValue("@priority", String.IsNullOrWhiteSpace(Priority) ? (object)DBNull.Value : (object)Convert.ToInt16(Priority));
                sqlCommand1.Parameters.AddWithValue("@planningDateOS", String.IsNullOrWhiteSpace(PlannedDate) ? (object)DBNull.Value : (object)PlannedDate);
                sqlCommand1.Parameters.AddWithValue("@startDateOS", String.IsNullOrWhiteSpace(StartDate) ? (object)DBNull.Value : (object)StartDate);
                sqlCommand1.Parameters.AddWithValue("@endDate", String.IsNullOrWhiteSpace(EndDate) ? (object)DBNull.Value : (object)EndDate);

                SqlCommand sqlCommand2 = new SqlCommand(query2, sqlConnection);
                sqlCommand2.Parameters.AddWithValue("@controlsName", String.IsNullOrWhiteSpace(ControlsName) ? (object)DBNull.Value : (object)ControlsName);

                int statusQuery1 = sqlCommand1.ExecuteNonQuery();
                int statusQuery2 = sqlCommand2.ExecuteNonQuery();

                sqlConnection.Close();

                //check if was successfully recorded
                if (statusQuery1 > 0 && statusQuery2 > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                string test = ex.ToString();
                sqlConnection.Close();
                return false;
            }

        }

        public int GetUserTimeElapse(string user)
        {
            string connectionString = PlantControl.Views.Index.ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(connectionString);

            try
            {
                sqlConnection.Open();
                string query = "SELECT ELAPSE FROM [PLANT_CONTROL].[USERS].[USER] WHERE USERNAME = '" + user + "' ";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                return Convert.ToInt16(dataTable.Rows[0][0]);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public DataTable GetControlsWorkForce(string user, string year)
        {
            string connectionString = PlantControl.Views.Index.ConnectionString;
            string query = "select START_DATE, END_DATE, WORK_SUMMARY from [ControlsPlant].[dbo].[CONTROLS_WORKFORCE] tA"
                           + " JOIN [ControlsPlant].[dbo].[WORK_REQUEST] tB ON tA.CONTROLS_NAME = '" + user + "' AND tB.OS_NUMBER = tA.OS_NUMBER"
                           + " JOIN [ControlsPlant].[DBO].[WORK_REQUEST_STATUS] tC ON tC.PROJECT = 'TRUE' and tC.OS_NUMBER = tA.OS_NUMBER"
                           + " AND tA.START_DATE  LIKE '%" + year + "%'";

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                    DataTable dataTable = new DataTable();
                    sqlDataAdapter.Fill(dataTable);
                    sqlConnection.Close();
                    return dataTable;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string[] GetServerName(string lineName)
        {
            string[] serverName = new string[3];
            string tableTo = null;
            string serverFrom = null;
            string tableFrom = null;

            switch (lineName)
            {
                case "FRONT AXLE":
                    tableTo = "LIMITS_FRONT";
                    serverFrom = "ARAFIS1AA05";
                    tableFrom = "SCADA";
                    break;
                case "MAN 3RDM":
                    tableTo = "LIMITS_MAN_3RDM";
                    serverFrom = "ARAFIS1AE05";
                    tableFrom = "SCADA_3rd_MEM";
                    break;
                case "MAN FINAL":
                    tableTo = "LIMITS_MAN_FINAL";
                    serverFrom = "ARAFIS1AE08";
                    tableFrom = "SCADA";
                    break;
                case "MAN WHUB_ASSY":
                    tableTo = "LIMITS_MAN_HUB";
                    serverFrom = "ARAFIS1AE08";
                    tableFrom = "SCADA_HUB";
                    break;
                case "MAN PROPSHAFT":
                    tableTo = "LIMITS_MAN_CARDAN";
                    serverFrom = "ARAFIS1AE05";
                    tableFrom = "SCADA_PSHAFT";
                    break;
                case "RPU_GM 3RDM":
                    tableTo = "LIMITS_RPU_3RDM";
                    serverFrom = "ARAFIS1AC05";
                    tableFrom = "SCADA_3rd_Mem";
                    break;
                case "RPU_GM FINAL":
                    tableTo = "LIMITS_RPU_FINAL";
                    serverFrom = "ARAFIS1AC08";
                    tableFrom = "SCADA";
                    break;
                case "RPU_GM_SHAFT":
                    tableTo = "LIMITS_RPU_SHAFT";
                    serverFrom = "ARAFIS1AC08";
                    tableFrom = "SHAFT_ASSY";
                    break;
                case "BANJO WELD":
                    tableTo = "LIMITS_BANJO";
                    serverFrom = "ARAFIS1AC05";
                    tableFrom = "SCADA_Banjo";
                    break;
                case "CVJ ASSY":
                    tableTo = "LIMITS_CVJ";
                    serverFrom = "AMFNT142";
                    tableFrom = "IPF20_CVJ";
                    break;
                default:
                    break;
            }

            serverName[0] = tableTo;
            serverName[1] = serverFrom;
            serverName[2] = tableFrom;

            return serverName;
        }

        public int UpdateSCADALimTable(string lineName)
        {
            string[] serverName = new string[3];
            serverName = GetServerName(lineName);
            string query = null;

            string tableTo = serverName[0];
            string serverFrom = serverName[1];
            string tableFrom = serverName[2];
            int res = 99;

            string connectionString = PlantControl.Views.Index.ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            try
            {
                sqlConnection.Open();

                if (lineName == "CVJ ASSY")
                {
                    query = "TRUNCATE TABLE [PLANT_CONTROL].[SCADA].[REVIEW_TEMP]"
                            + " INSERT INTO [PLANT_CONTROL].[SCADA].[REVIEW_TEMP]"
                            + " (PARAM_ID,PART_ID,REVIEWED)"
                            + " SELECT  PARAM_ID, PART_ID, REVIEWED"
                            + " FROM [PLANT_CONTROL].[SCADA].[" + tableTo + "]"

                            + " TRUNCATE TABLE [PLANT_CONTROL].[SCADA].[" + tableTo + "]"
                            + " INSERT INTO [PLANT_CONTROL].[SCADA].[" + tableTo + "]"
                            + " (PARAM_ID,PART_ID,TSTAMP,REINTRO_TYPE,LIMIT_TYPE,HI_LIM,LO_LIM,"
                                + "TARG,TOL,TEXT,STN_ID,IS_ACTIVE_ONLY_IN_STN_ID)"
                            + " SELECT tC.CharID AS PARAM_ID,"
                              + "tC.PartID AS PART_ID,"
                              + "tC.ModifiedDate AS TSTAMP,"
                              + "tc.ReintroType AS REINTRO_TYPE,"
                              + "tC.LimitType AS LIMIT_TYPE,"
                              + "tC.High AS HI_LIM,"
                              + "tC.Low AS LO_LIM,"
                              + "tC.Target AS TARG,"
                              + "tC.Tolerance AS TOL,"
                              + "tC.Text AS TEXT,"
                              + "tA.AreaID AS STN_ID,"
                              + "IS_ACTIVE_ONLY_IN_STN_ID = 'false' "
                            + "FROM [AMFNT142].[IPF20_CVJ].[ORG].[Area] tA, "
                               + "[AMFNT142].[IPF20_CVJ].[Product].[Part] tB, "
                               + "[AMFNT142].[IPF20_CVJ].[Process].[Limit] tC, "
                               + "[AMFNT142].[IPF20_CVJ].[System].[StdChar] tD "
                            + "WHERE tA.AreaID = tc.AreaId AND tB.PartID = tC.PartID AND tD.CharID = tC.CharID"

                            + " UPDATE  [PLANT_CONTROL].[SCADA].[" + tableTo + "]"
                            + " SET [PLANT_CONTROL].[SCADA].[" + tableTo + "].[REVIEWED] = tA.REVIEWED"
                            + " FROM [PLANT_CONTROL].[SCADA].[REVIEW_TEMP] tA"
                            + " WHERE [PLANT_CONTROL].[SCADA].[" + tableTo + "].[PART_ID] = tA.PART_ID"
                            + " AND [PLANT_CONTROL].[SCADA].[" + tableTo + "].[PARAM_ID] = tA.PARAM_ID";
                }
                else
                {
                    query = "TRUNCATE TABLE [PLANT_CONTROL].[SCADA].[REVIEW_TEMP]"
                            + " INSERT INTO [PLANT_CONTROL].[SCADA].[REVIEW_TEMP]"
                            + " (PARAM_ID,PART_ID,REVIEWED)"
                            + " SELECT  PARAM_ID, PART_ID, REVIEWED"
                            + " FROM [PLANT_CONTROL].[SCADA].[" + tableTo + "]"

                            + " TRUNCATE TABLE [PLANT_CONTROL].[SCADA].[" + tableTo + "] "
                            + " INSERT INTO [PLANT_CONTROL].[SCADA].[" + tableTo + "] "
                                + " (PARAM_ID,PART_ID,TSTAMP,REINTRO_TYPE,LIMIT_TYPE,HI_LIM,LO_LIM,"
                                + " TARG,TOL,TEXT,STN_ID,IS_ACTIVE_ONLY_IN_STN_ID)"
                            + " SELECT PARAM_ID,PART_ID,TSTAMP,REINTRO_TYPE,LIMIT_TYPE,HI_LIM,LO_LIM,"
                                + " TARG,TOL,TEXT,STN_ID,IS_ACTIVE_ONLY_IN_STN_ID"
                            + " FROM [" + serverFrom + "].[" + tableFrom + "].[RCP].[LIMITS] "

                            + " UPDATE  [PLANT_CONTROL].[SCADA].[" + tableTo + "]"
                            + " SET [PLANT_CONTROL].[SCADA].[" + tableTo + "].[REVIEWED] = tA.REVIEWED"
                            + " FROM [PLANT_CONTROL].[SCADA].[REVIEW_TEMP] tA"
                            + " WHERE [PLANT_CONTROL].[SCADA].[" + tableTo + "].[PART_ID] = tA.PART_ID"
                            + " AND [PLANT_CONTROL].[SCADA].[" + tableTo + "].[PARAM_ID] = tA.PARAM_ID";
                }

                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                int statusQuery = sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();

                //check if was successfully recorded
                if (statusQuery > 0)
                {
                    res = 1;
                }
                return res;
            }
            catch (Exception ex)
            {
                string test = ex.ToString();
                return 99;
            }
        }

        public DataTable SCADACompareTables(string lineName)
        {
            string[] serverName = new string[3];
            serverName = GetServerName(lineName);
            string query1 = null;
            string query2 = null;

            string tableTo = serverName[0];
            string serverFrom = serverName[1];
            string tableFrom = serverName[2];


            string connectionString = PlantControl.Views.Index.ConnectionString;

            if (lineName == "CVJ ASSY")
            {
                query1 = "TRUNCATE TABLE [PLANT_CONTROL].[SCADA].[LIMITS_TEMP] "
                              + "INSERT INTO [PLANT_CONTROL].[SCADA].[LIMITS_TEMP] "
                              + "SELECT tC.CharID AS PARAM_ID,"
                                  + "tC.PartID AS PART_ID,"
                                  + "tC.ModifiedDate AS TSTAMP,"
                                  + "tc.ReintroType AS REINTRO_TYPE,"
                                  + "tC.LimitType AS LIMIT_TYPE,"
                                  + "tC.High AS HI_LIM,"
                                  + "tC.Low AS LO_LIM,"
                                  + "tC.Target AS TARG,"
                                  + "tC.Tolerance AS TOL,"
                                  + "tC.Text AS TEXT,"
                                  + "tA.AreaID AS STN_ID,"
                                  + "IS_ACTIVE_ONLY_IN_STN_ID = 'false' "
                              + "FROM [AMFNT142].[IPF20_CVJ].[ORG].[Area] tA, "
                                   + "[AMFNT142].[IPF20_CVJ].[Product].[Part] tB, "
                                   + "[AMFNT142].[IPF20_CVJ].[Process].[Limit] tC, "
                                   + "[AMFNT142].[IPF20_CVJ].[System].[StdChar] tD "
                              + "WHERE tA.AreaID = tc.AreaId AND tB.PartID = tC.PartID AND tD.CharID = tC.CharID";

                query2 = "SELECT tC.Name AS STATION , tA.PART_ID, tD.PartNumber AS PART_NUM, "
                                + "tD.Description AS PART_DESC, tE.CharDesc AS PARAM_DESC, tA.LIMIT_TYPE AS TYPE, "
                                + "tA.PARAM_ID AS PARAM_ID,  tA.HI_LIM  AS OLD_HI, tA.LO_LIM  AS OLD_LOW, tA.TARG AS OLD_TARG, "
                                + "tA.TOL AS OLD_TOL, tA.TEXT AS OLD_TEXT, tB.HI_LIM_NEW AS NEW_HI, tB.LO_LIM_NEW AS NEW_LOW, "
                                + "tB.TARG_NEW  AS NEW_TARG, tB.TOL_NEW AS NEW_TOL, tB.TEXT_NEW AS NEW_TEXT "
                        + "FROM [PLANT_CONTROL].[SCADA].[LIMITS_CVJ] tA "
                        + "JOIN [PLANT_CONTROL].[SCADA].[LIMITS_TEMP] tB "
                            + "ON tA.PARAM_ID = tB.PARAM_ID AND "
                            + "tA.PART_ID = tB.PART_ID AND "
                            + "tA.REINTRO_TYPE = tB.REINTRO_TYPE AND "
                            + "(tA.LIMIT_TYPE != tB.LIMIT_TYPE OR "
                            + "tA.HI_LIM != tB.HI_LIM_NEW  OR "
                            + "tA.LO_LIM != tB.LO_LIM_NEW  OR "
                            + "tA.TARG != tB.TARG_NEW OR "
                            + "tA.TOL != tB.TOL_NEW OR "
                            + "tA.TEXT != tB.TEXT_NEW OR "
                            + "tA.STN_ID != tB.STN_ID) "
                        + "JOIN [AMFNT142].[IPF20_CVJ].[ORG].[AREA] tC ON tA.STN_ID = tC.AreaID "
                        + "JOIN [AMFNT142].[IPF20_CVJ].[Product].[Part] tD ON tA.PART_ID = tD.PartID "
                        + "JOIN [AMFNT142].[IPF20_CVJ].[System].[StdChar] tE ON tA.PARAM_ID = tE.CharID ";
            }
            else
            {
                query1 = "  TRUNCATE TABLE [PLANT_CONTROL].[SCADA].[LIMITS_TEMP]"
                       + "  INSERT INTO [PLANT_CONTROL].[SCADA].[LIMITS_TEMP]"
                       + "  SELECT * FROM [" + serverFrom + "].[" + tableFrom + "].[RCP].[LIMITS]"
                       + "  GO";

                query2 = "SELECT STN_NAME AS STATION, tA.PART_ID, PART_NUM, PART_DESC, PARAM_DESC, tA.LIMIT_TYPE AS TYPE, tA.PARAM_ID AS PARAM_ID, "
                            + " tA.HI_LIM AS OLD_HI, tA.LO_LIM AS OLD_LOW, tA.TARG AS OLD_TARG, tA.TOL AS OLD_TOL, tA.TEXT AS OLD_TEXT,  "
                            + " tB.HI_LIM_NEW AS NEW_HI, tB.LO_LIM_NEW AS NEW_LOW, tB.TARG_NEW AS NEW_TARG, tB.TOL_NEW AS NEW_TOL, tB.TEXT_NEW AS NEW_TEXT"
                            + " FROM [PLANT_CONTROL].[SCADA].[" + tableTo + "] tA"
                            + " JOIN [PLANT_CONTROL].[SCADA].[LIMITS_TEMP] tB ON tA.PARAM_ID = tB.PARAM_ID AND tA.PART_ID = tB.PART_ID AND tA.REINTRO_TYPE = tB.REINTRO_TYPE AND"
                            + " (tA.LIMIT_TYPE != tB.LIMIT_TYPE "
                            + "    OR tA.HI_LIM != tB.HI_LIM_NEW "
                            + "    OR tA.LO_LIM != tB.LO_LIM_NEW "
                            + "    OR tA.TARG != tB.TARG_NEW "
                            + "    OR tA.TOL != tB.TOL_NEW "
                            + "    OR tA.TEXT != tB.TEXT_NEW "
                            + "    OR tA.STN_ID != tB.STN_ID) "
                            + " JOIN [" + serverFrom + "].[" + tableFrom + "].[QIS].[STATION] tC ON tA.STN_ID = tC.STN_ID "
                            + " JOIN [" + serverFrom + "].[" + tableFrom + "].[RCP].[PART] tD ON tA.PART_ID = tD.PART_ID "
                            + " JOIN [" + serverFrom + "].[" + tableFrom + "].[RCP].[STD_PARAM] tE ON tA.PARAM_ID = tE.PARAM_ID";
            }

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    SqlCommand sqlCommand1 = new SqlCommand(query1, sqlConnection);
                    sqlCommand1.ExecuteNonQuery();

                    SqlCommand sqlCommand2 = new SqlCommand(query2, sqlConnection);
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand2);
                    DataTable dataTable = new DataTable();
                    sqlDataAdapter.Fill(dataTable);
                    sqlConnection.Close();
                    sqlConnection.Dispose();
                    return dataTable;
                }
            }
            catch (Exception ex)
            {
                string teste = ex.ToString();
                return null;
            }
        }

        public DataTable ViewLimits(string lineName)
        {
            string[] serverName = new string[3];
            serverName = GetServerName(lineName);
            string query = null;

            string tableTo = serverName[0];
            string serverFrom = serverName[1];
            string tableFrom = serverName[2];

            string connectionString = PlantControl.Views.Index.ConnectionString;

            if (lineName == "CVJ ASSY")
            {
                query = "SELECT tA.Name AS STN_NAME,"
                              + "tC.PartID AS PART_ID,"
                              + "tB.PartNumber AS PART_NUM,"
                              + "tB.Description AS PART_DESC,"
                              + "tD.CharDesc AS PARAM_DESC,"
                              + "tC.LimitType AS LIMIT_TYPE,"
                              + "tC.High AS HI_LIM,"
                              + "tC.Low AS LO_LIM,"
                              + "tC.Target AS TARG,"
                              + "tC.Tolerance AS TOL,"
                              + "tC.Text AS TEXT,"
                              + "tC.CharID AS PARAM_ID "
                          + "FROM [AMFNT142].[IPF20_CVJ].[ORG].[Area] tA, "
                               + "[AMFNT142].[IPF20_CVJ].[Product].[Part] tB, "
                               + "[AMFNT142].[IPF20_CVJ].[Process].[Limit] tC, "
                               + "[AMFNT142].[IPF20_CVJ].[System].[StdChar] tD "
                          + "WHERE tA.AreaID = tc.AreaId AND tB.PartID = tC.PartID AND tD.CharID = tC.CharID"
                          + " ORDER BY tc.AreaId ASC";
            }
            else
            {
                query = "SELECT STN_NAME, tA.PART_ID, PART_NUM, PART_DESC, PARAM_DESC, tA.LIMIT_TYPE, tA.HI_LIM, tA.LO_LIM, tA.TARG, tA.TOL, tA.TEXT, tA.PARAM_ID"
                      + " FROM [" + serverFrom + "].[" + tableFrom + "].[RCP].[LIMITS] tA "
                      + " JOIN [" + serverFrom + "].[" + tableFrom + "].[QIS].[STATION] tB ON tA.STN_ID = tB.STN_ID "
                      + " JOIN [" + serverFrom + "].[" + tableFrom + "].[RCP].[PART] tC ON tA.PART_ID = tC.PART_ID "
                      + " JOIN [" + serverFrom + "].[" + tableFrom + "].[RCP].[STD_PARAM] tD ON tA.PARAM_ID = tD.PARAM_ID"
                      + " ORDER BY tA.STN_ID ASC";
            }

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                    DataTable dataTable = new DataTable();
                    sqlDataAdapter.Fill(dataTable);
                    sqlConnection.Close();
                    sqlConnection.Dispose();
                    return dataTable;
                }
            }
            catch (Exception ex)
            {
                string test = ex.ToString();
                return null;
            }
        }

        public DataTable ViewLimitsReview(string lineName)
        {
            string[] serverName = new string[3];
            serverName = GetServerName(lineName);
            string query = null;

            string tableTo = serverName[0];
            string serverFrom = serverName[1];
            string tableFrom = serverName[2];

            string connectionString = PlantControl.Views.Index.ConnectionString;

            if (lineName == "CVJ ASSY")
            {
                    query = "SELECT tA.Name AS STN_NAME,"
                              + "tC.PART_ID,"
                              + "tB.PartNumber AS PART_NUM,"
                              + "tB.Description AS PART_DESC,"
                              + "tD.CharDesc AS PARAM_DESC,"
                              + "tC.LIMIT_TYPE,"
                              + "tC.HI_LIM,"
                              + "tC.LO_LIM,"
                              + "tC.TARG,"
                              + "tC.TOL,"
                              + "tC.TEXT,"
                              + "tC.PARAM_ID, "
                              + "tC.REVIEWED "
                          + "FROM [AMFNT142].[IPF20_CVJ].[ORG].[Area] tA, "
                               + "[AMFNT142].[IPF20_CVJ].[Product].[Part] tB, "
                               + "[PLANT_CONTROL].[SCADA].[" + tableTo + "] tC, "
                               + "[AMFNT142].[IPF20_CVJ].[System].[StdChar] tD "
                          + "WHERE tA.AreaID = tc.STN_ID AND tB.PartID = tC.PART_ID AND tD.CharID = tC.PARAM_ID "
                          + " ORDER BY tc.STN_ID ASC";
            }
            else
            {
                    query = "SELECT STN_NAME, tA.PART_ID, PART_NUM, PART_DESC, PARAM_DESC, tA.LIMIT_TYPE, tA.HI_LIM, tA.LO_LIM, tA.TARG, tA.TOL, tA.TEXT, tA.PARAM_ID, tA.REVIEWED"
                      + " FROM [PLANT_CONTROL].[SCADA].[" + tableTo + "] tA "
                      + " JOIN [" + serverFrom + "].[" + tableFrom + "].[QIS].[STATION] tB ON tA.STN_ID = tB.STN_ID "
                      + " JOIN [" + serverFrom + "].[" + tableFrom + "].[RCP].[PART] tC ON tA.PART_ID = tC.PART_ID "
                      + " JOIN [" + serverFrom + "].[" + tableFrom + "].[RCP].[STD_PARAM] tD ON tA.PARAM_ID = tD.PARAM_ID"
                      + " ORDER BY tA.STN_ID ASC";                                         
            }

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                    DataTable dataTable = new DataTable();
                    sqlDataAdapter.Fill(dataTable);
                    sqlConnection.Close();
                    sqlConnection.Dispose();
                    return dataTable;
                }
            }
            catch (Exception ex)
            {
                string test = ex.ToString();
                return null;
            }
        }

        public void UpdateLimitsReview(string lineName, int paramId, int partId, bool value)
        {
            string dateNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            string[] serverName = new string[3];
            serverName = GetServerName(lineName);
           
            string tableTo = serverName[0];

            string connectionString = PlantControl.Views.Index.ConnectionString;

            try
            {
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                
                sqlConnection.Open();

                string query = "UPDATE [PLANT_CONTROL].[SCADA].[" + tableTo + "] SET REVIEWED = '" + value + "' "
                                + " WHERE PARAM_ID = '" + paramId + "' AND  PART_ID = '" + partId + "' ";                        

                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                int statusQuery = sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();

            }
            catch (Exception ex)
            {
                string fault = ex.ToString();
            }
        }

        public int GetLimitsReviewTotal()
        {
            string connectionString = PlantControl.Views.Index.ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(connectionString);

            try
            {
                sqlConnection.Open();
                string query = "EXEC [dbo].[LimitsViewTotalCalc] ";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                return Convert.ToInt16(dataTable.Rows[0][0]);
            }
            catch (Exception ex)
            {
                string fault = ex.ToString();
                return 0;
            }
        }

        public int GetLimitsReviewNumOfLimits(string assemblyLine)
        {
            string connectionString = PlantControl.Views.Index.ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(connectionString);

            try
            {
                sqlConnection.Open();
                string query = "DECLARE @ROW_ALL INT;  "
                                + " SELECT @ROW_ALL = COUNT(*) FROM [PLANT_CONTROL].[SCADA].[" + assemblyLine + "]  "
                                + " SELECT @ROW_ALL";

                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                return Convert.ToInt16(dataTable.Rows[0][0]);
            }
            catch (Exception ex)
            {
                string fault = ex.ToString();
                return 0;
            }
        }

        public int GetLimitsReviewNumOfReviewed(string assemblyLine)
        {
            string connectionString = PlantControl.Views.Index.ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(connectionString);

            try
            {
                sqlConnection.Open();
                string query = "DECLARE @ROW_REVIEWED INT;  "
                                + " SELECT @ROW_REVIEWED = COUNT(*) FROM [PLANT_CONTROL].[SCADA].[" + assemblyLine + "] WHERE REVIEWED = 'TRUE'  "
                                + " SELECT @ROW_REVIEWED";

                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                return Convert.ToInt16(dataTable.Rows[0][0]);
            }
            catch (Exception ex)
            {
                string fault = ex.ToString();
                return 0;
            }
        }

        public DataTable GetStationName(string lineName)
        {
            string[] serverName = new string[3];
            serverName = GetServerName(lineName);

            string tableTo = serverName[0];
            string serverFrom = serverName[1];
            string tableFrom = serverName[2];

            string connectionString = PlantControl.Views.Index.ConnectionString;

            string query = "select STN_NAME  FROM [" + serverFrom + "].[" + tableFrom + "].[QIS].[STATION] ORDER BY STN_ID ASC";

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                    DataTable dataTable = new DataTable();
                    sqlDataAdapter.Fill(dataTable);
                    sqlConnection.Close();
                    sqlConnection.Dispose();
                    return dataTable;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool CheckCharIDExist(int charID, string station)
        {
            bool res = false;
            string connectionString = PlantControl.Views.Index.ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(connectionString);

            string query = "SELECT * FROM [ControlsPlant].[dbo].[HISTORY_CHAR_ID] WHERE CHAR_ID = '" + charID + "' AND OP_NAME = '" + station + "'";

            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        res = true;
                    }
                }
                sqlConnection.Close();

                return res;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public DataTable GetCharIDAvailable(string lineName, string station)
        {
            string[] serverName = new string[3];
            serverName = GetServerName(lineName);

            string tableTo = serverName[0];
            string serverFrom = serverName[1];
            string tableFrom = serverName[2];

            string connectionString = PlantControl.Views.Index.ConnectionString;

            string query = "select tA.CHAR_ID, tB.CHAR_DESC FROM [" + serverFrom + "].[" + tableFrom + "].[QIS].[NDATA] tA, "
                            + "  [" + serverFrom + "].[" + tableFrom + "].[QIS].[STD_CHAR] tB"
                            + "     WHERE tA.CHAR_ID = tB.CHAR_ID and tA.COLL_ID IN( "
                            + "         select top 1 COLL_ID from [" + serverFrom + "].[" + tableFrom + "].[QIS].[DATA_TSTAMP] tC "
                            + "             JOIN [" + serverFrom + "].[" + tableFrom + "].[QIS].[STATION] tD ON tD.STN_ID = tC.STN_ID AND tD.STN_NAME = '" + station + "'"
                            + "                 order by TSTAMP desc)";


            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                    DataTable dataTable = new DataTable();
                    sqlDataAdapter.Fill(dataTable);
                    sqlConnection.Close();
                    sqlConnection.Dispose();
                    return dataTable;
                }
            }
            catch (Exception ex)
            {
                string teste = ex.ToString();
                return null;
            }
        }

        public int InsertCharID(string lineName, string station, int charID)
        {
            int res = 99;

            string connectionString = PlantControl.Views.Index.ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            try
            {
                sqlConnection.Open();
                string query = "INSERT INTO [ControlsPlant].[dbo].[HISTORY_CHAR_ID]"
                                + " (CHAR_ID, LINE_NAME, OP_NAME)"
                                + " VALUES(" + charID + ", '" + lineName + "', '" + station + "')";

                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                int statusQuery = sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();

                //check if was successfully recorded
                if (statusQuery > 0)
                {
                    res = 1;
                }
                return res;
            }
            catch (Exception ex)
            {
                string fault = ex.ToString();
                return 99;
            }
        }

        public int DeleteCharID(string lineName, string station, int charID)
        {
            int res = 99;

            string connectionString = PlantControl.Views.Index.ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            try
            {
                sqlConnection.Open();
                string query = "DELETE FROM [ControlsPlant].[dbo].[HISTORY_CHAR_ID]"
                                + " WHERE CHAR_ID = " + charID + " AND LINE_NAME = '" + lineName + "' AND OP_NAME = '" + station + "'  ";

                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                int statusQuery = sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();

                //check if was successfully recorded
                if (statusQuery > 0)
                {
                    res = 1;
                }
                return res;
            }
            catch (Exception ex)
            {
                string fault = ex.ToString();
                return 99;
            }
        }

        public int GetLaborWorkForce(string user, string area, string year)
        {
            area = area.ToUpper();
            string connectionString = PlantControl.Views.Index.ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(connectionString);

            try
            {
                sqlConnection.Open();
                string query = "SELECT TOTAL = SUM(DATEDIFF(hour, START_DATE, END_DATE)) from [ControlsPlant].[dbo].[CONTROLS_WORKFORCE] tA "
                                + " JOIN [ControlsPlant].[dbo].[WORK_REQUEST_STATUS] tB ON tB.OS_NUMBER = tA.OS_NUMBER "
                                + "  AND tB." + area + " = 'true' "
                                + "  AND tA.CONTROLS_NAME =  '" + user + "' "
                                + "  AND tA.END_DATE like '%" + year + "%' ";

                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();

                return Convert.ToInt16(dataTable.Rows[0][0]);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int GetLaborWorkForce(string area, string year)
        {
            area = area.ToUpper();
            string connectionString = PlantControl.Views.Index.ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(connectionString);

            try
            {
                sqlConnection.Open();
                string query = "SELECT TOTAL = SUM(DATEDIFF(hour, START_DATE, END_DATE)) from [ControlsPlant].[dbo].[CONTROLS_WORKFORCE] tA "
                                + " JOIN [ControlsPlant].[dbo].[WORK_REQUEST_STATUS] tB ON tB.OS_NUMBER = tA.OS_NUMBER "
                                + "  AND tB." + area + " = 'true' "
                                + "  AND tA.END_DATE like '%" + year + "%' ";

                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();

                return Convert.ToInt16(dataTable.Rows[0][0]);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public DataTable GetProcessEngMail(string cellName)
        {
            string connectionString = PlantControl.Views.Index.ConnectionString;
          
            string query = "SELECT EMAIL FROM USERS.EMAIL tA, USERS.CELL tB "
                               + "WHERE tA.NAME = tB.NAME AND tA.ENABLE = 'TRUE' "
                               + " AND tA.PROC_ENG = 'TRUE'AND tB.CELL = '" + cellName + "' ";

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                    DataTable dataTable = new DataTable();
                    sqlDataAdapter.Fill(dataTable);
                    sqlConnection.Close();
                    sqlConnection.Dispose();
                    return dataTable;
                }
            }
            catch (Exception ex)
            {
                string teste = ex.ToString();
                return null;
            }
        }

        public DataTable GetCopyEngMail()
        {
            string connectionString = PlantControl.Views.Index.ConnectionString;

            string query = "SELECT EMAIL FROM USERS.EMAIL tA "
                               + " WHERE tA.NAME NOT IN(SELECT NAME FROM USERS.CELL) "
                               + " AND tA.ENABLE = 'TRUE' AND tA.PROC_ENG = 'FALSE' ";
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                    DataTable dataTable = new DataTable();
                    sqlDataAdapter.Fill(dataTable);
                    sqlConnection.Close();
                    sqlConnection.Dispose();
                    return dataTable;
                }
            }
            catch (Exception ex)
            {
                string teste = ex.ToString();
                return null;
            }
        }

        public void ExecuteDBBackup()
        {
            string connectionString = PlantControl.Views.Index.ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            string fileName = "PLANT_CONTROL_" + DateTime.Now.ToString("yyMMdd") +".bak";
            string path = "N'C:\\Program Files\\Microsoft SQL Server\\MSSQL15.SQLEXPRESS\\MSSQL\\Backup\\" + fileName + "'";         

            try
            {
                sqlConnection.Open();
                string query = "BACKUP DATABASE [PLANT_CONTROL]"
                                + " TO DISK = " + path + " "
                                + " WITH NOFORMAT, NOINIT,"
                                + " NAME = N'SQLTestDB-Full Database Backup', SKIP, NOREWIND, NOUNLOAD,  STATS = 10";

                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                string teste = ex.ToString();
            }
        }

    }
}