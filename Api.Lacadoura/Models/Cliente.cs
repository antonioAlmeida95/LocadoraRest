using System.Collections.Generic;

namespace Api.Locadoura.Model
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Perfil { get; set; }
        public IList<Locacoes> Locados { get; set; }

        public Cliente()
        {
            this.Locados = new List<Locacoes>();
        }

    }
}
