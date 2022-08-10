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
    public partial class customer_edit_new_booking : System.Web.UI.Page
    {
        string sqlConnectionString = ConfigurationManager.AppSettings["sqlConnectionString"];

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {


                if (Session["userID"] != null)
                {

                    string bookingID = Session["BookingID"].ToString();
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



        public void getDataForRequestedService()
        {


            string serviceTitle = "";
            string companyName = "";
            string serviceDesc = "";
            DateTime appoointmentDateTime;
            string customerComment = "";
            string companyAddress = "";
            string companyRating = "";
            //form the query to get service details
            string getServiceData = "  select Booking.[BookingID],Booking.[customerComment],Booking.[appoointmentDateTime]," +
                "Booking.[bookingPrice],BookingServices.[serviceTitle],BookingServices.[serviceDesc],BookingServices.[servicePrice]," +
                "ProfileInfo.[companyName],ProfileInfo.[emailID],ProfileInfo.[companyAddress],ProfileInfo.[contactPhoneNumber]," +
                "ProfileInfo.[serviceCategory],ProfileInfo.[companyRating] from " +
                "[RepairAppDB].[customer].[CustomerBookingInfo] Booking,[RepairAppDB].[Business].[BusinessServiceInfo] BookingServices, " +
                "[RepairAppDB].[Business].[BusinessProfileInfo] ProfileInfo WHERE [BookingID] = '" + Session["BookingID"].ToString() + "' " +
                "AND Booking.[serviceID]= BookingServices.[ServiceID] AND BookingServices.[businessUserID]= ProfileInfo.[userID]";

            //get the service details into db dbServiceDetail
            DataTable dbBookingDetail = getData(getServiceData);


            //check if has rows for the resultant table 
            if (dbBookingDetail.Rows.Count > 0)
            {
                //get the first row of the data table
                DataRow row_dbServiceDetail = dbBookingDetail.Rows[0];

                //access the column values for the first row           
                serviceTitle = row_dbServiceDetail["serviceTitle"].ToString();
                companyName = row_dbServiceDetail["companyName"].ToString();
                serviceDesc = row_dbServiceDetail["serviceDesc"].ToString();
                customerComment = row_dbServiceDetail["customerComment"].ToString();
                appoointmentDateTime = Convert.ToDateTime(row_dbServiceDetail["appoointmentDateTime"].ToString());
                companyAddress = row_dbServiceDetail["companyAddress"].ToString();
                companyRating = row_dbServiceDetail["companyRating"].ToString();



                //lbl.Text = Session["BookingID"].ToString();
                lblServiceTitle.Text = serviceTitle;
                lblCompanyName.Text = companyName;
                txtServiceDetails.Text = serviceDesc;
                txtUserComment.Text = customerComment;
                txtCurrentAppoinmentDateTime.Text = appoointmentDateTime.ToString();
                lblAddress.Text = companyAddress;
                txtCompRating.Text = companyRating;




            }


        }



        public bool ValidateInputFields()
        {

            //check user comment has input
            if (!string.IsNullOrEmpty(txtUserComment.Text.ToString()) && !string.IsNullOrWhiteSpace(txtUserComment.Text.ToString()))
            {

                DateTime bookingDateValidate;
                DateTime bookingTimeValidate;


                //Check if the data and time in has entry
                if (DateTime.TryParse(txtNewBookingDate.Text, out bookingDateValidate) && DateTime.TryParse(txtNewBookingTime.Text, out bookingTimeValidate))
                {


                    //check if the booking date time is greater than current date time

                    DateTime newbookingDate = DateTime.Parse(txtNewBookingDate.Text.ToString());
                    DateTime newbookingTime = DateTime.Parse(txtNewBookingTime.Text.ToString());
                    DateTime newbookingDateTime = newbookingDate.Date.Add(newbookingTime.TimeOfDay);

                    DateTime oldBookingDateTime = DateTime.Parse(txtCurrentAppoinmentDateTime.Text.ToString());

                    DateTime currentDateTime = DateTime.Now;
                    DateTime dateTime3hrsLater = currentDateTime.AddHours(3);


                    if (newbookingDateTime >= dateTime3hrsLater)
                    {
                        //lblErrorMsg.Text = "";

                        return true;
                    }
                    else
                    {
                        //lblErrorMsg.Text = "Please choose a appointment datetime after:" + oldBookingDateTime.ToString();
                        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Please choose a appointment datetime after:" + dateTime3hrsLater.ToString() + "');", true);


                        return false;

                    }//booking time is less than 3 hours of current time



                }
                else
                {

                    //lblErrorMsg.Text = "Please enter in the Appoitment date time for bookings";
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Please enter in the Appoitment date time for bookings" + "');", true);

                    return false;
                }//for missing date time inputs


            }
            else
            {

                //lblErrorMsg.Text = "Missing Required Inputs";
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Missing Required Inputs" + "');", true);

                return false;
            }//for missing user comment


        }



        public int updateNewBooking(string Query)
        {
            int result = 0;

            try
            {

                using (SqlConnection connection = new SqlConnection(sqlConnectionString))
                {


                    using (SqlCommand command = new SqlCommand(Query, connection))
                    {



                        DateTime bookingDate = DateTime.Parse(txtNewBookingDate.Text.ToString());

                        DateTime bookingTime = DateTime.Parse(txtNewBookingTime.Text.ToString());

                        DateTime bookingDateTime = bookingDate.Date.Add(bookingTime.TimeOfDay);

                        DateTime bookingUpdationDateTime = DateTime.Now;

                        command.Parameters.AddWithValue("@appoointmentDateTime", bookingDateTime);
                        command.Parameters.AddWithValue("@customerComment", txtUserComment.Text.ToString());
                        command.Parameters.AddWithValue("@BookingID", Session["BookingID"].ToString());
                        command.Parameters.AddWithValue("@bookingUpdationDateTime", bookingUpdationDateTime);

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





        protected void btnEditBooking_Click(object sender, EventArgs e)
        {

            bool IsrequiredfieldComplete = ValidateInputFields();

            if (IsrequiredfieldComplete)
            {


                string queryUpdateService = "update [RepairAppDB].[customer].[CustomerBookingInfo] " +
                "set [appoointmentDateTime]=@appoointmentDateTime, [customerComment]=@customerComment,[bookingUpdationDateTime]=@bookingUpdationDateTime where [BookingID]=@BookingID ";


                int checkupdateStatus = updateNewBooking(queryUpdateService);

                if (checkupdateStatus == 1)
                {

                    Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('Booking Updated successfully.');window.location='customer-view-new-bookings.aspx';", true);

                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('Booking Failed to update.');window.location='customer-view-new-bookings.aspx';", true);

                }

            }

        }




        protected void btnCancelChnages_Click(object sender, EventArgs e)
        {

            Response.Redirect("customer-home-page.aspx");

        }




    }
}