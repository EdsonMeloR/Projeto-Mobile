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
    public class Entrega
    {
        //Atributos
        private int id;
        private byte[] assinatura;
        private string rg;
        private DateTime data;
        private string status;
        private NotaTransporte idNotaTransporte;
        Banco db;
        //Propiedades
        public int Id { get => id; set => id = value; }
        public byte[] Assinatura { get => assinatura; set => assinatura = value; }
        public string Rg { get => rg; set => rg = value; }
        public DateTime Data { get => data; set => data = value; }
        public string Status { get => status; set => status = value; }
        public NotaTransporte IdNotaTransporte { get => idNotaTransporte; set => idNotaTransporte = value; }
        //Métodos construtores
        public Entrega(int id, byte[] assinatura, string rg, DateTime data, string status, NotaTransporte idNotaTransporte)
        {
            this.Id = id;
            this.Assinatura = assinatura;
            this.Rg = rg;
            this.Data = data;
            this.Status = status;
            this.IdNotaTransporte = idNotaTransporte;
        }
        public Entrega()
        { }
        //Métodos
        /// <summary>
        /// Inserindo uma nova entrega
        /// </summary>       
        public void InserirEntrega(byte[] _assinatura, string _rg, string _status, int _idNotaTransporte)
        {
            db = new Banco();
            try
            {
                var comm = db.AbrirConexao();
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.CommandText = "insert_entrega";
                comm.Parameters.Add("_assinatura", MySqlDbType.Blob).Value = _assinatura;
                comm.Parameters.Add("_rg", MySqlDbType.VarChar).Value = _rg;
                comm.Parameters.Add("_status", MySqlDbType.VarChar).Value = _status;
                comm.Parameters.Add("_idnotatransporte", MySqlDbType.Int32).Value = _idNotaTransporte;
                var dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    this.Id = dr.GetInt32(0);
                }
            }
            catch (Exception e)
            {
                e.Message.ToString();
            }
        }
        /// <summary>
        /// Atualizando status da entrea
        /// </summary>        
        /// <returns>Retorna true no sucesso e false em falha</returns>
        public bool AtualizarEntrega(int id, string status)
        {
            db = new Banco();
            try
            {
                var comm = db.AbrirConexao();
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.CommandText = "update_entrega";
                comm.Parameters.Add("_identrega", MySqlDbType.Int32).Value = id;
                comm.Parameters.Add("status", MySqlDbType.VarChar).Value = status;
                comm.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                e.Message.ToString();
                return false;
            }
        }
        /// <summary>
        /// Consultando entrega
        /// </summary>        
        public void ConsultarEntrega(int _id)
        {
            db = new Banco();
            try
            {
                var comm = db.AbrirConexao();
                comm.CommandText = "select * from entrega where idEntrega = " + _id;
                var dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    this.Id = dr.GetInt32(0);
                    //this.Assinatura = dr.GetByte(1);
                    this.Rg = dr.GetString(2);
                    this.Data = dr.GetDateTime(3);
                    this.Status = dr.GetString(4);
                    this.IdNotaTransporte.Id = dr.GetInt32(5);
                }
            }
            catch (Exception e)
            {
                e.Message.ToString();
            }
        }
        /// <summary>
        /// Listando entrega com base na nota de transporte
        /// </summary>      
        public List<Entrega> ListarEntregasNota(int idNota)
        {
            db = new Banco();
            Entrega e = new Entrega();
            List<Entrega> lista = new List<Entrega>();
            try
            {
                var comm = db.AbrirConexao();
                comm.CommandText = "select * from entrega where idNotaTransporte = " + idNota;
                var dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    e.Id = dr.GetInt32(0);
                    //this.Assinatura = dr.GetByte(1);
                    e.Rg = dr.GetString(2);
                    e.Data = dr.GetDateTime(3);
                    e.Status = dr.GetString(4);
                    e.IdNotaTransporte.Id = dr.GetInt32(5);
                    lista.Add(e);
                }
                return lista;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return null;
            }
        }
        /// <summary>
        /// Retorna uma lista de entregas
        /// </summary>        
        public List<Entrega> ListarEntregas()
        {
            db = new Banco();
            Entrega e = new Entrega();
            List<Entrega> lista = new List<Entrega>();
            try
            {
                var comm = db.AbrirConexao();
                comm.CommandText = "select * from entrega ";
                var dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    e.Id = dr.GetInt32(0);
                    //this.Assinatura = dr.GetByte(1);
                    e.Rg = dr.GetString(2);
                    e.Data = dr.GetDateTime(3);
                    e.Status = dr.GetString(4);
                    e.IdNotaTransporte.Id = dr.GetInt32(5);
                    lista.Add(e);
                }
                return lista;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return null;
            }
        }
    }
}