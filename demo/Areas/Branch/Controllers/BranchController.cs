using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using demo.Areas.Branch.Models;

namespace demo.Areas.Branch.Controllers
{
    [Area("Branch")]
    [Route("Branch/[controller]/[action]")]
    public class BranchController : Controller
    {
        private IConfiguration Configuration;

        public BranchController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IActionResult BranchList()
        {
            string connectionString = this.Configuration.GetConnectionString("MyConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_MST_Branch_SelectAll";

            SqlDataReader reader = command.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            return View(dt);
        }

        public IActionResult MST_BranchSave(MST_Branch branch)
        {
            string connectionString = this.Configuration.GetConnectionString("MyConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;

            if (branch.BranchID == null)
            {
                command.CommandText = "PR_MST_Branch_Insert";
            }

            else
            {
                command.CommandText = "PR_MST_Branch_UpdateByPK";
                command.Parameters.AddWithValue("@BranchID", branch.BranchID);
            }

            command.Parameters.AddWithValue("@BranchName", branch.BranchName);
            command.Parameters.AddWithValue("@BranchCode", branch.BranchCode);
            command.ExecuteNonQuery();
            connection.Close();
            return RedirectToAction("BranchList");
        }

        public IActionResult MST_BranchAddEdit(int BranchID = 0)
        {
            string connectionString = this.Configuration.GetConnectionString("MyConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_MST_Branch_SelectByPK";
            command.Parameters.Add("@BranchID", SqlDbType.Int).Value = BranchID;

            SqlDataReader reader = command.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);

            MST_Branch branch = new MST_Branch();
            foreach (DataRow row in dt.Rows)
            {
                branch.BranchID = Convert.ToInt32(row["BranchID"]);
                branch.BranchName = row["BranchName"].ToString();
                branch.BranchCode = row["BranchCode"].ToString();
            }

            return View(branch);
        }

        public IActionResult MST_BranchDelete(int BranchID)
        {
            string connectionString = this.Configuration.GetConnectionString("MyConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_MST_Branch_DeleteByPK";
            command.Parameters.Add("@BranchID",SqlDbType.Int).Value = BranchID;
            command.ExecuteNonQuery();
            return RedirectToAction("BranchList");


        }

        public IActionResult MST_BranchFilter(string? BranchName, string? BranchCode)
        {

            string connection_string = this.Configuration.GetConnectionString("MyConnectionString");
            SqlConnection conn = new SqlConnection(connection_string);
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_Branch_Filter";

            if (BranchName == null)
            {
                BranchName = DBNull.Value.ToString();
            }

            if (BranchCode == null)
            {
                BranchCode = DBNull.Value.ToString();
            }

            cmd.Parameters.AddWithValue("@BranchName", BranchName);
            cmd.Parameters.AddWithValue("@BranchCode", BranchCode);

            SqlDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);

            return View("BranchList", dt);
        }

    }
}
