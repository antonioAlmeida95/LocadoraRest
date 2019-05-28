using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Locadora.Models.Interfaces;

namespace Api.Locadora.Models
{
    public class ClienteHistorico : IHistorico, ICliente
    {
        public int Id { get; set; }
        public int Versao { get; set; }
        public DateTimeOffset DHModificacao { get; set; }

        public Status Tipo { get; set; }
        public string Nome { get; set; }
        public string Perfil { get; set; }

        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
    }
}
