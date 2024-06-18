namespace TerrainExplorer.Exploration;

public enum ExplorationOutcome
{
    NotYetDetermined,
    Timeout,
    ColonizableResourcesWithinRange,
    ColonizableSufficientResourcesFound,
    DiscoveryRateReached
}