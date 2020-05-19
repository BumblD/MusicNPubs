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
    public class BarController : ControllerBase
    {

        private AppDb Db { get; set; }

        public BarController(AppDb db)
        {
            Db = db;
        }

        /// <summary>
        /// Gets all events
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllBars")]
        public IActionResult GetAllBars()
        {
            List<BarModel> Bars = new List<BarModel>();

            try
            {
                Db.Connection.Open();
                var cmd = Db.Connection.CreateCommand();
                cmd.CommandText = @"SELECT * FROM Barai";

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Bars.Add(new BarModel()
                        {
                            BarID = Convert.ToInt32(reader["id"]),
                            Name = Convert.ToString(reader["Pavadinimas"]),
                            Lat = Convert.ToString(reader["Platuma"]),
                            Lng = Convert.ToString(reader["Ilguma"]),
                            BankNumber = Convert.ToString(reader["Banko_saskaita"]),
                            Grade = Convert.ToInt32(reader["Ivertinimas"]),
                        });
                    }
                }
                return Ok(Bars);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Gets events for specified bar
        /// </summary>
        /// <param name="id">Bar id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetBars/{id}")]
        public IActionResult GetBars([FromRoute] int id)
        {
            List<BarModel> bars = new List<BarModel>();

            try
            {
                Db.Connection.Open();
                var cmd = Db.Connection.CreateCommand();
                cmd.CommandText = $"SELECT * FROM Barai WHERE id = {id}";

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        bars.Add(new BarModel()
                        {
                            BarID = Convert.ToInt32(reader["id"]),
                            Name = Convert.ToString(reader["Pavadinimas"]),
                            Lat = Convert.ToString(reader["Platuma"]),
                            Lng = Convert.ToString(reader["Ilguma"]),
                            BankNumber = Convert.ToString(reader["Banko_saskaita"]),
                            Grade = Convert.ToInt32(reader["Ivertinimas"]),
                            Description = Convert.ToString(reader["Aprasymas"]),
                        });
                    }
                }
                return Ok(bars);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }



    }
}
