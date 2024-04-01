using Microsoft.EntityFrameworkCore;
using SwiftPay.DAL;
using SwiftPay.Models;
using System.Linq.Expressions;

namespace SwiftPay.Services
{
	public class EmpresaService
	{
		private readonly Context _context;

		public EmpresaService(Context Context) 
		{
			_context = Context;
		}

		public async Task<bool> Existe(int empresaId)
		{
			return await _context.Empresas!.AnyAsync(e => e.EmpresaId == empresaId);
		}

		public async Task<bool> Insertar(Empresa empresa)
		{
			_context.Add(empresa);
			return await _context.SaveChangesAsync() > 0;
		}

		public async Task<bool> Modificar(Empresa empresa)
		{
			_context.Update(empresa);
			var guardado = await _context.SaveChangesAsync() > 0;
			_context.Empresas!.Entry(empresa).State = EntityState.Detached;
			return guardado;
		}

		public async Task<bool> Eliminar(Empresa empresa)
		{
            _context.Empresas.Remove(empresa);
            return await _context.SaveChangesAsync() > 0;
        }

		public async Task<Empresa?> Buscar(int empresaId)
		{
			return await _context.Empresas!.AsNoTracking().FirstOrDefaultAsync(e=> e.EmpresaId == empresaId);
		}

		public async Task<List<Empresa>> Listar(Expression<Func<Empresa, bool>> criterio) 
		{
			return await _context.Empresas!.AsNoTracking().Where(criterio).ToListAsync();
		}

		public async Task<bool> Guardar(Empresa empresa)
		{
			if(await Existe(empresa.EmpresaId))
			{
				return await Modificar(empresa);
			}
			else
			{
				return await Insertar(empresa);
			}
		}
	}
}
