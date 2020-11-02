namespace MServices.Domain.Dtos
{
    public class RetornoDto
    {
        public bool Sucesso { get; set; }

        public string Mensagem { get; set; }
    }

    public class RetornoDto<T>
    {
        public bool Sucesso { get; set; }

        public string Mensagem { get; set; }

        public T Objeto { get; set; }
    }
}
