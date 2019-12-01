using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;

namespace Projeto_Mobile.Class
{
    public class BancoSqLite
    {
        readonly SQLiteAsyncConnection banco;
        public BancoSqLite(string dbPath)
        {
            banco = new SQLiteAsyncConnection(dbPath);
            banco.CreateTableAsync<SessaoMotorista>();
        }
        public Task<List<SessaoMotorista>> ObterListaSessao()
        {
            return banco.Table<SessaoMotorista>().ToListAsync();
        }
        public Task<List<SessaoMotorista>> ObterSessaoAberta()
        {
            return banco.QueryAsync<SessaoMotorista>("SELECT * FROM SessaoMotorista WHERE Sessao = 1");
        }
        public Task<List<SessaoMotorista>> ObterId(string nome)
        {
            return banco.QueryAsync<SessaoMotorista>("SELECT * FROM SessaoMotorista WHERE User = " + nome + "");
        }
        public Task<SessaoMotorista> ObterUser(string nome)
        {
            return banco.Table<SessaoMotorista>().Where(i => i.Nome == nome).FirstOrDefaultAsync();
        }

        public Task<int> InserirSessao(SessaoMotorista sessao)
        {
            return banco.InsertAsync(sessao);
        }
        public Task<int> AtualizarSessao(SessaoMotorista sessao)
        {
            return banco.UpdateAsync(sessao);
        }
        public Task<int> DeletarSessao(SessaoMotorista sessao)
        {
            return banco.DeleteAsync(sessao);
        }
    }
}