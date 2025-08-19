using System.Collections;
using IOLink.NET.Core.Models;

namespace IOLink.NET.Integration;

/// <summary>
/// Responsible for wrapping conversion results into strongly-typed ConversionResult objects.
/// </summary>
public class ConversionResultWrapper
{
    /// <summary>
    /// Wraps a converted object into a ConversionResult that distinguishes between scalar and complex values.
    /// </summary>
    /// <param name="convertedValue">The object to wrap.</param>
    /// <returns>A ConversionResult containing either a ScalarResult or ComplexResult.</returns>
    public ConversionResult WrapConversionResult(object? convertedValue)
    {
        if (convertedValue is null)
        {
            // null is treated as a scalar value
            return new ScalarResult(null!, typeof(object));
        }

        var type = convertedValue.GetType();

        // Check if it's a scalar type
        if (IsScalarType(type))
        {
            return new ScalarResult(convertedValue, type);
        }

        // If it's a complex type, try to extract key-value pairs
        if (convertedValue is IDictionary<string, object> dictionary)
        {
            var values = dictionary
                .Select(kvp =>
                {
                    var scalarResult =
                        kvp.Value is null ? new ScalarResult(null!, typeof(object))
                        : IsScalarType(kvp.Value.GetType())
                            ? new ScalarResult(kvp.Value, kvp.Value.GetType())
                        : new ScalarResult(kvp.Value, kvp.Value.GetType()); // Complex nested values treated as scalar for now

                    return new KeyValuePair<string, ScalarResult>(kvp.Key, scalarResult);
                })
                .ToList();

            return new ComplexResult(values);
        }

        // Handle tuple-based complex types (like from IODD conversion)
        if (convertedValue is IEnumerable<(string key, object value)> tupleEnumerable)
        {
            var values = tupleEnumerable
                .Select(tuple =>
                {
                    var scalarResult =
                        tuple.value is null ? new ScalarResult(null!, typeof(object))
                        : IsScalarType(tuple.value.GetType())
                            ? new ScalarResult(tuple.value, tuple.value.GetType())
                        : new ScalarResult(tuple.value, tuple.value.GetType());

                    return new KeyValuePair<string, ScalarResult>(tuple.key, scalarResult);
                })
                .ToList();

            return new ComplexResult(values);
        }

        // For other complex types, use reflection to extract properties as key-value pairs
        var properties = type.GetProperties()
            .Where(p => p.CanRead)
            .Select(p =>
            {
                var value = p.GetValue(convertedValue);
                var scalarResult = value is null
                    ? new ScalarResult(null!, typeof(object))
                    : new ScalarResult(value, value.GetType());

                return new KeyValuePair<string, ScalarResult>(p.Name, scalarResult);
            })
            .ToList();

        return new ComplexResult(properties);
    }

    private static bool IsScalarType(Type type)
    {
        return type.IsPrimitive
            || type.IsEnum
            || type == typeof(string)
            || type == typeof(decimal)
            || type == typeof(DateTime)
            || type == typeof(DateTimeOffset)
            || type == typeof(TimeSpan)
            || type == typeof(Guid)
            || (
                type.IsGenericType
                && type.GetGenericTypeDefinition() == typeof(Nullable<>)
                && IsScalarType(Nullable.GetUnderlyingType(type)!)
            );
    }
}
