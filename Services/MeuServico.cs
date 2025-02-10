namespace APIRequest.Services
{
    public class MeuServico : IMeuServico
    {
        public string Saudacao(string nome)
        {
            return $"Olá, {nome}! {DateTime.UtcNow}";
        }

    }
}
 