using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Medicine.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        void connectionString()
        {
            con.ConnectionString = "Data Source=tcp:localhost\\SQLEXPRESS,49801;Initial Catalog=medicine;Integrated Security=True;Trust Server Certificate=True";
        }

        //提供前端醫生資料(科別, 姓名)並顯示在醫師頁面上方(/Doctor)
        [HttpGet]
        [Route("{num}")]
        public Doctor DoctorInfo(int num)
        {
            Doctor doctor = new Doctor();
            connectionString();
            con.Open();
            com.Connection = con;
            com.CommandText = "SELECT name, department FROM doctor WHERE num = " + num + ";";
            var reader = com.ExecuteReader();
            if (reader.Read()) 
            {
                doctor.Name = reader.GetString(0);
                doctor.Department = reader.GetString(1);
                con.Close();
                return doctor;
            }
            con.Close();
            return null;
        }

        //新增病患
        [HttpPost]
        [Route("{num}/AddPatient")]
        public IActionResult AddPatient([FromBody]Patient patient)
        {
            connectionString();
            con.Open();
            com.Connection = con;
            com.CommandText = "INSERT INTO patient VALUES ( " + 
                "'" + patient.Name + "', " +
                patient.DoctorNum + " );";
            if (com.ExecuteNonQuery() > 0)
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

        //醫師登入後可在頁面下方看見自己所有患者資訊
        [HttpGet]
        [Route("{num}/Patients")]
        public List<Patient> DisplayPatients(int num)
        {
            List<Patient> patients = new List<Patient>();
            connectionString();
            con.Open();
            com.Connection = con;
            com.CommandText = "SELECT * FROM patient WHERE doctorNum = " + num + ";";
            var reader = com.ExecuteReader();
            while (reader.Read())
            {
                Patient patient = new Patient();
                patient.Id = reader.GetInt32(0);
                patient.Name = reader.GetString(1);
                patient.DoctorNum = reader.GetInt32(2);
                patients.Add(patient);
            }
            con.Close();
            return patients;
        }

        //醫師頁面尋找患者
        [HttpGet]
        [Route("{num}/Patient/{id}")]
        public Patient FindPatient(int id)
        {
            Patient patient = new Patient();
            connectionString();
            con.Open();
            com.Connection = con;
            com.CommandText = "SELECT * FROM patient WHERE id = " + id + ";";
            var reader = com.ExecuteReader();
            if (reader.Read())
            {
                patient.Id = reader.GetInt32(0);
                patient.Name = reader.GetString(1);
                patient.DoctorNum = reader.GetInt32(2);
                con.Close();
                return patient;
            }
            con.Close();
            return null;
        }

        public class Doctor
        {
            public string Name { get; set; }
            public string Department { get; set; }
        }

        //患者資訊含ID(Id), 名字(Name)及員工(醫師)編號(DoctorNum)
        public class Patient
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int DoctorNum { get; set; }
        }
    }
}
