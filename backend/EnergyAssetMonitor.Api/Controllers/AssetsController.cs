using EnergyAssetMonitor.Application.DTOs;
using EnergyAssetMonitor.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnergyAssetMonitor.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AssetsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public AssetsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AssetDto>>> GetAssets()
    {
        var assets = await _context.Assets.AsNoTracking() //Performance optimization: AsNoTracking() is used for read-only queries to improve performance
            .Select(a => new AssetDto
            {
                Id = a.Id,
                Name = a.Name,
                Type = a.Type.ToString(),
                Location = a.Location,
                InstalledCapacity = a.InstalledCapacity,
                CurrentOutput = a.CurrentOutput,
                Status = a.Status.ToString(),
                LastUpdated = a.LastUpdated
            })
            .ToListAsync();
        return Ok(assets);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AssetDto>> GetAsset(int id)
    {
        var asset = await _context.Assets.AsNoTracking()
        .Where(a => a.Id == id).Select(a => new AssetDto
        {
            Id = a.Id,
            Name = a.Name,
            Type = a.Type.ToString(),
            Location = a.Location,
            InstalledCapacity = a.InstalledCapacity,
            CurrentOutput = a.CurrentOutput,
            Status = a.Status.ToString(),
            LastUpdated = a.LastUpdated
        }).FirstOrDefaultAsync();
        if (asset == null)
        {
            return NotFound();
        }
        return Ok(asset);
    }
}