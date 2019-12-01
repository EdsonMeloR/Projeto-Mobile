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
        MySqlConnectionStringBuilder Builder = new MySqlConnectionStringBuilder();

        public Banco ()
        { }
        public MySqlCommand AbrirConexao()
        {            
            Builder.Database = "softklee_prosperity";
            Builder.Server = "softkleen.com.br";
            Builder.Port = 3306;
            Builder.UserID = "softklee_prosperity";
            Builder.Password = "senac@prosp";
            MySqlCommand comm;
            try
            {
                var cn = new MySqlConnection(Builder.ToString());
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