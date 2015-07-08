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
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string updateTempTime()
        {
            masterPage.Write("1#");  // requests temp from arduino 

            int counter = 1000;
            while (!masterPage.tempIsReturned && (counter--) != 0) { }
            
            int isReturned = masterPage.tempIsReturned ? 1 : 0;
            string JSONtemp = "{\"tempIsReturned\":"
                    + isReturned + ",\"temp\":" + masterPage.temperature + "}";

            masterPage.tempIsReturned = false;
            
            return JSONtemp;
        }

    }
}

