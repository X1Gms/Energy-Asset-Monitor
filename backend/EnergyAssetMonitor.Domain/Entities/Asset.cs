namespace EnergyAssetMonitor.Domain.Entities;

public class Asset
{
    public int Id {get; private set;} 
    public string Name {get; private set; } = string.Empty;
    public AssetType Type {get; private set;}
    public string Location {get; private set;} = string.Empty;
    public decimal InstalledCapacity {get; private set;}
    public decimal CurrentOutput {get; private set;}
    public AssetStatus Status {get; private set;}
    public DateTime LastUpdated {get; private set;}

    // Constructor required by EF Core for materialization
    protected Asset() { }

    public Asset(string name, AssetType type, string location, decimal installedCapacity, decimal currentOutput, AssetStatus status)
    {
        ValidateDetails(name, location);
        Name = name;
        Type = type;
        Location = location;
        ValidateInstalledCapacity(installedCapacity);
        InstalledCapacity = installedCapacity;
        Status = status;
        setOutput(currentOutput, installedCapacity);

        LastUpdated = DateTime.UtcNow;
    }

    public void UpdateDetails(string name, AssetType type, string location){
        ValidateDetails(name, location);
        Name = name;
        Type = type;
        Location = location;
        LastUpdated = DateTime.UtcNow;
    }

    public void UpdateOutput(decimal currentOutput)
    {
        setOutput(currentOutput, InstalledCapacity);
        LastUpdated = DateTime.UtcNow;
    }

    public void UpdateStatus(AssetStatus status)
    {
        Status = status;
        if(status == AssetStatus.Offline)
        {
            CurrentOutput = 0;
        }
        LastUpdated = DateTime.UtcNow;
    }

    private static void ValidateDetails(string name, string location)
    {
        if(string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Asset name cannot be empty.");
        }
        if(string.IsNullOrWhiteSpace(location))
        {
            throw new ArgumentException("Asset location cannot be empty.");
        }
    }

    private void ValidateInstalledCapacity(decimal installedCapacity)
    {
        if(installedCapacity < 0)
        {
            throw new ArgumentException("Installed capacity cannot be negative.");
        }
    }

    private void setOutput(decimal currentOutput, decimal installedCapacity)
    {
        if(currentOutput < 0)
        {
            throw new ArgumentException("Current output cannot be negative.");
        }
        if(currentOutput > installedCapacity)
        {
            throw new ArgumentException("Current output cannot exceed installed capacity.");
        }
        if(Status == AssetStatus.Offline && currentOutput != 0)
        {
            throw new ArgumentException("Current output must be zero when the asset is offline.");
        }

        CurrentOutput = currentOutput;
    }
}