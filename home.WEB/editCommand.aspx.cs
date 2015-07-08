using home.DAL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace home.WEB
{
    public partial class editCommand : System.Web.UI.Page
    {
        string[] months = {"January", "February", "March", "April", "May",
						 "June", "July", "August", "September", "October", "November", "December"};
        Int32 id;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["username"] == null)
            {
                Response.Redirect("login.aspx");

            } else if (!Page.IsPostBack)
            {
                id = Convert.ToInt32(Request.QueryString["ListID"]);
                attachDDL(id);
            }
        }

        private void attachDDL(int id)
        {




            list l = BLL.BLL.getCommand(id);

            ArrayList yearList = new ArrayList();
            ArrayList dayList = new ArrayList();
            List<string> monthList = months.ToList();
            ArrayList hourList = new ArrayList();
            ArrayList minutesSecondsList = new ArrayList();

            for (int i = 1; i < 99; i++)
            {
                yearList.Add(i + 2000);
                if (i <= 30) dayList.Add(i);
                if (i <= 24) hourList.Add(i - 1);
                if (i <= 60) minutesSecondsList.Add(i - 1);

            }


            DDLyear.DataSource = yearList;
            DDLyear.SelectedValue = l.time.Year.ToString();
            DDLyear.DataBind();

            DDLmonth.DataSource = monthList;
            DDLmonth.SelectedValue = months[l.time.Month - 1];
            DDLmonth.DataBind();


            DDLday.DataSource = dayList;
            DDLday.SelectedValue = l.time.Day.ToString();
            DDLday.DataBind();

            DDLhour.DataSource = hourList;
            DDLhour.SelectedValue = l.time.Hour.ToString();
            DDLhour.DataBind();


            DDLminute.DataSource = minutesSecondsList;
            DDLminute.SelectedValue = l.time.Minute.ToString();
            DDLminute.DataBind();

            DDLsecond.DataSource = minutesSecondsList;
            DDLsecond.SelectedValue = l.time.Second.ToString();
            DDLsecond.DataBind();

            ArrayList states = new ArrayList();
            states.Add("ON");
            states.Add("OFF");
            DDLstate.DataSource = states;
            DDLstate.SelectedValue = l.state ? "ON" : "OFF";
            DDLstate.DataBind();

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int year = Convert.ToInt32(DDLyear.SelectedValue);
            int month = months.ToList().IndexOf(DDLmonth.SelectedValue) + 1;
            int day = Convert.ToInt32(DDLday.SelectedValue);
            int hour = Convert.ToInt32(DDLhour.SelectedValue);
            int minute = Convert.ToInt32(DDLminute.SelectedValue);
            int second = Convert.ToInt32(DDLsecond.SelectedValue);
            bool isOn = DDLstate.SelectedValue.Equals("ON") ? true : false;
            int listID = Convert.ToInt32(Request.QueryString["ListID"]);
            DateTime updatedTime = new DateTime(year, month, day, hour, minute, second);
            BLL.BLL.saveUpdate(listID, isOn, updatedTime);
            Response.Redirect("List.aspx");

        }


    }
}