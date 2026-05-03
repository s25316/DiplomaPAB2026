using RADON.Database.Models.Institutions;

namespace RADON.Database.Models;

public class DataSource
{
    public Guid DataSourceId { get; set; }
    public string Name { get; set; } = null!;


    public virtual ICollection<Institution> Institutions { get; set; } = [];
}