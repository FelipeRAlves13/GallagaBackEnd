using System;
using api.Model;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class SistemaContext : DbContext
    {
        public SistemaContext(DbContextOptions<SistemaContext> opt) : base(opt)
        {
        }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<HistoricoCliente> HistoricoClientes { get; set; }
        public DbSet<Quarto> Quartos { get; set; }
        public DbSet<Hotel> Hotel { get; set; }
    }
}