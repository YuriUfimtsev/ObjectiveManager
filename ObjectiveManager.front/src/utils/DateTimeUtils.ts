export default class DateTimeUtils {
    public static DateOnlyFormat: string = 'DD.MM.YYYY'
    
    public static renderDateWithoutHours = (date: Date) => {
        const options: Intl.DateTimeFormatOptions = {
            year: 'numeric',
            month: '2-digit',
            day: '2-digit',
        };

        return date.toLocaleString(undefined, options)
    }
}