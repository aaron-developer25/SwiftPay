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
    public class SalidaCajaService
    {
        private readonly Context _context;

        public SalidaCajaService(Context context)
        {
            _context = context;
        }

        public async Task<bool> Verificar(int CajaId)
        {
            return await _context.SalidaCaja.AnyAsync(c => c.CajaId == CajaId);
        }

        public async Task<bool> Agregar(SalidaCaja Caja)
        {
            _context.SalidaCaja.Add(Caja);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Modificar(SalidaCaja Caja)
        {
            _context.Update(Caja);
            int cantidad = await _context.SaveChangesAsync();
            _context.Entry(Caja).State = EntityState.Detached;
            return cantidad > 0;
        }

        public async Task<bool> Guardar(SalidaCaja Caja)
        {
            if (!await Verificar(Caja.CajaId))
                return await Agregar(Caja);
            else
                return await Modificar(Caja);
        }

        public async Task<bool> Eliminar(SalidaCaja Caja)
        {
            _context.SalidaCaja.Remove(Caja);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<SalidaCaja?> Buscar(int CajaId)
        {
            return await _context.SalidaCaja
				.AsNoTracking()
                .FirstOrDefaultAsync(c => c.CajaId == CajaId);
        }

        public async Task<List<SalidaCaja>> Listar(Expression<Func<SalidaCaja, bool>> criterio)
        {
            return await _context.SalidaCaja
				.AsNoTracking()
                .Where(criterio)
                .ToListAsync();
        }
    }
}
