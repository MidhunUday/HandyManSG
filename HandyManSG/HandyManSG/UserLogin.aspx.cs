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
    public partial class UserLogin : System.Web.UI.Page
    {
        string sqlConnectionString = ConfigurationManager.AppSettings["sqlConnectionString"];


        protected void Page_Load(object sender, EventArgs e)
        {

            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Current.Response.Cache.SetNoServerCaching();
            HttpContext.Current.Response.Cache.SetNoStore();


            Session["userID"] = null;

            Session.RemoveAll();


        }


        protected void btnLogin_Click(object sender, EventArgs e)
        {

            bool IsRequiredFieldsFilled = ValidateInputFields();
            if (IsRequiredFieldsFilled)
            {

                string getServieDetails = "SELECT * FROM " +
                "[RepairAppDB].[dbo].[loginCredential]  where [userName]= '" + txtUserName.Text.ToString().Trim() + "' AND [userPassword]= '" + txtUserPassword.Text.ToString().Trim() + "'";

                //get the service details into db dbServiceDetail
                DataTable userInfo = getData(getServieDetails);

                if (userInfo.Rows.Count > 0)
                {
                    DataRow row_userInfo = userInfo.Rows[0];

                    //access the column values for the first row           
                    string userID = row_userInfo["userID"].ToString();
                    string userType = row_userInfo["userType"].ToString();

                    Session["userID"] = userID;

                    if (userType == "Business")
                    {

                        Response.Redirect("business-home.aspx");

                    }
                    else
                    {
                        Response.Redirect("customer-home-page.aspx");

                    }





                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Incorrect username/password." + "');", true);

                }

            }


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




        public bool ValidateInputFields()
        {

            //check user comment has input
            if (!string.IsNullOrEmpty(txtUserName.Text.ToString()) && !string.IsNullOrWhiteSpace(txtUserName.Text.ToString())
                && !string.IsNullOrEmpty(txtUserPassword.Text.ToString()) && !string.IsNullOrWhiteSpace(txtUserPassword.Text.ToString()))
            {

                return true;
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Missing Required Inputs" + "');", true);



                return false;

            }//for missing user comment


        }

        protected void btnSignUpCus_Click(object sender, EventArgs e)
        {
            Response.Redirect("customer-signup.aspx");
        }


        protected void btnSignUpBus_Click(object sender, EventArgs e)
        {
            Response.Redirect("business-signup.aspx");
        }
    }
}