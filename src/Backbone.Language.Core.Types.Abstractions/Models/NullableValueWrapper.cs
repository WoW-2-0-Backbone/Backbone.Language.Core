namespace Backbone.Language.Core.Types.Abstractions.Models;

/// <summary>
/// Represents base implementation of nullable value wrapper.
/// </summary>
/// <typeparam name="TValue">The type of the wrapped nullable value.</typeparam>
public record NullableValueWrapper<TValue>(TValue? Value) : INullableValueWrapper<TValue>
{
    /// <inheritdoc/>
    public bool HasValue => Value is not null;
}