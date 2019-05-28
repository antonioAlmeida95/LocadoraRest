using System;
using System.Collections.Generic;

namespace Api.Locadora.Models.Interfaces
{
    public interface IHistorico
    {
        int Versao { get; set; }

        int Id { get; set; }

        DateTimeOffset DHModificacao { get; set; }
    }
}
