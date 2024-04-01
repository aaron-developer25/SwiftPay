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
    public class UsuariosService
    {
        private readonly Context _context;

        public UsuariosService(Context context)
        {
            _context = context;
        }

		public async Task<Usuarios?> IniciarSesion(string usuario, string contrasena)
		{
			return await _context.Usuarios!.AsNoTracking().FirstOrDefaultAsync(u => u.Usuario == usuario && u.Contrasena == contrasena);
		}
		public async Task<bool> Verificar(int UsuarioId)
        {
            return await _context.Usuarios.AnyAsync(u => u.UsuarioId == UsuarioId);
        }

        public async Task<bool> Agregar(Usuarios Usuario)
        {
            _context.Usuarios.Add(Usuario);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Modificar(Usuarios Usuario)
        {
            _context.Update(Usuario);
            int cantidad = await _context.SaveChangesAsync();
            _context.Entry(Usuario).State = EntityState.Detached;
            return cantidad > 0;
        }

        public async Task<bool> Guardar(Usuarios Usuario)
        {
            if (!await Verificar(Usuario.UsuarioId))
                return await Agregar(Usuario);
            else
                return await Modificar(Usuario);
        }

        public async Task<bool> Eliminar(Usuarios Usuario)
        {
            _context.Usuarios.Remove(Usuario);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Usuarios?> Buscar(int UsuarioId)
        {
            return await _context.Usuarios
				.AsNoTracking()
                .FirstOrDefaultAsync(u => u.UsuarioId == UsuarioId);
        }

        public async Task<List<Usuarios>> Listar(Expression<Func<Usuarios, bool>> criterio)
        {
            return await _context.Usuarios
				.AsNoTracking()
                .Where(criterio)
                .ToListAsync();
        }
    }
}
