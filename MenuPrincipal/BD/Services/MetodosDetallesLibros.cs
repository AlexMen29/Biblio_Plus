using MenuPrincipal.BD.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using System.Data.SqlClient;

namespace MenuPrincipal.BD.Services
{
    public class MetodosDetallesLibros
    {


        public static List<DetallesLibros> MostrarLibros()
        {
            List<DetallesLibros> lstLibros = new List<DetallesLibros>();
            try
            {
                using (var conn = new SqlConnection(Properties.Settings.Default.conexionDB))
                {
                    conn.Open();

                    using (var command = conn.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "SP_ObtenerDetallesLibros"; // Asegúrate de que este sea el nombre correcto

                        using (DbDataReader dr = command.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                DetallesLibros libro = new DetallesLibros
                                {
                                    Titulo = dr["Titulo"].ToString(),
                                    Autor = dr["Autor"].ToString(),
                                    Editorial = dr["Editorial"].ToString(),
                                    Categoria = dr["Categoria"].ToString(),
                                    Edicion = dr["Edicion"].ToString(), // Asegúrate de que este sea el tipo correcto
                                    StockActual = int.Parse(dr["StockActual"].ToString())
                                };

                                lstLibros.Add(libro);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Ocurrió un error al intentar obtener los libros: " + e.Message, "Validación", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return lstLibros;
        }
        //fin de metodo detalles libros


       


    }
}
