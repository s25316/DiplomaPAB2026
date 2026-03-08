// Ignore Spelling: Zaloguj, Wyloguj, Dane
using System.Xml.Linq;

namespace GUS.REGON.Configurations;

internal class ElementDefinition
{
    private const string NAMESPACE_BASE = "http://CIS/BIR/PUBL/2014/07";
    private const string NAMESPACE_GET_VALUE = "http://CIS/BIR/2014/07";

    private const string ELEMENT_NAME_ZALOGUJ = "ZalogujResult";
    private const string ELEMENT_NAME_WYLOGUJ = "WylogujResult";
    private const string ELEMENT_NAME_GET_VALUE_RESULT = "GetValueResult";
    private const string ELEMENT_NAME_DANE = "dane";

    public XNamespace Namespace { get; }
    public string ElementName { get; }


    private ElementDefinition(XNamespace @namespace, string elementName)
    {
        Namespace = @namespace;
        ElementName = elementName;
    }


    public static readonly ElementDefinition Zaloguj = new(NAMESPACE_BASE, ELEMENT_NAME_ZALOGUJ);
    public static readonly ElementDefinition Wyloguj = new(NAMESPACE_BASE, ELEMENT_NAME_WYLOGUJ);
    public static readonly ElementDefinition Dane = new(NAMESPACE_BASE, ELEMENT_NAME_DANE);
    public static readonly ElementDefinition GetValue = new(NAMESPACE_GET_VALUE, ELEMENT_NAME_GET_VALUE_RESULT);
}