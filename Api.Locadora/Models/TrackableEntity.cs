using System;
using System.Collections.Generic;
using Api.Locadora.Models.Interfaces;

namespace Api.Locadora.Models
{
    public abstract class TrackableEntity : Entity
    {
        public abstract IList<IHistorico> Historicos { get; set; }

        public IEnumerable<IHistorico> InserirNovoHistorico()
        {
            InserirHistorico(CriarHistorico());
            return Historicos;
        }

        public IEnumerable<IHistorico> InserirHistorico(IHistorico historico)
        {
            if (Historicos == null)
            {
                Historicos = new List<IHistorico>();
            }

            historico.DHModificacao = DateTime.Now;

            Historicos.Add(historico);

            return Historicos;
        }

        public abstract IHistorico CriarHistorico();
    }
}
