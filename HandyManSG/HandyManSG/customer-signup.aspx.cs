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
    public partial class customer_signup : System.Web.UI.Page
    {
        string sqlConnectionString = ConfigurationManager.AppSettings["sqlConnectionString"];


        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

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
                    string userID = "CusID-" + guidLoginFinal;



                    //insert the details into login DB
                    string insertionQueryloginDB = "INSERT INTO [RepairAppDB].[dbo].[loginCredential] ( [userID],[userName],[userPassword],[userType],[userCreationdateTime] )" +
                    " VALUES ( '" + userID + "',@userName,@userPassword,@userType,@userCreationdateTime ) ";

                    int suceessInsertLoginDB = UpdateinsertData(insertionQueryloginDB);


                    //insert into profile DB
                    string insertionQueryProfileDB = "INSERT INTO [RepairAppDB].[customer].[CustomerProfileInfo]( [profileID],[userID],[customerName],[contactEmail],[contactPhoneNumber]," +
                        "[customerAddress],[profileCreationDateTime],[profileUpdateDateTime] )" +
                    " VALUES ( @profileID,'" + userID + "',@customerName,@contactEmail,@contactPhoneNumber,@customerAddress,@profileCreationDateTime,@profileUpdateDateTime )";



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




                        string userName = txtUserName.Text.ToString().Trim();
                        string UserPass = txtUserPassword.Text.ToString();
                        string cusName = txtCusName.Text.ToString().Trim();
                        string cusEmail = txtCusEmailID.Text.ToString().Trim();
                        string cusPhone = txtPhoneNumber.Text.ToString().Trim();
                        string cusAddress = txtAddress.Text.ToString().Trim();

                        DateTime userCreationDateTime = DateTime.Now;


                        command.Parameters.AddWithValue("@userName", userName);
                        command.Parameters.AddWithValue("@userPassword", UserPass);
                        command.Parameters.AddWithValue("@userType", "Customer");
                        command.Parameters.AddWithValue("@userCreationdateTime", userCreationDateTime);

                        //VALUES(@profileID, @userID, @customerName, @contactEmail, @contactPhoneNumber, @customerAddress, @profileCreationDateTime, @profileUpdateDateTime)";


                        //generate  new user id for customer
                        Guid guidProfile = Guid.NewGuid();
                        string guidProfileFinal = guidProfile.ToString().Substring(0, 23);

                        //append service to the id
                        string profileID = "PID-" + guidProfileFinal;


                        command.Parameters.AddWithValue("@profileID", profileID);
                        command.Parameters.AddWithValue("@customerName", cusName);
                        command.Parameters.AddWithValue("@contactEmail", cusEmail);
                        command.Parameters.AddWithValue("@contactPhoneNumber", cusPhone);
                        command.Parameters.AddWithValue("@customerAddress", cusAddress);
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
                !string.IsNullOrEmpty(txtCusName.Text.ToString()) && !string.IsNullOrWhiteSpace(txtCusName.Text.ToString()) &&
                !string.IsNullOrEmpty(txtCusEmailID.Text.ToString()) && !string.IsNullOrWhiteSpace(txtCusEmailID.Text.ToString()) &&
                !string.IsNullOrEmpty(txtPhoneNumber.Text.ToString()) && !string.IsNullOrWhiteSpace(txtPhoneNumber.Text.ToString()) &&
                !string.IsNullOrEmpty(txtAddress.Text.ToString()) && !string.IsNullOrWhiteSpace(txtAddress.Text.ToString()))
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


    }
}