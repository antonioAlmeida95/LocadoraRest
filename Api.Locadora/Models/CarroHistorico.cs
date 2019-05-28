using System;
using Api.Locadora.Models.Interfaces;

namespace Api.Locadora.Models
{
    public class CarroHistorico : IHistorico, ICarro
    {
        public int Id { get; set; }
        public int Versao { get; set; }
        public DateTimeOffset DHModificacao { get; set; }

        public string Modelo { get; set; }
        public int Ano { get; set; }
        public int Velocidade { get; set; }
        public int Diaria { get; set; }

        public int CarroId { get; set; }
        public Carro Carro { get; set; }
    }
}
