using System.ComponentModel;

namespace Backbone.Language.Core.Extensions.Enums.UnitTests.EnumDescriptions.Models;

internal enum ValidEnum
{
    [Description("First Value")]
    FirstValue,

    [Description("Second Value")]
    SecondValue,

    [Description("Third Value")]
    ThirdValue
}