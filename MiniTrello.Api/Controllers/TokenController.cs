using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MiniTrello.Api.Controllers
{
    public class TokenController : Controller
    {
        //
        // GET: /Token/
       /* public string CalculateMd5Hash(string input)
        {
            // step 1, calculate MD5 hash from input
            MD5 md5 = MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            foreach (byte t in hash)
            {
                sb.Append(t.ToString("X2"));
            }
            return sb.ToString();
        }*/
       // public ActionResult Index()
        //{return View();}
        

        public string Createtoken(string identifier)
        {
            Guid id;
            try
            {
                id = new Guid(Request.QueryString[identifier]);
            }
            catch
            {
                id = Guid.Empty;
            }
            return id.ToString();
        }

        public string Gettoken(Guid token)
        {
            string uuid = token.ToString();

            return uuid;

        }
    }
}
