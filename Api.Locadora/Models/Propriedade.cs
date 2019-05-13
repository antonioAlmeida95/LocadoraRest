using System;
using System.Collections.Generic;
using Api.Locadora.Models.Interfaces;
using Newtonsoft.Json;

namespace Api.Locadora.Models
{
    public class Propriedade : IHistorico
    {
        public int Id { get; set; }
        public string Valor { get; set; } = string.Empty;
        public int Versao { get; set; }


        [JsonIgnore]
        public List<Cliente> NomeClientes { get; set; }
        [JsonIgnore]
        public List<Cliente> PerfilClientes { get; set; }
        [JsonIgnore]
        public List<Carro> ModeloCarros { get; set; }

        public List<Versionamento> Versoes { get; set; } = new List<Versionamento>();

        public void AdcionarVersao(Versionamento versionamento, string tipo, int versao)
        {
            versionamento.Nome = tipo;
            versionamento.Versao = this.Versao = versao;
            versionamento.Valor = this.Valor;
            this.Versoes.Add(versionamento);
        }
    }
}
