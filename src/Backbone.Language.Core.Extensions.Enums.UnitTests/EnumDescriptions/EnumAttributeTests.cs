using System.ComponentModel;
using Backbone.Language.Core.Extensions.Enums.Extensions;
using Backbone.Language.Core.Extensions.Enums.UnitTests.EnumDescriptions.Models;
using FluentAssertions;

namespace Backbone.Language.Core.Extensions.Enums.UnitTests.EnumDescriptions;

/// <summary>
/// Represents tests for enum attributes
/// </summary>
public class EnumAttributeTests
{
    [Fact]
    public void GetCustomAttributeValue_WhenAttributeExists_ShouldReturnAttribute()
    {
        // Arrange
        var enumValue = MixedEnum.ThirdValue;

        // Act
        var attr = enumValue.GetCustomAttributeValue<DescriptionAttribute>();

        // Assert
        attr.Should().NotBeNull();
        attr!.Description.Should().Be("Third Value");
    }

    [Fact]
    public void GetCustomAttributeValue_WhenAttributeMissing_ShouldReturnNull()
    {
        // Arrange
        var enumValue = MixedEnum.SecondValue;

        // Act
        var attr = enumValue.GetCustomAttributeValue<DescriptionAttribute>();

        // Assert
        attr.Should().BeNull();
    }
    
    [Fact]
    public void GetAllValuesAndAttributeValues_ShouldReturnAllAttributes_WhenEnumIsValid()
    {
        // Act
        var list = EnumAttributeExtensions.GetAllValuesAndAttributeValues<ValidEnum, DescriptionAttribute>();
    
        // Assert
        list.Should().HaveCount(3);
        list.Should().Contain(kvp =>
            kvp.Key.Equals(ValidEnum.FirstValue) && kvp.Value != null && kvp.Value.Description == "First Value");
        list.Should().Contain(kvp => 
            kvp.Key.Equals(ValidEnum.SecondValue) && kvp.Value != null && kvp.Value.Description == "Second Value");
        list.Should().Contain(kvp =>
            kvp.Key.Equals(ValidEnum.ThirdValue) && kvp.Value != null && kvp.Value.Description == "Third Value");
    }
    
    [Fact]
    public void GetAllValuesAndAttributeValues_ShouldThrowException_WhenEnumIsInvalid()
    {
        // Act
        var list = EnumAttributeExtensions.GetAllValuesAndAttributeValues<MixedEnum, DescriptionAttribute>();
    
        // Assert
        list.Should().HaveCount(3);
        list.Should().Contain(kvp =>
            kvp.Key.Equals(MixedEnum.FirstValue) && kvp.Value != null && kvp.Value.Description == "First Value");
        list.Should().Contain(kvp => 
            kvp.Key.Equals(MixedEnum.SecondValue) && kvp.Value == null);
        list.Should().Contain(kvp =>
            kvp.Key.Equals(MixedEnum.ThirdValue) && kvp.Value != null && kvp.Value.Description == "Third Value");
    }
}