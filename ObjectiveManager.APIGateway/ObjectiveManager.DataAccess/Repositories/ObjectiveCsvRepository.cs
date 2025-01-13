using System.Globalization;
using CsvHelper;
using ObjectiveManager.Domain.Dto;
using ObjectiveManager.Domain.Entities;
using ObjectiveManager.Domain.Interfaces;

namespace ObjectiveManager.DataAccess.Repositories;

// todo: проверять при каждом обращении к файлу, что формат файла остается корректен
// (по крайней мере первая строка хедеров)
public class ObjectiveCsvRepository : IObjectiveRepository
{
    private readonly string _pathToCsvFile = "../Data/Objectives.csv";
    private readonly object _csvFileLockObject = new();

    // Разрешаем сохранять цели с совпадающими названиями
    public Task<Guid> Create(ObjectiveEntity newObjective)
    {
        var id = Guid.NewGuid();
        var objective = newObjective with { Id = id };

        lock (_csvFileLockObject)
        {
            using var streamWriter = new StreamWriter(_pathToCsvFile, true);
            using var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);

            csvWriter.NextRecord();
            csvWriter.WriteRecordsAsync(new List<ObjectiveEntity> { objective });
        }

        return Task.FromResult(id);
    }

    public Task<ObjectiveEntity?> Get(Guid id)
    {
        lock (_csvFileLockObject)
        {
            using var streamReader = new StreamReader(_pathToCsvFile);
            using var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture);

            return Task.FromResult(
                csvReader.GetRecords<ObjectiveEntity>()
                    .FirstOrDefault(obj => obj.Id == id));
        }
    }

    public Task<List<ObjectiveEntity>> GetAll()
        => Task.FromResult(FetchObjectivesThreadSafe());

    public Task Update(ObjectiveEntity updatedObjective)
    {
        var objectives = FetchObjectivesThreadSafe();

        var index = objectives.FindIndex(obj => obj.Id == updatedObjective.Id);
        if (index == -1)
        {
            throw new ArgumentException($"Objective with Id {updatedObjective.Id} not found");
        }

        objectives[index] = updatedObjective;

        WriteObjectivesThreadSafe(objectives);
        return Task.CompletedTask;
    }

    public Task Delete(Guid id)
    {
        var objectives = FetchObjectivesThreadSafe();

        var index = objectives.FindIndex(obj => obj.Id == id);
        if (index == -1)
        {
            throw new ArgumentException($"Objective with Id {id} not found");
        }

        objectives.RemoveAt(index);
        WriteObjectivesThreadSafe(objectives);
        return Task.CompletedTask;
    }

    private List<ObjectiveEntity> FetchObjectivesThreadSafe()
    {
        lock (_csvFileLockObject)
        {
            using var reader = new StreamReader(_pathToCsvFile);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            return csv.GetRecords<ObjectiveEntity>().ToList();
        }
    }

    private void WriteObjectivesThreadSafe(List<ObjectiveEntity> objectives)
    {
        lock (_csvFileLockObject)
        {
            using var writer = new StreamWriter(_pathToCsvFile);
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);

            csv.WriteHeader<ObjectiveEntity>();
            csv.NextRecord();
            csv.WriteRecords(objectives);
        }
    }
}