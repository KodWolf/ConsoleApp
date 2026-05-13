public class Report
{
    public string Title { get; set; }
    public List<string> Headers { get; set; } = new List<string>();
    public List<string> Contents { get; set; } = new List<string>();
    public List<string> Footers { get; set; } = new List<string>();
    public string Format { get; set; }

    public void Generate()
    {
        Console.WriteLine($"--- Генерация {Format} отчёта: {Title} ---");
        Console.WriteLine($"Заголовки: {string.Join(", ", Headers)}");
        Console.WriteLine($"Содержание: {string.Join(" | ", Contents)}");
        Console.WriteLine($"Нижние колонтитулы: {string.Join(", ", Footers)}");
        Console.WriteLine("Отчёт успешно сохранён.\n");
    }
}

public interface IReportBuilder
{
    void SetTitle(string title);
    void AddHeader(string header);
    void AddContent(string contentType, string data);
    void SetFooter(string footer);
    void SetFormat(string format);
    Report GetReport();
}

public class ReportBuilder : IReportBuilder
{
    private Report _report = new Report();

    public void SetTitle(string title) => _report.Title = title;
    public void AddHeader(string header) => _report.Headers.Add(header);
    public void AddContent(string contentType, string data) => _report.Contents.Add($"[{contentType}]: {data}");
    public void SetFooter(string footer) => _report.Footers.Add(footer);
    public void SetFormat(string format) => _report.Format = format;

    public Report GetReport()
    {
        Report finishedReport = _report;
        _report = new Report();
        return finishedReport;
    }
}

public class ReportDirector
{
    private readonly IReportBuilder _builder;

    public ReportDirector(IReportBuilder builder) => _builder = builder;

    public void ConstructSalesReport()
    {
        _builder.SetTitle("Отчёт о продажах");
        _builder.AddHeader("Региональные продажи за Q1");
        _builder.AddContent("Диаграмма", "Рост продаж 2024");
        _builder.AddContent("Таблица", "Выручка по продуктам");
        _builder.SetFormat("PDF");
    }

    public void ConstructFinancialReport()
    {
        _builder.SetTitle("Годовой финансовый отчёт");
        _builder.AddContent("Таблица", "Бухгалтерский баланс");
        _builder.SetFormat("Excel");
    }

    public void ConstructEmployeeReport()
    {
        _builder.SetTitle("Статистика персонала");
        _builder.AddContent("Таблица", "Список сотрудников");
        _builder.SetFormat("HTML");
    }
}

class Program
{
    static void Main()
    {
        var builder = new ReportBuilder();
        var director = new ReportDirector(builder);

        director.ConstructSalesReport();
        Report salesReport = builder.GetReport();
        salesReport.Generate();

        builder.SetTitle("Пользовательский исследовательский отчёт");
        builder.AddHeader("Автор: КазНИТУ имени К.И. Сатпаева");
        builder.AddContent("Диаграмма", "Кривая технического роста");
        builder.SetFormat("PDF");

        Report customReport = builder.GetReport();
        customReport.Generate();
    }
}