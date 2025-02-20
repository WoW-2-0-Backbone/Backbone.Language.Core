using System.ComponentModel;
using Backbone.Language.Core.Extensions.Enums.Extensions;
using Backbone.Language.Core.Extensions.Enums.UnitTests.EnumDescriptions.Models;
using FluentAssertions;

namespace Backbone.Language.Core.Extensions.Enums.UnitTests.EnumDescriptions;

/// <summary>
/// Represents tests for enum descriptions
/// </summary>
public class EnumDescriptionTests
{
    #region Getting description

    [Fact]
    public void GetDescription_WhenDescriptionExists_ShouldReturnDescription()
    {
        // Arrange
        var enumValue = MixedEnum.FirstValue;

        // Act
        var description = enumValue.GetDescription();

        // Assert
        description.Should().Be("First Value");
    }

    [Fact]
    public void GetDescription_WhenDescriptionMissing_ShouldThrowException()
    {
        // Arrange
        var enumValue = MixedEnum.SecondValue;

        // Act
        Action act = () => enumValue.GetDescription();

        // Assert
        act
            .Should()
            .Throw<ArgumentException>()
            .WithMessage($"No description is set for enum value {enumValue.ToString()}.");
    }

    [Fact]
    public void GetDescriptionOrValue_WhenDescriptionExists_ShouldReturnDescription()
    {
        // Arrange
        var enumValue = MixedEnum.FirstValue;

        // Act
        var result = enumValue.GetDescriptionOrValue();

        // Assert
        result.Should().Be("First Value");
    }

    [Fact]
    public void GetDescriptionOrValue_WhenDescriptionMissing_ShouldReturnEnumValue()
    {
        // Arrange
        var enumValue = MixedEnum.SecondValue;

        // Act
        var result = enumValue.GetDescriptionOrValue();

        // Assert
        result.Should().Be("SecondValue");
    }

    [Fact]
    public void TryGetDescription_WhenDescriptionExists_ShouldReturnTrueAndDescription()
    {
        // Arrange
        var enumValue = MixedEnum.FirstValue;

        // Act
        var isSuccess = enumValue.TryGetDescription(out var description);

        // Assert
        isSuccess.Should().BeTrue();
        description.Should().Be("First Value");
    }

    [Fact]
    public void TryGetDescription_WhenDescriptionMissing_ShouldReturnFalseAndEnumValue()
    {
        // Arrange
        var enumValue = MixedEnum.SecondValue;

        // Act
        var isSuccess = enumValue.TryGetDescription(out var description);

        // Assert
        isSuccess.Should().BeFalse();
        description.Should().Be("SecondValue");
    }

    #endregion

    #region Parsing by description

    [Fact]
    public void ParseByDescription_WithValidDescription_ShouldReturnMatchingEnumValue()
    {
        // Arrange
        var description = "Third Value";

        // Act
        var result = EnumDescriptionExtensions.ParseByDescription<MixedEnum>(description);

        // Assert
        result.Should().Be(MixedEnum.ThirdValue);
    }

    [Fact]
    public void ParseByDescription_WithInvalidDescription_ShouldThrowArgumentException()
    {
        // Arrange
        var description = "Nonexistent";

        // Act
        Action act = () => EnumDescriptionExtensions.ParseByDescription<ValidEnum>(description);

        // Assert
        act
            .Should()
            .Throw<ArgumentException>()
            .WithMessage($"No enum value with description '{description}' found in {nameof(ValidEnum)}.");
    }

    [Fact]
    public void TryParseByDescription_WithValidDescription_ShouldReturnTrueAndEnumValue()
    {
        // Arrange
        var description = "First Value";

        // Act
        var isSuccessA = EnumDescriptionExtensions
            .TryParseByDescription<MixedEnum>(description, out var resultA);
        var isSuccessB = EnumDescriptionExtensions
            .TryParseByDescription(typeof(MixedEnum), description, out var resultB);

        // Assert
        isSuccessA.Should().BeTrue();
        resultA.Should().Be(MixedEnum.FirstValue);

        isSuccessB.Should().BeTrue();
        resultB.Should().Be(MixedEnum.FirstValue);
    }

    [Fact]
    public void TryParseByDescription_WithInvalidDescription_ShouldReturnFalse()
    {
        // Arrange
        var description = "Nonexistent";
    
        // Act
        var isSuccessA = EnumDescriptionExtensions
            .TryParseByDescription<MixedEnum>(description, out var resultA);
        var isSuccessB = EnumDescriptionExtensions
            .TryParseByDescription(typeof(MixedEnum), description, out var resultB);

        // Assert
        isSuccessA.Should().BeFalse();
        resultA.Should().Be(MixedEnum.FirstValue);

        isSuccessB.Should().BeFalse();
        resultB.Should().Be(MixedEnum.FirstValue);
    }

    #endregion

    #region GetAllValuesAndDescriptions Tests

    [Fact]
    public void GetAllValuesAndDescriptions_ShouldReturnAllEnumValuesWithCorrectDescriptions()
    {
        // Act
        var listA = EnumDescriptionExtensions.GetAllValuesAndDescriptions<ValidEnum>();
        var listB = EnumDescriptionExtensions.GetAllValuesAndDescriptions(typeof(ValidEnum));

        // Assert
        listA.Should().HaveCount(3);
        listA.Should().Contain(kvp => kvp.Key.Equals(ValidEnum.FirstValue) && kvp.Value == "First Value");
        listA.Should().Contain(kvp => kvp.Key.Equals(ValidEnum.SecondValue) && kvp.Value == "Second Value");
        listA.Should().Contain(kvp => kvp.Key.Equals(ValidEnum.ThirdValue) && kvp.Value == "Third Value");
        
        listB.Should().HaveCount(3);
        listB.Should().Contain(kvp => kvp.Key.Equals(ValidEnum.FirstValue) && kvp.Value == "First Value");
        listB.Should().Contain(kvp => kvp.Key.Equals(ValidEnum.SecondValue) && kvp.Value == "Second Value");
        listB.Should().Contain(kvp => kvp.Key.Equals(ValidEnum.ThirdValue) && kvp.Value == "Third Value");
    }

    [Fact]
    public void GetAllValuesAndDescriptions_ShouldThrowArgumentException_WhenDescriptionIsMissing()
    {
        // Act
        Action actA = () => EnumDescriptionExtensions.GetAllValuesAndDescriptions<MixedEnum>();
        Action actB = () => EnumDescriptionExtensions.GetAllValuesAndDescriptions(typeof(MixedEnum));

        // Assert
        actA
            .Should()
            .Throw<ArgumentException>()
            .WithMessage($"No description is set for enum value {MixedEnum.SecondValue.ToString()}.");
        
        actB
            .Should()
            .Throw<ArgumentException>()
            .WithMessage($"No description is set for enum value {MixedEnum.SecondValue.ToString()}.");
    }

    [Fact]
    public void GetAllValuesAndDescriptions_ShouldReturnAllValues_WhenEnumIsValid()
    {
        // Act
        var listA = EnumDescriptionExtensions.GetAllAvailableValuesAndDescriptions<ValidEnum>();
        var listB = EnumDescriptionExtensions.GetAllAvailableValuesAndDescriptions(typeof(ValidEnum));
    
        // Assert
        listA.Should().HaveCount(3);
        listA.Should().Contain(kvp => kvp.Key.Equals(ValidEnum.FirstValue) && kvp.Value == "First Value");
        listA.Should().Contain(kvp => kvp.Key.Equals(ValidEnum.SecondValue) && kvp.Value == "Second Value");
        listA.Should().Contain(kvp => kvp.Key.Equals(ValidEnum.ThirdValue) && kvp.Value == "Third Value");
        
        listB.Should().HaveCount(3);
        listB.Should().Contain(kvp => kvp.Key.Equals(ValidEnum.FirstValue) && kvp.Value == "First Value");
        listB.Should().Contain(kvp => kvp.Key.Equals(ValidEnum.SecondValue) && kvp.Value == "Second Value");
        listB.Should().Contain(kvp => kvp.Key.Equals(ValidEnum.ThirdValue) && kvp.Value == "Third Value");
    }
    
    [Fact]
    public void GetAllValuesAndDescriptions_ShouldReturnAvailableValues_WhenEnumIsInvalid()
    {
        // Act
        var listA = EnumDescriptionExtensions.GetAllAvailableValuesAndDescriptions<MixedEnum>();
        var listB = EnumDescriptionExtensions.GetAllAvailableValuesAndDescriptions(typeof(MixedEnum));
    
        // Assert
        listA.Should().HaveCount(2);
        listA.Should().Contain(kvp => kvp.Key.Equals(MixedEnum.FirstValue) && kvp.Value == "First Value");
        listA.Should().Contain(kvp => kvp.Key.Equals(MixedEnum.ThirdValue) && kvp.Value == "Third Value");
        
        listB.Should().HaveCount(2);
        listB.Should().Contain(kvp => kvp.Key.Equals(MixedEnum.FirstValue) && kvp.Value == "First Value");
        listB.Should().Contain(kvp => kvp.Key.Equals(MixedEnum.ThirdValue) && kvp.Value == "Third Value");
    }
    
    #endregion
}