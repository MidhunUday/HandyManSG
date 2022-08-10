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
    public partial class customer_booking_page : System.Web.UI.Page
    {
        string sqlConnectionString = ConfigurationManager.AppSettings["sqlConnectionString"];


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                if (Session["userID"] != null)
                {



                    //Show result based on selected service ID
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



        protected void btnMakeBooking_Click(object sender, EventArgs e)
        {
            //validate all required fields are filled
            bool IsrequiredfieldComplete = ValidateInputFields();


            if (IsrequiredfieldComplete)
            {

                string insertionQuery = "   INSERT INTO [RepairAppDB].[customer].[CustomerBookingInfo] ([BookingID],[customerUserID],[serviceID]," +
                "[customerComment],[appoointmentDateTime],[appoointmentStatus]) " +
                "VALUES(@BookingID, @customerUserID, @serviceID, @customerComment, @appoointmentDateTime, @appoointmentStatus)";


                int suceessInsert = UpdateinsertData(insertionQuery);

                if (suceessInsert == 1)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('New Boooking created successfully.');window.location='customer-view-new-bookings.aspx';", true);

                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('Failed to create booking.');window.location='customer-view-new-bookings.aspx';", true);

                }

            }



        }



        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("customer-home-page.aspx");
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

            string ServiceID = "";
            string businessUserID = "";
            string serviceTitle = "";
            string serviceDesc = "";
            string servicePrice = "";

            string companyName = "";
            string emailID = "";
            string companyAddress = "";
            string contactPhoneNumber = "";
            string companyRating = "";

            //form the query to get service details
            string getServiceData = "  SELECT serviceData.[ServiceID], serviceData.[businessUserID],serviceData.[serviceTitle],serviceData.[serviceDesc],serviceData.[servicePrice]," +
                "busProfileData.[companyName],busProfileData.[emailID],busProfileData.[companyAddress],busProfileData.[contactPhoneNumber],busProfileData.[companyRating] FROM " +
                "[RepairAppDB].[Business].[BusinessServiceInfo] serviceData, [RepairAppDB].[Business].[BusinessProfileInfo] busProfileData " +
                "where serviceData.[ServiceID]='" + Session["ServiceID"].ToString() + "' and serviceData.[businessUserID]= busProfileData.[userID] ";

            //get the service details into db dbServiceDetail
            DataTable dbServiceDetail = getData(getServiceData);


            //check if has rows for the resultant table 
            if (dbServiceDetail.Rows.Count > 0)
            {
                //get the first row of the data table
                DataRow row_dbServiceDetail = dbServiceDetail.Rows[0];

                //access the column values for the first row


                ServiceID = row_dbServiceDetail["ServiceID"].ToString();
                businessUserID = row_dbServiceDetail["businessUserID"].ToString();
                serviceTitle = row_dbServiceDetail["serviceTitle"].ToString();
                serviceDesc = row_dbServiceDetail["serviceDesc"].ToString();
                servicePrice = row_dbServiceDetail["servicePrice"].ToString();

                companyName = row_dbServiceDetail["companyName"].ToString();
                emailID = row_dbServiceDetail["emailID"].ToString();
                companyAddress = row_dbServiceDetail["companyAddress"].ToString();
                contactPhoneNumber = row_dbServiceDetail["contactPhoneNumber"].ToString();
                companyRating = row_dbServiceDetail["companyRating"].ToString();




                lblServiceTitle.Text = serviceTitle;
                lblCompAddress.Text = companyAddress;
                lblCompName.Text = companyName;
                //txtPhone.Text = contactPhoneNumber;
                txtServiceDetails.Text = serviceDesc;



                Console.WriteLine();

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
                if (DateTime.TryParse(txtBookingDate.Text, out bookingDateValidate) && DateTime.TryParse(txtBookingTime.Text, out bookingTimeValidate))
                {


                    //check if the booking date time is greater than current date time

                    DateTime bookingDate = DateTime.Parse(txtBookingDate.Text.ToString());
                    DateTime bookingTime = DateTime.Parse(txtBookingTime.Text.ToString());
                    DateTime bookingDateTime = bookingDate.Date.Add(bookingTime.TimeOfDay);

                    DateTime currentDateTime = DateTime.Now;
                    DateTime dateTime3hrsLater = currentDateTime.AddHours(3);

                    if (bookingDateTime >= dateTime3hrsLater)
                    {
                        //lblErrorMsg.Text = "";
                        return true;
                    }
                    else
                    {
                        //lblErrorMsg.Text = "Please choose a appointment datetime after:" + dateTime3hrsLater.ToString();

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




        public int UpdateinsertData(string Query)
        {
            int result = 0;


            try
            {


                using (SqlConnection connection = new SqlConnection(sqlConnectionString))
                {



                    using (SqlCommand command = new SqlCommand(Query, connection))
                    {

                        //generate  new id for the booking
                        Guid guid = Guid.NewGuid();
                        string guidFinal = guid.ToString().Substring(0, 23);

                        //append service to the id
                        string bookingID = "Booking-" + guidFinal.ToString();


                        //get from session variable after user logins
                        string ServiceID = Session["ServiceID"].ToString();


                        string customerUserID = Session["userID"].ToString();

                        //Get the booking date
                        DateTime bookingDate = DateTime.Parse(txtBookingDate.Text.ToString());

                        DateTime bookingTime = DateTime.Parse(txtBookingTime.Text.ToString());

                        DateTime bookingDateTime = bookingDate.Date.Add(bookingTime.TimeOfDay);

                        //@BookingID, @customerUserID, @serviceID, @customerComment, @appoointmentDateTime, @appoointmentStatus



                        command.Parameters.AddWithValue("@BookingID", bookingID);
                        command.Parameters.AddWithValue("@customerUserID", Session["userID"].ToString());
                        command.Parameters.AddWithValue("@serviceID", Session["ServiceID"].ToString());
                        command.Parameters.AddWithValue("@customerComment", txtUserComment.Text.ToString());
                        command.Parameters.AddWithValue("@appoointmentDateTime", bookingDateTime);
                        command.Parameters.AddWithValue("@appoointmentStatus", "Pending-Approval");

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







    }
}