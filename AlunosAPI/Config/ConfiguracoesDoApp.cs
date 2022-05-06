namespace AlunosAPI.Config
{

    public class StringConexao
    {
        public string SQLServer { get; set; }
    }
    public class ConfiguracoesDoApp
    {
        public StringConexao ConnectionStrings { get; set; }
    }
}
