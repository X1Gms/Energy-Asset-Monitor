using EnergyAssetMonitor.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EnergyAssetMonitor.Infrastructure.Persistence;

public static class DbInitializer
{
    public static void Initialize(ApplicationDbContext context)
    {
        context.Database.Migrate();

        if (context.Assets.Any())
        {
            return;   // DB has been seeded
        }

        var demoAssets = new List<Asset>
        {
            new Asset(
                name: "Parque Solar do Alqueva",
                type: AssetType.Solar,
                location: "Évora",
                installedCapacity: 45.0m,
                currentOutput: 32.5m,
                status: AssetStatus.Operational
            ),
            new Asset(
                name: "Parque Eólico do Alto Minho",
                type: AssetType.Wind,
                location: "Viana do Castelo",
                installedCapacity: 60.0m,
                currentOutput: 48.2m,
                status: AssetStatus.Operational
            ),
            new Asset(
                name: "Central de Baterias do Carregado",
                type: AssetType.Battery,
                location: "Lisboa",
                installedCapacity: 25.0m,
                currentOutput: 15.0m,
                status: AssetStatus.Warning
            ),
            new Asset(
                name: "Parque Solar de Ourique",
                type: AssetType.Solar,
                location: "Beja",
                installedCapacity: 35.0m,
                currentOutput: 0.0m,
                status: AssetStatus.Offline
            ),
            new Asset(
                name: "Parque Eólico da Serra da Estrela",
                type: AssetType.Wind,
                location: "Guarda",
                installedCapacity: 50.0m,
                currentOutput: 41.8m,
                status: AssetStatus.Operational
            ),
            new Asset(
                name: "Sistema de Baterias de Sines",
                type: AssetType.Battery,
                location: "Setúbal",
                installedCapacity: 30.0m,
                currentOutput: 22.0m,
                status: AssetStatus.Operational
            )
        };

        context.Assets.AddRange(demoAssets);
        context.SaveChanges();
    }
}