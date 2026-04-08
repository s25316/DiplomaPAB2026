using GUS.REGON.Database.Models;
using GUS.REGON.Database.Models.Addresses;
using Microsoft.EntityFrameworkCore;

namespace GUS.REGON.Database;

public class RegonDbContext(DbContextOptions options) : DbContext(options)
{
    public virtual DbSet<Query> Queries { get; set; }
    public virtual DbSet<Report> Reports { get; set; }

    public virtual DbSet<Address> Addresses { get; set; }
    public virtual DbSet<Kraj> Kraje { get; set; }
    public virtual DbSet<Wojewodztwo> Wojewodztwa { get; set; }
    public virtual DbSet<Powiat> Powiaty { get; set; }
    public virtual DbSet<Gmina> Gminy { get; set; }
    public virtual DbSet<MiejscowoscPoczty> MiejscowosciPoczty { get; set; }
    public virtual DbSet<Miejscowosc> Miejscowosci { get; set; }
    public virtual DbSet<Ulica> Ulicy { get; set; }
}