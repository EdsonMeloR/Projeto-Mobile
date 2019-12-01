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
    public class Principal : Activity
    {
        SessaoMotorista Sm;
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
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_principal);

            Button btnRelatorio = (Button)FindViewById(Resource.Id.btn_relatorio_menu);
            Button btnEntrega = (Button)FindViewById(Resource.Id.btn_entregas_menu);
            ImageButton btnPerfil = (ImageButton)FindViewById(Resource.Id.btn_perfil_pessoal);
            Button btnDesconectar = (Button)FindViewById(Resource.Id.btn_desconectar_menu);


            btnRelatorio.Click += delegate
            {
                StartActivity(typeof(Relatorio));
            };

            btnEntrega.Click += delegate
            {
                StartActivity(typeof(Entrega));
            };

            btnPerfil.Click += delegate
            {
                StartActivity(typeof(Motorista));
            };
            btnDesconectar.Click += delegate
            {
                try
                {
                    var a = BancoLocal.ObterListaSessao();
                    if (a.Result.Count == 1)
                    {
                        foreach (var user in a.Result)
                        {
                            if (user.Sessao)
                            {
                                Sm = new SessaoMotorista();
                                Sm.Id = 1;
                                Sm.IdMotorista = 0;
                                Sm.Nome = "";
                                Sm.Sessao = false;
                                BancoLocal.AtualizarSessao(Sm);
                                StartActivity(typeof(Login));
                                Toast.MakeText(this, "Desconectado com sucesso", ToastLength.Long).Show();
                                Finish();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Toast.MakeText(this, ex.Message.ToString(), ToastLength.Long).Show();
                }
            };
        }
    }
}