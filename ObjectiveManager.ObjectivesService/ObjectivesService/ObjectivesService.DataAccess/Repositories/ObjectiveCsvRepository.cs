using System.Globalization;
using CsvHelper;
using ObjectivesService.Domain.DTO;
using ObjectivesService.Domain.Entities;
using ObjectivesService.Domain.Interfaces;

namespace ObjectivesService.DataAccess.Repositories;

// todo: подготовить к работе с текущими версиями моделей объектов
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

    public Task<List<ObjectiveEntity>> GetAllForUser(string userId)
        => Task.FromResult(FetchObjectivesThreadSafe());

    // TODO: implement
    public Task<int> Update(UpdateObjectiveDTO updatedObjective)
    {
        throw new NotImplementedException();
    }

    // TODO: implement
    public Task<int> UpdateStatusObject(UpdateStatusObjectDTO updateStatusObjectDto)
    {
        throw new NotImplementedException();
    }

    // TODO: refactor to new model
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

    public Task<int> Delete(Guid id)
    {
        var objectives = FetchObjectivesThreadSafe();

        var index = objectives.FindIndex(obj => obj.Id == id);
        if (index == -1)
        {
            Task.FromResult(0);
        }

        objectives.RemoveAt(index);
        WriteObjectivesThreadSafe(objectives);
        return Task.FromResult(1);
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