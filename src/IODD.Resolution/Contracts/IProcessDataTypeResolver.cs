namespace IOLinkNET.IODD.Resolution.Contracts;

public interface IProcessDataTypeResolver
{
    bool HasCondition();

    ResolvedCondition ResolveCondition();

    ParsableDatatype ResolveProcessDataIn(int? condition = null);

    ParsableDatatype ResolveProcessDataOut(int? condition = null);
}