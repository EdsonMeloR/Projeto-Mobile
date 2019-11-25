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
    public class PedidosEnderecos
    {
        //Atributos
        private int id;
        private Endereco idEndereco;
        private Pedido idPedido;
        private TipoEndereco idTipoEndereco;
        Banco db;
        //Propiedades
        public int Id { get => id; set => id = value; }
        public Endereco IdEndereco { get => idEndereco; set => idEndereco = value; }
        public Pedido IdPedido { get => idPedido; set => idPedido = value; }
        public TipoEndereco IdTipoEndereco { get => idTipoEndereco; set => idTipoEndereco = value; }
        //Métodos Construtores
        public PedidosEnderecos(int id, Endereco idEndereco, Pedido idPedido, TipoEndereco idTipoEndereco)
        {
            this.id = id;
            this.idEndereco = idEndereco;
            this.idPedido = idPedido;
            this.idTipoEndereco = idTipoEndereco;
        }
        public PedidosEnderecos()
        { }
        //Métodos
        /// <summary>
        /// Inserindo Endereco do remetente e destinatario ao pedido
        /// </summary>       
        public void InserirPedidoEndereco(int _idEndereco, int _idPedido, int _idTipoEndereco)
        {
            db = new Banco();
            try
            {
                var comm = db.AbrirConexao();
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.CommandText = "insert_pedidosenderecos";
                comm.Parameters.Add("_idEndereco", MySqlDbType.Int32).Value = _idEndereco;
                comm.Parameters.Add("_idPedido", MySqlDbType.Int32).Value = _idPedido;
                comm.Parameters.Add("_idTipoEndereco", MySqlDbType.Int32).Value = _idTipoEndereco;
                this.Id = Convert.ToInt32(comm.ExecuteScalar());
            }
            catch (Exception e)
            {
                e.Message.ToString();
            }
        }
        /// <summary>
        /// Alterando Pedido Endereco
        /// </summary>      
        public void AlterarPedidoEndereco(int _idEndereco, int _idTipoEndereco, int _id)
        {
            db = new Banco();
            try
            {
                var comm = db.AbrirConexao();
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.CommandText = "update_pedidosenderecos";
                comm.Parameters.Add("_idEndereco", MySqlDbType.Int32).Value = _idEndereco;
                comm.Parameters.Add("_idTipoEndereco", MySqlDbType.Int32).Value = _idTipoEndereco;
                comm.Parameters.Add("_id", MySqlDbType.Int32).Value = _id;
                comm.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                e.Message.ToString();
            }
        }
        /// <summary>
        /// Listando Enderecos dos pedidos com Inner Join
        /// </summary>        
        public List<PedidosEnderecos> ListarPedidoEndereco()
        {
            db = new Banco();
            PedidosEnderecos pedend;
            List<PedidosEnderecos> lista = new List<PedidosEnderecos>();
            try
            {
                var comm = db.AbrirConexao();
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.CommandText = "view_listar_pedidosenderecos";
                var dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    pedend = new PedidosEnderecos();
                    pedend.Id = dr.GetInt32(0);
                    pedend.IdEndereco.Logradouro = dr.GetString(1);
                    pedend.IdEndereco.Cep = dr.GetString(2);
                    pedend.IdEndereco.Numero = Convert.ToInt32(dr.GetString(3));
                    pedend.IdEndereco.IdCliente.RazaoSocial = dr.GetString(4);
                    pedend.IdPedido.Situacao = dr.GetString(5);
                    lista.Add(pedend);
                }
                return lista;
            }
            catch (Exception e)
            {
                e.Message.ToString();
                return null;
            }
        }
        /// <summary>
        /// Consultando Pedido Endereco
        /// </summary>        
        public void ConsultarPedidoEndereco(int _idEndereco, int _idPedido)
        {
            db = new Banco();
            try
            {
                var comm = db.AbrirConexao();
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.CommandText = "view_listar_pedidosenderecos";
                comm.Parameters.Add("_idEndereco", MySqlDbType.Int32).Value = _idEndereco;
                comm.Parameters.Add("_idPedido", MySqlDbType.Int32).Value = _idPedido;
                var dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    this.Id = dr.GetInt32(0);
                    this.IdEndereco.Logradouro = dr.GetString(1);
                    this.IdEndereco.Cep = dr.GetString(2);
                    this.IdEndereco.Numero = Convert.ToInt32(dr.GetString(3));
                    this.IdEndereco.IdCliente.RazaoSocial = dr.GetString(4);
                    this.IdPedido.Situacao = dr.GetString(5);
                }
            }
            catch (Exception e)
            {
                e.Message.ToString();
            }
        }
    }
}
