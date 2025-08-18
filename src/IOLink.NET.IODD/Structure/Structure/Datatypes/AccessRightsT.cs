namespace IOLink.NET.IODD.Structure.Datatypes;

public enum AccessRightsT
{
    ReadOnly,
    ReadWrite,
    WriteOnly

}

public static class AccessRightsTConverter
{
    public static AccessRightsT? ParseOptional(string accessRights) => accessRights.ToLower(System.Globalization.CultureInfo.CurrentCulture) switch
    {
        "ro" => AccessRightsT.ReadOnly,
        "rw" => AccessRightsT.ReadWrite,
        "wo" => AccessRightsT.WriteOnly,
        _ => null
    };

    public static AccessRightsT Parse(string? accessRights)
    => ParseOptional(accessRights ?? throw new ArgumentNullException(nameof(accessRights)))
        ?? throw new ArgumentOutOfRangeException($"{accessRights} could not parsed to AccessRightsT.");
}
