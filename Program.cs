public class Report
{
    public string Title { get; set; }
    public List<string> Headers { get; set; } = new List<string>();
    public List<string> Contents { get; set; } = new List<string>();
    public List<string> Footers { get; set; } = new List<string>();
    public string Format { get; set; }

    public void Generate()
    {

        Console.WriteLine($"--- Generating {Format} Report: {Title} ---");
        Console.WriteLine($"Headers: {string.Join(", ", Headers)}");
        Console.WriteLine($"Content: {string.Join(" | ", Contents)}");
        Console.WriteLine($"Footers: {string.Join(", ", Footers)}");
        Console.WriteLine("Report saved successfully.\n");
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
        _builder.SetTitle("Sales Performance Report");
        _builder.AddHeader("Q1 Regional Sales");
        _builder.AddContent("Chart", "Sales Growth 2024");
        _builder.AddContent("Table", "Revenue by Product");
        _builder.SetFormat("PDF");
    }

    public void ConstructFinancialReport()
    {
        _builder.SetTitle("Annual Financial Statement");
        _builder.AddContent("Table", "Balance Sheet");
        _builder.SetFormat("Excel");
    }

    public void ConstructEmployeeReport()
    {
        _builder.SetTitle("Staffing and HR Statistics");
        _builder.AddContent("Table", "Employee List");
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

        builder.SetTitle("Custom Research Report");
        builder.AddHeader("Author: K.I. Satbayev University");
        builder.AddContent("Diagram", "Technical Growth Curve");
        builder.SetFormat("PDF");

        Report customReport = builder.GetReport();
        customReport.Generate();
    }
}