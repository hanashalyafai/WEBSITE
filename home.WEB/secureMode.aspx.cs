using home.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace home.WEB
{
    public partial class secureMode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["username"] == null)
            {
                Response.Redirect("login.aspx");

            }
        }


        [WebMethod]
        public static void randomDay(bool isEnabled)
        {
            try
            {
                if (isEnabled)
                {
                    string command = "";
                    List<list> randDayCommands = BLL.BLL.getRandomDay();
                    foreach(list l in randDayCommands){
                        command = "2#" + l.time.Year.ToString() + " " + l.time.Month.ToString() + " " + l.time.Day.ToString() + " " + l.time.Hour.ToString() + " " + l.time.Minute.ToString() + " " + l.time.Second.ToString() + " " + l.pin.ToString() + " " + l.state.ToString();
                        masterPage.Write(command);
                    }
                }
                else
                {
                    // stop secure mode!
                }
            }
            catch (Exception ex)
            {
            }
            

        }


        [WebMethod]
        public static void pictureStreaming(bool isEnabled)
        {


            /// stop video streaming


        }
        [WebMethod]
        public static void motionDetection(bool isEnabled)
        {

            // motion detection 


        }

        [WebMethod]
        public static void videoStreaming(bool isEnabled)
        {
            /// stop picture streaming 
            /// 


        }


    }
}