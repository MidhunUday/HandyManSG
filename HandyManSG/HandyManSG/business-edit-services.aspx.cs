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
    public partial class business_edit_services : System.Web.UI.Page
    {
        string sqlConnectionString = ConfigurationManager.AppSettings["sqlConnectionString"];


        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {

                if (Session["userID"] != null)
                {
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

            string serviceTitle = "";
            string serviceDesc = "";
            string servicePrice = "";

            //form the query to get service details
            string getServieDetails = "SELECT [ServiceID],[businessUserID],[serviceTitle],[serviceDesc],[servicePrice] FROM " +
                    "[RepairAppDB].[Business].[BusinessServiceInfo] where [ServiceID] = '" + Session["ServiceID"].ToString() + "'";

            //get the service details into db dbServiceDetail
            DataTable dbServiceDetail = getData(getServieDetails);


            //check if has rows for the resultant table 
            if (dbServiceDetail.Rows.Count > 0)
            {
                //get the first row of the data table
                DataRow row_dbServiceDetail = dbServiceDetail.Rows[0];

                //access the column values for the first row
                serviceTitle = row_dbServiceDetail["serviceTitle"].ToString();
                serviceDesc = row_dbServiceDetail["serviceDesc"].ToString();
                servicePrice = row_dbServiceDetail["servicePrice"].ToString();


                //bind to the text box
                txtServiceID.Text = Session["ServiceID"].ToString();
                txtServiceTitle.Text = serviceTitle;
                txtServiceDesc.Text = serviceDesc;
                txtServicePrice.Text = servicePrice;


                Console.WriteLine();

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



        protected void btnUpdateBooking_Click(object sender, EventArgs e)
        {


            bool IsrequiredfieldComplete = ValidateInputFields();

            if (IsrequiredfieldComplete)
            {


                string queryUpdateService = "UPDATE  [RepairAppDB].[Business].[BusinessServiceInfo]" +
                " SET [serviceTitle]= @serviceTitle, [serviceDesc]= @serviceDesc,servicePrice=@servicePrice,serviceUpdationDateTime=@serviceUpdationDateTime  WHERE [ServiceID] = @ServiceID ";


                int checkupdateStatus = modifyTableRecord(queryUpdateService);

                if (checkupdateStatus == 1)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('Service updated successfully.');window.location='business-view-all-services.aspx';", true);

                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('Service fail to update successfully.');window.location='business-view-all-services.aspx';", true);

                }



            }





        }





        public bool ValidateInputFields()
        {

            //check user comment has input
            if (
                !string.IsNullOrEmpty(txtServiceTitle.Text.ToString()) && !string.IsNullOrWhiteSpace(txtServiceTitle.Text.ToString())
                && !string.IsNullOrEmpty(txtServiceDesc.Text.ToString()) && !string.IsNullOrWhiteSpace(txtServiceDesc.Text.ToString())
                && !string.IsNullOrEmpty(txtServicePrice.Text.ToString()) && !string.IsNullOrWhiteSpace(txtServicePrice.Text.ToString())
                )
            {

                return true;

            }
            else
            {

                //lblErrorMsg.Text = "Missing Required Inputs";
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Missing Required Inputs" + "');", true);

                return false;
            }//for missing user comment



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
                        string ServiceID = Session["ServiceID"].ToString();
                        DateTime serviceUpdationDateTime = DateTime.Now;

                        command.Parameters.AddWithValue("@ServiceID", ServiceID);
                        command.Parameters.AddWithValue("@serviceTitle", txtServiceTitle.Text.ToString());
                        command.Parameters.AddWithValue("@serviceDesc", txtServiceDesc.Text.ToString());
                        command.Parameters.AddWithValue("@servicePrice", txtServicePrice.Text.ToString());
                        command.Parameters.AddWithValue("@serviceUpdationDateTime", serviceUpdationDateTime);


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