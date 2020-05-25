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
    public class PlaylistController : ControllerBase
    {
        private AppDb Db { get; set; }

        public PlaylistController(AppDb db)
        {
            Db = db;
        }

        /// <summary>
        /// Gets playlists for specified bar
        /// </summary>
        /// <param name="id">Bar id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetBarPlaylists/{id}")]
        public IActionResult GetBarPlaylists([FromRoute] int id)
        {
            List<PlaylistModel> playlists = new List<PlaylistModel>();

            try
            {
                Db.Connection.Open();
                var cmd = Db.Connection.CreateCommand();
                cmd.CommandText = $"SELECT * FROM Grojarasciai WHERE Baras = {id}";

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        playlists.Add(new PlaylistModel()
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Name = Convert.ToString(reader["Pavadinimas"]),
                            BarId = Convert.ToInt32(reader["Baras"]),
                        });
                    }
                }
                return Ok(playlists);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Gets songs for specified playlist
        /// </summary>
        /// <param name="id">Playlist id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetPlaylistSongs/{id}")]
        public IActionResult GetPlaylistSongs([FromRoute] int id)
        {
            List<SongModel> songs = new List<SongModel>();

            try
            {
                Db.Connection.Open();
                var cmd = Db.Connection.CreateCommand();
                cmd.CommandText = $"SELECT d.* " +
                                $"FROM DainosGrojarasciai dg " +
                                $"JOIN Dainos d ON d.id = dg.Daina " +
                                $"WHERE dg.Grojarastis = {id}";

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


        [HttpPost]
        [Route("{playlist}/BlockSong/{id}")]
        public IActionResult BlockSong([FromRoute] int playlist, [FromRoute] int id)
        {
            if (playlist < 0)
                return BadRequest();

            if (id < 0)
                return BadRequest();

            try
            {
                Db.Connection.Open();
                var cmd = Db.Connection.CreateCommand();
                cmd.CommandText = $"INSERT INTO BlokuotosDainos(Daina, Baras) " +
                    $"SELECT dg.Daina, g.Baras " +
                    $"FROM DainosGrojarasciai dg " +
                    $"JOIN Grojarasciai g ON g.id = dg.Grojarastis " +
                    $"WHERE dg.Daina = {id} AND dg.Grojarastis = {playlist}";
                cmd.ExecuteNonQuery();

                cmd.CommandText = $"DELETE DainosGrojarasciai WHERE Daina = {id} AND Grojarastis = {playlist}";
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
                cmd.CommandText = $"SELECT DISTINCT TOP 10 * FROM Dainos ORDER BY Dainos.Klausymu_kiekis desc";
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
        [Route("GetTopTen/{id}")]
        public IActionResult GetTopTen([FromRoute] int id)
        {
            List<SongModel> songs = new List<SongModel>();

            try
            {
                Db.Connection.Open();
                var cmd = Db.Connection.CreateCommand();
                cmd.CommandText = $"SELECT DISTINCT TOP 10 d.* " +
                    $"FROM DainosGrojarasciai dg " +
                    $"JOIN Dainos d ON d.id = dg.Daina " +
                    $"JOIN Grojarasciai g on g.id = dg.Grojarastis " +
                    $"WHERE g.Baras = {id} " +
                    $"ORDER BY d.Ivertinimas DESC";

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

        [HttpPost]
        [Route("{song}/AddToPlaylist/{id}")]
        public IActionResult AddToPlaylist([FromRoute] int song, [FromRoute] int id)
        {
            if (song < 0)
                return BadRequest();

            if (id < 0)
                return BadRequest();

            try
            {
                Db.Connection.Open();
                var cmd = Db.Connection.CreateCommand();
                cmd.CommandText = $"SELECT * FROM DainosGrojarasciai " +
                            $" WHERE Daina = {song} AND Grojarastis = {id}";
                var reader = cmd.ExecuteReader();

                // song already exists in playlist
                if (reader.HasRows)
                    return BadRequest();
                reader.Close();

                cmd.CommandText = $"INSERT INTO DainosGrojarasciai VALUES ({song}, {id})";
                cmd.ExecuteNonQuery();

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        [Route("{song}/RemoveToPlaylist/{id}")]
        public IActionResult RemoveToPlaylist([FromRoute] int song, [FromRoute] int id)
        {
            if (song < 0)
                return BadRequest();

            if (id < 0)
                return BadRequest();

            try
            {
                Db.Connection.Open();
                var cmd = Db.Connection.CreateCommand();
                cmd.CommandText = $"delete FROM DainosGrojarasciai " +
                            $" WHERE Daina = {song} AND Grojarastis = {id}";
        
                cmd.ExecuteNonQuery();

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }


        [HttpGet]
        [Route("SearchSongs")]
        public IActionResult SearchSongs([FromBody] string query)
        {
            List<SongModel> songs = new List<SongModel>();

            try
            {
                Db.Connection.Open();
                var cmd = Db.Connection.CreateCommand();
                cmd.CommandText = $"SELECT * FROM Dainos d " +
                    $"WHERE d.Pavadinimas LIKE '%{query}%' " +
                    $"OR d.Atlikejas like '%{query}%'";

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
        [Route("GetBlockedSongs/{id}")]
        public IActionResult GetBlockedSongs([FromRoute] int id)
        {
            List<int> songIds = new List<int>();

            try
            {
                Db.Connection.Open();
                var cmd = Db.Connection.CreateCommand();
                cmd.CommandText = $"SELECT * FROM BlokuotosDainos WHERE Baras = {id}";

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        songIds.Add(Convert.ToInt32(reader["Daina"]));
                }
                return Ok(songIds);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [Route("RemoveBlockedSongs")]
        public IActionResult RemoveBlockedSongs([FromBody] RemoveModel data)
        {
            try
            {
                var songs = data.Songs.Where(x => !data.BlockedIds.Contains(x.Id)).Take(10).ToList();
                return Ok(songs);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [Route("{playlist}/NextSong/{id}")]
        public IActionResult NextSong([FromRoute] int playlist, [FromRoute] int id)
        {
            SongModel song = new SongModel();

            try
            {
                Db.Connection.Open();
                var cmd = Db.Connection.CreateCommand();
                cmd.CommandText = $"Select TOP 1 d.* " +
                    $"FROM DainosGrojarasciai dg " +
                    $"JOIN Dainos d on d.id = dg.Daina " +
                    $"WHERE dg.Grojarastis = {playlist} AND dg.Daina > {id} " +
                    $"ORDER BY dg.Daina";

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

                return Ok(song);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [Route("{playlist}/FirstSong")]
        public IActionResult FirstSong([FromRoute] int playlist)
        {
            SongModel song = new SongModel();

            try
            {
                Db.Connection.Open();
                var cmd = Db.Connection.CreateCommand();
                cmd.CommandText = $"Select TOP 1 d.* " +
                    $"FROM DainosGrojarasciai dg " +
                    $"JOIN Dainos d on d.id = dg.Daina " +
                    $"WHERE dg.Grojarastis = {playlist} " +
                    $"ORDER BY dg.Daina";

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

                return Ok(song);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        [Route("CreatePlaylist/{id}")]
        public IActionResult CreatePlaylist([FromRoute] int id, [FromBody] PlaylistModel playlist)
        {
            if (string.IsNullOrWhiteSpace(playlist.Name))
                return BadRequest();

            if (id < 1)
                return BadRequest();

            try
            {
                Db.Connection.Open();
                var cmd = Db.Connection.CreateCommand();
                cmd.CommandText = $"SELECT* FROM Grojarasciai WHERE Baras = {id} AND Pavadinimas = '{playlist.Name}'";
                var reader = cmd.ExecuteReader();

                // identical playlist already exists
                if (reader.HasRows)
                    return BadRequest();
                reader.Close();

                cmd.CommandText = $"INSERT INTO Grojarasciai (Pavadinimas, id, Baras) " +
                                  $"VALUES ('{playlist.Name}', (SELECT MAX(id) + 1 FROM Grojarasciai), {id})";
                cmd.ExecuteNonQuery();

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [Route("GetIntervals/{id}")]
        public IActionResult GetIntervals([FromRoute] int id)
        {
            List<IntervalModel> intervals = new List<IntervalModel>();

            try
            {
                Db.Connection.Open();
                var cmd = Db.Connection.CreateCommand();
                cmd.CommandText = $"SELECT * FROM LaikuIntervalai WHERE Baras = {id}";

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        intervals.Add(new IntervalModel()
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            BarId = Convert.ToInt32(reader["Baras"]),
                            Start = Convert.ToDateTime(reader["Pradzia"]),
                            End = Convert.ToDateTime(reader["Pabaiga"]),
                        });
                    }
                }
                return Ok(intervals);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }


        /// <summary>
        /// Gets playlists for specified bar
        /// </summary>
        /// <param name="id">Bar id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetSongs")]
        public IActionResult GetSongs()
        {
            List<SongModel> songs = new List<SongModel>();

            try
            {
                Db.Connection.Open();
                var cmd = Db.Connection.CreateCommand();
                cmd.CommandText = $"SELECT * FROM Dainos";

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


    }
}