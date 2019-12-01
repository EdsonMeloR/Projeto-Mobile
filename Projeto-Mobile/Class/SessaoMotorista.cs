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
using SQLite;

namespace Projeto_Mobile.Class
{
    public class SessaoMotorista
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int IdMotorista { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public bool Sessao { get; set; }
    }
}