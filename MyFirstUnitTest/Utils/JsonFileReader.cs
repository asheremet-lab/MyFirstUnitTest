using System;
using System.IO;
using System.Text.Json;

namespace MyFirstUnitTest.Utils;

public static class JsonFileReader
{
    public static T ReadAndDeserialize<T>(string relativePath)
    {
        // Преобразуем относительный путь в абсолютный относительно директории запущенного теста
        var baseDir = AppDomain.CurrentDomain.BaseDirectory;
        var fullPath = Path.Combine(baseDir, relativePath);

        if (!File.Exists(fullPath))
        {
            // Резервная проверка на случай, если файл скопировался без папки Resources
            var fileName = Path.GetFileName(relativePath);
            var fallbackPath = Path.Combine(baseDir, fileName);

            if (File.Exists(fallbackPath))
            {
                fullPath = fallbackPath;
            }
        }

        var json = File.ReadAllText(fullPath);
        return JsonSerializer.Deserialize<T>(json)!;
    }
}