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
    public partial class customer_view_completed_bookings : System.Web.UI.Page
    {
        string sqlConnectionString = ConfigurationManager.AppSettings["sqlConnectionString"];



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (Session["userID"] != null)
                {
                    GetCompletedBookingsData();
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


        public void GetCompletedBookingsData()
        {

            string getBookingData = "   SELECT  Booking.[BookingID],Booking.[appoointmentDateTime],Booking.[bookingPrice],Booking.[bookingCreationDateTime],ServiceInfo.[serviceTitle],ServiceInfo.[serviceDesc]," +
                "bussinessInfo.[companyName] FROM [RepairAppDB].[customer].[CustomerBookingInfo] Booking,[RepairAppDB].[Business].[BusinessServiceInfo] ServiceInfo" +
                ",[RepairAppDB].[Business].[BusinessProfileInfo] bussinessInfo where " +
                "Booking.[customerUserID]='" + Session["userID"].ToString() + "' and Booking.[appoointmentStatus]= 'Completed' " +
                "and Booking.[serviceID]= ServiceInfo.[ServiceID] and ServiceInfo.[businessUserID] = bussinessInfo.[userID] ";


            DataTable resultData = getData(getBookingData);


            gridViewCompletedBookings.DataSource = resultData;
            gridViewCompletedBookings.DataBind();


        }



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



        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridViewCompletedBookings.PageIndex = e.NewPageIndex;
            this.GetCompletedBookingsData();
        }





    }
}