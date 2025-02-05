using NotificationsService.Application.Models;
using ObjectiveManager.Models.AuthService;
using ObjectiveManager.Models.ObjectivesService.DTO;

namespace NotificationsService.Application.Services.Interfaces;

public interface IEmailBuilderService
{ 
    public Task<EmailMessageModel> BuildForMentor(AccountData worker, ObjectiveDTO[] objectives);
    
    public Task<EmailMessageModel> BuildForWorker(ObjectiveDTO[] objectives);
}