using System.Collections.Generic;
using Api.Locadora.Models.Interfaces;
using Newtonsoft.Json;

namespace Api.Locadora.Models
{
    public class Carro : TrackableEntity, ICarro
    {
        public int Id { get; set; }
        public int Versao { get; set; }


        public string Modelo { get; set; }

        public int Ano { get; set; }
        public int Velocidade { get; set; }
        public int Diaria { get; set; }
        public IList<Locacoes> Locacoes { get; set; }

        public Carro()
        {
            this.Locacoes = new List<Locacoes>();
        }

        public override IList<IHistorico> Historicos { get; set; }
        public override IHistorico CriarHistorico()
        {
            return new CarroHistorico
            {
                Versao = Versao,
                Modelo = Modelo,
                Ano = Ano,
                Diaria = Diaria,
                Velocidade = Velocidade
            };
        }
    }
}
