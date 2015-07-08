using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace home.WEB
{
    public partial class masterPage : System.Web.UI.MasterPage
    {
        public static bool tempIsReturned = false;
        protected void Page_Load(object sender, EventArgs e)
        {
               // initialize .. not to have garbage ... 
            if (!IsPostBack)
            {
                temperature = 0;
                try
                {
                    IPHostEntry iphe =  Dns.GetHostEntry("127.0.0.1");
                    IPAddress IpAddress = iphe.AddressList[0];
                    tcpListener = new TcpListener(IpAddress, port);
                    tcpListener.Start();

                    acceptClientThread = new Thread(new ThreadStart(acceptClient));
                    acceptClientThread.Start();
                }
                catch (Exception ex)
                {
                    //HttpContext.Current.Response.Write("aljdrfsgbadfjgb");
                }
            }
            else
            {
                setTemp();
            }




        }
        public void setTemp() {
            string str = temperature.ToString() + " C";
            temp.Text = str;
            return;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string updateTempTime()
        {
            masterPage.Write("1#");
            int counter = 50000;
            while (!masterPage.tempIsReturned || (counter--) != 0) ;
            string JSONtemp = "{\"tempIsReturned\":"
                                      + masterPage.tempIsReturned + ",\"temp\":\""
                                      + masterPage.temperature + "}";
            masterPage.tempIsReturned = false;
            return JSONtemp;
        }

        private void Read()
        {

            string str = "";
            byte[] msgBytes;
            while (true)
            {
                msgBytes = new byte[256];
                try
                {
                    stream.Read(msgBytes, 0, msgBytes.Length);
                }
                catch (Exception ex)
                {
                    Response.Redirect("error.aspx");
                    //Response.Write("you need to restart the system!, connection stream was lost!");
                }
                str = Encoding.ASCII.GetString(msgBytes, 0, msgBytes.Length);
                str = str.Substring(0, str.IndexOf('\0'));
                parseData(str);

            }

        }

        private void parseData(string str)
        {
            if (str.Length == 0) return;
            string commandType = str.Substring(0, str.IndexOf('#'));
            str = str.Substring(str.IndexOf('#') + 1);
            switch (commandType)
            {
                case "1": // temperature 
                    //tempID.Text = str ;
                    masterPage.tempIsReturned = true; //masterPage.tempIsReturned;
                    masterPage.temperature = Convert.ToInt32(str);

                    break;
                case "2": //  arduino is asking for commands .. not working yet!

                    break;
                case "3": // [4][8] .. status from arduino 

                    bool[,] status = new bool[4, 8];

                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 8; j++)
                            status[i, j] = Convert.ToBoolean(str[i * 4 + j]);
                    }

                    BLL.BLL.updateAppliancesStatus(status);

                    break;
                case "4": // time requested 

                    string dateTime = "4#" + DateTime.UtcNow.Year.ToString() + " " + DateTime.UtcNow.Month.ToString() + " " + DateTime.UtcNow.Day.ToString() + " ";
                    dateTime += DateTime.UtcNow.Hour.ToString() + 2 + " " + DateTime.UtcNow.Minute.ToString() + " " + DateTime.UtcNow.Second.ToString();
                    Write(dateTime);

                    break;

                default:
                    break;
            }


        }


        private void acceptClient()
        {
            while (true)
            {
                tcpClient = tcpListener.AcceptTcpClient();
               // Response.Write("client is accepted ");

                stream = tcpClient.GetStream();
               // Response.Write("stream is plah ");
                /*
                 we are supposed to have a list of threads, a thread for each connection
                 * and then when a read is invoked it's supposed to use a sppecific stream that is asociated with 
                 * the specific connection !!!
                 * 
                 * ............
                 * here, it's just working for one connection ! :) 
                 */
                readThread = new Thread(new ThreadStart(Read));
                readThread.Start();
            }
        }
        internal static void Write(string command)
        {

            byte[] msgBytes = Encoding.ASCII.GetBytes(command);
            stream.Write(msgBytes, 0, msgBytes.Length);

        }

        public Thread acceptClientThread { get; set; }

        public static byte[] bytesSent { get; set; }

        public static TcpClient client { get; set; }

        public static byte[] data { get; set; }

        public TcpListener tcpListener { get; set; }

        public int port = 8888;

        public static NetworkStream stream { get; set; }

        public TcpClient tcpClient { get; set; }

        public Thread readThread { get; set; }
        public static byte[] msgBytes { get; set; }
        public string str { get; set; }

        public static int temperature { get; set; }

    }
}