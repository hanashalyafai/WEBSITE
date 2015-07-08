using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using home.BLL;
using home.DAL;
using System.Collections;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Web.Script.Services;


namespace home.WEB
{
    public partial class addCommand : System.Web.UI.Page
    {
        public static string[] months = {"January", "February", "March", "April", "May",
						 "June", "July", "August", "September", "October", "November", "December"};

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["username"] == null)
            {
                Response.Redirect("login.aspx");

            }
            else
            {
                gvAppliances_SetDropDownList();
            }
        }
        protected void gvAppliances_SetDropDownList()
        {
            Guid id;
            try
            {
                id = new Guid(Request.QueryString["ID"]);
                string updateStatus = "3#";
               // masterPage.Write(updateStatus);
            }
            catch (Exception ex)
            {
                return;
            }
            gvAppliances.DataSource = BLL.BLL.getAppliances(id);
            //Response.Write(l.ToArray().Length);
            gvAppliances.DataBind();

            DropDownList yearDDL;
            DropDownList stateDDL;
            DropDownList monthDDL;
            DropDownList dayDDL;
            DropDownList hourDDL;
            DropDownList minuteDDL;
            DropDownList secondDDL;

            ArrayList yearList = new ArrayList();
            ArrayList dayList = new ArrayList();
            List<string> monthList = months.ToList();
            ArrayList hourList = new ArrayList();
            ArrayList minutesSecondsList = new ArrayList();

            DateTime current = DateTime.UtcNow.AddHours(2);
            for (int i = 1; i < 99; i++)
            {
                yearList.Add(i + current.Year - 1);
                if (i <= 30) dayList.Add(i);
                if (i <= 24) hourList.Add(i - 1);
                if (i <= 60) minutesSecondsList.Add(i - 1);
            }

            ArrayList states;


            //   foreach (GridViewRow row in gvAppliances.Rows)
            //   {
            for (int i = 0; i < gvAppliances.Rows.Count; i++)
            {

                states = new ArrayList();

                states.Add(gvAppliances.Rows[i].Cells[1].Text.Equals("True") ? "ON" : "OFF");
                states.Add(gvAppliances.Rows[i].Cells[1].Text.Equals("True") ? "OFF" : "ON");


                stateDDL = new DropDownList();
                stateDDL.DataSource = states;
                stateDDL.DataBind();
                stateDDL.ID = "stateDDL";
                gvAppliances.Rows[i].Cells[1].Controls.Add(stateDDL);

                yearDDL = new DropDownList();
                yearDDL.DataSource = yearList;
                yearDDL.DataBind();
                yearDDL.ID = "yearDDL";
                gvAppliances.Rows[i].Cells[2].Controls.Add(yearDDL);


                monthDDL = new DropDownList();
                monthDDL.DataSource = monthList;
                monthDDL.ID = "monthDDL";
                monthDDL.DataBind();
                gvAppliances.Rows[i].Cells[3].Controls.Add(monthDDL);

                dayDDL = new DropDownList();
                dayDDL.DataSource = dayList;
                dayDDL.ID = "dayDDL";
                dayDDL.DataBind();
                gvAppliances.Rows[i].Cells[4].Controls.Add(dayDDL);

                hourDDL = new DropDownList();
                hourDDL.DataSource = hourList;
                hourDDL.ID = "hourDDL";
                hourDDL.DataBind();
                gvAppliances.Rows[i].Cells[5].Controls.Add(hourDDL);


                minuteDDL = new DropDownList();
                minuteDDL.ID = "minuteDDL";
                minuteDDL.DataSource = minutesSecondsList;
                minuteDDL.DataBind();

                gvAppliances.Rows[i].Cells[6].Controls.Add(minuteDDL);

                secondDDL = new DropDownList();
                secondDDL.ID = "secondDDL";
                secondDDL.DataSource = minutesSecondsList;
                secondDDL.DataBind();
                gvAppliances.Rows[i].Cells[7].Controls.Add(secondDDL);

            }
        }
        protected void gvAppliances_RowDataBound(Object sender, GridViewRowEventArgs e)
        {

            //((Button)e.Row.FindControl("btn")).Attributes.Add("onclick", string.Format("getIndex('{0}')", e.Row.RowIndex));

        }

        public class timee
        {
            public int x { set; get; }

        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string getCurrentTime()
        {



            DateTime currentTime = DateTime.UtcNow.AddHours(2);
            //   var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            //  string JSONCurrentTime =  serializer.Serialize(currentTime);
            //   string str = "{\"year\":\"2012\" }";
            string month = months[currentTime.Month - 1];
            string JSONCurrentTime = "{\"year\":"
                                      + currentTime.Year + ",\"month\":\""
                                      + month + "\",\"day\":"
                                      + currentTime.Day + ",\"hour\":"
                                      + currentTime.Hour + ",\"minute\":"
                                      + currentTime.Minute + ",\"second\":"
                                      + currentTime.Second + "}";
            return JSONCurrentTime;

        }


        [WebMethod]
        public static string addSelectedCommand(string ID, string state, string pin, string year, string month, string day, string hour, string minute, string second)
        {
            int monthIndex = months.ToList().IndexOf(month) + 1;
            DateTime date = new DateTime(Convert.ToInt32(year), monthIndex, Convert.ToInt32(day),
                Convert.ToInt32(hour), Convert.ToInt32(minute), Convert.ToInt32(second));
            bool isOn = state.Equals("ON") ? true : false;
            bool isAdded = BLL.BLL.addCommand(new Guid(ID), isOn, date,
                Convert.ToInt32(pin));
            string command = "2#" + year.ToString() + " " + monthIndex.ToString() + " " + day.ToString() + " " + hour.ToString() + " " + minute.ToString() + " " + second.ToString() + " " + pin.ToString() + " " + isOn.ToString();

            masterPage.Write(command);
            return isAdded.ToString();

        }


        protected void gvAppliances_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Guid id = new Guid(((HiddenField)gvAppliances.Rows[e.RowIndex].FindControl("hfID")).Value);
            bool isOn = ((DropDownList)gvAppliances.Rows[e.RowIndex].FindControl("stateDDL")).SelectedValue.Equals("ON") ? true : false;
            int year = Convert.ToInt32(((DropDownList)gvAppliances.Rows[e.RowIndex].FindControl("yearDDL")).SelectedValue);
            int month = getMonth(((DropDownList)gvAppliances.Rows[e.RowIndex].FindControl("monthDDL")).SelectedValue);
            int day = Convert.ToInt32(((DropDownList)gvAppliances.Rows[e.RowIndex].FindControl("dayDDL")).SelectedValue);
            int hour = Convert.ToInt32(((DropDownList)gvAppliances.Rows[e.RowIndex].FindControl("hourDDL")).SelectedValue);
            int minute = Convert.ToInt32(((DropDownList)gvAppliances.Rows[e.RowIndex].FindControl("minuteDDL")).SelectedValue);
            int second = Convert.ToInt32(((DropDownList)gvAppliances.Rows[e.RowIndex].FindControl("secondDDL")).SelectedValue);
            DateTime date = new DateTime(year, month, day, hour, minute, second);
            int pin = Convert.ToInt32(((HiddenField)gvAppliances.Rows[e.RowIndex].FindControl("hfPin")).Value);

            BLL.BLL.addCommand(id, isOn, date, pin);
            string command = "2#" + year.ToString() + " " + month.ToString() + " " + day.ToString() + " " + hour.ToString() + " " + minute.ToString() + " " + second.ToString() + " " + pin.ToString() + " " + isOn.ToString();
            // Response.Write(command+"\n");
            //Response.Write(DateTime.Now.ToString());

            masterPage.Write(command);


        }

        private int getMonth(string month)
        {
            return months.ToList().IndexOf(month) + 1;
        }


        protected void gvAppliances_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            gvAppliances.PageIndex = e.NewPageIndex;
            gvAppliances.DataBind();
            gvAppliances_SetDropDownList();
        }

        protected void gvAppliances_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName.Equals("currentTime"))
            {

                var ddl = gvAppliances.Rows[index].Controls[1];

            }
            else if (e.CommandName.Equals(""))
            {


            }
        }



        /*
        protected void gvAppliances_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName.Equals("currentTime"))
            {

                

            }else if(e.CommandName.Equals(""){


            }
        }
        */


    }
}