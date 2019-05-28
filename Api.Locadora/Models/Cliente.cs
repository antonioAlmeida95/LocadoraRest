using System.Collections.Generic;
using Api.Locadora.Models.Interfaces;
using Newtonsoft.Json;

namespace Api.Locadora.Models
{
    public class Cliente : TrackableEntity, ICliente
    {
        public int Id { get; set; }
        public Status Tipo { get; set; }
        public int Versao { get; set; }

        public string Nome { get; set; }

        public string Perfil { get; set; }

        public List<Locacoes> Locados { get; set; } = new List<Locacoes>();


        public override IList<IHistorico> Historicos { get; set; }
        public override IHistorico CriarHistorico()
        {
            return new ClienteHistorico
            {
                Tipo = Tipo,
                Nome = Nome,
                Perfil = Perfil,
                Versao = Versao
            };
        }
    }

    public enum Status
    {
       Adimplente,
       Inadimplente
    }
}
