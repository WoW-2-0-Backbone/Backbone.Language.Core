namespace Backbone.Language.Core.Types.Abstractions.Models;

/// <summary>
/// Represents base implementation of abstract value wrapper.
/// </summary>
/// <typeparam name="TValue">The type of the wrapped value.</typeparam>
/// <param name="Value">The value to wrap.</param>
public record AbstractValueWrapper<TValue>(TValue Value) : IAbstractValueWrapper<TValue>;