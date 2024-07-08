using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using static Medicine.Server.Controllers.DoctorController;

namespace Medicine.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PharmacyController : ControllerBase
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        void connectionString()
        {
            con.ConnectionString = "Data Source=tcp:localhost\\SQLEXPRESS,49801;Initial Catalog=medicine;Integrated Security=True;Trust Server Certificate=True";
        }

        //在藥局頁面(/Pharmacy)新增藥物
        [HttpPost]
        [Route("AddMedicine")]
        public IActionResult addMedicine([FromBody]Medicine medicine)
        {
            connectionString();
            con.Open();
            com.Connection = con;
            com.CommandText = "INSERT INTO medicine VALUES ( '" + 
                medicine.Name + "', '" + medicine.Effect + "', '" + 
                medicine.Side + "', '" + medicine.Dose + "' );";
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

        //以藥物名稱查詢藥物資訊
        [HttpGet]
        [Route("Medicine/{name}")]
        public Medicine SearchMedicine(string name)
        {
            Medicine medicine = new Medicine();
            connectionString();
            con.Open();
            com.Connection = con;
            com.CommandText = "SELECT * FROM medicine WHERE name = '" + name + "';";
            var reader = com.ExecuteReader();
            if (reader.Read())
            {
                medicine.Id = reader.GetInt32(0);
                medicine.Name = reader.GetString(1);
                medicine.Effect = reader.GetString(2);
                medicine.Side = reader.GetString(3);
                medicine.Dose = reader.GetString(4);
                con.Close();
                return medicine;
            }
            con.Close();
            return null;
        }

        //提供前端使用藥物名稱尋找藥物ID
        [HttpGet]
        [Route("FindIdByName/{name}")]
        public int MedicineId(string name)
        {
            var requestId = -1;
            connectionString();
            con.Open();
            com.Connection = con;
            com.CommandText = "SELECT id FROM medicine WHERE name = '" + name + "';";
            var reader = com.ExecuteReader();
            if (reader.Read())
            {
                requestId = reader.GetInt32(0);
            }
            con.Close();
            return requestId;
        }

        //藥局端顯示開藥紀錄
        [HttpGet]
        [Route("Records")]
        public List<Display> Records() 
        {
            List<Display> displays = new List<Display>();
            List<Record> records = new List<Record>();
            connectionString();
            con.Open();
            com.Connection = con;
            com.CommandText = "SELECT * FROM record;";
            var reader = com.ExecuteReader();
            while (reader.Read())
            {
                Record record = new Record();
                record.Id = reader.GetInt32(0);
                record.Time = reader.GetString(1);
                record.Quant = reader.GetInt32(2);
                record.PatientId = reader.GetInt32(3);
                record.MedicineId = reader.GetInt32(4);
                record.DocNum = reader.GetInt32(5);
                record.GetMedTime = reader.GetString(6);
                records.Add(record);
            }
            reader.Close();

            //從原本資料庫裡的紀錄資料, 修改為要顯示在頁面的模樣
            foreach (var record in records)
            {
                Display display = new Display();
                display.Id = record.Id;
                display.Time = record.Time;
                display.Quant = record.Quant;
                display.GetMedTime = record.GetMedTime;
                com.CommandText = "SELECT name FROM patient WHERE id = " + record.PatientId + ";";
                reader = com.ExecuteReader();
                if (reader.Read())
                {
                    display.PatientName = reader.GetString(0);
                }
                reader.Close();
                com.CommandText = "SELECT name FROM medicine WHERE id = " + record.MedicineId + ";";
                reader = com.ExecuteReader();
                if (reader.Read())
                {
                    display.MedicineName = reader.GetString(0);
                }
                reader.Close();
                com.CommandText = "SELECT name, department FROM doctor WHERE num = " + record.DocNum + ";";
                reader = com.ExecuteReader();
                if (reader.Read())
                {
                    display.DoctorName = reader.GetString(0);
                    display.Department = reader.GetString(1);
                }
                reader.Close();
                displays.Add(display);
            }
            con.Close();
            return displays;
        }

        //藥物資訊含藥物ID(Id), 藥物名稱(Name), 藥效(Effect), 副作用(Side), 
        //以及使用方式(Dose)
        public class Medicine
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Effect { get; set; }
            public string Side { get; set; }
            public string Dose { get; set; }
        }

        public class Record
        {
            public int Id { get; set; }
            public string Time { get; set; }
            public int Quant { get; set; }
            public int PatientId { get; set; }
            public int MedicineId { get; set; }
            public int DocNum { get; set; }
            public string GetMedTime { get; set; }
        }

        //藥局端顯示開藥紀錄含紀錄ID(Id), 開藥時間(Time), 開藥數量(Quant), 
        //患者名稱(PatientName), 藥物名稱(MedicineName), 開藥醫師名稱(DoctorName), 
        //開藥醫師部門(Department), 領藥時間(GetMedTime)
        public class Display
        {
            public int Id { get; set; }
            public string Time { get; set; }
            public int Quant { get; set; }
            public string PatientName { get; set; }
            public string MedicineName { get; set; }
            public string DoctorName { get; set; }
            public string Department { get; set; }
            public string GetMedTime { get; set; }
        }
    }
}
