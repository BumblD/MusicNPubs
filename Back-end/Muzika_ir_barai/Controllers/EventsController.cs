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
    public class EventsController : ControllerBase
    {
        private AppDb Db { get; set; }

        public EventsController(AppDb db)
        {
            Db = db;
        }

        /// <summary>
        /// Gets all events
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllEvents")]
        public IActionResult GetAllEvents()
        {
            List<EventModel> events = new List<EventModel>();

            try
            {
                Db.Connection.Open();
                var cmd = Db.Connection.CreateCommand();
                cmd.CommandText = @"SELECT * FROM Renginiai";

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        events.Add(new EventModel()
                        {
                            EventId = Convert.ToInt32(reader["id"]),
                            Name = Convert.ToString(reader["Pavadinimas"]),
                            Date = Convert.ToDateTime(reader["Data"]),
                            Price = Convert.ToDouble(reader["Kaina"]),
                            Description = Convert.ToString(reader["Aprasymas"]),
                            Poster = Convert.ToString(reader["Plakatas"]),
                            BarId = Convert.ToInt32(reader["Baras"])
                        });
                    }
                }
                return Ok(events);
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
        [Route("GetEvents/{id}")]
        public IActionResult GetEvents([FromRoute] int id)
        {
            List<EventModel> events = new List<EventModel>();

            try
            {
                Db.Connection.Open();
                var cmd = Db.Connection.CreateCommand();
                cmd.CommandText = $"SELECT * FROM Renginiai WHERE id = {id}";

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        events.Add(new EventModel()
                        {
                            EventId = Convert.ToInt32(reader["id"]),
                            Name = Convert.ToString(reader["Pavadinimas"]),
                            Date = Convert.ToDateTime(reader["Data"]),
                            Price = Convert.ToDouble(reader["Kaina"]),
                            Description = Convert.ToString(reader["Aprasymas"]),
                            Poster = Convert.ToString(reader["Plakatas"]),
                            BarId = Convert.ToInt32(reader["Baras"])
                        });
                    }
                }
                return Ok(events);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }




        /// <summary>
        /// Gets all events
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllEvents2")]
        public IActionResult GetAllEvents2()
        {
            List<EventModel> events = new List<EventModel>();

            try
            {
                Db.Connection.Open();
                var cmd = Db.Connection.CreateCommand();
                cmd.CommandText = @"SELECT * FROM Event";

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        events.Add(new EventModel()
                        {
                            EventId = Convert.ToInt32(reader["EventId"]),
                            Name = Convert.ToString(reader["Name"]),
                            Date = Convert.ToDateTime(reader["Date"]),
                            Price = Convert.ToDouble(reader["Price"]),
                            Description = Convert.ToString(reader["Description"]),
                            Poster = Convert.ToString(reader["Poster"]),
                            BarId = Convert.ToInt32(reader["BarId"])
                        });
                    }
                }
                return Ok(events);
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
        [Route("GetEvents2/{id}")]
        public IActionResult GetEvents2([FromRoute] int id)
        {
            List<EventModel> events = new List<EventModel>();

            try
            {
                Db.Connection.Open();
                var cmd = Db.Connection.CreateCommand();
                cmd.CommandText = $"SELECT * FROM Event WHERE BarId = {id}";

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        events.Add(new EventModel()
                        {
                            EventId = Convert.ToInt32(reader["EventId"]),
                            Name = Convert.ToString(reader["Name"]),
                            Date = Convert.ToDateTime(reader["Date"]),
                            Price = Convert.ToDouble(reader["Price"]),
                            Description = Convert.ToString(reader["Description"]),
                            Poster = Convert.ToString(reader["Poster"]),
                            BarId = Convert.ToInt32(reader["BarId"])
                        });
                    }
                }
                return Ok(events);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Delete event
        /// </summary>
        /// <param name="id">Event id</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("DeleteEvent/{id}")]
        public IActionResult DeleteEvent([FromRoute] int id)
        {
            try
            {
                Db.Connection.Open();
                var cmd = Db.Connection.CreateCommand();
                cmd.CommandText = $"DELETE Renginiai WHERE id = {id}";
                cmd.ExecuteNonQuery();

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }




        /// <summary>
        /// Delete event
        /// </summary>
        /// <param name="id">Event id</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("DeleteEvent2/{id}")]
        public IActionResult DeleteEvent2([FromRoute] int id)
        {
            try
            {
                Db.Connection.Open();
                var cmd = Db.Connection.CreateCommand();
                cmd.CommandText = $"DELETE Event WHERE EventId = {id}";
                cmd.ExecuteNonQuery();

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }


        /// <summary>
        /// Edit Event
        /// </summary>
        /// <param name="data">Event data</param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateEvent")]
        public IActionResult UpdateEvent([FromBody] EventModel data)
        {
            if (data == null)
                return NoContent();

            if (data.EventId < 1)
                return NotFound();

            if (data.Price < 0)
                return BadRequest();

            try
            {
                Db.Connection.Open();
                var cmd = Db.Connection.CreateCommand();
                cmd.CommandText = $"UPDATE Renginiai " +
                                $"SET Pavadinimas = '{data.Name}', " +
                                    $"Data = '{data.Date}', " +
                                    $"Kaina = {data.Price}, " +
                                    $"Aprasymas = '{data.Description}', " +
                                    $"Plakatas = '{data.Poster}' " +
                                $"WHERE id = {data.EventId}";
                cmd.ExecuteNonQuery();

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Create Event
        /// </summary>
        /// <param name="data">Event data</param>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateEvent")]
        public IActionResult CreateEvent([FromBody] EventModel data)
        {
            if (data == null)
                return NoContent();

            if (data.BarId < 1)
                return NotFound();

            if (data.Price < 0)
                return BadRequest();

            try
            {
                Db.Connection.Open();
                var cmd = Db.Connection.CreateCommand();
                cmd.CommandText = $"INSERT INTO Renginiai(Pavadinimas, Data, Kaina, Aprasymas, Plakatas, Baras) " +
                                $"VALUES('{data.Name}', '{data.Date}', {data.Price}, '{data.Description}', '{data.Poster}', {data.BarId})";
                cmd.ExecuteNonQuery();

                // Send message to user

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Edit Event
        /// </summary>
        /// <param name="data">Event data</param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateEvent2")]
        public IActionResult UpdateEvent2([FromBody] EventModel data)
        {
            if (data == null)
                return NoContent();

            if (data.EventId < 1)
                return NotFound();

            if (data.Price < 0)
                return BadRequest();

            try
            {
                Db.Connection.Open();
                var cmd = Db.Connection.CreateCommand();
                cmd.CommandText = $"UPDATE Event " +
                                $"SET Name = '{data.Name}', " +
                                    $"Date = '{data.Date}', " +
                                    $"Price = {data.Price}, " +
                                    $"Description = '{data.Description}', " +
                                    $"Poster = '{data.Poster}' " +
                                $"WHERE EventId = {data.EventId}";
                cmd.ExecuteNonQuery();

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Create Event
        /// </summary>
        /// <param name="data">Event data</param>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateEvent2")]
        public IActionResult CreateEvent2([FromBody] EventModel data)
        {
            if (data == null)
                return NoContent();

            if (data.BarId < 1)
                return NotFound();

            if (data.Price < 0)
                return BadRequest();

            try
            {
                Db.Connection.Open();
                var cmd = Db.Connection.CreateCommand();
                cmd.CommandText = $"INSERT INTO Event(Name, Date, Price, Description, Poster, BarId) " +
                                $"VALUES('{data.Name}', '{data.Date}', {data.Price}, '{data.Description}', '{data.Poster}', {data.BarId})";
                cmd.ExecuteNonQuery();

                // Send message to user

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}