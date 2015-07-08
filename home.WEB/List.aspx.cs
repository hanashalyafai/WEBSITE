using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace home.WEB
{
    public partial class List : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["username"] == null)
            {
                Response.Redirect("login.aspx");

            }else
                loadCommands(false);
        }

        public void loadCommands(bool isAll)
        {
            gvList.DataSource = BLL.BLL.getList(isAll);
            gvList.DataBind();

        }
        protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvList.PageIndex = e.NewPageIndex;
            gvList.DataBind();
        }

        protected void lbDelete_Click(object sender, EventArgs e)
        {
            gvList.DataSource = BLL.BLL.deleteCommand(Convert.ToInt32(((LinkButton)sender).CommandArgument));
            gvList.DataBind();

        }
    }
}