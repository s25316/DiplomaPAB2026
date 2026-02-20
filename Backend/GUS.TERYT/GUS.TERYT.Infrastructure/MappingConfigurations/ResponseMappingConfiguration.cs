using Mapper;
using DatabaseGmina = GUS.TERYT.Database.Models.Tercs.Gmina;
using DatabaseGminaRodz = GUS.TERYT.Database.Models.Tercs.GminaRodz;
using DatabaseMiejscowosc = GUS.TERYT.Database.Models.Simcs.Simc;
using DatabaseMiejscowoscType = GUS.TERYT.Database.Models.Simcs.SimcType;
using DatabasePowiat = GUS.TERYT.Database.Models.Tercs.Powiat;
using DatabasePowiatType = GUS.TERYT.Database.Models.Tercs.PowiatType;
using DatabaseUlica = GUS.TERYT.Database.Models.Ulicy.Ulica;
using DatabaseUlicaType = GUS.TERYT.Database.Models.Ulicy.UlicaType;
using DatabaseWojewodztwo = GUS.TERYT.Database.Models.Tercs.Wojewodztwo;
using ResponseGmina = GUS.TERYT.Models.Responses.Gmina;
using ResponseGminaType = GUS.TERYT.Models.Responses.Gmina.Type;
using ResponseMiejscowosc = GUS.TERYT.Models.Responses.Miejscowosc;
using ResponseMiejscowoscType = GUS.TERYT.Models.Responses.Miejscowosc.Type;
using ResponsePowiat = GUS.TERYT.Models.Responses.Powiat;
using ResponsePowiatType = GUS.TERYT.Models.Responses.Powiat.Type;
using ResponseUlica = GUS.TERYT.Models.Responses.Ulica;
using ResponseUlicaType = GUS.TERYT.Models.Responses.Ulica.Type;
using ResponseWojewodztwo = GUS.TERYT.Models.Responses.Wojewodztwo;

namespace GUS.TERYT.Infrastructure.MappingConfigurations;

public class ResponseMappingConfiguration : MappingConfiguration
{
    public ResponseMappingConfiguration()
    {
        AddConfiguration<DatabaseWojewodztwo, ResponseWojewodztwo>(db => new ResponseWojewodztwo
        {
            WojewodztwoCode = db.WojewodztwoCode,
            Name = db.Name,
        });

        AddConfiguration<DatabasePowiatType, ResponsePowiatType>(db => new(db.TypeCode, db.Name));
        AddConfiguration<DatabasePowiat, ResponsePowiat>((m, db) => new ResponsePowiat
        {
            WojewodztwoCode = db.WojewodztwoCode,
            PowiatCode = db.PowiatCode,
            Name = db.Name,
            PowiatType = m.Map<ResponsePowiatType>(db.Type)
        });

        AddConfiguration<DatabaseGminaRodz, ResponseGminaType>(db => new(db.GminaRodzCode, db.Name));
        AddConfiguration<DatabaseGmina, ResponseGmina>((m, db) => new ResponseGmina
        {
            WojewodztwoCode = db.Powiat.WojewodztwoCode,
            PowiatCode = db.Powiat.PowiatCode,
            GminaCode = db.GminaCode,
            GminaRodzCode = db.GminaRodzCode,
            Name = db.Name,
            GminaType = m.Map<ResponseGminaType>(db.Rodz),
        });

        AddConfiguration<DatabaseMiejscowoscType, ResponseMiejscowoscType>(db => new(db.TypeCode, db.Name));
        AddConfiguration<DatabaseMiejscowosc, ResponseMiejscowosc>((m, db) => new ResponseMiejscowosc
        {
            WojewodztwoCode = db.Gmina.Powiat.WojewodztwoCode,
            PowiatCode = db.Gmina.Powiat.PowiatCode,
            GminaCode = db.Gmina.GminaCode,
            GminaRodzCode = db.Gmina.GminaRodzCode,
            MiejscowoscId = db.MiejscowoscCode,
            Name = db.Name,
            MiejscowoscType = m.Map<ResponseMiejscowoscType>(db.Type),
        });

        AddConfiguration<DatabaseUlicaType, ResponseUlicaType>(db => new(db.TypeCode, db.Name));
        AddConfiguration<DatabaseUlica, ResponseUlica>((m, db) => new ResponseUlica
        {
            UlicaId = db.UlicaCode,
            Name = db.Name,
            UlicaType = db.Type is null
                ? null
                : m.Map<ResponseUlicaType>(db.Type),
        });
    }
}