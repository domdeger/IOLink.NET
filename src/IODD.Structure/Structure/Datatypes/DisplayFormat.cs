namespace IOLinkNET.IODD.Structure.Structure.Datatypes;
public enum DisplayFormat
{
    Bin,
    Dec,
    Hex,
    Button,
    Event,
    MasterCycleTime,
    MinCycleTime
}

public static class DisplayFormatConverter
{
    public static DisplayFormat? ParseOptional(string displayFormat) => displayFormat.ToLower(System.Globalization.CultureInfo.CurrentCulture) switch
    {
        "bin" => DisplayFormat.Bin,
        "dec" => DisplayFormat.Dec,
        "hex" => DisplayFormat.Hex,
        "button" => DisplayFormat.Button,
        "event" => DisplayFormat.Event,
        "mastercycletime" => DisplayFormat.MasterCycleTime,
        "mincycletime" => DisplayFormat.MinCycleTime,
        _ => null
    };

    public static DisplayFormat Parse(string? displayFormat)
    => ParseOptional(displayFormat ?? throw new ArgumentNullException(nameof(displayFormat)))
        ?? throw new ArgumentOutOfRangeException($"{displayFormat} could not parsed to DisplayFormat.");
}
