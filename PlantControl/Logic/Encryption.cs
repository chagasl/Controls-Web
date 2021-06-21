using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace PlantControl
{
    public class Encryption
    {
        public string Encrypt(string encryptString)
        {
            string EncryptionKey = "XxxxXXXxxxxXXXxxxxXXXXxxxxXXXXxxxx";
            byte[] clearBytes = Encoding.Unicode.GetBytes(encryptString);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
                    0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
                    });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    encryptString = Convert.ToBase64String(ms.ToArray());
                }
            }
            return encryptString;
        }

        public string Decrypt(string cipherText)
        {
            string EncryptionKey = "XxxxXXXxxxxXXXxxxxXXXXxxxxXXXXxxxx";
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
                    0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
                    });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

        public void AddCookieUser(string user)
        {
            SQLQuery sQLQuery = new SQLQuery();
            HttpCookie cookie = new HttpCookie("mtyew");
            cookie.Value = Encrypt(user.ToUpper());
            cookie.Expires = DateTime.Now.AddMinutes(sQLQuery.GetUserTimeElapse(user));
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public void AddCookieRole(string role)
        {
            SQLQuery sQLQuery = new SQLQuery();
            HttpCookie cookie = new HttpCookie("kwqtyi");
            cookie.Value = Encrypt(role);
            string user = ReadCookieUser();
            cookie.Expires = DateTime.Now.AddMinutes(sQLQuery.GetUserTimeElapse(user));
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public int ReadCookieRole()
        {
            try
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies["kwqtyi"];
                return Convert.ToInt16(Decrypt(cookie.Value));
            }
            catch (Exception ex)
            {
                string test = ex.ToString();
                return 0;
            }
        }

        public string ReadCookieUser()
        {
            try
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies["mtyew"];
                return Decrypt(cookie.Value).ToUpper();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void DeleteCookie()
        {
            HttpContext.Current.Response.Cookies["kwqtyi"].Expires = DateTime.Now.AddHours(-7);
            HttpContext.Current.Response.Cookies["mtyew"].Expires = DateTime.Now.AddHours(-7);

        }
    }
}