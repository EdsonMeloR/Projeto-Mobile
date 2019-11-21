using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MySql.Data.MySqlClient;

namespace Projeto_Mobile.Class
{
    public class Banco
    {
        MySqlConnectionStringBuilder stringBuilder = new MySqlConnectionStringBuilder();

        public Banco()//Método Construtor
        { }
        public MySqlCommand AbrirConexao()//Método abre conexão com banco de dados
        {
            stringBuilder.Database = "prosperitydb";
            stringBuilder.Server = "localhost";
            stringBuilder.Port = 3306;
            stringBuilder.UserID = "root";
            MySqlCommand comm;
            try
            {
                var cn = new MySqlConnection(stringBuilder.ToString());
                cn.Open();
                comm = new MySqlCommand
                {
                    Connection = cn
                };
                return comm;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return null;
            }
        }
    }
}