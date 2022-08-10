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
    public partial class business_view_new_bookings : System.Web.UI.Page
    {
        string sqlConnectionString = ConfigurationManager.AppSettings["sqlConnectionString"];


        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {


                if (Session["userID"] != null)
                {

                    Session["BookingID"] = null;

                    getNewBookings();
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



        protected void getNewBookings()
        {


            //sql query to get for new bookings
            string queryGetNewBookings = "  SELECT Booking.[BookingID] ,Booking.[serviceID] ,Booking.[customerComment] ,Booking.[appoointmentDateTime] ," +
                "Booking.[bookingUpdationDateTime] as Booking_Creation_Time , cusProfile.[customerName] ,cusProfile.[contactEmail] ," +
                "cusProfile.[contactPhoneNumber] ,cusProfile.[customerAddress] FROM[RepairAppDB].[customer].[CustomerBookingInfo] Booking," +
                " [RepairAppDB].[customer].[CustomerProfileInfo] cusProfile where [serviceID] in (Select ServiceID from [RepairAppDB].[Business].[BusinessServiceInfo] " +
                "where [businessUserID]= '" + Session["userID"].ToString() + "' ) and Booking.[customerUserID] = cusProfile.[userID] and [appoointmentStatus]= 'Pending-Approval'";

            //get data function
            DataTable resultData = getData(queryGetNewBookings);


            gridViewNewBookings.DataSource = resultData;
            gridViewNewBookings.DataBind();

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





        protected void lnkEDIT_Click(object sender, EventArgs e)
        {

            LinkButton lnkbtn = sender as LinkButton;

            string selectedBookingID = lnkbtn.CommandArgument.ToString();

            Session["BookingID"] = selectedBookingID;

            Response.Redirect("business-edit-new-booking.aspx");

        }


        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridViewNewBookings.PageIndex = e.NewPageIndex;
            this.getNewBookings();
        }





    }
}