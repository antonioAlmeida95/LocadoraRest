using System.Collections.Generic;
using Api.Locadora.Models.Interfaces;
using Newtonsoft.Json;

namespace Api.Locadora.Models
{
    public class Carro : Entity
    {
        public int Id { get; set; }
        public int Versao { get; set; }

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
    }
}
