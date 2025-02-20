using System.ComponentModel;

namespace Backbone.Language.Core.Extensions.Enums.UnitTests.EnumDescriptions.Models;

internal enum MixedEnum
{
    [Description("First Value")]
    FirstValue,

    SecondValue,

    [Description("Third Value")]
    ThirdValue
}