using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using demo.Areas.State.Models;
using demo.Areas.Country.Models;

namespace demo.Areas.State.Controllers
{
    [Area("State")]
    [Route("State/[controller]/[action]")]
    public class StateController : Controller
    {
        private IConfiguration Configuration;

        public StateController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IActionResult StateList()
        {
            string connectionString = this.Configuration.GetConnectionString("MyConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_LOC_State_SelectAll";

            SqlDataReader reader = command.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            return View(dt);
        }

        public IActionResult LOC_StateSave(LOC_State state)
        {
            string connectionString = this.Configuration.GetConnectionString("MyConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;

            if (state.StateID == null)
            {
                command.CommandText = "PR_LOC_State_Insert";
            }

            else
            {
                command.CommandText = "PR_LOC_State_Update";
                command.Parameters.Add("@StateID",SqlDbType.Int).Value = state.StateID;
            }

            command.Parameters.Add("@StateName", SqlDbType.VarChar).Value = state.StateName;
            command.Parameters.Add("@StateCode", SqlDbType.VarChar).Value = state.StateCode;
            command.Parameters.Add("@CountryID", SqlDbType.Int).Value = state.CountryID;
            command.ExecuteNonQuery();
            connection.Close();

            return RedirectToAction("StateList");
        }

        public IActionResult LOC_StateAddEdit(int StateID = 0)
        {

            string connectionString = this.Configuration.GetConnectionString("MyConnectionString");
            SqlConnection connection1 = new SqlConnection(connectionString);

            connection1.Open();
            SqlCommand command1 = connection1.CreateCommand();
            command1.CommandType = CommandType.StoredProcedure;
            command1.CommandText = "PR_Country_ComboBox";
            
            SqlDataReader reader1 = command1.ExecuteReader();
            DataTable dt1 = new DataTable();
            dt1.Load(reader1);

            List<LOC_CountryDropdownModel> list = new List<LOC_CountryDropdownModel>();

            foreach(DataRow  row in dt1.Rows)
            {
                LOC_CountryDropdownModel model = new LOC_CountryDropdownModel();
                model.CountryID = Convert.ToInt32(row["CountryID"]);
                model.CountryName = row["CountryName"].ToString();
                list.Add(model);
            }

            ViewBag.CountryList = list;


            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_LOC_State_SelectByPK";
            command.Parameters.AddWithValue("@StateID", StateID);
            SqlDataReader reader = command.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);



            LOC_State state = new LOC_State();

            foreach(DataRow row in dt.Rows)
            {
                state.StateID = Convert.ToInt32(row["StateID"]);
                state.StateName = row["StateName"].ToString();
                state.StateCode = row["StateCode"].ToString();
                state.CountryID = Convert.ToInt32(row["CountryID"]);
            }

            return View(state);
        }

        public IActionResult LOC_StateDelete(int StateID)
        {
            string connectionString = this.Configuration.GetConnectionString("MyConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_LOC_State_Delete";
            command.Parameters.AddWithValue("@StateID", StateID);
            command.ExecuteNonQuery();
            connection.Close();

            return RedirectToAction("StateList");
        }

        public IActionResult LOC_StateFilter(string? StateName, string? StateCode, string? CountryName)
        {

            string connection_string = this.Configuration.GetConnectionString("MyConnectionString");
            SqlConnection conn = new SqlConnection(connection_string);
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_State_Filter";

            if (StateName == null)
            {
                StateName = DBNull.Value.ToString();
            }

            if (StateCode == null)
            {
                StateCode = DBNull.Value.ToString();
            }

            if (CountryName == null)
            {
                CountryName = DBNull.Value.ToString();
            }

            cmd.Parameters.AddWithValue("@StateName", StateName);
            cmd.Parameters.AddWithValue("@StateCode", StateCode);
            cmd.Parameters.AddWithValue("@CountryName", CountryName);

            SqlDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);

            return View("StateList", dt);
        }

    }
}
