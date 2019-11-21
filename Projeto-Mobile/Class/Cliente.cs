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
    public class Cliente
    {
        //Atributos Privados
        private int id;
        private string razaoSocial;
        private string cnpj;
        private string email;
        private string inscricaoEstadual;
        private string senha;
        private string telefone;
        private string nomeContato;
        Banco db;
        //Propiedades
        public int Id { get => id; set => id = value; }
        public string RazaoSocial { get => razaoSocial; set => razaoSocial = value; }
        public string Cnpj { get => cnpj; set => cnpj = value; }
        public string Email { get => email; set => email = value; }
        public string InscricaoEstadual { get => inscricaoEstadual; set => inscricaoEstadual = value; }
        public string Senha { get => senha; set => senha = value; }
        public string Telefone { get => telefone; set => telefone = value; }
        public string NomeContato { get => nomeContato; set => nomeContato = value; }
        //Métodos Construtores
        public Cliente(int id, string razaoSocial, string cnpj, string email, string inscricaoEstadual, string senha, string telefone, string nomeContato)
        {
            this.Id = id;
            this.RazaoSocial = razaoSocial;
            this.Cnpj = cnpj;
            this.Email = email;
            this.InscricaoEstadual = inscricaoEstadual;
            this.Senha = senha;
            this.Telefone = telefone;
            this.NomeContato = nomeContato;
        }
        public Cliente()
        { }
        //Métodos
        public void InserirCliente(string _razaoSocial, string _cnpj, string _email, string _inscricaoEstadual, string _senha, string _telefone, string _nomeContato)
        {
            db = new Banco();
            var comm = db.AbrirConexao();
            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandText = "insert_cliente";
            comm.Parameters.Add("_razaosocial", MySqlDbType.VarChar).Value = _razaoSocial;
            comm.Parameters.Add("_cnpj", MySqlDbType.VarChar).Value = _cnpj;
            comm.Parameters.Add("_email", MySqlDbType.VarChar).Value = _email;
            comm.Parameters.Add("_inscricaoestadual", MySqlDbType.VarChar).Value = _inscricaoEstadual;
            comm.Parameters.Add("_senha", MySqlDbType.VarChar).Value = GerarSenhaMd5(_senha);
            comm.Parameters.Add("_telefone", MySqlDbType.VarChar).Value = _telefone;
            comm.Parameters.Add("_nomecontato", MySqlDbType.VarChar).Value = _nomeContato;
            var dr = comm.ExecuteReader();
            while (dr.Read())
            {
                this.Id = dr.GetInt32(0);
            }
        }
        public bool AlterarCliente(int _idcliente, string _telefone, string _nomecontato, string _email, string _razaosocial)
        {
            db = new Banco();
            try
            {
                var comm = db.AbrirConexao();
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "update_cliente";
                comm.Parameters.Add("_idcliente", MySqlDbType.Int32).Value = _idcliente;
                comm.Parameters.Add("_telefone", MySqlDbType.VarChar).Value = _telefone;
                comm.Parameters.Add("_nomecontato", MySqlDbType.VarChar).Value = _nomecontato;
                comm.Parameters.Add("_email", MySqlDbType.VarChar).Value = _email;
                comm.Parameters.Add("_razaosocial", MySqlDbType.VarChar).Value = _razaosocial;
                comm.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                e.Message.ToString();
                return false;
            }
        }
        public void ConsultarClienteId(int _id)
        {
            db = new Banco();
            try
            {
                var comm = db.AbrirConexao();
                comm.CommandText = "select * from cliente where idCliente = " + _id;
                var dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    this.Id = dr.GetInt32(0);
                    this.RazaoSocial = dr.GetString(1);
                    this.Cnpj = dr.GetString(2);
                    this.Email = dr.GetString(3);
                    this.InscricaoEstadual = dr.GetString(4);
                    this.Senha = dr.GetString(5);
                    this.Telefone = dr.GetString(6);
                    this.NomeContato = dr.GetString(7);
                }
            }
            catch (Exception e)
            {
                e.Message.ToString();
            }
        }
        public void ConsultarClienteCnpj(string _cnpj)
        {
            db = new Banco();
            try
            {
                var comm = db.AbrirConexao();
                comm.CommandText = "select * from cliente where Cnpj = '" + _cnpj + "'";
                var dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    this.Id = dr.GetInt32(0);
                    this.RazaoSocial = dr.GetString(1);
                    this.Cnpj = dr.GetString(2);
                    this.Email = dr.GetString(3);
                    this.InscricaoEstadual = dr.GetString(4);
                    this.Senha = dr.GetString(5);
                    this.Telefone = dr.GetString(6);
                    this.NomeContato = dr.GetString(7);
                }
            }
            catch (Exception e)
            {
                e.Message.ToString();
            }
        }
        public List<Cliente> ListarCliente()
        {
            db = new Banco();
            List<Cliente> lista = new List<Cliente>();

            try
            {
                var comm = db.AbrirConexao();
                comm.CommandText = "select * from cliente";
                var dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    Cliente C = new Cliente
                    {
                        Id = dr.GetInt32(0),
                        RazaoSocial = dr.GetString(1),
                        Cnpj = dr.GetString(2),
                        Email = dr.GetString(3),
                        InscricaoEstadual = dr.GetString(4),
                        Senha = dr.GetString(5),
                        Telefone = dr.GetString(6),
                        NomeContato = dr.GetString(7)
                    };
                    lista.Add(C);
                }
                return lista;
            }
            catch (Exception e)
            {
                e.Message.ToString();
                return null;
            }
        }
        public bool AlterarSenhaCliente(int _idcliente, string _senhanova)
        {
            db = new Banco();
            try
            {
                var comm = db.AbrirConexao();
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "update_password_cliente";
                comm.Parameters.Add("_idcliente", MySqlDbType.Int32).Value = _idcliente;
                comm.Parameters.Add("_senha", MySqlDbType.VarChar).Value = _senhanova;
                comm.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                e.Message.ToString();
                return false;
            }
        }
        public bool EfetuarLogin(string _cnpj, string _senha)
        {
            db = new Banco();
            try
            {
                var comm = db.AbrirConexao();
                comm.CommandText = "select * from cliente where Cnpj = '" + _cnpj + "' && Senha = '" + GerarSenhaMd5(_senha) + "'";
                var dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    if (dr.GetInt32(0) > 0)
                        return true;
                    else
                        return false;
                }
                return false;
            }
            catch (Exception e)
            {
                e.Message.ToString();
                return false;
            }
        }
        /// <summary>
        /// Gera uma senha em md5
        /// </summary>
        /// <param name="senha"></param>
        /// <returns></returns>
        public string GerarSenhaMd5(string senha)
        {
            var hash = MD5.Create();
            byte[] chave = hash.ComputeHash(Encoding.UTF8.GetBytes(senha));
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < chave.Length; i++)
            {
                builder.Append(chave[i].ToString("X2"));
            }
            return builder.ToString();
        }
    }
}
