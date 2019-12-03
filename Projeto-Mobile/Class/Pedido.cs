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
    public class Pedido
    {
        //Atributos
        private int id;
        private string situacao;
        private DateTime dataPedido;
        private bool retirar;
        private Usuario idUsuario;
        private Cliente idCliente;
        Banco db;
        //Propiedades
        public int Id { get => id; set => id = value; }
        public string Situacao { get => situacao; set => situacao = value; }
        public DateTime DataPedido { get => dataPedido; set => dataPedido = value; }
        public bool Retirar { get => retirar; set => retirar = value; }
        public Usuario IdUsuario { get => idUsuario; set => idUsuario = value; }
        public Cliente IdCliente { get => idCliente; set => idCliente = value; }
        //Métodos construtores
        public Pedido(int id, string situacao, DateTime dataPedido, bool retirar, Usuario idUsuario, Cliente idCliente)
        {
            this.id = id;
            this.situacao = situacao;
            this.dataPedido = dataPedido;
            this.retirar = retirar;
            this.idUsuario = idUsuario;
            this.idCliente = idCliente;
        }
        public Pedido()
        {
            IdUsuario = new Usuario();
            IdCliente = new Cliente();
        }
        //Métodos
        /// <summary>
        /// Inserindo um novo pedido
        /// </summary>        
        public void InserirPedido(string situacao, bool retirar, int idUsuario, int idCliente)
        {
            db = new Banco();
            try
            {
                var comm = db.AbrirConexao();
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.CommandText = "insert_pedido";
                comm.Parameters.Add("_situacao", MySqlDbType.VarChar).Value = situacao;
                comm.Parameters.Add("_retirar", MySqlDbType.Bit).Value = retirar;
                comm.Parameters.Add("_idusuario", MySqlDbType.Int32).Value = idUsuario;
                comm.Parameters.Add("_idcliente", MySqlDbType.Int32).Value = idCliente;
                var dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    this.Id = dr.GetInt32(0);
                    this.DataPedido = dr.GetDateTime(2);
                }
            }
            catch (Exception e)
            {
                e.Message.ToString();
            }
        }
        /// <summary>
        /// Alterando pedido
        /// </summary>        
        /// <returns>Retorna true se alterar com sucesso e false se falhar</returns>
        public bool AlterarPedido(string situacao, int id)
        {
            db = new Banco();
            try
            {
                var comm = db.AbrirConexao();
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.CommandText = "update_pedido";
                comm.Parameters.Add("_situacao", MySqlDbType.VarChar).Value = situacao;
                comm.Parameters.Add("_idpedido", MySqlDbType.Int32).Value = id;
                comm.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                e.Message.ToString();
                return false;
            }
        }
        public void ConsultarPedido(int id)
        {
            db = new Banco();
            try
            {
                var comm = db.AbrirConexao();
                comm.CommandText = "select * from pedidos where idPedidos = " + id;
                var dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    this.Id = dr.GetInt32(0);
                    this.Situacao = dr.GetString(1);
                    this.DataPedido = dr.GetDateTime(2);
                    this.Retirar = dr.GetBoolean(3);
                    this.IdUsuario.Id = dr.GetInt32(4);
                    this.IdCliente.Id = dr.GetInt32(5);
                }
            }
            catch (Exception e)
            {
                e.Message.ToString();
            }
        }
        /// <summary>
        /// Consulta o pedido pelo idCliente e Id Pedido
        /// </summary>        
        public MySqlDataReader ConsultarPedidosCliente(int idCliente, int idPedido)
        {
            db = new Banco();
            try
            {
                var comm = db.AbrirConexao();
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.CommandText = "viewPedidosClientes";
                return comm.ExecuteReader();
            }
            catch (Exception e)
            {
                e.Message.ToString();
                return null;
            }
        }
        /// <summary>
        /// Retorna uma lista de pedidos efetuado pelo cliente
        /// </summary>
        /// <param name="idCliente"></param>
        /// <returns></returns>
        public List<Pedido> ConsultarPedidosCliente(int idCliente)
        {
            db = new Banco();
            Pedido p;
            List<Pedido> lista = new List<Pedido>();
            try
            {
                var comm = db.AbrirConexao();
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.CommandText = "select * from pedidos where cliente_IdCliente = " + idCliente;
                var dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    p = new Pedido();
                    this.Id = dr.GetInt32(0);
                    this.Situacao = dr.GetString(1);
                    this.DataPedido = dr.GetDateTime(2);
                    this.Retirar = dr.GetBoolean(3);
                    this.IdUsuario.Id = dr.GetInt32(4);
                    this.IdCliente.Id = dr.GetInt32(5);
                    lista.Add(p);
                }
                return lista;
            }
            catch (Exception e)
            {
                e.Message.ToString();
                return null;
            }
        }
        public void consultarPedidNota(int idnota)
        {
            db = new Banco();
            Pedido p;
            List<Pedido> lista = new List<Pedido>();
            try
            {
                var comm = db.AbrirConexao();
                comm.CommandText = "select p.* from pedidos as p inner join carga as c on c.idPedidos = p.idPedidos inner join notatransporte as nt inner join itensnotatransporte as ints on nt.idNotaTransporte = ints.idNotaTransporte on c.idCarga = ints.idCarga where nt.idNotaTransporte = " + idnota;
                var dr = comm.ExecuteReader();
                while(dr.Read())
                {                    
                    this.Id = dr.GetInt32(0);
                    this.Situacao = dr.GetString(1);
                    this.DataPedido = dr.GetDateTime(2);
                    this.Retirar = dr.GetBoolean(3);
                    this.IdUsuario.Id = dr.GetInt32(4);
                    this.IdCliente.Id = dr.GetInt32(5);                    
                }                
            }
            catch (Exception e)
            {
                e.Message.ToString();                
            }
        }
    }
}