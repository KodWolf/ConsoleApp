using System;

public class Report
{
    public string Title { get; set; }
    public string Header { get; set; }  
    public string Content { get; set; }
    public string Footer { get; set; } 
    public string Format { get; set; } 

    public void Generate()
    {
        Console.WriteLine($"--- ГЕНЕРАЦИЯ ОТЧЕТА ---");
        Console.WriteLine($"Заголовок: {Title}");
        Console.WriteLine($"Формат: {Format}");
        Console.WriteLine($"Шапка: {Header}");
        Console.WriteLine($"Содержимое: {Content}");
        Console.WriteLine($"Подвал: {Footer}");
        Console.WriteLine("Отчёт успешно сохранён.\n");
    }
}

public class ReportBuilder
{
    private Report _report = new Report();

    public void SetTitle(string title) => _report.Title = title;

    public void AddHeader(string header) => _report.Header = header;

    public void AddContent(string contentType, string data)
    {
        _report.Content += $"[{contentType}: {data}] ";
    }

    public void SetFooter(string footer) => _report.Footer = footer;

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
    private readonly ReportBuilder _builder;

    public ReportDirector(ReportBuilder builder) => _builder = builder;

    public void ConstructSalesReport()
    {
        _builder.SetTitle("SalesReport"); 
        _builder.AddContent("Диаграмма", "Данные о продажах");
        _builder.AddContent("Таблица", "Статистика за квартал");
        _builder.SetFormat("PDF");
    }

    public void ConstructFinancialReport()
    {
        _builder.SetTitle("FinancialReport"); 
        _builder.AddContent("Таблица", "Бухгалтерский баланс");
        _builder.SetFormat("Excel"); 
    }

    public void ConstructEmployeeReport()
    {
        _builder.SetTitle("EmployeeReport"); 
        _builder.AddContent("Таблица", "Персональные данные");
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

        builder.SetTitle("Пользовательский отчет КазНИТУ");
        builder.AddHeader("Автор: Студент");
        builder.AddContent("Диаграмма", "Кривая роста");
        builder.SetFormat("PDF");

        Report customReport = builder.GetReport();
        customReport.Generate();
    }
}