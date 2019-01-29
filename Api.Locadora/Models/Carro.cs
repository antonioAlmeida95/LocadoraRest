using System.Collections.Generic;

namespace Api.Locadora.Models
{
    public class Carro
    {
        public int Id { get; set; }
        public string Modelo { get; set; }
        public int Ano { get; set; }
        public int Velocidade { get; set; }
        public int Diaria { get; set; }
        public IList<Locacoes> Locacoes { get; set; }

        public Carro()
        {
            this.Locacoes = new List<Locacoes>();
        }
    }
}
