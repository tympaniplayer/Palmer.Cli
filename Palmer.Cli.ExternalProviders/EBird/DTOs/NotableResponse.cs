namespace Palmer.Cli.ExternalProviders.EBird.DTOs;

public class NotableResponse
{
    public string SpeciesCode { get; set; } = default!;
    public string ComName { get; set; } = default!;
    public string SciName { get; set; } = default!;
    public string LocId { get; set; } = default!;
    public string LocName { get; set; } = default!;
    public string ObsDt { get; set; } = default!;
    public int HowMany { get; set; }
    public double Lat { get; set; }
    public double Lng { get; set; }
    public bool ObsValid { get; set; }
    public bool ObsReviewed { get; set; }
    public bool LocationPrivate { get; set; }
    public string SubId { get; set; } = default!;
}