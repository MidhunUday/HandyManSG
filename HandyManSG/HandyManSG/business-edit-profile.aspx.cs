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
    public partial class business_edit_profile : System.Web.UI.Page
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

            string userName = "";
            string userPassword = "";
            string profileID = "";
            string companyName = "";
            string emailID = "";
            string companyAddress = "";
            string contactPhoneNumber = "";
            string serviceCategory = "";



            string queryGetProfileData = "select loginDB.[userName], loginDB.[userPassword],busProfile.[profileID],busProfile.[companyName]," +
                "busProfile.[emailID],busProfile.[companyAddress],busProfile.[contactPhoneNumber],busProfile.[serviceCategory] " +
                "from [RepairAppDB].[dbo].[loginCredential] loginDB,[RepairAppDB].[Business].[BusinessProfileInfo] busProfile " +
                "WHERE loginDB.[userID]='" + Session["userID"].ToString() + "' AND loginDB.[userID] = busProfile.[userID]";



            DataTable dbProfileDetail = getData(queryGetProfileData);


            if (dbProfileDetail.Rows.Count > 0)
            {

                DataRow row_dbProfileDetail = dbProfileDetail.Rows[0];

                //access the column values for the first row

                userName = row_dbProfileDetail["userName"].ToString();
                userPassword = row_dbProfileDetail["userPassword"].ToString();
                profileID = row_dbProfileDetail["profileID"].ToString();
                companyName = row_dbProfileDetail["companyName"].ToString();
                emailID = row_dbProfileDetail["emailID"].ToString();
                companyAddress = row_dbProfileDetail["companyAddress"].ToString();
                contactPhoneNumber = row_dbProfileDetail["contactPhoneNumber"].ToString();
                serviceCategory = row_dbProfileDetail["serviceCategory"].ToString();

                //bind to the text box
                txtCompName.Text = companyName;
                txtCompServiceCat.Text = serviceCategory;
                txtContactNumber.Text = contactPhoneNumber;
                txtCompEmail.Text = emailID;
                txtCompAddress.Text = companyAddress;


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


            if (!string.IsNullOrEmpty(txtCompName.Text) && !string.IsNullOrWhiteSpace(txtCompName.Text) &&
            !string.IsNullOrEmpty(txtContactNumber.Text) && !string.IsNullOrWhiteSpace(txtContactNumber.Text) &&
            !string.IsNullOrEmpty(txtCompEmail.Text) && !string.IsNullOrWhiteSpace(txtCompEmail.Text) &&
            !string.IsNullOrEmpty(txtCompAddress.Text) && !string.IsNullOrWhiteSpace(txtCompAddress.Text))
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

                        command.Parameters.AddWithValue("@companyName", txtCompName.Text.ToString().Trim());
                        command.Parameters.AddWithValue("@emailID", txtCompEmail.Text.ToString().Trim());
                        command.Parameters.AddWithValue("@contactPhoneNumber", txtContactNumber.Text.ToString().Trim());
                        command.Parameters.AddWithValue("@companyAddress", txtCompAddress.Text.ToString().Trim());
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
                string queryUpdateProfile = "  UPDATE  [RepairAppDB].[Business].[BusinessProfileInfo] SET [companyName]=@companyName," +
                    "[emailID]=@emailID,[companyAddress]=@companyAddress,[contactPhoneNumber]=@contactPhoneNumber,[profileUpdateDateTime]=@profileUpdateDateTime WHERE [userID]=@userID ";


                int checkupdateStatus = modifyTableRecord(queryUpdateProfile);

                if (checkupdateStatus == 1)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('Profile update successfully.');window.location='business-edit-profile.aspx';", true);

                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('Profile failed to update.');window.location='business-edit-profile.aspx';", true);

                }

            }



        }




    }
}