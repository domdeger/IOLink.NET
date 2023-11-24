using IOLinkNET.IODD.Structure.Datatypes;
using IOLinkNET.IODD.Structure.DeviceFunction;
using IOLinkNET.IODD.Structure.ProcessData;

namespace IOLinkNET.IODD.Resolution;

internal class ParsableDatatypeConverter
{
    private readonly DatatypeResolver _datatypeResolver;

    public ParsableDatatypeConverter(DatatypeResolver datatypeResolver)
    {
        _datatypeResolver = datatypeResolver;
    }

    public ParsableDatatype Convert(ProcessDataItemT processDataIn) => ConvertInternal(_datatypeResolver.Resolve(processDataIn), processDataIn.Id);
    public ParsableDatatype Convert(DatatypeT type) => ConvertInternal(type);

    internal ParsableDatatype Convert(DatatypeT type, string name) => ConvertInternal(type, name);
    public ParsableDatatype Convert(VariableT variable) => ConvertInternal(_datatypeResolver.Resolve(variable), variable.Id);

    private ParsableDatatype ConvertInternal(DatatypeT type, string? name = null)
    => type switch
    {
        ComplexDatatypeT complex => ConvertComplex(complex, name),
        SimpleDatatypeT simple => ConvertScalar(simple, name),
        _ => throw new InvalidOperationException($"{type.GetType().Name} cannot be converted to a parsable datatype.")
    };

    private static ParsableSimpleDatatypeDef ConvertScalar(SimpleDatatypeT scalarType, string? name = null)
    {
        var kindOfDataType = DetermineKindOfDatatype(scalarType);
        var length = DetermineScalarBitLength(scalarType);
        var typeFriendlyName = name ?? scalarType.Id ?? string.Empty;

        return kindOfDataType switch
        {
            KindOfSimpleType.String => new ParsableStringDef(typeFriendlyName, ((StringT)scalarType).FixedLength, ((StringT)scalarType).Encoding),
            _ => new ParsableSimpleDatatypeDef(typeFriendlyName, kindOfDataType, length)
        };
    }

    private static ushort DetermineScalarBitLength(SimpleDatatypeT scalarType)
        => scalarType switch
        {
            UIntegerT uInteger => uInteger.BitLength,
            IntegerT integer => integer.BitLength,
            StringT stringT => (ushort)(stringT.FixedLength * 8),
            OctetStringT octetString => (ushort)(octetString.FixedLength * 8),
            BooleanT => 1,
            TimeSpanT => 64,
            TimeT => 64,
            Float32T => 32,
            _ => throw new InvalidOperationException($"Length cannot be determined for {scalarType.GetType().Name}.")
        };

    private static KindOfSimpleType DetermineKindOfDatatype(SimpleDatatypeT scalarType)
        => scalarType switch
        {
            UIntegerT => KindOfSimpleType.UInteger,
            IntegerT => KindOfSimpleType.Integer,
            Float32T => KindOfSimpleType.Float,
            StringT => KindOfSimpleType.String,
            OctetStringT => KindOfSimpleType.OctetString,
            BooleanT => KindOfSimpleType.Boolean,
            TimeSpanT => KindOfSimpleType.TimespanT,
            _ => throw new InvalidOperationException($"{scalarType.GetType().Name} cannot be mapped to a simple type.")
        };

    private ParsableDatatype ConvertComplex(ComplexDatatypeT complexType, string? name = null)
        => complexType switch
        {
            RecordT recordT => ConvertRecord(recordT, name),
            _ => throw new InvalidOperationException()
        };

    private ParsableRecord ConvertRecord(RecordT recordType, string? name = null)
    {
        var parsableRecordItems = recordType.Items.Select(rItem => new ParsableRecordItem(
            ConvertScalar(_datatypeResolver.Resolve(rItem) as SimpleDatatypeT ?? throw new InvalidOperationException("RecordItem did not contain simple type."), rItem.Name.TextId),
                            rItem.Name.TextId, rItem.BitOffset, rItem.Subindex));
        var recordName = recordType.Id ?? name ?? throw new NullReferenceException("Name needs to be set.");

        return new ParsableRecord(recordName, recordType.BitLength, parsableRecordItems);
    }
}