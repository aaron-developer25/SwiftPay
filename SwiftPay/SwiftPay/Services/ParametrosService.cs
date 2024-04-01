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
    public class ParametrosService
    {
        private readonly Context _context;

        public ParametrosService(Context context)
        {
            _context = context;
        }

        public async Task<bool> Verificar(int Id)
        {
            return await _context.Parametros.AnyAsync(pos => pos.ParametroOperatacionalesId == Id);
        }

        public async Task<bool> Agregar(Parametros POS)
        {
            _context.Parametros.Add(POS);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Modificar(Parametros POS)
        {
            _context.Update(POS);
            int cantidad = await _context.SaveChangesAsync();
            _context.Entry(POS).State = EntityState.Detached;
            return cantidad > 0;
        }

        public async Task<bool> Guardar(Parametros POS)
        {
            if (!await Verificar(POS.ParametroOperatacionalesId))
                return await Agregar(POS);
            else
                return await Modificar(POS);
        }

        public async Task<bool> Eliminar(Parametros POS)
        {
            _context.Parametros.Remove(POS);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Parametros?> Buscar(int Id)
        {
            return await _context.Parametros
				.AsNoTracking()
                .FirstOrDefaultAsync(pos => pos.ParametroOperatacionalesId == Id);
        }

        public async Task<List<Parametros>> Listar(Expression<Func<Parametros, bool>> criterio)
        {
            return await _context.Parametros
				.AsNoTracking()
                .Where(criterio)
                .ToListAsync();
        }
    }
}
