using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace home.WEB
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
          

        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            // data from DB  .. check 
            string username = UserName.Text;
            string pass = Password.Text;

            // get passowrd from DB for username .. 
            //check password 
             string serverPass="";
            try{
                serverPass = BLL.BLL.getUserPassword(username);
            }catch(Exception ex){
                serverPass = "";

            }


            if (serverPass.Equals(pass) && !serverPass.Equals(""))
            {
                Session["username"] = UserName.Text;
                Response.Redirect("home.aspx");
            }
            else
            {
                InvalidCredentialsMessage.Visible = true;
            }



        }
    }
}