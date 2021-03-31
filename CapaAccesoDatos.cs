using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoContactos
{
    public class CapaAccesoDatos
    {
        private SqlConnection conn = new SqlConnection("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=ProyectoContactos;Data Source=DESKTOP-R97C2DG\\SQLEXPRESS");

        public void InsertContacto(Agenda agenda)
        {
            try
            {
                conn.Open();
                string query = @"
                                  INSERT INTO Contactos (Nombre, Apellido, Telefono, Direccion)
                                  VALUES (@Nombre, @Apellido, @Telefono, @Direccion) ";

                SqlParameter nombre = new SqlParameter();
                nombre.ParameterName = "@Nombre";
                nombre.Value = agenda.Nombre;
                nombre.DbType = System.Data.DbType.String;

                SqlParameter apellido = new SqlParameter("@Apellido", agenda.Apellido);
                SqlParameter telefono = new SqlParameter("@Telefono", agenda.Telefono);
                SqlParameter direccion = new SqlParameter("@Direccion", agenda.Direccion);

                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.Add(nombre);
                command.Parameters.Add(apellido);
                command.Parameters.Add(telefono);
                command.Parameters.Add(direccion);

                command.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public void UpdateContacto(Agenda agenda)
        {
            try
            {
                conn.Open();
                string query = @" UPDATE Contactos

                               SET   Nombre = @Nombre,
                                   Apellido = @Apellido,
                                   Telefono = @Telefono,
                                  Direccion = @Direccion

                               WHERE Id = @Id";
                SqlParameter id = new SqlParameter("@Id", agenda.Id);
                SqlParameter nombre = new SqlParameter("@Nombre", agenda.Nombre);
                SqlParameter apellido = new SqlParameter("@Apellido", agenda.Apellido);
                SqlParameter telefono = new SqlParameter("@Telefono", agenda.Telefono);
                SqlParameter direccion = new SqlParameter("@Direccion", agenda.Direccion);

                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.Add(id);
                command.Parameters.Add(nombre);
                command.Parameters.Add(apellido);
                command.Parameters.Add(telefono);
                command.Parameters.Add(direccion);

                command.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }
        }


        public void EliminarContacto(int Id)
        {
            try
            {
                conn.Open();
                string query = @"DELETE FROM Contactos WHERE Id = @Id";

                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.Add(new SqlParameter("@Id", Id));

                command.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public List<Agenda> GetAgendas(string buscar = null)
        {
            List<Agenda> agendas = new List<Agenda>();
            try
            {
                conn.Open();
                string query = @"SELECT Id, Nombre, Apellido, Telefono, Direccion
                                 FROM Contactos";

                SqlCommand command = new SqlCommand();

                if (!string.IsNullOrEmpty(buscar))
                {
                    query += @" WHERE Nombre Like @Search OR Apellido LIKE @Search OR Telefono LIKE @Search OR 
                                Direccion LIKE @Search";
                    command.Parameters.Add(new SqlParameter("@Search", $"%{buscar}%"));
                }


                command.CommandText = query;
                command.Connection = conn;


                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    agendas.Add(new Agenda
                    {
                        Id = int.Parse(reader["Id"].ToString()),
                        Nombre = reader["Nombre"].ToString(),
                        Apellido = reader["Apellido"].ToString(),
                        Telefono = reader["Telefono"].ToString(),
                        Direccion = reader["Direccion"].ToString()
                    });
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }
            return agendas;
        }
    }
}
