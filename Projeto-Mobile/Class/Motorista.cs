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
        //Métodos construtores
        public Motorista(int idMotorista, string nome, string cpf, string rg, string cnh, DateTime validadeCnh, string categoriaCnh, string senha)
        {
            this.IdMotorista = idMotorista;
            this.Nome = nome;
            this.Cpf = cpf;
            this.Rg = rg;
            this.Cnh = cnh;
            this.ValidadeCnh = validadeCnh;
            this.CategoriaCnh = categoriaCnh;
            this.Senha = senha;
        }
        public Motorista()
        { }
        //Métodos
        /// <summary>
        /// Inserindo novo motorista
        /// </summary>
        public void InserirMotorista(string nome, string cpf, string rg, string cnh, DateTime validadeCnh, string categoriaCnh, string senha)
        {
            db = new Banco();
            try
            {
                var comm = db.AbrirConexao();
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.CommandText = "insert_motorista";
                comm.Parameters.Add("_nome", MySqlDbType.VarChar).Value = nome;
                comm.Parameters.Add("_cpf", MySqlDbType.VarChar).Value = cpf;
                comm.Parameters.Add("_rg", MySqlDbType.VarChar).Value = rg;
                comm.Parameters.Add("_cnh", MySqlDbType.VarChar).Value = cnh;
                comm.Parameters.Add("_validadecnh", MySqlDbType.Date).Value = validadeCnh;
                comm.Parameters.Add("_categoriacnh", MySqlDbType.VarChar).Value = categoriaCnh;
                comm.Parameters.Add("_senha", MySqlDbType.VarChar).Value = senha;
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
        }
        /// <summary>
        /// Alterando dados do motorista
        /// </summary>
        public void AlterarMotorista(DateTime validadeCnh, string categoriaCnh, int _id)
        {
            db = new Banco();
            try
            {
                var comm = db.AbrirConexao();
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.CommandText = "update_motorista";
                comm.Parameters.Add("_id", MySqlDbType.Int32).Value = _id;
                comm.Parameters.Add("_validadecnh", MySqlDbType.Date).Value = validadeCnh;
                comm.Parameters.Add("_categoriacnh", MySqlDbType.VarChar).Value = categoriaCnh;
                comm.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                e.Message.ToString();
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
            try
            {
                var comm = db.AbrirConexao();
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
                        Senha = dr.GetString(7)
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
        }
        /// <summary>
        /// Consultando motorista pelo id
        /// </summary>
        public void ConsultarMotorista(int _id)
        {
            db = new Banco();
            Motorista m;
            try
            {
                var comm = db.AbrirConexao();
                comm.CommandText = "select * from motorista where idMotorista = " + _id;
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
                        Senha = dr.GetString(7)
                    };
                }

            }
            catch (Exception e)
            {
                e.Message.ToString();
            }
        }
        /// <summary>
        /// Consultando motorista pela cnh
        /// </summary>        
        public void ConsultarMotorista(string _cnh)
        {
            db = new Banco();
            Motorista m;
            try
            {
                var comm = db.AbrirConexao();
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
                        Senha = dr.GetString(7)
                    };
                }

            }
            catch (Exception e)
            {
                e.Message.ToString();
            }
        }
    }
}