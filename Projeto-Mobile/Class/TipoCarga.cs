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
    public class TipoCarga
    {
        //Atributos
        private int id;
        private string nome;
        private string descricao;
        Banco db;
        //Propiedades
        public int Id { get => id; set => id = value; }
        public string Nome { get => nome; set => nome = value; }
        public string Descricao { get => descricao; set => descricao = value; }
        //Métodos construtores
        public TipoCarga(int id, string nome, string descricao)
        {
            this.id = id;
            this.nome = nome;
            this.descricao = descricao;
        }
        public TipoCarga()
        { }
        //Métodos
        /// <summary>
        /// Inserindo tipo de carga
        /// </summary>        
        public void InserirTipoCarga(string nome, string descricao)
        {
            db = new Banco();
            try
            {
                var comm = db.AbrirConexao();
                comm.CommandText = "insert into tiposcargas (Nome,DescricaoTipo) values ('" + nome + "','" + descricao + "'); select * from tiposcargas where idTipo = last_insert_id();";
                this.Id = Convert.ToInt32(comm.ExecuteScalar());
            }
            catch (Exception e)
            {
                e.Message.ToString();
            }
        }
        /// <summary>
        /// Alterando tipo carga
        /// </summary>        
        public bool AlterarTipoCarga(string _nome, string _descricao, int _id)
        {
            db = new Banco();
            try
            {
                var comm = db.AbrirConexao();
                comm.CommandText = "update tiposcargas set Nome = '" + _nome + ", DescricaoTipo = '" + _descricao + "'";
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
        /// Excluindo Tipo Carga
        /// </summary>
        /// <param name="_id"></param>
        public bool DeleteTipoCarga(int _id)
        {
            db = new Banco();
            try
            {
                var comm = db.AbrirConexao();
                comm.CommandText = "delete from tiposcargas where idTipo = " + _id;
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
        /// Consultando tipo de carga
        /// </summary>
        /// <param name="_id"></param>
        public void ConsultarTipoCarga(int _id)
        {
            db = new Banco();
            try
            {
                var comm = db.AbrirConexao();
                comm.CommandText = "select * from tiposcargas where idTipo = " + _id;
                var dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    this.Id = dr.GetInt32(0);
                    this.Nome = dr.GetString(1);
                    this.Descricao = dr.GetString(2);
                }
            }
            catch (Exception e)
            {
                e.Message.ToString();
            }
        }
        /// <summary>
        /// Listando tipos de cargas
        /// </summary>        
        public List<TipoCarga> ListarTiposCargas()
        {
            db = new Banco();
            TipoCarga tp;
            List<TipoCarga> lista = new List<TipoCarga>();
            try
            {
                var comm = db.AbrirConexao();
                comm.CommandText = "select * from tiposcargas";
                var dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    tp = new TipoCarga();
                    tp.Id = dr.GetInt32(0);
                    tp.Nome = dr.GetString(1);
                    tp.Descricao = dr.GetString(2);
                    lista.Add(tp);
                }
                return lista;
            }
            catch (Exception e)
            {
                e.Message.ToString();
                return null;
            }
        }
    }
}
