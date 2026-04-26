// Ignore Spelling: regon, pib, krs

using RADON.Base;

namespace RADON.Institutions;

public sealed class InstitutionUriBuilder(Uri baseUri) : BaseUriBuilder<InstitutionQueryParameters>(baseUri)
{
    private const string QUERY_PARAMETER_RESULT_NUMBERS = "resultNumbers";
    private const string QUERY_PARAMETER_TOKEN = "token";

    private const string QUERY_PARAMETER_INSTITUTION_UUID = "institutionUuid";
    private const string QUERY_PARAMETER_INSTITUTION_UID = "institutionUid";
    private const string QUERY_PARAMETER_ID = "id";
    private const string QUERY_PARAMETER_NAME = "name";

    private const string QUERY_PARAMETER_I_KIND_CD = "iKindCd";
    private const string QUERY_PARAMETER_STATUS_CODE = "statusCode";

    private const string QUERY_PARAMETER_VOIVODESHIP_CODE = "voivodeshipCode";
    private const string QUERY_PARAMETER_REGON = "regon";
    private const string QUERY_PARAMETER_SUPERVISING_INSTITUTION_ID = "supervisingInstitutionId";
    private const string QUERY_PARAMETER_U_TYPE_CD = "uTypeCd";
    private const string QUERY_PARAMETER_SI_TYPE_CD = "siTypeCd";
    private const string QUERY_PARAMETER_PIB = "pib";
    private const string QUERY_PARAMETER_CITY = "city";
    private const string QUERY_PARAMETER_BRANCH_CITY = "branchCity";
    private const string QUERY_PARAMETER_KRS = "krs";
    private const string QUERY_PARAMETER_NIP = "nip";
    private const string QUERY_PARAMETER_MINISTRY_NUMBER = "ministryNumber";
    private const string QUERY_PARAMETER_PAN_NUMBER = "panNumber";
    private const string QUERY_PARAMETER_EUN_NUMBER = "eunNumber";
    private const string QUERY_PARAMETER_I_START_DATE_FROM = "iStartDateFrom";
    private const string QUERY_PARAMETER_I_START_DATE_TO = "iStartDateTo";
    private const string QUERY_PARAMETER_LAST_REFRESH = "lastRefresh";


    protected override HashSet<KeyValuePair<string, string>> PrepareInputParameters(InstitutionQueryParameters parameters)
    {
        var queryParameters = new HashSet<KeyValuePair<string, string>>();

        AddParameter(queryParameters, QUERY_PARAMETER_RESULT_NUMBERS, parameters.ResultNumbers.ToString());
        AddParameter(queryParameters, QUERY_PARAMETER_TOKEN, parameters.Token);
        AddParameter(queryParameters, QUERY_PARAMETER_INSTITUTION_UUID, parameters.InstitutionUuid);
        AddParameter(queryParameters, QUERY_PARAMETER_INSTITUTION_UID, parameters.InstitutionUid);
        AddParameter(queryParameters, QUERY_PARAMETER_ID, parameters.Id);
        AddParameter(queryParameters, QUERY_PARAMETER_NAME, parameters.Name);

        AddParameter(queryParameters, QUERY_PARAMETER_VOIVODESHIP_CODE, parameters.VoivodeshipCode);
        AddParameter(queryParameters, QUERY_PARAMETER_REGON, parameters.Regon);
        AddParameter(queryParameters, QUERY_PARAMETER_SUPERVISING_INSTITUTION_ID, parameters.SupervisingInstitutionId);
        AddParameter(queryParameters, QUERY_PARAMETER_U_TYPE_CD, parameters.UTypeCd);
        AddParameter(queryParameters, QUERY_PARAMETER_SI_TYPE_CD, parameters.SiTypeCd);
        AddParameter(queryParameters, QUERY_PARAMETER_PIB, parameters.Pib);
        AddParameter(queryParameters, QUERY_PARAMETER_CITY, parameters.City);
        AddParameter(queryParameters, QUERY_PARAMETER_BRANCH_CITY, parameters.BranchCity);
        AddParameter(queryParameters, QUERY_PARAMETER_KRS, parameters.Krs);
        AddParameter(queryParameters, QUERY_PARAMETER_NIP, parameters.Nip);
        AddParameter(queryParameters, QUERY_PARAMETER_MINISTRY_NUMBER, parameters.MinistryNumber);
        AddParameter(queryParameters, QUERY_PARAMETER_PAN_NUMBER, parameters.PanNumber);
        AddParameter(queryParameters, QUERY_PARAMETER_EUN_NUMBER, parameters.EunNumber);
        AddParameter(queryParameters, QUERY_PARAMETER_I_START_DATE_FROM, parameters.IStartDateFrom);
        AddParameter(queryParameters, QUERY_PARAMETER_I_START_DATE_TO, parameters.IStartDateTo);
        AddParameter(queryParameters, QUERY_PARAMETER_LAST_REFRESH, parameters.LastRefresh);

        var iKindCds = parameters.IKindCd
            .Where(i => !string.IsNullOrWhiteSpace(i))
            .ToHashSet();

        var statusCodes = parameters.StatusCode
            .Where(i => !string.IsNullOrWhiteSpace(i))
            .ToHashSet();

        foreach (var i in iKindCds)
        {
            AddParameter(queryParameters, QUERY_PARAMETER_I_KIND_CD, i);
        }

        foreach (var i in statusCodes)
        {
            AddParameter(queryParameters, QUERY_PARAMETER_STATUS_CODE, i);
        }

        return queryParameters;
    }
}