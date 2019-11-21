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
    public class Endereco
    {
        //Atributos
        private int id;
        private string logradouro;
        private string cep;
        private int numero;
        private string complemento;
        private string referencia;
        private Cliente idCliente;
        Banco db;
        //Propiedades
        public int Id { get => id; set => id = value; }
        public string Logradouro { get => logradouro; set => logradouro = value; }
        public string Cep { get => cep; set => cep = value; }
        public int Numero { get => numero; set => numero = value; }
        public string Complemento { get => complemento; set => complemento = value; }
        public string Referencia { get => referencia; set => referencia = value; }
        public Cliente IdCliente { get => idCliente; set => idCliente = value; }
        //Métodos construtores
        public Endereco(int id, string logradouro, string cep, int numero, string complemento, string referencia, Cliente idCliente)
        {
            this.id = id;
            this.logradouro = logradouro;
            this.cep = cep;
            this.numero = numero;
            this.complemento = complemento;
            this.referencia = referencia;
            this.idCliente = idCliente;
        }
        public Endereco()
        {
            IdCliente = new Cliente();
        }
        //Métodos
        /// <summary>
        /// Inserindo endereco do cliente
        /// </summary>        
        public void InserirEndereco(string _logradouro, string _cep, string _numero, string _complemento, string _referencia, int _idCliente)
        {
            db = new Banco();
            try
            {
                var comm = db.AbrirConexao();
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "insert_endereco";
                comm.Parameters.Add("_logradouro", MySqlDbType.VarChar).Value = _logradouro;
                comm.Parameters.Add("_cep", MySqlDbType.VarChar).Value = _cep;
                comm.Parameters.Add("_numero", MySqlDbType.Int32).Value = _numero;
                comm.Parameters.Add("_complemento", MySqlDbType.VarChar).Value = _complemento;
                comm.Parameters.Add("_referencia", MySqlDbType.VarChar).Value = _referencia;
                comm.Parameters.Add("_idcliente", MySqlDbType.Int32).Value = _idCliente;
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
        /// Alterando todas informações do endereço
        /// </summary>             
        public bool AlterarEndereco(string _logradouro, string _cep, string _numero, string _complemento, string _referencia, int _idEndereco)
        {
            db = new Banco();
            try
            {
                var comm = db.AbrirConexao();
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "update_endereco";
                comm.Parameters.Add("_logradouro", MySqlDbType.VarChar).Value = _logradouro;
                comm.Parameters.Add("_cep", MySqlDbType.VarChar).Value = _cep;
                comm.Parameters.Add("_numero", MySqlDbType.Int32).Value = _numero;
                comm.Parameters.Add("_complemento", MySqlDbType.VarChar).Value = _complemento;
                comm.Parameters.Add("_referencia", MySqlDbType.VarChar).Value = _referencia;
                comm.Parameters.Add("_idendereco", MySqlDbType.Int32).Value = _idEndereco;
                return true;
            }
            catch (Exception e)
            {
                e.Message.ToString();
                return false;
            }
        }
        /// <summary>
        /// Consultando endereco pelo id do Endereco e cliente
        /// </summary>        
        public void ConsultarEndereco(int _idcliente, int _idendereco)
        {
            db = new Banco();
            try
            {
                var comm = db.AbrirConexao();
                comm.CommandText = "select * from endereco where idCliente = " + _idcliente + " && idEndereco = " + _idendereco;
                var dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    this.Id = dr.GetInt32(0);
                    this.Logradouro = dr.GetString(1);
                    this.Cep = dr.GetString(2);
                    this.Numero = dr.GetInt32(3);
                    this.Complemento = dr.GetString(4);
                    this.Referencia = dr.GetString(5);
                    this.IdCliente.Id = dr.GetInt32(6);
                }
            }
            catch (Exception e)
            {
                e.Message.ToString();
            }
        }
        /// <summary>
        /// Retorna uma lista de endereços de todos os clientes
        /// </summary>        
        public List<Endereco> ListarEnderecos(int _id)
        {
            db = new Banco();
            List<Endereco> lista = new List<Endereco>();
            try
            {
                var comm = db.AbrirConexao();
                comm.CommandText = "select * from endereco where idEndereco = " + _id;
                var dr = comm.ExecuteReader();
                Endereco e = new Endereco();
                while (dr.Read())
                {
                    e.Id = dr.GetInt32(0);
                    e.Logradouro = dr.GetString(1);
                    e.Cep = dr.GetString(2);
                    e.Numero = dr.GetInt32(3);
                    e.Complemento = dr.GetString(4);
                    e.Referencia = dr.GetString(5);
                    e.IdCliente.Id = dr.GetInt32(6);
                    lista.Add(e);
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
        /// Listando todos os endereços do cliente
        /// </summary>
        /// <returns>Lista de endereços cadastrados, do cliente</returns>
        public List<Endereco> ListarEnderecosCliente(int _id)
        {
            db = new Banco();
            Endereco en;
            List<Endereco> lista = new List<Endereco>();
            try
            {
                var comm = db.AbrirConexao();
                comm.CommandText = "select end.idEndereco as Id, end.Logradouro, end.Cep, end.Numero,  end.Complemento, " +
                "c.RazaoSocial from endereco as end inner join cliente as c on end.IdCliente = c.IdCliente where end.IdCliente = " + _id + "";
                var dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    en = new Endereco();
                    en.Id = dr.GetInt32(0);
                    en.Logradouro = dr.GetString(1);
                    en.Cep = dr.GetString(2);
                    en.Numero = dr.GetInt32(3);
                    en.Complemento = dr.GetString(4);
                    en.IdCliente.RazaoSocial = dr.GetString(5);
                    lista.Add(en);
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
        /// Excluindo endereco do cliente
        /// </summary>        
        /// <returns>Retorna true se excluir e false se falhar</returns>
        public bool ExcluirEndereco(int _idEndereco, int _idcliente)
        {
            db = new Banco();
            try
            {
                var comm = db.AbrirConexao();
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "delete_endereco";
                comm.Parameters.Add("_idendereco", MySqlDbType.Int32).Value = _idEndereco;
                comm.Parameters.Add("_idcliente", MySqlDbType.Int32).Value = _idcliente;
                comm.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                e.Message.ToString();
                return false;
            }
        }
    }
}