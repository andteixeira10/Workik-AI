using Microsoft.EntityFrameworkCore;
using WellTrack.Api.Models;


namespace WellTrack.Api.Data
{
public class AppDbContext : DbContext
{
public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


public DbSet<Colaborador> Colaboradores { get; set; }
public DbSet<Empresa> Empresas { get; set; }
public DbSet<RegistroBemEstar> Registros { get; set; }
}
}