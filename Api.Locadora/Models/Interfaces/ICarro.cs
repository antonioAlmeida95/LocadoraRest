using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Locadora.Models.Interfaces
{
    public interface ICarro
    {
        string Modelo { get; set; }

        int Ano { get; set; }
        int Velocidade { get; set; }
        int Diaria { get; set; }
    }
}
