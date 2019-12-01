using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System;
using Projeto_Mobile.Class;

namespace Projeto_Mobile
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class Login : Activity
    {
        Class.Motorista m;
        protected override void OnCreate(Bundle savedInstanceState)
        {
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