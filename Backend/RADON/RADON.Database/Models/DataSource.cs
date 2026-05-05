using RADON.Database.Models.Courses;
using RADON.Database.Models.Institutions;

namespace RADON.Database.Models;

public class DataSource
{
    public Guid DataSourceId { get; set; }
    public string Name { get; set; } = null!;


    public virtual ICollection<Institution> Institutions { get; set; } = [];
    public virtual ICollection<Course> Courses { get; set; } = [];
}