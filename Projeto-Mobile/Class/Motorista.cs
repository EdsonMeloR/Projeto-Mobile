using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
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
    public class Motorista
    {
        //atributos
        private int idMotorista;
        private string nome;
        private string cpf;
        private string rg;
        private string cnh;
        private DateTime validadeCnh;
        private string categoriaCnh;
        private string senha;
        private bool primeiroLogin;
        Banco db;
        //Propiedades
        public int IdMotorista { get => idMotorista; set => idMotorista = value; }
        public string Nome { get => nome; set => nome = value; }
        public string Cpf { get => cpf; set => cpf = value; }
        public string Rg { get => rg; set => rg = value; }
        public string Cnh { get => cnh; set => cnh = value; }
        public DateTime ValidadeCnh { get => validadeCnh; set => validadeCnh = value; }
        public string CategoriaCnh { get => categoriaCnh; set => categoriaCnh = value; }
        public string Senha { get => senha; set => senha = value; }
        public bool PrimeiroLogin { get => primeiroLogin; set => primeiroLogin = value; }

        //Métodos construtores
        public Motorista(int idMotorista, string nome, string cpf, string rg, string cnh, DateTime validadeCnh, string categoriaCnh, string senha, bool primeiroLogin)
        {
            this.IdMotorista = idMotorista;
            this.Nome = nome;
            this.Cpf = cpf;
            this.Rg = rg;
            this.Cnh = cnh;
            this.ValidadeCnh = validadeCnh;
            this.CategoriaCnh = categoriaCnh;
            this.Senha = senha;
            this.PrimeiroLogin = primeiroLogin;
        }
        public Motorista()
        { }
        //Métodos
        /// <summary>
        /// Inserindo novo motorista
        /// </summary>
        public void InserirMotorista(string nome, string cpf, string rg, string cnh, DateTime validadeCnh, string categoriaCnh, string senha, bool primeiroLogin)
        {
            db = new Banco();
            var comm = db.AbrirConexao();
            try
            {
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "insert_motorista";
                comm.Parameters.Add("_nome", MySqlDbType.VarChar).Value = nome;
                comm.Parameters.Add("_cpf", MySqlDbType.VarChar).Value = cpf;
                comm.Parameters.Add("_rg", MySqlDbType.VarChar).Value = rg;
                comm.Parameters.Add("_cnh", MySqlDbType.VarChar).Value = cnh;
                comm.Parameters.Add("_validadecnh", MySqlDbType.Date).Value = validadeCnh;
                comm.Parameters.Add("_categoriacnh", MySqlDbType.VarChar).Value = categoriaCnh;
                comm.Parameters.Add("_senha", MySqlDbType.VarChar).Value = senha;
                comm.Parameters.Add("_primeirologin", MySqlDbType.Bit).Value = primeiroLogin;
                var dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    this.IdMotorista = dr.GetInt32(0);
                }
            }
            catch (Exception e)
            {
                e.Message.ToString();
            }
            finally
            {
                if (comm != null)
                    comm.Connection.Close();
                else
                    throw new Exception("Falha ao conectar-se com o banco de dados");
            }
        }
        /// <summary>
        /// Alterando dados do motorista
        /// </summary>
        public bool AlterarMotorista(DateTime validadeCnh, string categoriaCnh, int _id)
        {
            db = new Banco();
            var comm = db.AbrirConexao();
            try
            {
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "update_motorista";
                comm.Parameters.Add("_id", MySqlDbType.Int32).Value = _id;
                comm.Parameters.Add("_validadecnh", MySqlDbType.Date).Value = validadeCnh;
                comm.Parameters.Add("_categoriacnh", MySqlDbType.VarChar).Value = categoriaCnh;
                comm.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                e.Message.ToString();
                return false;
            }
            finally
            {
                if (comm != null)
                    comm.Connection.Close();
                else
                    throw new Exception("Falha ao conectar-se com o banco de dados");
            }
        }
        /// <summary>
        /// Listando Motoristas
        /// </summary>
        /// <returns>Retorna uma lista de motoristas</returns>
        public List<Motorista> ListaMotoristas()
        {
            db = new Banco();
            Motorista m;
            List<Motorista> lista = new List<Motorista>();
            var comm = db.AbrirConexao();
            try
            {
                comm.CommandText = "select * from motorista";
                var dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    m = new Motorista
                    {
                        IdMotorista = dr.GetInt32(0),
                        Nome = dr.GetString(1),
                        Cpf = dr.GetString(2),
                        Rg = dr.GetString(3),
                        Cnh = dr.GetString(4),
                        ValidadeCnh = dr.GetDateTime(5),
                        CategoriaCnh = dr.GetString(6),
                        Senha = dr.GetString(7),
                        PrimeiroLogin = dr.GetBoolean(8)
                    };
                    lista.Add(m);
                }
                return lista;
            }
            catch (Exception e)
            {
                e.Message.ToString();
                return null;
            }
            finally
            {
                if (comm != null)
                    comm.Connection.Close();
            }
        }
        /// <summary>
        /// Consultando motorista pelo id
        /// </summary>
        public void ConsultarMotorista(int _id)
        {
            db = new Banco();
            var comm = db.AbrirConexao();
            try
            {
                comm.CommandText = "select * from motorista where idMotorista = " + _id;
                var dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    this.IdMotorista = dr.GetInt32(0);
                    this.Nome = dr.GetString(1);
                    this.Cpf = dr.GetString(2);
                    this.Rg = dr.GetString(3);
                    this.Cnh = dr.GetString(4);
                    this.ValidadeCnh = dr.GetDateTime(5);
                    this.CategoriaCnh = dr.GetString(6);
                    this.Senha = dr.GetString(7);
                    this.PrimeiroLogin = dr.GetBoolean(8);
                }
            }
            catch (Exception e)
            {
                e.Message.ToString();
            }
            finally
            {
                if (comm != null)
                    comm.Connection.Close();
                else
                    throw new Exception("Falha ao conectar-se com o banco de dados");
            }
        }
        /// <summary>
        /// Consultando motorista pela cnh
        /// </summary>        
        public void ConsultarMotorista(string _cnh)
        {
            db = new Banco();
            Motorista m;
            var comm = db.AbrirConexao();
            try
            {
                comm.CommandText = "select * from motorista where Cnh = " + _cnh;
                var dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    m = new Motorista
                    {
                        IdMotorista = dr.GetInt32(0),
                        Nome = dr.GetString(1),
                        Cpf = dr.GetString(2),
                        Rg = dr.GetString(3),
                        Cnh = dr.GetString(4),
                        ValidadeCnh = dr.GetDateTime(5),
                        CategoriaCnh = dr.GetString(6),
                        Senha = dr.GetString(7),
                        PrimeiroLogin = dr.GetBoolean(8)
                    };
                }

            }
            catch (Exception e)
            {
                e.Message.ToString();
            }
            finally
            {
                if (comm != null)
                    comm.Connection.Close();
                else
                    throw new Exception("Falha ao conectar-se com o banco de dados");
            }
        }
        /// <summary>
        /// Efetuando login do Motorista
        /// </summary>
        public void EfetuarLogin(string cpf, string senha)
        {
            try
            {
                db = new Banco();
                var comm = db.AbrirConexao();
                comm.CommandText = "select * from where cpf = '" + cpf + "' && senha = '" + GerarSenhaMd5(senha) + "'";
                var dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    this.IdMotorista = dr.GetInt32(0);
                    this.Nome = dr.GetString(1);
                    this.Cpf = dr.GetString(2);
                    this.Rg = dr.GetString(3);
                    this.Cnh = dr.GetString(4);
                    this.ValidadeCnh = dr.GetDateTime(5);
                    this.CategoriaCnh = dr.GetString(6);
                    this.senha = dr.GetString(7);
                    this.PrimeiroLogin = dr.GetBoolean(8);
                }
            }
            catch(Exception ex)
            {
                ex.Message.ToString();
            }
        }
        /// <summary>
        /// Gera uma senha em md5
        /// </summary>            
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