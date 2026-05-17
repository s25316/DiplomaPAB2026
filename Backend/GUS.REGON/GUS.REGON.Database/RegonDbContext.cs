using GUS.REGON.Database.Models;
using GUS.REGON.Database.Models.Addresses;
using GUS.REGON.Database.Models.Contacts;
using GUS.REGON.Database.Models.Pkds;
using GUS.REGON.Database.Models.RegistrationDetails;
using Microsoft.EntityFrameworkCore;

namespace GUS.REGON.Database;

public class RegonDbContext(DbContextOptions options) : DbContext(options)
{
    public virtual DbSet<Error> Errors { get; set; }

    public virtual DbSet<Request> Requests { get; set; }
    public virtual DbSet<RequestStatus> RequestStatuses { get; set; }
    public virtual DbSet<Institution> Institutions { get; set; }
    public virtual DbSet<TypJednostki> TypyJednostki { get; set; }

    public virtual DbSet<Address> Addresses { get; set; }
    public virtual DbSet<Kraj> Kraje { get; set; }
    public virtual DbSet<Wojewodztwo> Wojewodztwa { get; set; }
    public virtual DbSet<Powiat> Powiaty { get; set; }
    public virtual DbSet<Gmina> Gminy { get; set; }
    public virtual DbSet<MiejscowoscPoczty> MiejscowosciPoczty { get; set; }
    public virtual DbSet<Miejscowosc> Miejscowosci { get; set; }
    public virtual DbSet<Ulica> Ulicy { get; set; }

    public virtual DbSet<FormaFinansowania> FormyFinansowania { get; set; }
    public virtual DbSet<FormaWlasnosci> FormyWlasnosci { get; set; }
    public virtual DbSet<OrganRejestrowy> OrganyRejestrowe { get; set; }
    public virtual DbSet<OrganZalozycielski> OrganyZalozycielskie { get; set; }
    public virtual DbSet<PodstawowaFormaPrawna> PodstawoweFormyPrawne { get; set; }
    public virtual DbSet<SzczegolnaFormaPrawna> SzczegolneFormyPrawne { get; set; }
    public virtual DbSet<RodzajRejestru> RodzajeRejestru { get; set; }

    public virtual DbSet<PhoneNumber> PhoneNumbers { get; set; }
    public virtual DbSet<Website> Websites { get; set; }
    public virtual DbSet<Email> Emails { get; set; }

    public virtual DbSet<Pkd> Pkds { get; set; }
    public virtual DbSet<InstitutionPkd> InstitutionPkds { get; set; }
}