using AutoMapper;
using ObjectiveManager.Models.EntityFramework.Infrastructure;
using ObjectivesService.Application.Dto;
using ObjectivesService.Domain.Entities;

namespace ObjectivesService.Application.Mapping;

public class CreatedAtResolver : IValueResolver<CreateObjectiveDto, ObjectiveEntity, DateTimeOffset>
{
    private readonly IDateTimeProvider _dateTimeProvider;

    public CreatedAtResolver(IDateTimeProvider dateTimeProvider)
    {
        _dateTimeProvider = dateTimeProvider;
    }

    public DateTimeOffset Resolve(CreateObjectiveDto source, ObjectiveEntity destination, DateTimeOffset destMember,
        ResolutionContext context)
        => _dateTimeProvider.Now();
}