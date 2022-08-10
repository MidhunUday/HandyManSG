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
    public partial class customer_edit_profile : System.Web.UI.Page
    {
        string sqlConnectionString = ConfigurationManager.AppSettings["sqlConnectionString"];


        protected void Page_Load(object sender, EventArgs e)
        {

            //if the page is loaded no beacuse of postback
            if (!IsPostBack)
            {

                if (Session["userID"] != null)
                {
                    getDataForRequestedProfile();
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



        public void getDataForRequestedProfile()
        {


            string profileID = "";
            string customerName = "";
            string emailID = "";
            string contactPhoneNumber = "";
            string companyAddress = "";




            string queryGetProfileData = "select loginDB.[userName], loginDB.[userPassword],cusProfile.[profileID],cusProfile.[customerName]," +
                 "cusProfile.[contactEmail],cusProfile.[contactPhoneNumber],cusProfile.[customerAddress] " +
                 "from [RepairAppDB].[dbo].[loginCredential] loginDB,[RepairAppDB].[customer].[CustomerProfileInfo] cusProfile " +
                 "WHERE loginDB.[userID]='" + Session["userID"].ToString() + "' AND loginDB.[userID] = cusProfile.[userID]";



            DataTable dbProfileDetail = getData(queryGetProfileData);


            if (dbProfileDetail.Rows.Count > 0)
            {

                DataRow row_dbProfileDetail = dbProfileDetail.Rows[0];

                //access the column values for the first row
                profileID = row_dbProfileDetail["profileID"].ToString();
                customerName = row_dbProfileDetail["customerName"].ToString();
                emailID = row_dbProfileDetail["contactEmail"].ToString();
                contactPhoneNumber = row_dbProfileDetail["contactPhoneNumber"].ToString();
                companyAddress = row_dbProfileDetail["customerAddress"].ToString();

                //bind to the text box
                txtCusName.Text = customerName;
                txtEmail.Text = emailID;
                txtPhoneNo.Text = contactPhoneNumber;
                txtCusAddress.Text = companyAddress;

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









        public bool ValidateInputFields()
        {


            if (!string.IsNullOrEmpty(txtCusName.Text) && !string.IsNullOrWhiteSpace(txtCusName.Text) &&
            !string.IsNullOrEmpty(txtEmail.Text) && !string.IsNullOrWhiteSpace(txtEmail.Text) &&
            !string.IsNullOrEmpty(txtPhoneNo.Text) && !string.IsNullOrWhiteSpace(txtPhoneNo.Text) &&
            !string.IsNullOrEmpty(txtCusAddress.Text) && !string.IsNullOrWhiteSpace(txtCusAddress.Text))
            {


                return true;

            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Missing Required Inputs" + "');", true);
                return false;
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


                        DateTime profileUpdationDateTime = DateTime.Now;

                        command.Parameters.AddWithValue("@customerName", txtCusName.Text.ToString().Trim());
                        command.Parameters.AddWithValue("@contactEmail", txtEmail.Text.ToString().Trim());
                        command.Parameters.AddWithValue("@contactPhoneNumber", txtPhoneNo.Text.ToString().Trim());
                        command.Parameters.AddWithValue("@customerAddress", txtCusAddress.Text.ToString().Trim());
                        command.Parameters.AddWithValue("@userID", Session["userID"].ToString());
                        command.Parameters.AddWithValue("@profileUpdateDateTime", profileUpdationDateTime);

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


        protected void btnSave_Click(object sender, EventArgs e)
        {


            bool IsRequiredFieldsFilled = ValidateInputFields();


            if (IsRequiredFieldsFilled)
            {





                //update the booking price and status based on the booking ID
                string queryUpdateProfile = "  UPDATE  [RepairAppDB].[customer].[CustomerProfileInfo] SET [customerName]=@customerName," +
                    "[contactEmail]=@contactEmail,[contactPhoneNumber]=@contactPhoneNumber,[customerAddress]=@customerAddress,[profileUpdateDateTime]=@profileUpdateDateTime WHERE [userID]=@userID ";


                int checkupdateStatus = modifyTableRecord(queryUpdateProfile);

                if (checkupdateStatus == 1)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('Profile update successfully.');window.location='customer-edit-profile.aspx';", true);

                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('Profile failed to update.');window.location='customer-edit-profile.aspx';", true);

                }

            }



        }




    }
}