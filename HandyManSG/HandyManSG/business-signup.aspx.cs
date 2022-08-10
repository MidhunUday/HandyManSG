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
    public partial class business_signup : System.Web.UI.Page
    {
        string sqlConnectionString = ConfigurationManager.AppSettings["sqlConnectionString"];


        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                ddlCompanyCategory.SelectedValue = "Cleaning";
            }


        }



        protected void btnRegister_Click(object sender, EventArgs e)
        {

            bool IsRequiredFieldsFilled = ValidateInputFields();


            if (IsRequiredFieldsFilled)
            {



                //check if the username already taken
                string chkUserExistsQuery = "Select count(*) from [RepairAppDB].[dbo].[loginCredential] where [userName]= @userName";

                bool IsUserNameTaken = checkUserExists(chkUserExistsQuery);

                if (IsUserNameTaken == false)
                {

                    //generate  new user id for customer
                    Guid guidLogin = Guid.NewGuid();
                    string guidLoginFinal = guidLogin.ToString().Substring(0, 23);

                    //append service to the id
                    string userID = "BusID-" + guidLoginFinal;



                    //insert the details into login DB
                    string insertionQueryloginDB = "INSERT INTO [RepairAppDB].[dbo].[loginCredential] ( [userID],[userName],[userPassword],[userType],[userCreationdateTime] )" +
                    " VALUES ( '" + userID + "',@userName,@userPassword,@userType,@userCreationdateTime ) ";

                    int suceessInsertLoginDB = UpdateinsertData(insertionQueryloginDB);


                    //insert into profile DB
                    string insertionQueryProfileDB = "INSERT INTO [RepairAppDB].[Business].[BusinessProfileInfo] ( [profileID],[userID],[companyName],[emailID]," +
                        "[companyAddress],[contactPhoneNumber],[serviceCategory],[companyRating],[profileCreationDateTime],[profileUpdateDateTime] )" +
                    " VALUES ( @profileID,'" + userID + "',@companyName,@emailID,@companyAddress,@contactPhoneNumber," +
                    "@serviceCategory,@companyRating,@profileCreationDateTime,@profileUpdateDateTime )";



                    int suceessInsertProfileDB = UpdateinsertData(insertionQueryProfileDB);


                    if (suceessInsertProfileDB == 1)
                    {


                        Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('Account has been created successfully.');window.location='UserLogin.aspx';", true);

                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('Account creation Failed.');window.location='UserLogin.aspx';", true);


                    }


                }
                else
                {

                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "UserName already taken" + "');", true);

                }







            }


        }



        public bool checkUserExists(string Query)
        {


            try
            {


                using (SqlConnection connection = new SqlConnection(sqlConnectionString))
                {



                    using (SqlCommand command = new SqlCommand(Query, connection))
                    {



                        command.Parameters.AddWithValue("@userName", txtUserName.Text.ToString().Trim().ToUpper());


                        connection.Open();

                        int result = (int)command.ExecuteScalar();


                        if (result == 0)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }




                    }

                }



            }
            catch (Exception ex)
            {

                return false;
            }



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




                        string userName = txtUserName.Text.ToString().Trim().ToUpper();
                        string UserPass = txtUserPassword.Text.ToString();
                        string compName = txtCompName.Text.ToString().Trim();
                        string compEmail = txtCompEmail.Text.ToString().Trim();
                        string compPhone = txtPhoneNo.Text.ToString().Trim();
                        string compAddress = txtCompAddress.Text.ToString().Trim();
                        string compServiceCategory = ddlCompanyCategory.SelectedValue.ToString();


                        DateTime userCreationDateTime = DateTime.Now;


                        command.Parameters.AddWithValue("@userName", userName);
                        command.Parameters.AddWithValue("@userPassword", UserPass);
                        command.Parameters.AddWithValue("@userType", "Business");
                        command.Parameters.AddWithValue("@userCreationdateTime", userCreationDateTime);





                        //generate  new user id for customer
                        Guid guidProfile = Guid.NewGuid();
                        string guidProfileFinal = guidProfile.ToString().Substring(0, 23);

                        //append service to the id
                        string profileID = "PID-" + guidProfileFinal;





                        command.Parameters.AddWithValue("@profileID", profileID);
                        command.Parameters.AddWithValue("@companyName", compName);
                        command.Parameters.AddWithValue("@emailID", compEmail);
                        command.Parameters.AddWithValue("@companyAddress", compAddress);
                        command.Parameters.AddWithValue("@contactPhoneNumber", compPhone);
                        command.Parameters.AddWithValue("@serviceCategory", compServiceCategory);
                        command.Parameters.AddWithValue("@companyRating", 0.0);
                        command.Parameters.AddWithValue("@profileCreationDateTime", userCreationDateTime);
                        command.Parameters.AddWithValue("@profileUpdateDateTime", userCreationDateTime);

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






        public bool ValidateInputFields()
        {

            //check user comment has input
            if (!string.IsNullOrEmpty(txtUserName.Text.ToString()) && !string.IsNullOrWhiteSpace(txtUserName.Text.ToString()) &&
                !string.IsNullOrEmpty(txtUserPassword.Text.ToString()) && !string.IsNullOrWhiteSpace(txtUserPassword.Text.ToString()) &&
                !string.IsNullOrEmpty(txtCompName.Text.ToString()) && !string.IsNullOrWhiteSpace(txtCompName.Text.ToString()) &&
                !string.IsNullOrEmpty(txtCompEmail.Text.ToString()) && !string.IsNullOrWhiteSpace(txtCompEmail.Text.ToString()) &&
                !string.IsNullOrEmpty(txtPhoneNo.Text.ToString()) && !string.IsNullOrWhiteSpace(txtPhoneNo.Text.ToString()) &&
                !string.IsNullOrEmpty(txtCompAddress.Text.ToString()) && !string.IsNullOrWhiteSpace(txtCompAddress.Text.ToString()))
            {


                return true;

            }
            else
            {

                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Missing Required Inputs" + "');", true);

                return false;
            }


        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserLogin.aspx");

        }

        protected void ddlCompanyCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}