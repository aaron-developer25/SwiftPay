using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SwiftPay.DAL;
using SwiftPay.Models;
using Microsoft.EntityFrameworkCore;

namespace SwiftPay.Services
{
	public class AsociacionesService
	{
		private readonly Context _context;

		public AsociacionesService(Context context)
		{
			_context = context;
		}

		public async Task<bool> Verificar(int AsociacionId)
		{
			return await _context.Asociaciones.AnyAsync(a => a.AsociacionId == AsociacionId);
		}

		public async Task<bool> Agregar(Asociaciones Asociacion)
		{
			_context.Asociaciones.Add(Asociacion);
			return await _context.SaveChangesAsync() > 0;
		}

		public async Task<bool> Modificar(Asociaciones Asociacion)
		{
			_context.Update(Asociacion);
			int cantidad = await _context.SaveChangesAsync();
			_context.Entry(Asociacion).State = EntityState.Detached;
			return cantidad > 0;
		}

		public async Task<bool> Guardar(Asociaciones Asociacion)
		{
			if (!await Verificar(Asociacion.AsociacionId))
				return await Agregar(Asociacion);
			else
				return await Modificar(Asociacion);
		}

		public async Task<bool> Eliminar(Asociaciones Asociacion)
		{
			_context.Asociaciones.Remove(Asociacion);
			return await _context.SaveChangesAsync() > 0;
		}

		public async Task<Asociaciones?> Buscar(int AsociacionId)
		{
			return await _context.Asociaciones
				.AsNoTracking()
				.FirstOrDefaultAsync(a => a.AsociacionId == AsociacionId);
		}

		public async Task<List<Asociaciones>> Listar(Expression<Func<Asociaciones, bool>> criterio)
		{
			return await _context.Asociaciones
				.AsNoTracking()
				.Where(criterio)
				.ToListAsync();
		}
	}
}
