using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace animeApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuariosController : ControllerBase
    {

        private const string ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Animes;Integrated Security=True;Connect Timeout=30;Encrypt=False;";

        [HttpPost]
        public ActionResult<Usuarios> ValidarLogin([FromBody] Usuarios login)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string querry = "SELECT * FROM Usuarios WHERE Nome = @Nome AND Senha = @Senha";
                SqlCommand command = new SqlCommand(querry, connection);
                command.Parameters.AddWithValue("@Nome", login.Nome);
                command.Parameters.AddWithValue("@Senha", login.Senha);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                        Usuarios usuario = new Usuarios();
                        usuario.Senha = reader[2].ToString();
                        usuario.Id = Convert.ToInt16(reader[0]);
                        usuario.Nome = reader[1].ToString();
                        return Ok(usuario);
                    }
                }

            return BadRequest(false);

        }
    }
}