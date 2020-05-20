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
                            SongId = Convert.ToInt32(reader["Daina"]),
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
                cmd.CommandText = $"SELECT * FROM DainosGrojarasciai WHERE Grojarastis = {id}";

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
        public IActionResult CreateEvent([FromRoute] int playlist, [FromRoute] int id)
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
                    $"SELECT dg.Daina, g.Baras" +
                    $"FROM DainosGrojarasciai dg" +
                    $"JOIN Grojarasciai g ON g.id = dg.Grojarastis" +
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
    }
}