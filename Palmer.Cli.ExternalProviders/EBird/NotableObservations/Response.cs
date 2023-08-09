namespace Palmer.Cli.ExternalProviders.EBird.NotableObservations;

public record Response(
    string Name,
    string LocationName,
    DateTime DateTime,
    int HowMany,
    bool IsValid,
    bool IsReviewed,
    bool IsPrivateLocation,
    Uri ChecklistUri);