namespace Backbone.Language.Core.Types.Abstractions.Models;

/// <summary>
/// Defines an abstraction value wrapper for a value of type <typeparamref name="TValue"/>.
/// </summary>
/// <remarks>
/// Used to wrap collections for cases where the collections are allowed in schema
/// </remarks>
/// <typeparam name="TValue">The type of the wrapped value.</typeparam>
public interface IAbstractValueWrapper<out TValue>
{
    /// <summary>
    /// Gets the wrapped value.
    /// </summary>
    TValue Value { get; }
}