namespace Mapper.Tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        var configuration = new M1M2MappingConfiguration();
        var mapper = new Mapper([configuration]);

        var field2 = 15;
        var field3 = false;
        var field4 = 3.14;

        var input = new Model1
        {
            Field1 = "Example",
            Field2 = field2.ToString(),
            Field3 = field3.ToString(),
            Field4 = field4.ToString(),
            Field5 = "A;B;C; ",
        };

        var output = mapper.Map<Model1, Model2>(input);

        Assert.NotNull(output);
        Assert.Equal(input.Field1, output.Field1);
        Assert.Equal(field2, output.Field2);
        Assert.Equal(field3, output.Field3);
        Assert.Equal(field4, output.Field4);
        Assert.Equal(3, output.Field5.Field1.Count());
    }
}

public class M1M2MappingConfiguration : MappingConfiguration
{
    public M1M2MappingConfiguration()
    {
        AddConfiguration<Model1, Model3>(item => new Model3
        {
            Field1 = item.Field5.Split(";", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries),
        });

        AddConfiguration<Model1, Model2>((m, item) => new Model2
        {
            Field1 = item.Field1,
            Field2 = int.Parse(item.Field2),
            Field3 = bool.Parse(item.Field3),
            Field4 = double.Parse(item.Field4),
            Field5 = m.Map<Model1, Model3>(item),
        });
    }
}

public class Model1
{
    public string Field1 { get; init; } = null!;
    public string Field2 { get; init; } = null!;
    public string Field3 { get; init; } = null!;
    public string Field4 { get; init; } = null!;
    public string Field5 { get; init; } = null!;
}

public class Model2
{
    public string Field1 { get; init; } = null!;
    public int Field2 { get; init; }
    public bool Field3 { get; init; }
    public double Field4 { get; init; }
    public Model3 Field5 { get; init; } = null!;
}

public class Model3
{
    public IEnumerable<string> Field1 { get; init; } = [];
}