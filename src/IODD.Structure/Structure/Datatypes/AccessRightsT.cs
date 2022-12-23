namespace IODD.Structure.Structure.Datatypes;

public enum AccessRightsT
{
    ReadOnly,
    ReadWrite,
    WriteOnly

}

public static class AccessRightsTConverter
{
    public static AccessRightsT? ParseOptional(string accessRights) => accessRights switch
    {
        "Ro" => AccessRightsT.ReadOnly,
        "Rw" => AccessRightsT.ReadWrite,
        "Wo" => AccessRightsT.WriteOnly,
        _ => null
    };

    public static AccessRightsT Parse(string? accessRights)
    => ParseOptional(accessRights ?? throw new ArgumentNullException(nameof(accessRights)))
        ?? throw new ArgumentOutOfRangeException($"{accessRights} could not parsed to AccessRightsT.");
}