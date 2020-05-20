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
    }
}