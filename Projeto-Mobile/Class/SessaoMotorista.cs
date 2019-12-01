using SQLite;

namespace Projeto_Mobile.Class
{
    public class SessaoMotorista
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int IdMotorista { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public bool Sessao { get; set; }
    }
}