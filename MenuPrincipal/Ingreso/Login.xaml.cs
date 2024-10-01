using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using System.Data;
using System.Data.SqlClient;


namespace MenuPrincipal.Ingreso
{
    /// <summary>
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

    

        #region DECLARACION DE VARIABLES LOCALES
        //Conexion a la DB
        SqlConnection conDB = new SqlConnection(MenuPrincipal.Properties.Settings.Default.conexionDB);

        //variables para SQL Querys
        string consultaSQL = null;

        #endregion

        #region METODOS PERSONALIZADOS
        void EncontrarUsuario()
        {
            //Nos aseguramos que el txtContraseña tenga asignado el valor correspondiente
            if (txtPassword.Visibility == Visibility.Visible)
            {
                txtPassword.Password =txtPassword.Password.Trim();
            }
            else
            {
                txtPassword.Password = txtMostrarContraseña.Text;
            }
            

            int resultado = 0;

            //aperturarBD
            if (conDB.State == ConnectionState.Closed)
            {
                conDB.Open();
                //Cosnulta para buscar usuario
                consultaSQL = null;

                consultaSQL = "SELECT DBO.FNENCONTRARUSUARIO(@User,@Password)";

                SqlCommand sqlCmd = new SqlCommand(consultaSQL, conDB);
                sqlCmd.CommandType = CommandType.Text;
                //enviar valores por paramtetros
                sqlCmd.Parameters.AddWithValue("@User", txtCorreo.Text.Trim());
                sqlCmd.Parameters.AddWithValue("@Password", txtPassword.Password.Trim());

                //ejecutar la consulta sql
                resultado = Convert.ToInt32(sqlCmd.ExecuteScalar());

                MessageBox.Show(resultado.ToString());

                //evaluar el resultado

                if (resultado == 1)
                {

                    MainWindow ventanaPrincipal = new MainWindow();
                    ventanaPrincipal.Show();
                    this.Close();

                }
                else
                {
                    MessageBox.Show("Usuario no encontrado", "error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                //cerrar la base de datos
                conDB.Close();

            }
        }

        #endregion

        private void BtnIngresar_Click(object sender, RoutedEventArgs e)
        {
            EncontrarUsuario();
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                EncontrarUsuario();
            }
           
        }

        private void Mostrar(object sender, RoutedEventArgs e)
        {
            
            txtMostrarContraseña.Text=txtPassword.Password;
            txtPassword.Visibility = Visibility.Collapsed;
            txtMostrarContraseña.Visibility = Visibility.Visible;

        }

        private void Ocultar(object sender, RoutedEventArgs e)
        {
            txtPassword.Password = txtMostrarContraseña.Text;
            txtPassword.Visibility = Visibility.Visible;
            txtMostrarContraseña.Visibility=Visibility.Collapsed;

        }
    }

}
