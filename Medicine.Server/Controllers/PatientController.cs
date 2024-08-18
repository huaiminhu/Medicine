using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using static Medicine.Server.Controllers.PharmacyController;

namespace Medicine.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        void connectionString()
        {
            con.ConnectionString = "Data Source=tcp:localhost\\SQLEXPRESS,49801;Initial Catalog=medicine;Integrated Security=True;Trust Server Certificate=True";
        }

        //患者頁面(/Patient)最下方顯示所有開藥紀錄a
        [HttpGet]
        [Route("{id}/Records")]
        public List<Display> DisplayRecords(int id)
        {
            List<Display> displays = new List<Display>();
            List<Record> records = new List<Record>();
            connectionString();
            con.Open();
            com.Connection = con;
            com.CommandText = "SELECT * FROM record WHERE PatientId = " + id + ";";
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
                records.Add(record);
            }
            reader.Close();

            //從原本資料庫裡的紀錄資料, 修改為要顯示在頁面的模樣
            foreach ( var record in records )
            {
                Display display = new Display();
                display.Id = record.Id;
                display.Time = record.Time;
                display.Quant = record.Quant;
                com.CommandText = "SELECT name FROM medicine WHERE id = " + record.MedicineId + ";";
                reader = com.ExecuteReader();
                if (reader.Read())
                {
                    display.MedicineName = reader.GetString(0);
                }
                reader.Close();
                displays.Add(display);
            }
            con.Close();
            return displays;
        }

        //查詢該病患單筆開藥紀錄
        [HttpGet]
        [Route("{id1}/Record/{id2}")]
        public Display FindRecord(int id1, int id2)
        {
            Display display = new Display();
            Record record = new Record();
            connectionString();
            con.Open();
            com.Connection = con;
            com.CommandText = "SELECT * FROM record WHERE PatientId = " + id1 + 
                " AND id = " + id2 + ";";
            var reader = com.ExecuteReader();
            if (reader.Read())
            {
                record.Id = reader.GetInt32(0);
                record.Time = reader.GetString(1);
                record.Quant = reader.GetInt32(2);
                record.PatientId = reader.GetInt32(3);
                record.MedicineId = reader.GetInt32(4);
                record.DocNum = reader.GetInt32(5);
            }
            reader.Close();
            display.Id = record.Id;
            display.Time = record.Time;
            display.Quant = record.Quant;
            com.CommandText = "SELECT name FROM medicine WHERE id = " + record.MedicineId + ";";
            reader = com.ExecuteReader();
            if (reader.Read())
            {
                display.MedicineName = reader.GetString(0);
            }
            con.Close();
            return display;
        }

        //新增開藥
        [HttpPost]
        [Route("{id}/AddRecord")]
        public IActionResult AddRecord([FromBody]Record record)
        {
            connectionString();
            con.Open();
            com.Connection = con;
            com.CommandText = "INSERT INTO record VALUES ( '" + 
                record.Time + "', " + record.Quant + ", " + 
                record.PatientId + ", " + record.MedicineId + 
                ", " + record.DocNum + ", '" + record.GetMedTime + "' );";
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

        //患者取藥時, 更新紀錄資料中的取藥時間欄位(原為空字元)
        [HttpPut]
        [Route("{id}/GetMed")]
        public IActionResult GetMed([FromRoute]int id, [FromBody]string getDate)
        {
            connectionString();
            con.Open();
            com.Connection = con;
            com.CommandText = "UPDATE record SET getMedTime = '" + getDate + 
                "' WHERE id = " + id;
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

        //使用時間查詢開藥紀錄
        [HttpGet]
        [Route("{id}/RecByTime/{time}")]
        public List<Display> SearchByTime(int id, string time)
        {
            List<Display> displays = new List<Display>();
            List<Record> records = new List<Record>();
            connectionString();
            con.Open();
            com.Connection = con;
            com.CommandText = "SELECT * FROM record WHERE time = '" + 
                time + "' AND patientId = " + id + ";";
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
                records.Add(record);
            }
            reader.Close();
            foreach(var record in records) 
            {
                Display display = new Display();
                display.Id = record.Id;
                display.Time = record.Time;
                display.Quant = record.Quant;
                com.CommandText = "SELECT name FROM medicine WHERE id = " + record.MedicineId + ";";
                reader = com.ExecuteReader();
                if (reader.Read())
                {
                    display.MedicineName = reader.GetString(0);
                }
                displays.Add(display);
                reader.Close();
            }
            con.Close();
            return displays;
        }

        //使用藥物名稱查詢開藥紀錄
        [HttpGet]
        [Route("{id}/RecByMed/{name}")]
        public List<Display> SearchByMed(int id, string name)
        {
            List<Display> displays = new List<Display>();
            List<Record> records = new List<Record>();
            connectionString();
            con.Open();
            com.Connection = con;
            com.CommandText = "SELECT * FROM record " + 
                "WHERE patientId = " + id + " AND medicineId = " + 
                "(SELECT id FROM medicine WHERE name = '" + 
                name +"' );";
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
                records.Add(record);
            }
            reader.Close();
            foreach (var record in records)
            {
                Display display = new Display();
                display.Id = record.Id;
                display.Time = record.Time;
                display.Quant = record.Quant;
                com.CommandText = "SELECT name FROM medicine WHERE id = " + record.MedicineId + ";";
                reader = com.ExecuteReader();
                if (reader.Read())
                {
                    display.MedicineName = reader.GetString(0);
                }
                displays.Add(display);
                reader.Close();
            }
            con.Close();
            return displays;
        }
    }

    //資料庫中開藥單筆記錄包括紀錄ID(Id), 開藥時間(Time), 開藥數量(Quant),
    //病患ID(PatientId), 藥物ID(MedicineId), 員工(醫師)編號(DocNum),
    //領藥時間(GetMedTime)
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

    //顯示在病患頁面的紀錄包含的資訊有紀錄ID(Id), 開藥時間(Time),
    //開藥數量(Quant), 藥物名稱(MedicineName)
    public class Display
    {
        public int Id { get; set; }
        public string Time { get; set; }
        public int Quant { get; set; }
        public string MedicineName { get; set; }
    }
}
