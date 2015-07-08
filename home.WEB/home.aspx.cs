using home.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace home.WEB
{
    public partial class home : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((masterPage)Page.Master).setTemp();
            }
            else
            {
                ((masterPage)Page.Master).setTemp();
            }

            if (Session["username"] == null)
            {
                Response.Redirect("login.aspx");

            }else
            {
                List<house> list = BLL.BLL.getHouse();
                gvPlaces.DataSource = list;
                gvPlaces.DataBind();
            }


        }
    }
}