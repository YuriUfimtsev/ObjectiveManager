using System.Text;
using NotificationsService.Application.Models;
using NotificationsService.Application.Services.Interfaces;
using ObjectiveManager.Models.AuthService;
using ObjectiveManager.Models.ObjectivesService.DTO;
using ObjectivesService.Client;

namespace NotificationsService.Application.Services;

public class EmailBuilderService : IEmailBuilderService
{
    private readonly IObjectivesServiceClient _objectivesServiceClient;

    public EmailBuilderService(IObjectivesServiceClient objectivesServiceClient)
    {
        _objectivesServiceClient = objectivesServiceClient;
    }

    public async Task<EmailMessageModel> BuildForMentor(AccountData worker, ObjectiveDTO[] objectives)
        => new(
            $"Цели работника {worker.Name} {worker.Surname}",
            await BuildEmailBody(objectives, true, worker));

    public async Task<EmailMessageModel> BuildForWorker(ObjectiveDTO[] objectives)
        => new("Цели", await BuildEmailBody(objectives));

    private async Task<string> BuildEmailBody(ObjectiveDTO[] objectives, bool isForMentor = false, AccountData? worker = null)
    {
        var bodyBuilder = new StringBuilder();
        var titleAddition = isForMentor && worker != null ? $"({worker.Name} {worker.Surname})" : string.Empty;

        var header = @$"
<!DOCTYPE html>
<html lang=""ru"">
  <body>
    <table style=""width:100%; color:black; border-collapse:collapse;"">
      <tr>
        <td colspan=""4"">
          <h2>Состояние целей на текущий момент {titleAddition}</h2>
        </td>
      </tr>
      <tr>
        <th style=""border: 1px solid black; padding: 8px; text-align: left;"">Название</th>
        <th style=""border: 1px solid black; padding: 8px; text-align: left;"">Контрольная дата</th>
        <th style=""border: 1px solid black; padding: 8px; text-align: left;"">Статус</th>
        <th style=""border: 1px solid black; padding: 8px; text-align: left;"">Комментарий</th>
      </tr>";
        bodyBuilder.Append(header);

        foreach (var objective in objectives)
        {
            var objectiveRow = @$"
      <tr>
        <td style=""border: 1px solid black; padding: 8px;"">{objective.Definition}</td>
        <td style=""border: 1px solid black; padding: 8px;"">{objective.FinalDate.ToString("dd.MM.yyyy")}</td>
        <td style=""border: 1px solid black; padding: 8px;"">{objective.StatusObject.StatusValue.Name}</td>
        <td style=""border: 1px solid black; padding: 8px;"">{objective.Comment}</td>
      </tr>";
            bodyBuilder.Append(objectiveRow);

            if (isForMentor && worker != null)
            {
                await AppendStatusesInfo(bodyBuilder, objective.Id, worker.UserId);
            }
        }

        var finish =
            @$"
    </table>
  </body>
</html>";
        bodyBuilder.Append(finish);

        return bodyBuilder.ToString();
    }

    private async Task AppendStatusesInfo(StringBuilder builder, string objectiveId, string workerId)
    {
        var statusesHistory = await _objectivesServiceClient.GetStatusesHistory(objectiveId, workerId);
        var statusesHeader = GetStatusesHtmlHeader();
        var statusesRows = GetStatusesHtmlRows(statusesHistory.Value);

        builder.Append(statusesHeader);
        builder.Append(statusesRows);
    }

    private static string GetStatusesHtmlHeader()
        => $@"
      <tr>
        <th style=""border: 1px solid black; padding: 8px;""/>
        <th style=""border: 1px solid black; padding: 8px;"">Дата изменения статуса</th>
        <th style=""border: 1px solid black; padding: 8px;"">Статус</th>
        <th style=""border: 1px solid black; padding: 8px;"">Комментарий</th>
      </tr>";

    private static string GetStatusesHtmlRows(StatusObjectDTO[] statusObjects)
    {
        var builder = new StringBuilder();
        foreach (var statusObject in statusObjects)
        {
            var statusObjectRow = $@"
      <tr>
        <td style=""border: 1px solid black; padding: 8px;""/>
        <td style=""border: 1px solid black; padding: 8px;"">{statusObject.CreatedAt.ToString("dd.MM.yyyy")}</td>
        <td style=""border: 1px solid black; padding: 8px;"">{statusObject.StatusValue.Name}</td>
        <td style=""border: 1px solid black; padding: 8px;"">{statusObject.Comment}</td>
      </tr>";
            builder.Append(statusObjectRow);
        }

        return builder.ToString();
    }
}