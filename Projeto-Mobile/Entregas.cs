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
using Projeto_Mobile.Class;
namespace Projeto_Mobile
{
    [Activity(Label = "Entregas")]
    public class Entregas : Activity
    {
        NotaTransporte notatransporte;
        ItensNotaTransporte itensnotatransporte;
        Motorista m;
        Carga c;
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
        int idUsuariologado;
        //Definindo objetos a serem utilizados
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_entregas);
            var a = BancoLocal.ObterSessaoAberta();
            foreach (var item in a.Result)
            {
                idUsuariologado = item.IdMotorista;
            }
            //Obtendo objetos do layout            
            Button btnSair = (Button)FindViewById(Resource.Id.btn_sairentrege_entregas);
            Button btnFinalizarEntrega = (Button)FindViewById(Resource.Id.btn_finalizarentrege_entregas);
            Spinner spnNotasTransportes = (Spinner)FindViewById(Resource.Id.spn_notatransportes_entregas);
            EditText edtEnderecoDestino = (EditText)FindViewById(Resource.Id.edt_enderecodestino_entregas);
            EditText edtEnderecoRemetente = (EditText)FindViewById(Resource.Id.edt_enderecoremetente_entregas);
            ListView lvCargasNota = (ListView)FindViewById(Resource.Id.lsv_cargas_entregas);

            //Ações
            //Carregando spiner de notas de transportes pendentes

            try
            {
                notatransporte = new NotaTransporte();
                var listaNt = notatransporte.ListandoServicosPendentesMotorista(idUsuariologado);
                List<string> NotasSemEntregas = new List<string>();
                foreach (var item in listaNt)
                {
                    NotasSemEntregas.Add(item.Id.ToString());
                }
                ArrayAdapter adapterNt = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem, NotasSemEntregas);
                spnNotasTransportes.Adapter = adapterNt;                
                spnNotasTransportes.ItemSelected += delegate
                {
                    c = new Carga();
                    lvCargasNota.Adapter = new EntregasAdapter(this, c.ListarCargasNotaTransporte(Convert.ToInt32(spnNotasTransportes.SelectedItem.ToString())));
                    Pedido p = new Pedido();
                    p.consultarPedidNota(Convert.ToInt32(spnNotasTransportes.SelectedItem.ToString()));
                    Endereco endereco = new Endereco();
                    endereco.ConsultarEnderecosPedidoEnderecoDestinatario(p.Id);
                    edtEnderecoDestino.Text = endereco.Cep + ", " + endereco.Logradouro + ", N° " + endereco.Numero;
                    edtEnderecoDestino.Enabled = false;
                    endereco.ConsultarEnderecosPedidoEnderecoRementente(p.Id);
                    edtEnderecoRemetente.Text = endereco.Cep + ", " + endereco.Logradouro + ", N° " + endereco.Numero;
                    edtEnderecoRemetente.Enabled = false;
                };
            }            
            catch(Exception ex)
            {
                Toast.MakeText(this, ex.Message.ToString(), ToastLength.Long).Show();
            }            
        }
    }
}