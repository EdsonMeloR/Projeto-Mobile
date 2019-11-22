using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using System.Net.Mail;
using System.Net;

namespace Projeto_Desktop.Classes
{
    public class Usuario
    {
        //Atributos
        private int id;
        private string nome;
        private string cpf;
        private string telefone;
        private string senha;
        private string email;
        private Niveis idNivel;
        private bool primeiroLogin;
        Banco db;
        //Propiedades
        public int Id { get => id; set => id = value; }
        public string Nome { get => nome; set => nome = value; }
        public string Cpf { get => cpf; set => cpf = value; }
        public string Telefone { get => telefone; set => telefone = value; }
        public string Senha { get => senha; set => senha = value; }
        public string Email { get => email; set => email = value; }
        public Niveis IdNivel { get => idNivel; set => idNivel = value; }
        public bool PrimeiroLogin { get => primeiroLogin; set => primeiroLogin = value; }

        //Métodos construtores
        public Usuario(int id, string nome, string cpf, string telefone, string senha, string email, Niveis idNivel, bool primeiroLogin)
        {
            this.id = id;
            this.nome = nome;
            this.cpf = cpf;
            this.telefone = telefone;
            this.senha = senha;
            this.email = email;
            this.idNivel = idNivel;
            this.primeiroLogin = primeiroLogin;
        }
        public Usuario()
        {
            IdNivel = new Niveis();
        }
        //Métodos
        /// <summary>
        /// Inserindo usuario
        /// </summary>        
        public void InserirUsuario(string nome, string cpf, string telefone, string senha, string email, int idNivel)
        {
            db = new Banco();
            try
            {
                var comm = db.AbrirConexao();
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "insert_usuario";
                comm.Parameters.Add("_nome", MySqlDbType.VarChar).Value = nome;
                comm.Parameters.Add("_cpf", MySqlDbType.VarChar).Value = cpf;
                comm.Parameters.Add("_telefone", MySqlDbType.VarChar).Value = telefone;
                comm.Parameters.Add("_senha", MySqlDbType.VarChar).Value = GerarSenhaMd5(senha).Trim();
                comm.Parameters.Add("_email", MySqlDbType.VarChar).Value = email;
                comm.Parameters.Add("_nivel", MySqlDbType.Int32).Value = idNivel;
                comm.Parameters.Add("_firstlogin", MySqlDbType.Bit).Value = 1;
                this.Id = Convert.ToInt32(comm.ExecuteScalar());                
            }
            catch(Exception e)
            {
                e.Message.ToString();
            }
        }
        /// <summary>
        /// Alterando dados usuario
        /// </summary>
        public bool AlterarUsuario(int id, string telefone, string email)
        {
            db = new Banco();
            try
            {
                var comm = db.AbrirConexao();
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "update_usuario";
                comm.Parameters.Add("_id", MySqlDbType.Int32).Value = id;
                comm.Parameters.Add("_telefone", MySqlDbType.VarChar).Value = telefone;                
                comm.Parameters.Add("_email", MySqlDbType.VarChar).Value = email;
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
        /// Consultando usuario pelo id
        /// </summary>
        /// <param name="id"></param>
        public void ConsultarUsuario(int id)
        {
            db = new Banco();
            try
            {
                var comm = db.AbrirConexao();
                comm.CommandText = "select * from usuario where idUsuario = " + id;
                var dr = comm.ExecuteReader();              
                while(dr.Read())
                {
                    this.Id = dr.GetInt32(0);
                    this.Nome = dr.GetString(1);
                    this.Cpf = dr.GetString(2);
                    this.Telefone = dr.GetString(3);
                    this.Senha = dr.GetString(4);
                    this.Email = dr.GetString(5);
                    this.IdNivel.IdNivel = dr.GetInt32(6);
                    this.PrimeiroLogin = dr.GetBoolean(7);
                }
            }
            catch (Exception e)
            {
                e.Message.ToString();                
            }
        }
        /// <summary>
        /// Consultando usuario pelo CPF
        /// </summary>       
        public void ConsultarUsuario(string cpf)
        {
            db = new Banco();
            try
            {
                var comm = db.AbrirConexao();
                comm.CommandText = "select * from usuario where Cpf = " + cpf;
                var dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    this.Id = dr.GetInt32(0);
                    this.Nome = dr.GetString(1);
                    this.Cpf = dr.GetString(2);
                    this.Telefone = dr.GetString(3);
                    this.Senha = dr.GetString(4);
                    this.Email = dr.GetString(5);
                    this.IdNivel.IdNivel = dr.GetInt32(6);
                    this.PrimeiroLogin = dr.GetBoolean(7);
                }
            }
            catch (Exception e)
            {
                e.Message.ToString();
            }
        }
        /// <summary>
        /// Gerando lista de usuarios
        /// </summary>
        /// <returns></returns>
        public List<Usuario> ListarUsuarios()
        {
            db = new Banco();
            Usuario user;
            List<Usuario> lista = new List<Usuario>();
            try
            {
                var comm = db.AbrirConexao();
                comm.CommandText = "select * from usuario";
                var dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    user = new Usuario();
                    user.Id = dr.GetInt32(0);
                    user.Nome = dr.GetString(1);
                    user.Cpf = dr.GetString(2);
                    user.Telefone = dr.GetString(3);
                    user.Senha = dr.GetString(4);
                    user.Email = dr.GetString(5);
                    user.IdNivel.IdNivel = dr.GetInt32(6);
                    user.PrimeiroLogin = dr.GetBoolean(7);
                    lista.Add(user);
                }
                return lista;
            }
            catch (Exception e)
            {
                e.Message.ToString();
                return null;
            }            
        }
        public bool EfetuarLogin(string cpf, string senha)
        {
            db = new Banco();
            var senhac = GerarSenhaMd5(senha);
            try
            {
                var comm = db.AbrirConexao();
                comm.CommandText = "select * from usuario where Cpf = '" + cpf + "' && Senha = '"+senhac+"'";
                var dr = comm.ExecuteReader();
                IdNivel = new Niveis();
                while (dr.Read())
                {
                    this.Id = dr.GetInt32(0);
                    this.Nome = dr.GetString(1);
                    this.Cpf = dr.GetString(2);
                    this.Telefone = dr.GetString(3);
                    this.Senha = dr.GetString(4);
                    this.Email = dr.GetString(5);
                    this.IdNivel.IdNivel = dr.GetInt32(6);
                    this.PrimeiroLogin = dr.GetBoolean(7);
                }
                if (this.Id > 0)
                    return true;

                else
                    return false;                
            }
            catch (Exception e)
            {
                e.Message.ToString();
                return false;
            }
        }       
        /// <summary>
        /// Alterando senha do usuario
        /// </summary>
        /// <param name="id"></param>
        /// <param name="senha"></param>
        public bool AlterarSenha(int id, string senha)
        {
            var senhac = GerarSenhaMd5(senha);
            db = new Banco();
            try
            {
                var comm = db.AbrirConexao();
                comm.CommandText = "update usuario set Senha = '" + senhac + "' where idUsuario =" + id;                
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
        /// Alterando primeiro login para false
        /// </summary>
        /// <param name="id"></param>
        public void AlterarFirstLogin(int id)
        {
            db = new Banco();
            try
            {
                var comm = db.AbrirConexao();                
                comm.CommandText = "update usuario set PrimeiroLogin = 0";                
                comm.ExecuteNonQuery();                
            }
            catch (Exception e)
            {
                e.Message.ToString();                
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
        public bool RecuperarSenha(string CPF)
        {
            IdNivel = new Niveis();
            db = new Banco();
            try
            {
                var comm = db.AbrirConexao();
                comm.CommandText = "select * from usuario where Cpf = '" + CPF + "'";
                var dr = comm.ExecuteReader();                
                while (dr.Read())
                {
                    this.Id = dr.GetInt32(0);
                    this.Nome = dr.GetString(1);
                    this.Cpf = dr.GetString(2);
                    this.Telefone = dr.GetString(3);
                    this.Senha = dr.GetString(4);
                    this.Email = dr.GetString(5);
                    this.IdNivel.IdNivel = dr.GetInt32(6);
                }
                //cria uma mensagem
                MailMessage mail = new MailMessage();

                //define os endereços
                mail.From = new MailAddress("prosperitylogistica@gmail.com");
                mail.To.Add(this.Email.ToString());
                mail.Priority = MailPriority.Normal;
                mail.IsBodyHtml = true;

                //define o conteúdo
                var senhaRecuperada = GerarSenhaMd5(DateTime.Now.ToString());
                mail.Subject = "Esté é um email de recuperação de senha da prosperity";
                mail.Body = "Utilize esta senha : "+ senhaRecuperada;
                mail.SubjectEncoding = Encoding.GetEncoding("ISO-8859-1");
                mail.BodyEncoding = Encoding.GetEncoding("ISO-8859-1");

                //envia a mensagem
                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                smtp.EnableSsl = true;
                smtp.Port = 587;
                smtp.Credentials = new NetworkCredential("prosperitylogistica@gmail.com", "prospe123");
                smtp.Send(mail);

                //Redefine a senha
                comm.Connection.Close();
                comm.Connection.Open();
                comm.CommandText = "update usuario set Senha = '"+ GerarSenhaMd5(senhaRecuperada)+"', PrimeiroLogin = 1 where idUsuario = "+this.Id;
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
