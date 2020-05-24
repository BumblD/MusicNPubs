using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Muzika_ir_barai.Models;

namespace Muzika_ir_barai.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemController : ControllerBase
    {
        private AppDb Db { get; set; }        

        public SystemController(AppDb db)
        {
            Db = db;
        }

        public IntervalModel GetInterval([FromRoute] int id)
        {
            IntervalModel interval = new IntervalModel();

            try
            {
                Db.Connection.Open();
                var cmd = Db.Connection.CreateCommand();
                cmd.CommandText = $"SELECT TOP 1 * FROM LaikuIntervalai WHERE id = {id}";

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        interval = new IntervalModel()
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            BarId = Convert.ToInt32(reader["Baras"]),
                            Start = Convert.ToDateTime(reader["Pradzia"]),
                            End = Convert.ToDateTime(reader["Pabaiga"]),
                        };
                    }
                }

                return interval;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public SongModel GetRandomSong()
        {
            SongModel song = new SongModel();

            try
            {
                Db.Connection.Open();
                var cmd = Db.Connection.CreateCommand();
                cmd.CommandText = $"SELECT TOP 1 * FROM Dainos ORDER BY NEWID()";

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        song = new SongModel()
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Name = Convert.ToString(reader["Pavadinimas"]),
                            Author = Convert.ToString(reader["Atlikejas"]),
                            ListeningCount = Convert.ToInt32(reader["Klausymu_kiekis"]),
                            Rating = Convert.ToInt32(reader["Ivertinimas"]),
                        };
                    }
                    reader.Close();
                }

                cmd.CommandText = $"UPDATE Dainos SET Klausymu_kiekis = Klausymu_kiekis + 1 WHERE id = {song.Id}";
                cmd.ExecuteNonQuery();

                return song;
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpPost]
        [Route("PlayRandomSongsInInterval/{id}")]
        public IActionResult PlayRandomSongsInInterval([FromRoute] int id)
        {
            var interval = GetInterval(id);
            int songLength = 0;

            while (interval.Start >= DateTime.Now && interval.End < DateTime.Now)
            {
                var song = GetRandomSong();
                songLength = song.Length ?? 0;

                Thread.Sleep(songLength * 1000);
            }

            return Ok();
        }

        [HttpPost]
        [Route("FormPopularSongsList")]
        public IActionResult FormPopularSongsList()
        {
            try
            {
                Db.Connection.Open();
                var cmd = Db.Connection.CreateCommand();
                cmd.CommandText = $"TRUNCATE TABLE PopuliariausiosDainos";
                cmd.ExecuteNonQuery();

                cmd.CommandText = $"INSERT INTO PopuliariausiosDainos " +
                    $"SELECT TOP 10 d.id " +
                    $"FROM DainosGrojarasciai dg " +
                    $"JOIN Dainos d on d.id = dg.Daina " +
                    $"GROUP BY d.id, d.Klausymu_kiekis " +
                    $"ORDER BY d.Klausymu_kiekis, COUNT(*) DESC";
                cmd.ExecuteNonQuery();

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [Route("GetTop10")]
        public IActionResult GetTop10()
        {
            List<SongModel> songs = new List<SongModel>();

            try
            {
                Db.Connection.Open();
                var cmd = Db.Connection.CreateCommand();
                cmd.CommandText = $"SELECT d.* FROM PopuliariausiosDainos pd JOIN Dainos d ON d.id = pd.Daina";

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        songs.Add(new SongModel()
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Name = Convert.ToString(reader["Pavadinimas"]),
                            Author = Convert.ToString(reader["Atlikejas"]),
                            ListeningCount = Convert.ToInt32(reader["Klausymu_kiekis"]),
                            Rating = Convert.ToInt32(reader["Ivertinimas"]),
                        });
                    }
                }
                return Ok(songs);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [Route("GetClients")]
        public IActionResult GetClients()
        {
            List<ClientModel> clients = new List<ClientModel>();

            try
            {
                Db.Connection.Open();
                var cmd = Db.Connection.CreateCommand();
                cmd.CommandText = $"SELECT l.*, n.Prisijungimo_Vardas FROM Lankytojai l JOIN Naudotojai n ON n.id = l.id";

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        clients.Add(new ClientModel()
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Username = Convert.ToString(reader["Prisijungimo_vardas"]),
                            IsBlocked = Convert.ToBoolean(reader["Blokuotas"]),
                            WaitTime = Convert.ToInt32(reader["Laukimo_laikas"]),
                        });
                    }
                }
                return Ok(clients);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}