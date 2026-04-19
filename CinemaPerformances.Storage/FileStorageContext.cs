using Microsoft.Maui.Storage;
using System.Text.Json;

using CinemaPerformances.DBModels;

namespace CinemaPerformances.Storage;

public class FileStorageContext : IStorageContext
{
    private static readonly string s_StoragePath = Path.Combine(FileSystem.AppDataDirectory, "Storage");

    private readonly SemaphoreSlim _semaphore;

    public FileStorageContext()
    {
        _semaphore = new(1, 1);
    }

    private async Task Initialize()
    {
        await _semaphore.WaitAsync();
        try
        {
            if (!Directory.Exists(s_StoragePath))
                await CreateMockStorage();

        }
        finally
        {
            _semaphore.Release();
        }
    }

    private async Task CreateMockStorage()
    {
        Directory.CreateDirectory(s_StoragePath);

        InMemoryStorageContext inMemoryStorage = new();
        List<Task> tasks = new();

        await foreach (var cinemaHall in inMemoryStorage.GetCinemaHalls())
        {
            Directory.CreateDirectory(Path.Combine(s_StoragePath, cinemaHall.Id.ToString()));

            tasks.Add(File.WriteAllTextAsync(CinemaHallFilePath(cinemaHall.Id), JsonSerializer.Serialize(cinemaHall)));

            foreach (var performance in await inMemoryStorage.GetPerformancesByCinemaHall(cinemaHall.Id))
                tasks.Add(File.WriteAllTextAsync(PerformanceFilePath(cinemaHall.Id, performance.Id), JsonSerializer.Serialize(performance)));
        }
        await Task.WhenAll(tasks);
    }

    private string CinemaHallFilePath(Guid id)
    {
        return Path.Combine(s_StoragePath, $"{id.ToString()}.json");
    }

    private string CinemaHallDirectoryPath(Guid id)
    {
        return Path.Combine(s_StoragePath, id.ToString());
    }

    private string PerformanceFilePath(Guid cinemaHallId, Guid id)
    {
        return Path.Combine(CinemaHallDirectoryPath(cinemaHallId), $"{id.ToString()}.json");
    }

    private string PerformanceFilePath(string cinemaHallPath, Guid id)
    {
        return Path.Combine(cinemaHallPath, $"{id.ToString()}.json");
    }

    public async IAsyncEnumerable<CinemaHallDBModel> GetCinemaHalls()
    {
        await Initialize();
        foreach (var file in Directory.GetFiles(s_StoragePath, "*.json"))
        {
            var json = await File.ReadAllTextAsync(file);
            var result = JsonSerializer.Deserialize<CinemaHallDBModel>(json);
            if (result is not null) yield return result;
        }
    }

    public async Task<CinemaHallDBModel?> GetCinemaHall(Guid cinemaHallId)
    {
        await Initialize();
        var filePath = CinemaHallFilePath(cinemaHallId);
        if (!File.Exists(filePath)) return null;
        var json = await File.ReadAllTextAsync(filePath);
        return JsonSerializer.Deserialize<CinemaHallDBModel>(json);
    }

    public async IAsyncEnumerable<PerformanceDBModel> GetPerformances()
    {
        await Initialize();
        foreach (var directory in Directory.GetDirectories(s_StoragePath))
        {
            foreach (var file in Directory.GetFiles(directory, "*.json"))
            {
                var json = await File.ReadAllTextAsync(file);
                var result = JsonSerializer.Deserialize<PerformanceDBModel>(json);
                if (result is not null) yield return result;
            }
        }
    }

    public async Task<IEnumerable<PerformanceDBModel>> GetPerformancesByCinemaHall(Guid cinemaHallId)
    {
        await Initialize();
        List<PerformanceDBModel> performances = new();
        var cinemaHallDirectory = CinemaHallDirectoryPath(cinemaHallId);
        if (!Directory.Exists(cinemaHallDirectory))
            return performances;
        foreach (var file in Directory.GetFiles(cinemaHallDirectory, "*.json"))
        {
            var json = await File.ReadAllTextAsync(file);
            var result = JsonSerializer.Deserialize<PerformanceDBModel>(json);
            if (result is not null) performances.Add(result);
        }
        return performances;
    }

    public async Task<PerformanceDBModel?> GetPerformance(Guid id)
    {
        await Initialize();
        foreach (var directory in Directory.GetDirectories(s_StoragePath))
        {
            var filePath = PerformanceFilePath(directory, id);
            if (!File.Exists(filePath)) continue;
            var json = await File.ReadAllTextAsync(filePath);
            var result = JsonSerializer.Deserialize<PerformanceDBModel>(json);
            if (result is not null) return result;
        }
        return null;
    }

    public async Task<double> GetTotalDurationByCinemaHall(Guid cinemaHallId)
    {
        await Initialize();
        double duration = 0.0;
        var directoryPath = CinemaHallDirectoryPath(cinemaHallId);
        if (!Directory.Exists(directoryPath)) return duration;
        foreach (var file in Directory.GetFiles(directoryPath, "*.json"))
        {
            var json = await File.ReadAllTextAsync(file);
            var result = JsonSerializer.Deserialize<PerformanceDBModel>(json);
            if (result is not null) duration += result.Duration;
        }
        return duration;
    }

    public async Task SaveCinemaHall(CinemaHallDBModel cinemaHall)
    {
        await Initialize();
        var filePath = CinemaHallFilePath(cinemaHall.Id);
        await File.WriteAllTextAsync(filePath, JsonSerializer.Serialize(cinemaHall));
    }

    public async Task DeleteCinemaHall(Guid id)
    {
        await Initialize();
        var filePath = CinemaHallFilePath(id);
        if (File.Exists(filePath)) File.Delete(filePath);
        var directoryPath = CinemaHallDirectoryPath(id);
        if (Directory.Exists(directoryPath)) Directory.Delete(directoryPath, true);
    }

    public async Task SavePerformance(PerformanceDBModel performance)
    {
        await Initialize();
        var directoryPath = CinemaHallDirectoryPath(performance.CinemaHallId);
        if (!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);
        var filePath = PerformanceFilePath(performance.CinemaHallId, performance.Id);
        await File.WriteAllTextAsync(filePath, JsonSerializer.Serialize(performance));
    }

    public async Task DeletePerformance(Guid id)
    {
        await Initialize();
        foreach (var directory in Directory.GetDirectories(s_StoragePath))
        {
            var filePath = PerformanceFilePath(directory, id);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                return;
            }
        }
    }
}
