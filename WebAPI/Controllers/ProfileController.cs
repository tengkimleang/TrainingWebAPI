using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using WebAPI.ConnectionString;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class ProfileController : Controller
    {
        SqlConnection sql = new SqlConnection("Data Source=LABSAPB192P5;Initial Catalog=DB_KOFI_API_TESTING_06-09-2021;User Id=sa;Password=SAPB1Admin;");//ConnectionSQLServer.Connection();192.168.0.179
        [HttpPost("CreateProfile")]
        public IActionResult Index(Profile profile)
        {
            try
            {
                
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
        [HttpGet("GetProfile")]
        public IActionResult GetProfile(int code=0)
        {
            sql.Open();
            DataTable dataTable=new DataTable();
            SqlCommand sqlCommand=sql.CreateCommand();
            sqlCommand.CommandText= "SELECT * FROM dbo.\"@TBPROFILE\" WHERE Code=CASE WHEN @id=0 THEN Code ELSE @id END;";
            sqlCommand.Parameters.AddWithValue("@id", code);
            SqlDataAdapter sqlDataAdapter= new SqlDataAdapter(sqlCommand);
            sqlDataAdapter.Fill(dataTable);
            sql.Close() ;
            List<Profile> profile=new List<Profile>();
            foreach(DataRow dataRow in dataTable.Rows)
            {
                profile.Add(new Profile
                {
                    Id = Convert.ToInt32(dataRow["Code"]),
                    FirstName = dataRow["U_FirstName"].ToString(),
                    LastName = dataRow["U_LastName"].ToString(),
                    ImagePath = dataRow["U_ImagePath"].ToString(),
                    Email = dataRow["U_Email"].ToString(),
                    Password = dataRow["U_Password"].ToString(),
                });
            }
            return Ok(profile);
        }
        [HttpPut("UpdateProfile")]
        public IActionResult UpdateProfite(Profile profile)
        {
            sql.Open();
            SqlCommand sqlCommand= sql.CreateCommand();
            sqlCommand.CommandText = "UPDATE dbo.[@TBPROFILE] SET U_ImagePath=CASE WHEN @ImagePath='' THEN U_ImagePath ELSE @ImagePath END,U_FirstName=CASE WHEN @FirstName=''" +
                " THEN U_FirstName ELSE @FirstName END,U_LastName=CASE WHEN @LastName=''" +
                "THEN U_LastName ELSE @LastName END,U_Email=CASE WHEN @Email='' " +
                "THEN U_Email ELSE @Email END,U_Password=CASE WHEN @Password=''" +
                " THEN U_Password ELSE @Password END WHERE Code=@id;";
            sqlCommand.Parameters.AddWithValue("@ImagePath", string.IsNullOrEmpty(profile.ImagePath)?"": profile.ImagePath);
            sqlCommand.Parameters.AddWithValue("@FirstName", string.IsNullOrEmpty(profile.FirstName) ? "" : profile.FirstName);
            sqlCommand.Parameters.AddWithValue("@LastName", string.IsNullOrEmpty(profile.LastName) ? "" : profile.LastName);
            sqlCommand.Parameters.AddWithValue("@Email", string.IsNullOrEmpty(profile.Email) ? "" : profile.Email);
            sqlCommand.Parameters.AddWithValue("@Password", string.IsNullOrEmpty(profile.Password)?"":profile.Password);
            sqlCommand.Parameters.AddWithValue("@id", profile.Id);
            var a=sqlCommand.ExecuteNonQuery();
            sql.Close();
            return Ok(a==0?"Don't Update":"Updated");   
        }
        [HttpDelete("DeleteProfile")]
        public IActionResult DeleteProfite(int id)
        {
            sql.Open();
            SqlCommand sqlCommand= sql.CreateCommand();
            sqlCommand.CommandText = "DELETE FROM dbo.[@TBPROFILE] WHERE Code=@id";
            sqlCommand.Parameters.AddWithValue("@id", id);
            var a=sqlCommand.ExecuteNonQuery();
            sql.Close();
            if (a == 0)
            {
                return Ok("Delete Not Found");
            }
            else
            {
                return Ok("Deleted");
            }
            //return Ok(a==0?"Don't Update":"Updated");
        }
    }
}
