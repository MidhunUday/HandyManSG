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
    public partial class business_edit_upcoming_booking : System.Web.UI.Page
    {
        string sqlConnectionString = ConfigurationManager.AppSettings["sqlConnectionString"];


        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {


                if (Session["userID"] != null)
                {

                    string BookingID = Session["BookingID"].ToString();
                    txtBookingID.Text = BookingID;
                    ddlChangeBookingStatus.SelectedValue = "Completed";
                    getDataForRequestedService();
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


        public void getDataForRequestedService()
        {

            string bookingCreationDate = "";
            string userComment = "";
            string appoitmentDate = "";
            string customerName = "";
            string customerAddress = "";
            string bookingPrice = "";


            //form the query to get service details
            //sql query to get for new bookings
            string queryGetNewBookingInfo = "  SELECT Booking.[BookingID] ,Booking.[serviceID] ,Booking.[customerComment] ,Booking.[appoointmentDateTime],Booking.[bookingPrice] ," +
                "Booking.[bookingUpdationDateTime] as Booking_Creation_Time , cusProfile.[customerName] ,cusProfile.[contactEmail] ," +
                "cusProfile.[contactPhoneNumber] ,cusProfile.[customerAddress] FROM[RepairAppDB].[customer].[CustomerBookingInfo] Booking," +
                " [RepairAppDB].[customer].[CustomerProfileInfo] cusProfile where [serviceID] in (Select ServiceID from [RepairAppDB].[Business].[BusinessServiceInfo] " +
                "where Booking.[BookingID]= '" + Session["BookingID"].ToString() + "' )";

            //get the service details into db dbServiceDetail
            DataTable dbServiceDetail = getData(queryGetNewBookingInfo);


            //check if has rows for the resultant table 
            if (dbServiceDetail.Rows.Count > 0)
            {
                //get the first row of the data table
                DataRow row_dbServiceDetail = dbServiceDetail.Rows[0];

                //access the column values for the first row

                bookingCreationDate = row_dbServiceDetail["Booking_Creation_Time"].ToString();
                userComment = row_dbServiceDetail["customerComment"].ToString();
                appoitmentDate = row_dbServiceDetail["appoointmentDateTime"].ToString();
                customerName = row_dbServiceDetail["customerName"].ToString();
                customerAddress = row_dbServiceDetail["customerAddress"].ToString();
                bookingPrice = row_dbServiceDetail["bookingPrice"].ToString();

                //bind to the text box
                txtBookingID.Text = Session["BookingID"].ToString();
                txtBookingCreationDate.Text = bookingCreationDate;
                txtUserComment.Text = userComment;
                txtAppointmentDate.Text = appoitmentDate;
                txtCusName.Text = customerName;
                txtCusAddress.Text = customerAddress;
                txtBookingFinalPrice.Text = bookingPrice;

            }


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


        protected void ddlChangeBookingStatus_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        protected void btnUpdateBooking_Click(object sender, EventArgs e)
        {

            //update the booking price and status based on the booking ID
            string queryUpdateBooking = "  UPDATE  [RepairAppDB].[customer].[CustomerBookingInfo] SET " +
                "[appoointmentStatus]=@appoointmentStatus,[bookingUpdationDateTime]=@bookingUpdationDateTime WHERE [BookingID]=@BookingID ";


            int checkupdateStatus = modifyTableRecord(queryUpdateBooking);

            if (checkupdateStatus == 1)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('Booking updated successfully.');window.location='business-view-completed-bookings.aspx';", true);

            }
            else
            {

                Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('Booking Fail to Update.');window.location='business-view-completed-bookings.aspx';", true);


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
                        string BookingID = Session["BookingID"].ToString();
                        DateTime BookingUpdationDateTime = DateTime.Now;


                        command.Parameters.AddWithValue("@BookingID", BookingID);
                        command.Parameters.AddWithValue("@appoointmentStatus", ddlChangeBookingStatus.SelectedValue.ToString());
                        command.Parameters.AddWithValue("@bookingUpdationDateTime", BookingUpdationDateTime);


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




        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("business-home.aspx");
        }




    }
}