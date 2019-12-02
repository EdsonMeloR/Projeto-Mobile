using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Controls;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics;
using System.IO;
using static Android.Graphics.Bitmap;
using Projeto_Mobile.Class;

namespace Projeto_Mobile
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
    public class Entrega : Activity
    {
        SessaoMotorista Sm;
        static BancoSqLite bancoLocal;
        public static BancoSqLite BancoLocal
        {
            get
            {
                if (bancoLocal == null)
                {
                    bancoLocal = new BancoSqLite(System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), "ProjetoSQLite.db"));
                }
                return bancoLocal;
            }
        }
        Class.Entrega e;
        int idUsuariologado;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            var a = BancoLocal.ObterSessaoAberta();
            foreach (var item in a.Result)
            {
                idUsuariologado = item.IdMotorista;
            }
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_detentrega);
            //Recuperando informações do layout
            EditText edtRg = (EditText)FindViewById(Resource.Id.edt_rg_entrega);
            EditText edtData = (EditText)FindViewById(Resource.Id.edt_dataentrega_entrega);
            EditText edtStatus = (EditText)FindViewById(Resource.Id.edt_statusentrega_entrega);
            EditText edtIdEntrega = (EditText)FindViewById(Resource.Id.edt_identrega_entrega);
            SignaturePadView assinatura = (SignaturePadView)FindViewById(Resource.Id.assinatura_entrega);
            Button btnNovaEntrega = (Button)FindViewById(Resource.Id.btn_gerarentrega_entrega);
            Button btnSair = (Button)FindViewById(Resource.Id.btn_saur_entrega);
            Spinner spnEntregas = (Spinner)FindViewById(Resource.Id.spn_entregas);
            assinatura.SetBackgroundColor(Color.White);
            assinatura.StrokeColor = Color.Black;
            btnSair.Click += delegate
            {
                StartActivity(typeof(Principal));
                Finish();
            };
            try
            {                
                //Carregando spiner de Notas de transportes pendentes
                NotaTransporte nt = new NotaTransporte();
                var listaNt = nt.ListandoServicosPendentesMotorista(idUsuariologado);
                List<string> NotasSemEntregas = new List<string>();
                foreach (var item in listaNt)
                {
                    NotasSemEntregas.Add(item.Id.ToString());
                }                
                
                ArrayAdapter adapterNt = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem, NotasSemEntregas);
                spnEntregas.Adapter = adapterNt;
            }
            catch(Exception ex)
            {
                Toast.MakeText(this, ex.Message.ToString(), ToastLength.Long).Show();
            }            
            spnEntregas.ItemSelected += delegate
            {
                Toast.MakeText(this, "Nota " + spnEntregas.SelectedItem.ToString(), ToastLength.Long).Show();
            };
            btnNovaEntrega.Click += delegate
            {
                try
                {
                    if (edtRg.Text == string.Empty)
                        throw new Exception("Preencha o campo RG");
                    if (edtStatus.Text == string.Empty)
                        throw new Exception("Preencha o campo Status");
                    e = new Class.Entrega();
                    var assBitMap = assinatura.GetImage();
                    var imagem = ImageToByteArray(assBitMap);
                    var itemSelecionado = Convert.ToInt32(spnEntregas.SelectedItem.ToString());
                    e.InserirEntrega(imagem, edtRg.Text, edtStatus.Text, itemSelecionado);
                    if (e.Id > 0)
                    {
                        Toast.MakeText(this, "Inserido com sucesso !!", ToastLength.Long).Show();
                        edtIdEntrega.Text = e.Id.ToString();                        
                    }                        
                    else
                        Toast.MakeText(this, "Erro ao inserir !!", ToastLength.Long).Show();
                }
                catch(Exception ex)
                {
                    Toast.MakeText(this, ex.Message.ToString(), ToastLength.Long).Show();
                }                
            };
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        private byte[] ImageToByteArray(Bitmap image)
        {
            if (image == null) return null;

            byte[] result;

            using (var stream = new MemoryStream())
            {                
                image.Compress(CompressFormat.Png, 100, stream);
                result = stream.ToArray();
                stream.Flush();
            }
            return result;
        }
    }
}