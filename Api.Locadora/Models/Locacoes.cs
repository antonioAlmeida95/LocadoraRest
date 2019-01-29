namespace Api.Locadora.Models
{
    public class Locacoes
    {
        public int ClienteId { get; set; }
        public int CarroId { get; set; }

        public Cliente Cliente { get; set; }
        public Carro Carro { get; set; }
    }
}
