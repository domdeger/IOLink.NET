using System.Xml.Serialization;

using IOLinkNET.Visualization.Structure.Structure.XML;
namespace IOLinkNET.Visualization.Structure.Structure;
public static class MenuUserRoleDefinition
{
    private static readonly IODDMenuUserRoleDefinitions? _ioddMenuUserRoleDefinitions;
    public static readonly string IdentificationMenu = "STD_TN_MN_Identification";
    public static readonly string ParameterMenu = "STD_TN_MN_Parameter";
    public static readonly string ObservationMenu = "STD_TN_MN_Observation";
    public static readonly string DiagnosisMenu = "STD_TN_MN_Diagnosis";
    public static readonly string ObserverRoleMenuSet = "STD_TN_MR_Observer";
    public static readonly string MaintenanceRoleMenuSet = "STD_TN_MR_Maintenance";
    public static readonly string SpecialistRoleMenuSet = "STD_TN_MR_Specialist";

    static MenuUserRoleDefinition()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(IODDMenuUserRoleDefinitions));
        using (StreamReader reader = new StreamReader("./XML/Tool-MenuUserRole_X113.xml"))
        {
            _ioddMenuUserRoleDefinitions = (IODDMenuUserRoleDefinitions?)serializer.Deserialize(reader);
        }
    }

    public static string GetTranslatedText(string id, string lang)
    {
        if (id == string.Empty)
        {
            return string.Empty;
        }

        var texts = _ioddMenuUserRoleDefinitions?.ExternalTextCollection.PrimaryLanguage.Text;
        return texts?.Where(x => x.Id == id).Single().Value ?? string.Empty;
    }
}
