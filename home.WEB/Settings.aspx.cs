using home.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace home.WEB
{
    public partial class Settings : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["username"] == null)
            {
                Response.Redirect("login.aspx");

            } else if (!IsPostBack)
            {
                List<settings> allSettings = BLL.BLL.getSettings();

                gvSettings.DataSource = allSettings;
                gvSettings.DataBind();

            }



        }

        

        protected void gvSettings_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int hdfIndex = Convert.ToInt32(((HiddenField)gvSettings.Rows[e.RowIndex].FindControl("hdfIndex")).Value);
            string txtbxValue;
            try
            {
                 txtbxValue = Convert.ToInt32( ((TextBox)gvSettings.Rows[e.RowIndex].FindControl("txtbxValue")).Text).ToString();
            }
            catch (Exception ex)
            {
                return;
            }
          
            string Title = gvSettings.Rows[e.RowIndex].Cells[0].Text;

            if (txtbxValue.Equals(""))
            {
                txtbxValue = Convert.ToInt32( ((TextBox)gvSettings.Rows[e.RowIndex].FindControl("txtbxValue")).Attributes["placeholder"].ToString()).ToString();
            }

            gvSettings.DataSource = BLL.BLL.UpdateSettings(new settings { 
                name = Title,
                value = txtbxValue,
                index = hdfIndex
            });
            gvSettings.DataBind();

            string command = "5#";// #5 for settings @ .. 1 for max light , 2 for min light , 3 for max temp , 4 for min temp
            switch (Title)
            {
                case "Max Light Threshold":
                    command += "1@"+txtbxValue;
                    break;
                case "Max Temp Threshold":
                    command += "3@" + txtbxValue;
                    break;
                case "Min Light Threshold":
                    command += "2@" + txtbxValue;
                    break;
                case "Min Temp Threshold":
                    command += "4@" + txtbxValue;
                    break;
            }

            masterPage.Write(command); 
        }
    }
}