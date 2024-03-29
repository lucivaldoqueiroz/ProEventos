using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Contexto;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Persistence
{
    public class EventoPersist : IEventoPersist
    {
        private readonly ProEventosContext _context;
        public EventoPersist(ProEventosContext context)
        {
            _context = context;
            //_context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            
        }
        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos.Include(e => e.Lotes).Include(e => e.RedesSociais);
            if(includePalestrantes) {
                query = query.Include(e => e.PalestranteEventos).ThenInclude(pe => pe.Palestrante);
            }
            query = query.AsNoTracking().OrderBy(e => e.EventoId);

            return await query.ToArrayAsync();
        }

        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos.Include(e => e.Lotes).Include(e => e.RedesSociais);
            if(includePalestrantes) {
                query = query.Include(e => e.PalestranteEventos).ThenInclude(pe => pe.Palestrante);
            }
            query = query.AsNoTracking().OrderBy(e => e.EventoId).Where(e => e.Tema.ToLower().Contains(tema.ToLower()));

            return await query.ToArrayAsync();
        }

          public async Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos.Include(e => e.Lotes).Include(e => e.RedesSociais);
            if(includePalestrantes) {
                query = query.Include(e => e.PalestranteEventos).ThenInclude(pe => pe.Palestrante);
            }
            query = query.AsNoTracking().OrderBy(e => e.EventoId).Where(e => e.EventoId == eventoId);

            return await query.FirstOrDefaultAsync();
        }
    }
}