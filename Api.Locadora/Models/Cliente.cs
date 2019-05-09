using System.Collections.Generic;
using Newtonsoft.Json;

namespace Api.Locadora.Models
{
    public class Cliente : IHistorico
    {
        public int Id { get; set; }

        public int Versao { get; set; } = 0;

        [JsonIgnore]
        public int NomeId { get; set; }
        public Propriedade Nome { get; set; }

        [JsonIgnore]
        public int PerfilId { get; set; }
        public Propriedade Perfil { get; set; }

        public List<Locacoes> Locados { get; set; } = new List<Locacoes>();

    }
}
