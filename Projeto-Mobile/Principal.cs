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

namespace Projeto_Mobile
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
    public class Principal : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_principal);

            Button btnRelatorio = (Button)FindViewById(Resource.Id.btn_relatorio_menu);
            Button btnEntrega = (Button)FindViewById(Resource.Id.btn_entregas_menu);
            ImageButton btnPerfil = (ImageButton)FindViewById(Resource.Id.btn_perfil_pessoal);


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
        }

    }
}