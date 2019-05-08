using System.Collections.Generic;
using Newtonsoft.Json;

namespace Api.Locadora.Models
{
    public class Cliente : IHistorico
    {
        public int Id { get; set; }

        public int Versao { get; set; } = 0;

        public Propriedade Nome { get; set; }


        public string Perfil { get; set; }

        public List<Locacoes> Locados { get; set; } = new List<Locacoes>();

    }
}
