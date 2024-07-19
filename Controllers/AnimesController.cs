using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace animeApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnimesController : Controller
    {

        private const string ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Animes;Integrated Security=True;Connect Timeout=30;Encrypt=False;";

        [HttpGet(Name = "GetEmpregados")]

        public IEnumerable<Animes> Get()
        {
            List<Animes> animes = new List<Animes>();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string querry = "SELECT * FROM animes";
                SqlCommand command = new SqlCommand(querry, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Animes anime = new Animes()
                    {
                        Id = reader.GetInt32(0),
                        Nome = reader.GetString(1),
                        Descricao = reader.GetString(2),
                        Nota = reader.GetInt32(3)
                    };

                    animes.Add(anime);
                }

                reader.Close();

            }

            return animes;
        }

        [HttpGet("{idUsuario}", Name = "GetAnimeById")]

        public IEnumerable<Animes> GetAnimeById(int idUsuario)
        {
            List<Animes> animes = new List<Animes>();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string querry = "SELECT * FROM animes WHERE id_usuario = @idUsuario";
                SqlCommand command = new SqlCommand(querry, connection);
                command.Parameters.AddWithValue("@idUsuario", idUsuario);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Animes anime = new Animes()
                    {
                        Id = reader.GetInt32(0),
                        Nome = reader.GetString(1),
                        Descricao = reader.GetString(2),
                        Nota = reader.GetInt32(3)
                    };

                    animes.Add(anime);
                }

                reader.Close();

            }

            return animes;
        }

        [HttpPost]

        public ActionResult CreateAnime(Animes anime)
        {
            using SqlConnection connection = new SqlConnection(ConnectionString);
            {
                string querry = "INSERT INTO animes (nome, descricao, nota, id_usuario) VALUES (@nome, @descricao, @nota, @idUsuario)";
                SqlCommand command = new SqlCommand(querry, connection);
                command.Parameters.AddWithValue("@nome", anime.Nome);
                command.Parameters.AddWithValue("@descricao", anime.Descricao);
                command.Parameters.AddWithValue("@nota", anime.Nota);
                command.Parameters.AddWithValue("@idUsuario", anime.idUsuario);
                connection.Open();

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    return Ok();
                }
            }

            return BadRequest();
        }

        [HttpPut("{Id}")]
        //[HttpPut]

        public ActionResult UpdateEmpregado(int Id, [FromBody] Animes anime)
        {
            using SqlConnection connection = new SqlConnection(ConnectionString);
            {
                string querry = "UPDATE animes SET nome = @nome, descricao = @descricao, nota = @nota WHERE Id = @Id";
                SqlCommand command = new SqlCommand(querry, connection);
                command.Parameters.AddWithValue("@nome", anime.Nome);
                command.Parameters.AddWithValue("@descricao", anime.Descricao);
                command.Parameters.AddWithValue("@nota", anime.Nota);
                command.Parameters.AddWithValue("@Id", Id);
                connection.Open();

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    return Ok();
                }
                return NotFound();
            }
        }

        [HttpDelete("{Id}")]

        public ActionResult DeleteEmpregado(int Id)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string querry = "DELETE FROM animes WHERE Id = @Id";
                SqlCommand command = new SqlCommand(querry, connection);
                command.Parameters.AddWithValue("@Id", Id);
                connection.Open();

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    return Ok();
                }
            }

            return NotFound();

        }
    }
}
