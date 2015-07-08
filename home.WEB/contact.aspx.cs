using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace home.WEB
{
    public partial class contact : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static bool sendMessage(string name, string email, string phoneNum, string msg)
        {
            try
            {
                BLL.BLL.addMessage(name, email, phoneNum, msg);

                return false;
            }
            catch (Exception ex)
            {
                return true;
            }


        }
    }
}