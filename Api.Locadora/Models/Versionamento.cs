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
        public string Nome { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public string Valor { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;

        [JsonIgnore]
        public int PropriedadeId { get; set; }
        public Propriedade Propriedade { get; set; }
    }
}
