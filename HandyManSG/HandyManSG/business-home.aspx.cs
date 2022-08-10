using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HandyManSG
{
    public partial class business_home : System.Web.UI.Page
    {
        string sqlConnectionString = ConfigurationManager.AppSettings["sqlConnectionString"];


        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                if (Session["userID"] != null)
                {
                    GetNewBookingsCount();
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


        public void GetNewBookingsCount()
        {

            string getNewBookingsCount = "select count ([BookingID]) from [RepairAppDB].[customer].[CustomerBookingInfo] " +
                "where [serviceID] IN (SELECT [ServiceID] FROM [RepairAppDB].[Business].[BusinessServiceInfo] where [businessUserID] = @userID )" +
                " and [appoointmentStatus]='Pending-Approval' ";

            int totalNewBookings = GetNewBookingsCount(getNewBookingsCount);


            txtNewBookingCount.Text = totalNewBookings.ToString();

        }





        public int GetNewBookingsCount(string Query)
        {
            int newBookingCount = 0;


            try
            {


                using (SqlConnection connection = new SqlConnection(sqlConnectionString))
                {



                    using (SqlCommand command = new SqlCommand(Query, connection))
                    {



                        command.Parameters.AddWithValue("@userID", Session["userID"].ToString());


                        connection.Open();
                        newBookingCount = (int)command.ExecuteScalar();


                        return newBookingCount;


                    }

                }



            }
            catch (Exception ex)
            {

                return newBookingCount;
            }




        }

        protected void CheckNowButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("business-view-new-bookings.aspx");
        }

        protected void btnaddNewService_Click(object sender, EventArgs e)
        {
            Response.Redirect("business-add-new-service.aspx");
        }
    }
}