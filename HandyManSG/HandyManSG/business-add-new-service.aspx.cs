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
    public partial class business_add_new_service : System.Web.UI.Page
    {
        string sqlConnectionString = ConfigurationManager.AppSettings["sqlConnectionString"];


        protected void Page_Load(object sender, EventArgs e)
        {

            //if the page is loaded no beacuse of postback
            if (!IsPostBack)
            {

                if (Session["userID"] != null)
                {
                    Session["BusinessCategory"] = null;
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



        protected void btnAddNewService_Click(object sender, EventArgs e)
        {


            bool IsRequiredFieldsFilled = ValidateInputFields();


            if (IsRequiredFieldsFilled)
            {

                //get the service category for the business user
                string getBusinessCategory = "SELECT serviceCategory FROM [RepairAppDB].[Business].[BusinessProfileInfo] " +
                    "where [userID]= @userID ";


                string BusinessCategory = GetBusinessCategory(getBusinessCategory);

                Session["BusinessCategory"] = BusinessCategory;

                string insertionQuery = "INSERT INTO RepairAppDB.Business.BusinessServiceInfo ( [ServiceID],[businessUserID],[serviceTitle],[serviceDesc],[servicePrice],[serviceCategory] )" +
                " VALUES ( @ServiceID,@businessUserID,@serviceTitle,@serviceDesc,@servicePrice,@serviceCategory ) ";


                int suceessInsert = UpdateinsertData(insertionQuery);

                if (suceessInsert == 1)
                {


                    Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('New Service created successfully.');window.location='business-view-all-services.aspx';", true);

                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('New Service creation failed.');window.location='business-view-all-services.aspx';", true);


                }

            }






        }





        public bool ValidateInputFields()
        {

            //check user comment has input
            if (!string.IsNullOrEmpty(txtServiceTitle.Text.ToString()) && !string.IsNullOrWhiteSpace(txtServiceTitle.Text.ToString()) &&
                !string.IsNullOrEmpty(txtServiceDesc.Text.ToString()) && !string.IsNullOrWhiteSpace(txtServiceDesc.Text.ToString()) &&
                   !string.IsNullOrEmpty(txtServiceprice.Text.ToString()) && !string.IsNullOrWhiteSpace(txtServiceprice.Text.ToString()))
            {

                //lblErrorMsg.Text = "";

                return true;

            }
            else
            {

                //lblErrorMsg.Text = "Missing Required Inputs";
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Missing Required Inputs" + "');", true);

                return false;
            }//for missing user comment


        }







        public string GetBusinessCategory(string Query)
        {
            string businessCategory = "";


            try
            {


                using (SqlConnection connection = new SqlConnection(sqlConnectionString))
                {



                    using (SqlCommand command = new SqlCommand(Query, connection))
                    {



                        command.Parameters.AddWithValue("@userID", Session["userID"].ToString());


                        connection.Open();
                        businessCategory = (string)command.ExecuteScalar();


                        return businessCategory;


                    }

                }



            }
            catch (Exception ex)
            {

                return businessCategory;
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

                        //generate  new id for the booking
                        Guid guid = Guid.NewGuid();
                        string guidFinal = guid.ToString().Substring(0, 23);

                        //append service to the id
                        string serviceID = "Service-" + guidFinal;


                        //get from session variable after user logins
                        string businessUserID = Session["userID"].ToString();
                        //get from session variable after user logins
                        string serviceCategory = Session["businessCategory"].ToString();


                        string serviceTitle = txtServiceTitle.Text.ToString();
                        string serviceDesc = txtServiceDesc.Text.ToString();
                        string servicePrice = txtServiceprice.Text.ToString();






                        command.Parameters.AddWithValue("@ServiceID", serviceID);
                        command.Parameters.AddWithValue("@businessUserID", businessUserID);
                        command.Parameters.AddWithValue("@serviceTitle", serviceTitle);
                        command.Parameters.AddWithValue("@serviceDesc", serviceDesc);
                        command.Parameters.AddWithValue("@servicePrice", servicePrice);
                        command.Parameters.AddWithValue("@serviceCategory", serviceCategory);

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