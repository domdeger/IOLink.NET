namespace IOLinkNET.IODD.Resolution.Contracts;

public interface IParameterTypeResolver
{
    ParsableDatatype GetParameter(int index, byte? subIndex = null);
}