using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using WebAPI.ConnectionString;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class ProfileController : Controller
    {
        [HttpPost("CreateProfile")]
        public IActionResult Index(Profile profile)
        {
            try
            {
                SqlConnection sql =  new SqlConnection("Data Source=LABSAPB192P5;Initial Catalog=DB_KOFI_API_TESTING_06-09-2021;User Id=sa;Password=SAPB1Admin;");//ConnectionSQLServer.Connection();192.168.0.179
                sql.Open();
                SqlCommand cmd = sql.CreateCommand();
                cmd.CommandText = "Insert into dbo.[@TBPROFILE](U_ImagePath,U_FirstName,U_LastName,U_Email,U_Password,U_CreateBy) Values(@image,@firstName,@lastName,@email,@password,@userid)";
                cmd.Parameters.AddWithValue("@image", profile.ImagePath);
                cmd.Parameters.AddWithValue("@firstName", profile.FirstName);
                cmd.Parameters.AddWithValue("@lastName", profile.LastName);
                cmd.Parameters.AddWithValue("@email", profile.Email);
                cmd.Parameters.AddWithValue("@password", profile.Password);
                cmd.Parameters.AddWithValue("@userid", profile.CreateBy);
                cmd.ExecuteNonQuery();
                sql.Close();
                return Ok(profile);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }            
        }
    }
}
