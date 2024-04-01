using Microsoft.EntityFrameworkCore;
using SwiftPay.Models;

namespace SwiftPay.DAL
{
	public class Context : DbContext
	{
		public DbSet<Empresa> Empresas { get; set; }
		public DbSet<Asociaciones> Asociaciones { get; set; }
		public DbSet<Bloques> Bloques { get; set; }
		public DbSet<SalidaCaja> SalidaCaja { get; set; }
		public DbSet<Regantes> Regantes { get; set; }
		public DbSet<DetalleRegante> DetalleRegantes { get; set; }
		public DbSet<Parametros> Parametros { get; set; }
		public DbSet<Usuarios> Usuarios { get; set; }
		public DbSet<Pagos> Pagos { get; set; }
		public DbSet<DetallePagos> DetallePagos { get; set; }

		public Context(DbContextOptions<Context> options) : base(options) { }
	}
}
