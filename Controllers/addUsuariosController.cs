using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace animeApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class addUsuariosController : ControllerBase
    {

        private const string ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Animes;Integrated Security=True;Connect Timeout=30;Encrypt=False;";
        [HttpPost]
        public ActionResult novoUsuario([FromBody] Usuarios usuario)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string querry = "INSERT INTO Usuarios (Nome, Senha) VALUES (@Nome, @Senha)";
                SqlCommand command = new SqlCommand(querry, connection);
                command.Parameters.AddWithValue("@Nome", usuario.Nome);
                command.Parameters.AddWithValue("@Senha", usuario.Senha);
                connection.Open();

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    return Ok();
                }
            }

            return BadRequest();
        }
    }
}