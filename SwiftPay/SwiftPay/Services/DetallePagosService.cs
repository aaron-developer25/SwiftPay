using Microsoft.EntityFrameworkCore;
using SwiftPay.DAL;
using SwiftPay.Models;
using System.Linq.Expressions;

namespace SwiftPay.Services
{
	public class DetallePagosService
	{
		private readonly Context _context;

		public DetallePagosService(Context Context)
		{
			_context = Context;
		}

		public async Task<bool> Insertar(DetallePagos detallePago)
		{
			_context.Add(detallePago);
			var guardado = await _context.SaveChangesAsync() > 0;
			_context.DetallePagos!.Entry(detallePago).State = EntityState.Detached;
			return guardado;
		}

		public async Task<DetallePagos?> Buscar(int detallePagoId)
		{
			return await _context.DetallePagos!.AsNoTracking().FirstOrDefaultAsync(d => d.DetallePagoId == detallePagoId);
		}

		public async Task<List<DetallePagos>> Listar(Expression<Func<DetallePagos, bool>> criterio)
		{
			return await _context.DetallePagos!.AsNoTracking().Where(criterio).ToListAsync();
		}
	}
}
