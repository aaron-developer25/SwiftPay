using SwiftPay.DAL;
using SwiftPay.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SwiftPay.Services
{
    public class RegantesService
    {
        private readonly Context _context;

        public RegantesService(Context context)
        {
            _context = context;
        }

        public async Task<bool> Verificar(int ReganteId)
        {
            return await _context.Regantes.AnyAsync(r => r.ReganteId == ReganteId);
        }

		public async Task<bool> Agregar(Regantes Regante)
        {
            _context.Regantes.Add(Regante);
            return await _context.SaveChangesAsync() > 0;
        }

		public async Task<int> AgregarNuevo(Regantes Regante)
		{
			_context.Add(Regante);
			await _context.SaveChangesAsync();
			return Regante.ReganteId;
		}

		public async Task<bool> Modificar(Regantes Regantes)
        {
            _context.Update(Regantes);
            int cantidad = await _context.SaveChangesAsync();
            _context.Entry(Regantes).State = EntityState.Detached;
            return cantidad > 0;
        }

        public async Task<bool> Guardar(Regantes Regante)
        {
            if (!await Verificar(Regante.ReganteId))
                return await Agregar(Regante);
            else
                return await Modificar(Regante);
        }

        public async Task<bool> Eliminar(Regantes Regante)
        {
            _context.Regantes.Remove(Regante);
            return await _context.SaveChangesAsync() > 0;
        }

		public async Task<Regantes?> Buscar(int ReganteId)
		{
			return await _context.Regantes
					.AsNoTracking()
					.FirstOrDefaultAsync(r => r.ReganteId == ReganteId);
		}



		public async Task<List<Regantes>> Listar(Expression<Func<Regantes, bool>> criterio)
        {
            return await _context.Regantes
                .AsNoTracking()
                .Where(criterio)
                .ToListAsync();
        }

		public async Task<Regantes> BusquedaGeneral(Expression<Func<Regantes, bool>> criterio)
		{
			return await _context.Regantes
				.AsNoTracking()
				.FirstOrDefaultAsync(criterio);
		}

	}
}
