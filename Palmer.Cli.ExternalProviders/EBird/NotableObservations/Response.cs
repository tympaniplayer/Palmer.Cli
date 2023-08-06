namespace Palmer.Cli.ExternalProviders.EBird.NotableObservations;

public record Response(
    string Name,
    string LocationName,
    DateTime DateTime,
    string HowMany,
    bool IsValid,
    bool IsReviewed,
    bool IsPrivateLocation,
    Uri ChecklistUri);