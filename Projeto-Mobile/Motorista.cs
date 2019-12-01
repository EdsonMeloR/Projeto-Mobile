using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Projeto_Mobile.Class;

namespace Projeto_Mobile
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
    public class Motorista : Activity
    {
        static BancoSqLite bancoLocal;
        public static BancoSqLite BancoLocal
        {
            get
            {
                if (bancoLocal == null)
                {
                    bancoLocal = new BancoSqLite(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), "ProjetoSQLite.db"));
                }
                return bancoLocal;
            }
        }
        Class.Motorista m;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            //Consultando banco SQLITE

            m = new Class.Motorista();
            var a = BancoLocal.ObterListaSessao();
            if (a.Result.Count == 1)
            {
                foreach (var user in a.Result)
                {
                    if (user.Sessao)
                    {
                        m.ConsultarMotorista(user.IdMotorista);
                    }
                }
            }
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_colaborador);
            //Recuperando informações do layout
            Button btnVoltar = (Button)FindViewById(Resource.Id.btn_motor_voltar);
            EditText edtNome = (EditText)FindViewById(Resource.Id.txt_nome);
            EditText edtCpf = (EditText)FindViewById(Resource.Id.txt_cpf);
            EditText edtRg = (EditText)FindViewById(Resource.Id.txt_rg);
            EditText edtCnh = (EditText)FindViewById(Resource.Id.txt_cnh);
            EditText edtCategoria = (EditText)FindViewById(Resource.Id.txt_categoria);
            EditText edtValidadeCnh = (EditText)FindViewById(Resource.Id.txt_validadeCnh);
            //Atribuindo valores ao edits
            edtNome.Text = m.Nome;
            edtCpf.Text = m.Cpf;
            edtRg.Text = m.Rg;
            edtCnh.Text = m.Cnh;
            edtCategoria.Text = m.CategoriaCnh;
            edtValidadeCnh.Text = m.ValidadeCnh.ToString();
            //Eventos
            btnVoltar.Click += delegate
            {
                StartActivity(typeof(Principal));
                Finish();
            };
        }
    }
}