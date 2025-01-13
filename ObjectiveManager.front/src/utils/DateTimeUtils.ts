export default class Utils {
    static toISOString(date: Date | undefined) {
        if (date == null) return undefined
        const pad = (num: number) => (num < 10 ? '0' : '') + num

        return date.getFullYear() +
            '-' + pad(date.getMonth() + 1) +
            '-' + pad(date.getDate()) +
            'T' + pad(date.getHours()) +
            ':' + pad(date.getMinutes()) +
            ':' + pad(date.getSeconds())
    }

    static renderDateWithoutHours = (date: Date) => {
        const options: Intl.DateTimeFormatOptions = {
            year: 'numeric',
            month: '2-digit',
            day: '2-digit',
        };

        return date.toLocaleString(undefined, options)
    }
}