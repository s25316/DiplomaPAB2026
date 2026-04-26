using RADON.Base;

namespace RADON.Courses;

internal sealed class CourseUriBuilder(Uri baseUri) : BaseUriBuilder<CourseQueryParameters>(baseUri)
{
    private const string QUERY_PARAMETER_RESULT_NUMBERS = "resultNumbers";
    private const string QUERY_PARAMETER_TOKEN = "token";

    private const string QUERY_PARAMETER_COURSE_UUID = "courseUuid";

    private const string QUERY_PARAMETER_COURSE_INSTANCE_UUID = "courseInstanceUuid";

    private const string QUERY_PARAMETER_COURSE_CODE = "courseCode";
    private const string QUERY_PARAMETER_COURSE_INSTANCE_CODE = "courseInstanceCode";
    private const string QUERY_PARAMETER_COURSE_NAME = "courseName";
    private const string QUERY_PARAMETER_LEVEL_CODE = "levelCode";
    private const string QUERY_PARAMETER_PROFILE_CODE = "profileCode";
    private const string QUERY_PARAMETER_CURRENT_STATUS_CODE = "currentStatusCode";

    private const string QUERY_PARAMETER_LEADING_INSTITUTION_UUID = "leadingInstitutionUuid";
    private const string QUERY_PARAMETER_LEADING_INSTITUTION_NAME = "leadingInstitutionName";
    private const string QUERY_PARAMETER_LEADING_INSTITUTION_IS_FOREIGN = "leadingInstitutionIsForeign";
    private const string QUERY_PARAMETER_LEADING_INSTITUTION_VOIVODESHIP_CODE = "leadingInstitutionVoivodeshipCode";
    private const string QUERY_PARAMETER_LEADING_INSTITUTION_CITY = "leadingInstitutionCity";

    private const string QUERY_PARAMETER_MAIN_INSTITUTION_UUID = "mainInstitutionUuid";
    private const string QUERY_PARAMETER_MAIN_INSTITUTION_NAME = "mainInstitutionName";
    private const string QUERY_PARAMETER_MAIN_INSTITUTION_KIND_CODE = "mainInstitutionKindCode";
    private const string QUERY_PARAMETER_SUPERVISING_INSTITUTION_UUID = "supervisingInstitutionUuid";

    private const string QUERY_PARAMETER_CO_LEADING_INSTITUTION_UUID = "coLeadingInstitutionUuid";
    private const string QUERY_PARAMETER_CO_LEADING_INSTITUTION_NAME = "coLeadingInstitutionName";
    private const string QUERY_PARAMETER_CO_LEADING_INSTITUTION_IS_FOREIGN = "coLeadingInstitutionIsForeign";

    private const string QUERY_PARAMETER_ORGANIZATIONAL_UNIT_UUID = "organizationalUnitUuid";
    private const string QUERY_PARAMETER_ORGANIZATIONAL_UNIT_FULL_NAME = "organizationalUnitFullName";

    private const string QUERY_PARAMETER_DISCIPLINE_CODE = "disciplineCode";
    private const string QUERY_PARAMETER_DISCIPLINE_NAME = "disciplineName";

    private const string QUERY_PARAMETER_LEGAL_BASIS_TYPE_CODE = "legalBasisTypeCode";
    private const string QUERY_PARAMETER_FORM_CODE = "formCode";
    private const string QUERY_PARAMETER_TITLE_CODE = "titleCode";
    private const string QUERY_PARAMETER_STATUS_CODE = "statusCode";
    private const string QUERY_PARAMETER_TEACHER_TRAINING = "teacherTraining";
    private const string QUERY_PARAMETER_PHILOLOGICAL = "philological";
    private const string QUERY_PARAMETER_PHILOLOGY_LANGUAGE_CODE = "philologyLanguageCode";
    private const string QUERY_PARAMETER_CO_LED = "coLed";
    private const string QUERY_PARAMETER_DUAL = "dual";
    private const string QUERY_PARAMETER_BRIDGING = "bridging";
    private const string QUERY_PARAMETER_COOP_WITH_VOCATIONAL = "coopWithVocational";
    private const string QUERY_PARAMETER_EDUCATION_LANGUAGE_CODE = "educationLanguageCode";

    private const string QUERY_PARAMETER_LAST_REFRESH = "lastRefresh";

    protected override HashSet<KeyValuePair<string, string>> PrepareInputParameters(CourseQueryParameters parameters)
    {
        var queryParams = new HashSet<KeyValuePair<string, string>>();

        AddParameter(queryParams, QUERY_PARAMETER_RESULT_NUMBERS, parameters.ResultNumbers.ToString());
        AddParameter(queryParams, QUERY_PARAMETER_TOKEN, parameters.Token);

        AddParameter(queryParams, QUERY_PARAMETER_COURSE_UUID, parameters.CourseUuid);

        AddParameter(queryParams, QUERY_PARAMETER_COURSE_INSTANCE_UUID, parameters.CourseInstanceUuid);

        AddParameter(queryParams, QUERY_PARAMETER_COURSE_CODE, parameters.CourseCode);
        AddParameter(queryParams, QUERY_PARAMETER_COURSE_INSTANCE_CODE, parameters.CourseInstanceCode);
        AddParameter(queryParams, QUERY_PARAMETER_COURSE_NAME, parameters.CourseName);
        AddParameter(queryParams, QUERY_PARAMETER_LEVEL_CODE, parameters.LevelCode);
        AddParameter(queryParams, QUERY_PARAMETER_PROFILE_CODE, parameters.ProfileCode);
        AddParameter(queryParams, QUERY_PARAMETER_CURRENT_STATUS_CODE, parameters.CurrentStatusCode);

        AddParameter(queryParams, QUERY_PARAMETER_LEADING_INSTITUTION_UUID, parameters.LeadingInstitutionUuid);
        AddParameter(queryParams, QUERY_PARAMETER_LEADING_INSTITUTION_NAME, parameters.LeadingInstitutionName);
        AddParameter(queryParams, QUERY_PARAMETER_LEADING_INSTITUTION_IS_FOREIGN, parameters.LeadingInstitutionIsForeign);
        AddParameter(queryParams, QUERY_PARAMETER_LEADING_INSTITUTION_VOIVODESHIP_CODE, parameters.LeadingInstitutionVoivodeshipCode);
        AddParameter(queryParams, QUERY_PARAMETER_LEADING_INSTITUTION_CITY, parameters.LeadingInstitutionCity);

        AddParameter(queryParams, QUERY_PARAMETER_MAIN_INSTITUTION_UUID, parameters.MainInstitutionUuid);
        AddParameter(queryParams, QUERY_PARAMETER_MAIN_INSTITUTION_NAME, parameters.MainInstitutionName);
        AddParameter(queryParams, QUERY_PARAMETER_MAIN_INSTITUTION_KIND_CODE, parameters.MainInstitutionKindCode);
        AddParameter(queryParams, QUERY_PARAMETER_SUPERVISING_INSTITUTION_UUID, parameters.SupervisingInstitutionUuid);

        AddParameter(queryParams, QUERY_PARAMETER_CO_LEADING_INSTITUTION_UUID, parameters.CoLeadingInstitutionUuid);
        AddParameter(queryParams, QUERY_PARAMETER_CO_LEADING_INSTITUTION_NAME, parameters.CoLeadingInstitutionName);
        AddParameter(queryParams, QUERY_PARAMETER_CO_LEADING_INSTITUTION_IS_FOREIGN, parameters.CoLeadingInstitutionIsForeign);

        AddParameter(queryParams, QUERY_PARAMETER_ORGANIZATIONAL_UNIT_UUID, parameters.OrganizationalUnitUuid);
        AddParameter(queryParams, QUERY_PARAMETER_ORGANIZATIONAL_UNIT_FULL_NAME, parameters.OrganizationalUnitFullName);

        AddParameter(queryParams, QUERY_PARAMETER_DISCIPLINE_CODE, parameters.DisciplineCode);
        AddParameter(queryParams, QUERY_PARAMETER_DISCIPLINE_NAME, parameters.DisciplineName);

        AddParameter(queryParams, QUERY_PARAMETER_LEGAL_BASIS_TYPE_CODE, parameters.LegalBasisTypeCode);
        AddParameter(queryParams, QUERY_PARAMETER_FORM_CODE, parameters.FormCode);
        AddParameter(queryParams, QUERY_PARAMETER_TITLE_CODE, parameters.TitleCode);
        AddParameter(queryParams, QUERY_PARAMETER_STATUS_CODE, parameters.StatusCode);

        AddParameter(queryParams, QUERY_PARAMETER_TEACHER_TRAINING, parameters.TeacherTraining);
        AddParameter(queryParams, QUERY_PARAMETER_PHILOLOGICAL, parameters.Philological);
        AddParameter(queryParams, QUERY_PARAMETER_PHILOLOGY_LANGUAGE_CODE, parameters.PhilologyLanguageCode);
        AddParameter(queryParams, QUERY_PARAMETER_CO_LED, parameters.CoLed);
        AddParameter(queryParams, QUERY_PARAMETER_DUAL, parameters.Dual);
        AddParameter(queryParams, QUERY_PARAMETER_BRIDGING, parameters.Bridging);
        AddParameter(queryParams, QUERY_PARAMETER_COOP_WITH_VOCATIONAL, parameters.CoopWithVocational);
        AddParameter(queryParams, QUERY_PARAMETER_EDUCATION_LANGUAGE_CODE, parameters.EducationLanguageCode);

        AddParameter(queryParams, QUERY_PARAMETER_LAST_REFRESH, parameters.LastRefresh);

        return queryParams;
    }
}