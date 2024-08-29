using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

using ObjectiveManager.Domain;
using ObjectiveManager.Domain.Models;

namespace ObjectiveManager.DataAccess;

public class ObjectiveCsvRepository : IObjectiveRepository
{
    private readonly string _pathToCsvFile = "../../../Data/Objectives.csv";
    
    public string Create(ObjectiveCreation newObjective)
    {
        var id = new Guid().ToString();
        var objective = new Objective(
            Id: id,
            Definition: newObjective.Definition,
            FinalDate: newObjective.FinalDate,
            Status: ObjectiveStatus.Opened,
            Comment: newObjective.Comment);

        using var streamWriter = new StreamWriter(_pathToCsvFile);
        using var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);
        
        // todo: проверка на совпадающий id с существующей записью
        csvWriter.WriteRecord(objective);
        
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

        var objective = objectives.FirstOrDefault(obj => obj.Id == updatedObjective.Id);
        if (objective is null)
        {
            throw new ArgumentException($"Objective with Id {updatedObjective.Id} not found");
        }

        objective = updatedObjective;

        using (var writer = new StreamWriter(_pathToCsvFile))
        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
            csv.WriteRecords(objectives);
        }

        return objective;
    }

    public Objective Delete(string id)
    {
        var objectives = new List<Objective>();
        
        using (var reader = new StreamReader(_pathToCsvFile))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            objectives = csv.GetRecords<Objective>().ToList();
        }

        var objective = objectives.FirstOrDefault(obj => obj.Id == id);
        if (objective is null)
        {
            throw new ArgumentException($"Objective with Id {id} not found");
        }

        objective = objective with { Status = ObjectiveStatus.Cancelled };

        using (var writer = new StreamWriter(_pathToCsvFile))
        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
            csv.WriteRecords(objectives);
        }

        return objective; 
    }
}