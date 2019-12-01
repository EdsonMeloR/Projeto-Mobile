using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System;
using Projeto_Mobile.Class;
using System.IO;

namespace Projeto_Mobile
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class Login : Activity
    {
        Class.Motorista m;
        static BancoSqLite bancoLocal;
        static SessaoMotorista Sm;
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
        protected override void OnCreate(Bundle savedInstanceState)
        {
            var a = BancoLocal.ObterListaSessao();
            if (a.Result.Count == 1)
            {
                foreach (var user in a.Result)
                {
                    if (user.Sessao)
                    {                        
                        StartActivity(typeof(Principal));
                        Toast.MakeText(this, "Bem vindo " + user.Nome, ToastLength.Long).Show();
                        Finish();
                    }                    
                }
            }
            else
            {
                Sm = new SessaoMotorista { Sessao = false };
                BancoLocal.InserirSessao(Sm);
            }
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_login);

            Button btnEntrar = (Button)FindViewById(Resource.Id.btn_login_entrar);
            EditText edtCpf = (EditText)FindViewById(Resource.Id.edt_login_main);
            EditText edtSenha = (EditText)FindViewById(Resource.Id.edt_senha_main);
            btnEntrar.Click += delegate
            {
                try
                {
                    if (edtCpf.Text == string.Empty)
                    {
                        throw new Exception("É necessário colocar o cpf");
                    }                        
                    if (edtSenha.Text == string.Empty)
                    {
                        throw new Exception("É necessário colocar a senha");
                    }
                    m = new Class.Motorista();
                    m.EfetuarLogin(edtCpf.Text, edtSenha.Text);                    
                    if(m.IdMotorista > 0 )
                    {
                        StartActivity(typeof(Principal));
                        Toast.MakeText(this, "Bem vindo "+m.Nome, ToastLength.Long).Show();
                        Sm = new SessaoMotorista();
                        Sm.Id = m.IdMotorista;
                        Sm.IdMotorista = m.IdMotorista;
                        Sm.Cpf = m.Cpf;
                        Sm.Nome = m.Nome;
                        Sm.Sessao = true;
                        BancoLocal.AtualizarSessao(Sm);
                        Finish();
                    }
                    else
                    {
                        Toast.MakeText(this, "Senha ou Cpf incorreto, tente novamente", ToastLength.Long).Show();
                    }
                }
                catch(Exception ex)
                {
                    Toast.MakeText(this,ex.Message.ToString(), ToastLength.Long).Show();
                }                
            };
        }


    }
}