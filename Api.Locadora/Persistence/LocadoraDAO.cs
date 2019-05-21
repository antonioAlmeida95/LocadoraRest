using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Api.Locadora.Models;
using Api.Locadora.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Api.Locadora.Persistencia
{
    public class LocadoraDAO: IDisposable
    {
        private LocadoraContext _context;

        public LocadoraDAO()
        {
            _context = new LocadoraContext();
        }

        public Cliente AddCliente(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            _context.SaveChanges();
            return cliente;
        }

        public Carro AddCarro(Carro carro)
        {
            _context.Carros.Add(carro);
            _context.SaveChanges();
            return carro;
        }

        public Cliente GetClienteById(Cliente cliente)
        {
            return _context.Clientes
                .Include(ca => ca.Locados)
                .ThenInclude(c => c.Carro)
                .FirstOrDefault(c => cliente.Id != 0 && cliente.Id == c.Id);
        }

        public Cliente GetClienteById(int id)
        {
            return _context.Clientes
                .Include(c => c.Nome)
                .ThenInclude(h => h.Versoes)
                .Include(c => c.Perfil)
                .ThenInclude(h => h.Versoes)
                .Include(ca => ca.Locados)
                .ThenInclude(c => c.Carro)
                .FirstOrDefault(c => id != 0 && id == c.Id);
        }

        public IList<Cliente> GetClientes()
        {
            return _context.Clientes
                .Include(ca => ca.Locados)
                .ThenInclude(c => c.Carro)
                .Include(c => c.Nome)
                .ThenInclude(h => h.Versoes)
                .Include(c => c.Perfil)
                .ThenInclude(h => h.Versoes)
                .ToList();
        }

        public Carro GetCarroById(Carro carro)
        {
            return _context.Carros
                .Include(cl => cl.Locacoes)
                .ThenInclude(c => c.Cliente)
                .Include(c => c.Modelo)
                .ThenInclude(c => c.Versoes)
                .FirstOrDefault(c => carro.Id != 0 && c.Id == carro.Id);
        }

        public Carro GetCarroById(int id)
        {
            return _context.Carros
                .Include(cl => cl.Locacoes)
                .ThenInclude(c => c.Cliente)
                .Include(c => c.Modelo)
                .ThenInclude(c => c.Versoes)
                .FirstOrDefault(c => id != 0 && c.Id == id);
        }

        public IList<Carro> GetCarros()
        {
            return _context.Carros
                .Include(cl => cl.Locacoes)
                .ThenInclude(c => c.Cliente)
                .ToList();
        }

        public Carro AtualizarCarro(Carro carro)
        {
            var carroDb = new Carro();

            if (carro.Id > 0)
            {
                carroDb = GetCarroById(carro.Id) ?? throw new ArgumentException("Não foi possivel localizar o carro.");
            }

            carroDb.Atualizar(carro);

            var entity = _context.Set<Carro>().Update(carroDb).Entity;
            _context.SaveChanges();
            return entity;
        }

        public Cliente AtualizarCliente(Cliente cliente)
        {
            var clienteDb = new Cliente();

            if (cliente.Id > 0)
            {
                clienteDb = GetClienteById(cliente.Id) ??
                            throw new ArgumentException("Não foi possivel localizar o cliente");
            }

            clienteDb.Atualizar(cliente);
            var entry = _context.Clientes.Update(clienteDb);
            _context.SaveChanges();

            return entry.Entity;
        }

        public void ExcluirCliente(Expression<Func<Cliente, bool>> clasulaWhere)
        {
            _context.Set<Cliente>()
                .Where(clasulaWhere).ToList()
                .ForEach(del => _context.Set<Cliente>().Remove(del));
            _context.SaveChanges();
        }

        public void ExcluirCarro(Expression<Func<Carro, bool>> clasulaWhere)
        {
            _context.Set<Carro>()
                .Where(clasulaWhere).ToList()
                .ForEach(del => _context.Set<Carro>().Remove(del));
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
