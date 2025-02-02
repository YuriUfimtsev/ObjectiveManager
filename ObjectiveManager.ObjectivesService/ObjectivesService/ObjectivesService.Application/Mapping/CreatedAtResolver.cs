using AutoMapper;
using ObjectivesService.Application.Dto;
using ObjectivesService.Domain.Entities;
using ObjectivesService.Domain.Interfaces;

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