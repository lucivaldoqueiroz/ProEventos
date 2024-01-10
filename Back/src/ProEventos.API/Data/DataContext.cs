using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.API.Models;

namespace ProEventos.API.Data
{
    public class DataContext : DbContext
    {
        //construtor do data context
        public DataContext(DbContextOptions<DataContext> options) : base(options) {}
        
        // para que está referencia vire um banco de dados.
        public DbSet<Evento> Eventos { get; set; }
    }
}