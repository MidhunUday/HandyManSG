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
    public partial class customer_view_approved_bookings : System.Web.UI.Page
    {
        string sqlConnectionString = ConfigurationManager.AppSettings["sqlConnectionString"];


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (Session["userID"] != null)
                {
                    Session["BookingID"] = null;

                    GetApprovedBookingsData();

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

        public void GetApprovedBookingsData()
        {

            string getBookingData = "   SELECT  Booking.[BookingID],Booking.[appoointmentDateTime]," +
                "Booking.[bookingPrice],Booking.[bookingUpdationDateTime],Booking.[customerComment],ServiceInfo.[serviceTitle]," +
                "bussinessInfo.[companyName] FROM [RepairAppDB].[customer].[CustomerBookingInfo] Booking,[RepairAppDB].[Business].[BusinessServiceInfo] ServiceInfo" +
                ",[RepairAppDB].[Business].[BusinessProfileInfo] bussinessInfo where " +
                "Booking.[customerUserID]='" + Session["userID"].ToString() + "' and Booking.[appoointmentStatus]= 'Approved' " +
                "and Booking.[serviceID]= ServiceInfo.[ServiceID] and ServiceInfo.[businessUserID] = bussinessInfo.[userID] ";


            DataTable resultData = getData(getBookingData);


            gridViewApprovedBookings.DataSource = resultData;
            gridViewApprovedBookings.DataBind();


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




        protected void lnkDelete_Click(object sender, EventArgs e)
        {

            LinkButton lnkbtn = sender as LinkButton;

            string selectedBookingID = lnkbtn.CommandArgument.ToString();

            Session["BookingID"] = selectedBookingID;

            string deletionQuery = "DELETE FROM [RepairAppDB].[customer].[CustomerBookingInfo] WHERE [BookingID]= @BookingID ";

            int checkdeletionStatus = modifyTableRecord(deletionQuery);


            if (checkdeletionStatus == 1)
            {

                Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('Booking deleted successfully.');window.location='customer-view-approved-bookings.aspx';", true);


            }
            else
            {

                Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('Booking deletion failed.');window.location='customer-view-approved-bookings.aspx';", true);


            }

        }




        public int modifyTableRecord(string Query)
        {
            int result = 0;


            try
            {


                using (SqlConnection connection = new SqlConnection(sqlConnectionString))
                {



                    using (SqlCommand command = new SqlCommand(Query, connection))
                    {


                        command.Parameters.AddWithValue("@BookingID", Session["BookingID"].ToString());



                        connection.Open();
                        result = command.ExecuteNonQuery();

                        return result;


                    }

                }



            }
            catch (Exception ex)
            {

                return 0;
            }




        }



        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridViewApprovedBookings.PageIndex = e.NewPageIndex;
            this.GetApprovedBookingsData();
        }



    }
}