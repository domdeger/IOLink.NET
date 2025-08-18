namespace IOLink.NET.IODD.Resolution.Contracts;

public interface IProcessDataTypeResolver
{
    /// <summary>
    /// Checks if a process data condition is present.
    /// </summary>
    /// <returns>True if a condition is present, false otherwise.</returns>
    bool HasCondition();

    /// <summary>
    /// Resolves the condition of the process data. If no condition is available, an exception is thrown. Existance of a condition can be checked with <see cref="HasCondition"/>.
    /// </summary>
    /// <returns>A representation of the condition parameter that can be used to retrieve and decode the condition value.</returns>
    ResolvedCondition ResolveCondition();

    /// <summary>
    /// Resolves the process data type for the process data in direction. If no process data is available, null is returned.
    /// </summary>
    /// <param name="condition">The currently active condition value</param>
    /// <returns>A representation of the iodd data type that can be used to convert binary IO-Link data.</returns>
    ParsableDatatype? ResolveProcessDataIn(int? condition = null);

    /// <summary>
    /// Resolves the process data type for the process data out direction. If no process data is available, null is returned.
    /// </summary>
    /// <param name="condition">The currently active condition value</param>
    /// <returns>A representation of the iodd data type that can be used to convert binary IO-Link data.</returns>
    ParsableDatatype? ResolveProcessDataOut(int? condition = null);
}
