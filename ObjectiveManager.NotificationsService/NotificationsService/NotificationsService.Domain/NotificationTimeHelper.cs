namespace NotificationsService.Domain;

public static class NotificationTimeHelper
{
    // Время для уведомлений по умолчанию: понедельник, 12:00.
    public static DateTimeOffset GetNextMondayNoon(DateTimeOffset now)
    {
        // Вычисляем число дней до ближайшего понедельника
        var nextMondayOffset = DayOfWeek.Monday - now.DayOfWeek;
        var daysUntilNextMonday = nextMondayOffset < 0
            ? nextMondayOffset + 7
            : nextMondayOffset;
        
        // Если сегодня понедельник, отправим в следующий
        if (daysUntilNextMonday == 0)
        {
            daysUntilNextMonday = 7;
        }

        // Возвращаем новый объект DateTimeOffset с датой ближайшего понедельника и временем: 12:00
        DateTimeOffset nextMondayNoon = new DateTimeOffset(
            now.Year,
            now.Month,
            now.Day,
            12, 0, 0,
            now.Offset
        ).AddDays(daysUntilNextMonday);
        return nextMondayNoon;
    }
}