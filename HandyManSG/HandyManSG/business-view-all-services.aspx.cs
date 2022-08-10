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
    public partial class business_view_all_services : System.Web.UI.Page
    {
        string sqlConnectionString = ConfigurationManager.AppSettings["sqlConnectionString"];


        protected void Page_Load(object sender, EventArgs e)
        {
            //if the page is loaded no beacuse of postback
            if (!IsPostBack)
            {


                if (Session["userID"] != null)
                {

                    Session["ServiceID"] = null;

                    getAllServices();
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

        protected void gridViewServices_SelectedIndexChanged(object sender, EventArgs e)
        {

        }




        protected void btnHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("BusinessUserHome.aspx");
        }


        public void getAllServices()
        {

            //sql query to get all data
            string getAllDataQuery = "SELECT [ServiceID],[businessUserID],[serviceTitle],[serviceDesc],[servicePrice],[serviceCreationDateTime] FROM " +
                "[RepairAppDB].[Business].[BusinessServiceInfo] where [businessUserID] = '" + Session["userID"].ToString() + "'";

            //get data function
            DataTable resultData = getData(getAllDataQuery);


            gridViewServices.DataSource = resultData;
            gridViewServices.DataBind();

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



        protected void lnkEDIT_Click(object sender, EventArgs e)
        {

            LinkButton lnkbtn = sender as LinkButton;

            string selectedServiceID = lnkbtn.CommandArgument.ToString();

            Session["ServiceID"] = selectedServiceID;

            Response.Redirect("business-edit-services.aspx");


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






        protected void lnkDelete_Click(object sender, EventArgs e)
        {


            LinkButton lnkbtn = sender as LinkButton;

            string selectedServiceID = lnkbtn.CommandArgument.ToString();

            Session["ServiceID"] = selectedServiceID;

            //delete the booking which have these services
            string deletionbookingQuery = "DELETE FROM [RepairAppDB].[customer].[CustomerBookingInfo] WHERE [ServiceID]= @ServiceID ";
            int checkdeletionStatusNew = modifyTableRecord(deletionbookingQuery);






            string deletionQuery = "DELETE FROM [RepairAppDB].[Business].[BusinessServiceInfo] WHERE [ServiceID]= @ServiceID ";

            int checkdeletionStatus = modifyTableRecord(deletionQuery);


            if (checkdeletionStatus == 1)
            {

                Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('Service deleted successfully.');window.location='business-view-all-services.aspx';", true);

            }
            else
            {

                Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('Service deletion Failed.');window.location='business-view-all-services.aspx';", true);

            }




        }


        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridViewServices.PageIndex = e.NewPageIndex;
            this.getAllServices();
        }








    }
}