using System;
using Newtonsoft.Json;

namespace Api.Locadora.Models
{
    public class Versionamento
    {
        public int Versao { get; set; }
        [JsonIgnore]
        public int Id { get; set; }
        public DateTimeOffset Data { get; set; }
        public string Nome { get; set; }
        public string Tipo { get; set; }
        public string Valor { get; set; }
        public string Status { get; set; }

        [JsonIgnore]
        public int PropriedadeId { get; set; }
        public Propriedade Propriedade { get; set; }
    }
}
