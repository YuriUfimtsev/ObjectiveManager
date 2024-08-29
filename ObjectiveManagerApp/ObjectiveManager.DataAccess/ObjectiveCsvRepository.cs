using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

using ObjectiveManager.Domain;
using ObjectiveManager.Domain.Models;

namespace ObjectiveManager.DataAccess;

// todo: проверять при каждом запросе, ято формат файла остается корректен
// (по крайней мере первая строка хедеров)
// todo: ограничивать с помощью критических секций доступ к файлу
public class ObjectiveCsvRepository : IObjectiveRepository
{
    private readonly string _pathToCsvFile = "../Data/Objectives.csv";
    
    public string Create(ObjectiveCreation newObjective)
    {
        var id = Guid.NewGuid().ToString();
        var objective = new Objective(
            Id: id,
            Definition: newObjective.Definition,
            FinalDate: newObjective.FinalDate,
            Status: ObjectiveStatus.Opened,
            Comment: newObjective.Comment);

        using var streamWriter = new StreamWriter(_pathToCsvFile, true);
        using var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);
        
        // todo: проверка на совпадающий id с существующей записью
        csvWriter.NextRecord();
        csvWriter.WriteRecord<Objective>(objective);
        
        return id;
    }

    public Objective? Get(string id)
    {
        using var streamReader = new StreamReader(_pathToCsvFile);
        using var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture);
        
        var result = csvReader.GetRecords<Objective>()
            .FirstOrDefault(obj => obj.Id == id);
        
        return result;
    }

    public Objective Update(Objective updatedObjective)
    {
        var objectives = new List<Objective>();
        
        using (var reader = new StreamReader(_pathToCsvFile))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            objectives = csv.GetRecords<Objective>().ToList();
        }

        var index = objectives.FindIndex(obj => obj.Id == updatedObjective.Id);
        if (index == -1)
        {
            throw new ArgumentException($"Objective with Id {updatedObjective.Id} not found");
        }

        objectives[index] = updatedObjective;

        using (var writer = new StreamWriter(_pathToCsvFile))
        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
            csv.WriteHeader<Objective>();
            csv.NextRecord();
            csv.WriteRecords(objectives);
        }

        return updatedObjective;
    }

    public Objective Delete(string id)
    {
        var objectives = new List<Objective>();
        
        using (var reader = new StreamReader(_pathToCsvFile))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            objectives = csv.GetRecords<Objective>().ToList();
        }

        var index = objectives.FindIndex(obj => obj.Id == id);
        if (index == -1)
        {
            throw new ArgumentException($"Objective with Id {id} not found");
        }

        var objective = objectives[index];

        objectives[index] = objective with { Status = ObjectiveStatus.Cancelled };

        using (var writer = new StreamWriter(_pathToCsvFile))
        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
            csv.WriteHeader<Objective>();
            csv.NextRecord();
            csv.WriteRecords(objectives);
        }

        return objective; 
    }
}