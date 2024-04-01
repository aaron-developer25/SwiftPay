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
    public class PagosService
	{
        private readonly Context _context;

        public PagosService(Context context)
        {
            _context = context;
        }

        public async Task<bool> Verificar(int PagoId)
        {
            return await _context.Pagos.AnyAsync(a => a.PagoId == PagoId);
        }

		public async Task<int> Insertar(Pagos Pago)
		{
			_context.Add(Pago);
			await _context.SaveChangesAsync();
			return Pago.PagoId;
		}

		public async Task<bool> Modificar(Pagos pago)
		{
			if (await Verificar(pago.PagoId))
			{
				_context.Update(pago);
				var guardado = await _context.SaveChangesAsync() > 0;
				_context.Pagos!.Entry(pago).State = EntityState.Detached;
				return guardado;
			}
			else
			{
				return false;
			}
		}

		public async Task<Pagos?> Buscar(int PagoId)
        {
            return await _context.Pagos
				.AsNoTracking()
                .FirstOrDefaultAsync(a => a.PagoId == PagoId);
        }

        public async Task<List<Pagos>> Listar(Expression<Func<Pagos, bool>> criterio)
        {
            return await _context.Pagos
				.AsNoTracking()
                .Where(criterio)
                .ToListAsync();
        }
    }
}
