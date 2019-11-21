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
    public class TipoEndereco
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
        public TipoEndereco(int id, string nome, string descricao)
        {
            this.id = id;
            this.nome = nome;
            this.descricao = descricao;
        }
        public TipoEndereco()
        { }
        //Métodos
        /// <summary>
        /// Inserindo tipo de endereco
        /// </summary>        
        public void InserirTipoEndereco(string nome, string descricao)
        {
            db = new Banco();
            try
            {
                var comm = db.AbrirConexao();
                comm.CommandText = "insert into tiposenderecos (Nome,Descricao) values ('" + nome + "','" + descricao + "'); select * from tiposcargas where idTiposEnderecos = last_insert_id();";
                this.Id = Convert.ToInt32(comm.ExecuteScalar());
            }
            catch (Exception e)
            {
                e.Message.ToString();
            }
        }
        /// <summary>
        /// Alterando tipo endereco
        /// </summary>        
        public bool AlterarTipoEndereco(string _nome, string _descricao, int _id)
        {
            db = new Banco();
            try
            {
                var comm = db.AbrirConexao();
                comm.CommandText = "update tiposenderecos set Nome = '" + _nome + ", Descricao = '" + _descricao + "'";
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
        /// Excluindo Tipo Endereco
        /// </summary>        
        public bool DeleteTipoEndereco(int _id)
        {
            db = new Banco();
            try
            {
                var comm = db.AbrirConexao();
                comm.CommandText = "delete from tiposenderecos where idTipo = " + _id;
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
        /// Consultando tipo de endereco
        /// </summary>        
        public void ConsultarTipoEnderecoNome(string _nome)
        {
            db = new Banco();
            try
            {
                var comm = db.AbrirConexao();
                comm.CommandText = "select * from tiposenderecos where Nome = '" + _nome + "'";
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
        /// Consultando tipo de endereco
        /// </summary>        
        public void ConsultarTipoEndereco(int _id)
        {
            db = new Banco();
            try
            {
                var comm = db.AbrirConexao();
                comm.CommandText = "select * from tiposenderecos where idTiposEnderecos = " + _id;
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
        /// Listando tipos de enderecos
        /// </summary>        
        public List<TipoEndereco> ListarTiposEnderecos()
        {
            db = new Banco();
            TipoEndereco te;
            List<TipoEndereco> lista = new List<TipoEndereco>();
            try
            {
                var comm = db.AbrirConexao();
                comm.CommandText = "select * from tiposenderecos";
                var dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    te = new TipoEndereco();
                    te.Id = dr.GetInt32(0);
                    te.Nome = dr.GetString(1);
                    te.Descricao = dr.GetString(2);
                    lista.Add(te);
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
