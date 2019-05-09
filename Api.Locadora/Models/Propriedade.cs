using System.Collections.Generic;
using Newtonsoft.Json;

namespace Api.Locadora.Models
{
    public class Propriedade : IHistorico
    {
        public int Id { get; set; }
        public string Valor { get; set; }
        public int Versao { get; set; }

        [JsonIgnore]
        public List<Cliente> NomeClientes { get; set; }
        [JsonIgnore]
        public List<Cliente> PerfilClientes { get; set; }
        [JsonIgnore]
        public List<Carro> ModeloCarros { get; set; }

        public List<Versionamento> Versoes { get; set; } = new List<Versionamento>();
    }
}
