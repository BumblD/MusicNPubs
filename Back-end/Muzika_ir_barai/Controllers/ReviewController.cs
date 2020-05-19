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
    public class ReviewController : ControllerBase
    {
        private AppDb Db { get; set; }

        public ReviewController(AppDb db)
        {
            Db = db;
        }

        /// <summary>
        /// Gets all events
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllReviews/{id}")]
        public IActionResult GetAllReviews([FromRoute] int id)
        {
            List<ReviewModel> Reviews = new List<ReviewModel>();

            try
            {
                Db.Connection.Open();
                var cmd = Db.Connection.CreateCommand();
                cmd.CommandText = $"SELECT * FROM Atsiliepimai WHERE Baras = {id}";

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Reviews.Add(new ReviewModel()
                        {
                            BarID = Convert.ToInt32(reader["baras"]),
                            UserID = Convert.ToInt32(reader["Lankytojas"]),
                            ReviewID = Convert.ToInt32(reader["id"]),
                            Date = Convert.ToDateTime(reader["Data"]),
                            Review = Convert.ToString(reader["Atsiliepimas"]),
                            Grade = Convert.ToInt32(reader["Ivertinimas"]),
                        });
                    }
                }
                return Ok(Reviews);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
