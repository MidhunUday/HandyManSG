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
    public partial class customer_view_services : System.Web.UI.Page
    {
        string sqlConnectionString = ConfigurationManager.AppSettings["sqlConnectionString"];



        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {


                if (Session["userID"] != null)
                {



                    GetServiceDataOnCategory();

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





        public void GetServiceDataOnCategory()
        {

            string getServiceData = "  SELECT serviceData.[ServiceID], serviceData.[businessUserID],serviceData.[serviceTitle],serviceData.[serviceDesc],serviceData.[servicePrice]," +
                "busProfileData.[companyName],busProfileData.[emailID],busProfileData.[companyAddress],busProfileData.[contactPhoneNumber],busProfileData.[companyRating] FROM " +
                "[RepairAppDB].[Business].[BusinessServiceInfo] serviceData, [RepairAppDB].[Business].[BusinessProfileInfo] busProfileData " +
                "where serviceData.[serviceCategory]='" + Session["selectedServiceCategory"].ToString() + "' and serviceData.[businessUserID]= busProfileData.[userID] ";


            //get data function
            DataTable resultData = getData(getServiceData);


            gridViewShowServices.DataSource = resultData;
            gridViewShowServices.DataBind();


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



        protected void gridViewShowServices_SelectedIndexChanged(object sender, EventArgs e)
        {

        }



        protected void lnkView_Click(object sender, EventArgs e)
        {

            LinkButton lnkbtn = sender as LinkButton;

            string selectedServiceID = lnkbtn.CommandArgument.ToString();

            Session["ServiceID"] = selectedServiceID;

            Response.Redirect("customer-booking-page.aspx");

        }

        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridViewShowServices.PageIndex = e.NewPageIndex;
            this.GetServiceDataOnCategory();
        }




    }
}