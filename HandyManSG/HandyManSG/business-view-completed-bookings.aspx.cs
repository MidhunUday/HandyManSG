using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HandyManSG
{
    public partial class business_view_completed_bookings : System.Web.UI.Page
    {
        string sqlConnectionString = ConfigurationManager.AppSettings["sqlConnectionString"];

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                if (Session["userID"] != null)
                {

                    getCompletedBookings();

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


        //get data from sql server
        public DataTable getData(string Query)
        {
            //declare a new data table
            DataTable resultData = new DataTable();

            //init the sql connection
            SqlConnection sqlConnection = new SqlConnection(sqlConnectionString);

            try
            {

                using (sqlConnection)
                {
                    //opening the sql connection
                    sqlConnection.Open();

                    //creating the object
                    SqlDataAdapter sqlDa = new SqlDataAdapter(Query, sqlConnection);

                    //fill the data table with the result from the sql server
                    sqlDa.Fill(resultData);

                    //close the connection
                    sqlConnection.Close();

                    //return the data table
                    return resultData;

                }



            }
            catch (Exception ex)
            {

                //close the connection
                sqlConnection.Close();

                return resultData;

            }


        }

        protected void getCompletedBookings()
        {
            string queryGetPendingBookings = "  SELECT Booking.[BookingID] ,Booking.[serviceID] ,Booking.[customerComment] ,Booking.[appoointmentDateTime] ," +
                "Booking.[bookingUpdationDateTime] as Booking_Creation_Time,Booking.[bookingPrice] , cusProfile.[customerName] ,cusProfile.[contactEmail] ," +
                "cusProfile.[contactPhoneNumber] ,cusProfile.[customerAddress] FROM[RepairAppDB].[customer].[CustomerBookingInfo] Booking," +
                " [RepairAppDB].[customer].[CustomerProfileInfo] cusProfile where [serviceID] in (Select ServiceID from[RepairAppDB].[Business].[BusinessServiceInfo] " +
                "where [businessUserID]= '" + Session["userID"].ToString() + "' ) and Booking.[customerUserID] = cusProfile.[userID] and [appoointmentStatus]= 'Completed'";

            //get data function
            DataTable resultData = getData(queryGetPendingBookings);


            gridViewCompletedBookings.DataSource = resultData;
            gridViewCompletedBookings.DataBind();

        }


        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridViewCompletedBookings.PageIndex = e.NewPageIndex;
            this.getCompletedBookings();
        }





    }
}