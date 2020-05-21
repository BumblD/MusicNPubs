using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Muzika_ir_barai.Models;

namespace Muzika_ir_barai.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillController : ControllerBase
    {
        private AppDb Db { get; set; }

        public BillController(AppDb db)
        {
            Db = db;
        }

        [HttpGet]
        [Route("GetBarBills/{id}")]
        public IActionResult GetBarBills([FromRoute] int id)
        {
            List<BillModel> bills = new List<BillModel>();

            try
            {
                Db.Connection.Open();
                var cmd = Db.Connection.CreateCommand();
                cmd.CommandText = $"SELECT * FROM Saskaitos WHERE Baras = {id} ORDER BY Pateikimo_data DESC";

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        bills.Add(new BillModel()
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            TableNo = Convert.ToInt32(reader["Staliuko_nr"]),
                            PayAmount = Convert.ToDouble(reader["Suma"]),
                            PayDate = Convert.ToDateTime(reader["Apmokejimo_data"]),
                            ReceiptDate = Convert.ToDateTime(reader["Pateikimo_data"]),
                            Paid = Convert.ToBoolean(reader["Apmoketa"]),
                            CustomerId = Convert.ToInt32(reader["Lankytojas"]),
                            BarId = Convert.ToInt32(reader["Baras"]),
                        });
                    }
                }
                return Ok(bills);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}