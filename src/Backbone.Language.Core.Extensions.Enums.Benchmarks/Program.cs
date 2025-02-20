using System.ComponentModel;
using Backbone.Language.Core.Extensions.Enums.Extensions;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;

var summary = BenchmarkRunner.Run<EnumExtensionsBenchmark>();

public enum TestEnum
{
    [Description("First Value")]
    FirstValue,

    SecondValue,

    [Description("Third Value")]
    ThirdValue
}

[MemoryDiagnoser]
public class EnumExtensionsBenchmark
{
    private const TestEnum ValueWithDesc = TestEnum.FirstValue;
    private const TestEnum ValueWithoutDesc = TestEnum.SecondValue;
    private const string ValidDescription = "First Value";
    private const string InvalidDescription = "Nonexistent";

    #region GetDescription & GetDescriptionOrValue

    [Benchmark]
    public string Benchmark_GetDescription_Existing() => ValueWithDesc.GetDescription();

    [Benchmark]
    public string Benchmark_GetDescriptionOrValue_Existing() => ValueWithDesc.GetDescriptionOrValue();

    [Benchmark]
    public string Benchmark_GetDescriptionOrValue_Missing() => ValueWithoutDesc.GetDescriptionOrValue();

    #endregion

    #region TryGetDescription

    [Benchmark]
    public bool Benchmark_TryGetDescription_Existing()
    {
        return ValueWithDesc.TryGetDescription(out var _);
    }

    [Benchmark]
    public bool Benchmark_TryGetDescription_Missing()
    {
        return ValueWithoutDesc.TryGetDescription(out var _);
    }

    #endregion

    #region ParseByDescription

    [Benchmark]
    public TestEnum Benchmark_ParseByDescription_Valid() =>
        EnumDescriptionExtensions.ParseByDescription<TestEnum>(ValidDescription);

    // Note: For performance tests, catching exceptions (for invalid cases) might skew results.
    [Benchmark]
    public bool Benchmark_TryParseByDescription_Generic_Valid()
    {
        return EnumDescriptionExtensions.TryParseByDescription<TestEnum>(ValidDescription, out var _);
    }

    [Benchmark]
    public bool Benchmark_TryParseByDescription_Generic_Invalid()
    {
        return EnumDescriptionExtensions.TryParseByDescription<TestEnum>(InvalidDescription, out var _);
    }

    [Benchmark]
    public bool Benchmark_TryParseByDescription_NonGeneric_Valid()
    {
        return EnumDescriptionExtensions.TryParseByDescription(typeof(TestEnum), ValidDescription, out var _);
    }

    [Benchmark]
    public bool Benchmark_TryParseByDescription_NonGeneric_Invalid()
    {
        return EnumDescriptionExtensions.TryParseByDescription(typeof(TestEnum), InvalidDescription, out var _);
    }

    #endregion

    #region GetAllValuesAndDescriptions

    [Benchmark]
    public int Benchmark_GetAllValuesAndDescriptions_Generic()
    {
        var list = EnumDescriptionExtensions.GetAllValuesAndDescriptions<TestEnum>();
        return list.Count;
    }

    [Benchmark]
    public int Benchmark_GetAllValuesAndDescriptions_NonGeneric()
    {
        var list = EnumDescriptionExtensions.GetAllValuesAndDescriptions(typeof(TestEnum));
        return list.Count;
    }

    [Benchmark]
    public int Benchmark_GetAllAvailableValuesAndDescriptions_Generic()
    {
        var list = EnumDescriptionExtensions.GetAllAvailableValuesAndDescriptions<TestEnum>();
        return list.Count;
    }

    [Benchmark]
    public int Benchmark_GetAllAvailableValuesAndDescriptions_NonGeneric()
    {
        var list = EnumDescriptionExtensions.GetAllAvailableValuesAndDescriptions(typeof(TestEnum));
        return list.Count;
    }

    #endregion

    #region Custom Attribute Methods

    [Benchmark]
    public DescriptionAttribute Benchmark_GetCustomAttributeValue_Existing()
    {
        return ValueWithDesc.GetCustomAttributeValue<DescriptionAttribute>()!;
    }

    [Benchmark]
    public DescriptionAttribute Benchmark_GetCustomAttributeValue_Missing()
    {
        return ValueWithoutDesc.GetCustomAttributeValue<DescriptionAttribute>()!;
    }

    [Benchmark]
    public int Benchmark_GetAllValuesAndAttributeValues()
    {
        var list = EnumAttributeExtensions.GetAllValuesAndAttributeValues<TestEnum, DescriptionAttribute>();
        return list.Count;
    }

    #endregion
}