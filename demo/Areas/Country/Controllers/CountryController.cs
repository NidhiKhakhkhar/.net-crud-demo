using Microsoft.AspNetCore.Mvc;
using demo.Areas.Country.Models;
using System.Data;
using System.Data.SqlClient;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace demo.Areas.Country.Controllers
{
    [Area("Country")]
    [Route("Country/[controller]/[action]")]
    public class CountryController : Controller
    {
        private IConfiguration Configuration;

        #region Configuration
        public CountryController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

        #endregion


        #region CountryList
        public IActionResult CountryList(string CountryName = null)
        {
            string connectionstring = this.Configuration.GetConnectionString("MyConnectionString");
            SqlConnection connection = new SqlConnection(connectionstring);

            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_LOC_Country_SelectAll";


            SqlDataReader reader = command.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            return View(dt);
        }
        #endregion

        #region LOC_CountrySave
        public IActionResult LOC_CountrySave(Loc_Country country)
        {
            if(string.IsNullOrEmpty(country.CountryName))
            {
                ModelState.AddModelError("CountryName", "Country name is required");
            }
            if(string.IsNullOrEmpty(country.CountryCode))
            {
                ModelState.AddModelError("CountryCode", "Country code is required");
            }

            if(ModelState.IsValid)
            {
                string connectionString = this.Configuration.GetConnectionString("MyConnectionString");
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                if (country.CountryID == null)
                {
                    command.CommandText = "PR_LOC_Country_Insert";

                }

                else
                {
                    command.CommandText = "PR_LOC_Country_Update";
                    command.Parameters.Add("@CountryID", SqlDbType.Int).Value = country.CountryID;
                }

                command.Parameters.Add("@CountryName", SqlDbType.VarChar).Value = country.CountryName;
                command.Parameters.Add("@CountryCode", SqlDbType.VarChar).Value = country.CountryCode;
                command.ExecuteNonQuery();
                connection.Close();
                return RedirectToAction("CountryList");


            }
            return View("LOC_CountryAddEdit",country);



        }
        #endregion


        #region LOC_CountryAddEdit
        public IActionResult LOC_CountryAddEdit(int CountryID = 0)
        {

            string connectionString = this.Configuration.GetConnectionString("MyConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_LOC_Country_SelectByPK";
            command.Parameters.Add("@CountryID", SqlDbType.Int).Value = CountryID;
            SqlDataReader reader = command.ExecuteReader();

            DataTable dt = new DataTable();

            dt.Load(reader);

            Loc_Country country = new Loc_Country();

            foreach (DataRow row in dt.Rows)
            {
                country.CountryID = Convert.ToInt32(row["CountryID"]);
                country.CountryName = row["CountryName"].ToString();
                country.CountryCode = row["CountryCode"].ToString();

            }
            return View(country);
        }
        #endregion

        #region LOC_CountryDelete

        public IActionResult LOC_CountryDelete(int CountryID)
        {
            string connectionString = this.Configuration.GetConnectionString("MyConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_LOC_Country_DeleteByPK";
            command.Parameters.AddWithValue("@CountryID", CountryID);
            command.ExecuteNonQuery();
            connection.Close();

            return RedirectToAction("CountryList");
        }

        #endregion


        public IActionResult LOC_Country_Filter(string? CountryName, string? CountryCode)
        {
            string connectionString = this.Configuration.GetConnectionString("MyConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_LOC_Country_Like";

            if (CountryName == null)
            {
                CountryName = DBNull.Value.ToString();
            }

            if (CountryCode == null)
            {
                CountryCode = DBNull.Value.ToString();
            }



            command.Parameters.AddWithValue("@CountryName", CountryName);
            command.Parameters.AddWithValue("@CountryCode", CountryCode);


            SqlDataReader reader = command.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            

            return View("CountryList", dt);
        }

    }


}
