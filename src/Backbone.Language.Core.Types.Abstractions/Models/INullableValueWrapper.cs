namespace Backbone.Language.Core.Types.Abstractions.Models;

/// <summary>
/// Defines a nullable value wrapper for a value of type <typeparamref name="TValue"/>.
/// </summary>
/// <remarks>
/// Used to wrap collections for cases where the collections are allowed in schema
/// </remarks>
/// <typeparam name="TValue">The type of the wrapped nullable value.</typeparam>
public interface INullableValueWrapper<out TValue>
{
    /// <summary>
    /// Gets the wrapped value.
    /// </summary>
    TValue? Value { get; }

    /// <summary>
    /// Gets a value indicating whether this instance has a non-null value.
    /// </summary>
    bool HasValue { get; }
}