using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WWT_Automation.Config
{
    public class AppSettings
    {
        public SeleniumSettings Selenium { get; set; } = new();
    }

    public class SeleniumSettings
    {
        public string Browser { get; set; } = "Chrome";
        public string BaseUrl { get; set; } = "";
        public int Timeout { get; set; } = 30;
    }

    public static class TestConfig
    {
        private static readonly Lazy<AppSettings> _current = new(() => Load());

        public static AppSettings Current => _current.Value;

        private static AppSettings Load()
        {
            var path = Path.Combine(AppContext.BaseDirectory, "appsettings.json");
            if (!File.Exists(path))
                throw new FileNotFoundException($"Missing config file: {path}");

            var json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<AppSettings>(
                json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            ) ?? new AppSettings();
        }
    }
}
