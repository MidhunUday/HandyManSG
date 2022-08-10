using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HandyManSG
{
    public partial class customer_home_page : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {

                if (Session["userID"] != null)
                {
                    Session["selectedServiceCategory"] = "Cleaning";
                }
                else
                {
                    Response.Redirect("UserLogin.aspx");
                }


            }

            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Current.Response.Cache.SetNoServerCaching();
            HttpContext.Current.Response.Cache.SetNoStore();

        }


        protected void lnkPlumbing_Click(object sender, EventArgs e)
        {

            LinkButton lnkbtn = sender as LinkButton;

            string selectedServiceCategory = lnkbtn.CommandArgument.ToString();

            Session["selectedServiceCategory"] = selectedServiceCategory;

            Response.Redirect("customer-view-services.aspx");

        }


        protected void lnkCleaning_Click(object sender, EventArgs e)
        {

            LinkButton lnkbtn = sender as LinkButton;

            string selectedServiceCategory = lnkbtn.CommandArgument.ToString();

            Session["selectedServiceCategory"] = selectedServiceCategory;

            Response.Redirect("customer-view-services.aspx");

        }


        protected void lnkElectrical_Click(object sender, EventArgs e)
        {

            LinkButton lnkbtn = sender as LinkButton;

            string selectedServiceCategory = lnkbtn.CommandArgument.ToString();

            Session["selectedServiceCategory"] = selectedServiceCategory;

            Response.Redirect("customer-view-services.aspx");

        }


        protected void lnkAircon_Click(object sender, EventArgs e)
        {

            LinkButton lnkbtn = sender as LinkButton;

            string selectedServiceCategory = lnkbtn.CommandArgument.ToString();

            Session["selectedServiceCategory"] = selectedServiceCategory;

            Response.Redirect("customer-view-services.aspx");

        }




    }
}