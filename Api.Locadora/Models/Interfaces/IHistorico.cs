using System.Collections.Generic;

namespace Api.Locadora.Models.Interfaces
{
    public interface IHistorico
    {
        int Versao { get; set; }

        List<Versionamento> Versoes { get; set; }

        void AdcionarVersao(Versionamento versionamento, string tipo, int versao);

    }
}
