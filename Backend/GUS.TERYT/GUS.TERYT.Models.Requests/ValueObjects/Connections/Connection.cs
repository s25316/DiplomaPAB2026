// Ignore Spelling: Miejscowosc, Ulica
using Base.Models.Exceptions;
using GUS.TERYT.Models.Requests.ValueObjects.Miejscowosci;
using GUS.TERYT.Models.Requests.ValueObjects.Ulicy;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace GUS.TERYT.Models.Requests.ValueObjects.Connections;

[ModelBinder(typeof(ConnectionBinder))]
public sealed record Connection
{
    public MiejscowoscId MiejscowoscId { get; }
    public UlicaId? UlicaId { get; }


    private Connection(MiejscowoscId miejscowoscId, UlicaId? ulicaId)
    {
        MiejscowoscId = miejscowoscId;
        UlicaId = ulicaId;
    }


    public static implicit operator string(Connection value) => value.ToString();
    public static implicit operator Connection(string value) => Parse(value);

    public static bool TryParse([NotNullWhen(true)] string? value, [NotNullWhen(true)] out Connection? result)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            result = null;
            return false;
        }

        var trimmedValue = value.Trim();
        if (Regexes.Miejscowosc.IsMatch(trimmedValue))
        {
            result = new Connection(trimmedValue, null);
            return true;
        }
        if (Regexes.MiejscowoscUlica.IsMatch(trimmedValue))
        {
            var items = trimmedValue.Split('.');
            result = new Connection(items[0], items[1]);
            return true;
        }

        result = null;
        return false;
    }

    public static Connection Parse(string value)
    {
        if (!TryParse(value, out var result))
        {
            throw new ResourceException.IncorrectFormat(PrepareErrorMessage(value));
        }
        return result;
    }

    internal static string PrepareErrorMessage(string value)
    {
        var items = value.Trim().Split('.');
        var builder = new StringBuilder();

        try
        {
            MiejscowoscId.Parse(items[0]);
        }
        catch (Exception ex)
        {
            builder.AppendLine(ex.Message);
        }

        if (items.Count() == 1)
        {
            return builder.ToString();
        }

        try
        {
            UlicaId.Parse(items[1]);
        }
        catch (Exception ex)
        {
            builder.AppendLine(ex.Message);
        }

        return builder.ToString();
    }

    internal static string GetDescription() => Messages.MiejscowoscUlicaIdsDescription;
    public override string ToString() => UlicaId is not null
        ? $"{MiejscowoscId}.{UlicaId}"
        : MiejscowoscId.ToString();
}