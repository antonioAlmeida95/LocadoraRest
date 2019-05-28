using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Locadora.Models.Interfaces
{
    public interface ICliente
    {
        Status Tipo { get; set; }
        string Nome { get; set; }
        string Perfil { get; set; }
    }
}
