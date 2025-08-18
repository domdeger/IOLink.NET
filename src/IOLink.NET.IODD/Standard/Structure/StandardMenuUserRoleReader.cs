using System.Xml.Serialization;
using IOLink.NET.IODD.Standard.Definition;
using IOLink.NET.IODD.Standard.Definition.IODDMenuUserRoleDefinitions;

namespace IOLink.NET.IODD.Standard.Structure;

public static class StandardMenuUserRoleReader
{
    private static readonly IODDMenuUserRoleDefinitions? _ioddMenuUserRoleDefinitions;

    public const string IdentificationMenu = "STD_TN_MN_Identification";
    public const string ParameterMenu = "STD_TN_MN_Parameter";
    public const string ObservationMenu = "STD_TN_MN_Observation";
    public const string DiagnosisMenu = "STD_TN_MN_Diagnosis";
    public const string ObserverRoleMenuSet = "STD_TN_MR_Observer";
    public const string MaintenanceRoleMenuSet = "STD_TN_MR_Maintenance";
    public const string SpecialistRoleMenuSet = "STD_TN_MR_Specialist";

    static StandardMenuUserRoleReader()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(IODDMenuUserRoleDefinitions));
        using (var reader = new StreamReader("./XML/Tool-MenuUserRole_X113.xml"))
        {
            _ioddMenuUserRoleDefinitions = (IODDMenuUserRoleDefinitions?)
                serializer.Deserialize(reader);
        }
    }

    public static string GetStandardMenuUserRoleText(string id, string lang)
    {
        if (id == string.Empty)
        {
            return string.Empty;
        }

        var texts = _ioddMenuUserRoleDefinitions?.ExternalTextCollection.PrimaryLanguage.Text;
        return texts?.Where(x => x.Id == id).Single().Value ?? string.Empty;
    }
}
