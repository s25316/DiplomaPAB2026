namespace GUS.REGON.Database.Models.RegistrationDetails;

public class FormaFinansowania
{
    public string FormaFinansowaniaId { get; set; } = null!;
    public string Name { get; set; } = null!;

    public virtual ICollection<Report> Reports { get; set; } = [];
}