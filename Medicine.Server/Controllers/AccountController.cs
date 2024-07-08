using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;

namespace Medicine.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();

        //SQL SERVER 連接字串
        void connectionString() 
        {
            con.ConnectionString = "Data Source=tcp:localhost\\SQLEXPRESS,49801;Initial Catalog=medicine;Integrated Security=True;Trust Server Certificate=True";
        }

        //新增員工(醫生)
        [HttpPost]
        [Route("NewEmployee")]
        public IActionResult AddDoctor([FromBody]Account acc)
        {
            connectionString();
            con.Open();
            com.Connection = con;

            //若員工編號已經存在於資料庫, 則無法新增
            com.CommandText = "SELECT * FROM doctor WHERE num = " + 
                acc.Num + ";";
            if(com.ExecuteNonQuery() > 0)
            {
                con.Close();
                return BadRequest();
            }

            com.CommandText = "INSERT INTO doctor VALUES( " + 
                acc.Num + ", '" + acc.Name + "', '" + acc.Department + "' );";
            if(com.ExecuteNonQuery() > 0)
            {
                con.Close();
                return Ok();
            }
            else
            {
                con.Close();
                return BadRequest();
            }
            
        }

        //員工(醫生)使用員工編號登入系統
        [HttpPost]
        [Route("DoctorSignIn")]
        public IActionResult SignInDoctor([FromBody]int num)
        {
            connectionString();
            con.Open();
            com.Connection = con;
            com.CommandText = "SELECT * FROM doctor WHERE num = " + num + ";";
            if (com.ExecuteReader().Read())
            {
                con.Close();
                return Ok();
            }
            else
            {
                con.Close();
                return BadRequest();
            }
        }

        //(藥局端)員工登入系統
        [HttpPost]
        [Route("LogInPharmacy")]
        public IActionResult LogInPharmacy([FromBody]Pharmacy phar)
        {
            if (phar.Email == "test@email.com" && phar.Password == "test123456")
                return Ok();
            else
                return BadRequest();
        }

        //員工(醫生)帳戶資料包含員工編號(Num), 醫師姓名(Name)
        //及部門(Department)
        public class Account
        {
            public int Num { get; set; }
            public string Name { get; set; }
            public string Department { get; set; }
        }

        public class Pharmacy
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }
    }
}
