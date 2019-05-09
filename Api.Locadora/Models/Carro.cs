using System.Collections.Generic;
using Newtonsoft.Json;

namespace Api.Locadora.Models
{
    public class Carro : IHistorico
    {
        public int Id { get; set; }

        [JsonIgnore]
        public int ModeloId { get; set; }
        public Propriedade Modelo { get; set; }

        public int Ano { get; set; }
        public int Velocidade { get; set; }
        public int Diaria { get; set; }
        public IList<Locacoes> Locacoes { get; set; }

        public Carro()
        {
            this.Locacoes = new List<Locacoes>();
        }

        public int Versao { get; set; }
    }
}
