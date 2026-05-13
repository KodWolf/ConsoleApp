using System;
using System.Collections.Generic;
using System.Linq;

namespace ReportSystem
{

    public enum ContentType
    {
        Table,
        BarChart,
        PieChart,
        LineChart,
        FinancialTable,
        EmployeeTable
    }

    public class ContentItem
    {
        public ContentType Type { get; set; }
        public object Data { get; set; }
        public string Title { get; set; }

        public ContentItem(ContentType type, object data, string title = "")
        {
            Type = type;
            Data = data;
            Title = title;
        }
    }


    public class Report
    {
        private string _title = "";
        private List<string> _headers = new List<string>();
        private List<ContentItem> _contents = new List<ContentItem>();
        private string _footer = "";
        private string _format = "html";

        public string Title => _title;
        public IReadOnlyList<string> Headers => _headers.AsReadOnly();
        public IReadOnlyList<ContentItem> Contents => _contents.AsReadOnly();
        public string Footer => _footer;
        public string Format => _format;

        public void SetTitle(string title) => _title = title;
        public void AddHeader(string header) => _headers.Add(header);
        public void AddContent(ContentType type, object data, string title = "")
            => _contents.Add(new ContentItem(type, data, title));
        public void SetFooter(string footer) => _footer = footer;
        public void SetFormat(string format) => _format = format.ToLower();

        public void Generate()
        {
            Console.WriteLine($"\n=== Генерация отчета '{_title}' в формате {_format.ToUpper()} ===\n");

            var exporter = ExporterFactory.Create(_format);
            exporter.Export(this);
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Заголовок: {_title}");
            Console.WriteLine("Заголовки/колонтитулы:");
            foreach (var header in _headers)
                Console.WriteLine($"  - {header}");
            Console.WriteLine($"Содержимое: {_contents.Count} элемент(ов)");
            foreach (var content in _contents)
                Console.WriteLine($"  - {content.Type}: {content.Title}");
            Console.WriteLine($"Футер: {_footer}");
            Console.WriteLine($"Формат: {_format}");
        }
    }
}