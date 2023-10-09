using Microsoft.AspNetCore.Mvc;
using demo.Areas.City.Models;
using System.Data.SqlClient;
using System.Data;
using demo.Areas.State.Models;
using demo.Areas.Country.Models;

namespace demo.Areas.City.Controllers
{
    [Area("City")]
    public class CityController : Controller
    {
     
        private IConfiguration Configuration;

        public CityController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

        public IActionResult CityList()
        {
            string connectionString = this.Configuration.GetConnectionString("MyConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_LOC_City_SelectAll";

            SqlDataReader reader = command.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);    
            return View(dt);    

        }

        public IActionResult LOC_CitySave(Loc_City city)
        {
            string connectionString = this.Configuration.GetConnectionString("MyConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType= CommandType.StoredProcedure;
            if(city.CityID == null)
            {
                command.CommandText = "PR_LOC_City_Insert";
            }

            else
            {
                command.CommandText = "PR_LOC_City_Update";
                command.Parameters.Add("@CityID",SqlDbType.Int).Value  = city.CityID;
            }

            command.Parameters.Add("@CityName", SqlDbType.VarChar).Value = city.CityName;
            command.Parameters.Add("@CityCode", SqlDbType.VarChar).Value = city.CityCode;
            command.Parameters.Add("@StateID", SqlDbType.Int).Value = city.StateID;
            command.Parameters.Add("@CountryID", SqlDbType.Int).Value = city.CountryID;

            command.ExecuteNonQuery();
            connection.Close();
            return RedirectToAction("CityList");


        }

        public IActionResult LOC_CityAddEdit(int CityID = 0)
        {
            
            string connectionString = this.Configuration.GetConnectionString("MyConnectionString");


            //SqlConnection connection1 = new SqlConnection(connectionString);
            //connection1.Open();
            //SqlCommand command1 = connection1.CreateCommand();
            //command1.CommandType= CommandType.StoredProcedure;
            //command1.CommandText = "PR_State_ComboBox";
            //SqlDataReader reader1 = command1.ExecuteReader();

            //DataTable dt1 = new DataTable();
            //dt1.Load(reader1);
            //connection1.Close();

            //List<LOC_StateDropdownModel> list1 = new List<LOC_StateDropdownModel>();

            //foreach(DataRow dr in dt1.Rows)
            //{
            //    LOC_StateDropdownModel model1 = new LOC_StateDropdownModel();
            //    model1.StateID = Convert.ToInt32(dr["StateID"]);
            //    model1.StateName = dr["StateName"].ToString();
            //    list1.Add(model1);
            //}
            if (CityID == 0)
            {
                // Insert

                SqlConnection connection12 = new SqlConnection(connectionString);
                connection12.Open();
                SqlCommand command12 = connection12.CreateCommand();
                command12.CommandType = CommandType.StoredProcedure;
                command12.CommandText = "PR_Country_ComboBox";
                SqlDataReader reader12 = command12.ExecuteReader();
                DataTable dt12 = new DataTable();
                dt12.Load(reader12);
                connection12.Close();

                List<LOC_CountryDropdownModel> list12 = new List<LOC_CountryDropdownModel>();

                foreach (DataRow dr in dt12.Rows)
                {
                    LOC_CountryDropdownModel model1 = new LOC_CountryDropdownModel();
                    model1.CountryID = Convert.ToInt32(dr["CountryID"]);
                    model1.CountryName = dr["CountryName"].ToString();
                    list12.Add(model1);
                }

                ViewBag.CountryList = list12;
                List<LOC_StateDropdownModel> list13 = new List<LOC_StateDropdownModel>();

                ViewBag.StateList = list13;
                return View();
            }
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_LOC_City_SelectByPK";
            command.Parameters.Add("@CityID", SqlDbType.Int).Value = CityID;
            SqlDataReader reader = command.ExecuteReader();

            DataTable dt = new DataTable();

            dt.Load(reader);

            Loc_City city = new Loc_City();

            foreach (DataRow row in dt.Rows)
            {
                city.CityID = Convert.ToInt32(row["CityID"]);
                city.CityName = row["CityName"].ToString();
                city.CityCode = row["CityCode"].ToString();
                city.StateID = Convert.ToInt32(row["StateID"]);
                city.CountryID = Convert.ToInt32(row["CountryID"]);
            }

            string connectionString4 = this.Configuration.GetConnectionString("MyConnectionString");
            SqlConnection connection4 = new SqlConnection(connectionString4);
            connection4.Open();
            SqlCommand command4 = connection.CreateCommand();
            command4.CommandType = CommandType.StoredProcedure;
            command4.CommandText = "PR_State_StateByCountryID";
            command4.Parameters.AddWithValue("@CountryID", city.CountryID);

            SqlDataReader reader4 = command4.ExecuteReader();
            DataTable dt4 = new DataTable();
            dt4.Load(reader4);
            connection4.Close();

            List<LOC_StateDropdownModel> list4 = new List<LOC_StateDropdownModel>();
            foreach (DataRow dr in dt.Rows)
            {
                LOC_StateDropdownModel state = new LOC_StateDropdownModel();
                state.StateID = Convert.ToInt32(dr["StateID"]);
                state.StateName = dr["StateName"].ToString();
                list4.Add(state);

            }

            ViewBag.StateList = list4;

            SqlConnection connection2 = new SqlConnection(connectionString);
            connection2.Open();
            SqlCommand command2 = connection2.CreateCommand();
            command2.CommandType = CommandType.StoredProcedure;
            command2.CommandText = "PR_Country_ComboBox";
            SqlDataReader reader2 = command2.ExecuteReader();
            DataTable dt2 = new DataTable();
            dt2.Load(reader2);
            connection2.Close();

            List<LOC_CountryDropdownModel> list2 = new List<LOC_CountryDropdownModel>();

            foreach (DataRow dr in dt2.Rows)
            {
                LOC_CountryDropdownModel model1 = new LOC_CountryDropdownModel();
                model1.CountryID = Convert.ToInt32(dr["CountryID"]);
                model1.CountryName = dr["CountryName"].ToString();
                list2.Add(model1);
            }

            ViewBag.CountryList = list2;

            return View(city);

        }

        public IActionResult LOC_CityDelete(int CityID)
        {
            string connectionString = this.Configuration.GetConnectionString("MyConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_LOC_City_Delete";
            command.Parameters.AddWithValue("@CityID", CityID);
            command.ExecuteNonQuery();
            connection.Close();
            return RedirectToAction("CityList");
        }

        public dynamic StatesForComboBox(int CountryID)
        {
            string connectionString = this.Configuration.GetConnectionString("MyConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_State_StateByCountryID";
            command.Parameters.AddWithValue("@CountryID", CountryID);

            SqlDataReader reader = command.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            connection.Close();

            List<LOC_StateDropdownModel> list = new List<LOC_StateDropdownModel>();
            foreach(DataRow dr in dt.Rows)
            {
                LOC_StateDropdownModel state = new LOC_StateDropdownModel();
                state.StateID = Convert.ToInt32(dr["StateID"]);
                state.StateName = dr["StateName"].ToString();
                list.Add(state);

            }

            ViewBag.StateList = list;

            return Json(list);
        }

        public IActionResult LOC_CityFilter(string? CityName, string? CityCode, string? StateName, string? CountryName)
        {

            string connection_string = this.Configuration.GetConnectionString("MyConnectionString");
            SqlConnection conn = new SqlConnection(connection_string);
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_City_Filter";

            if (CityName == null)
            {
                CityName = DBNull.Value.ToString();
            }

            if (CityCode == null)
            {
                CityCode = DBNull.Value.ToString();
            }

            if (StateName == null)
            {
                StateName = DBNull.Value.ToString();
            }

            if (CountryName == null)
            {
                CountryName = DBNull.Value.ToString();
            }

            cmd.Parameters.AddWithValue("@CityName", CityName);
            cmd.Parameters.AddWithValue("@CityCode", CityCode);
            cmd.Parameters.AddWithValue("@StateName", StateName);
            cmd.Parameters.AddWithValue("@CountryName", CountryName);

            SqlDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);

            return View("CityList", dt);
        }
    }
}
