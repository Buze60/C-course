namespace TaskManager.Console;

/// <summary>
/// Small helpers for reading and validating console input.
/// Keeping them here (instead of inside <see cref="App"/>) keeps the menu
/// code short and the input logic readable.
/// </summary>
public static class ConsoleUi
{
    public static string ReadString(string prompt)
    {
        System.Console.Write($"  {prompt}: ");
        return System.Console.ReadLine()?.Trim() ?? string.Empty;
    }

    public static string ReadRequiredString(string prompt)
    {
        while (true)
        {
            var value = ReadString(prompt);
            if (!string.IsNullOrWhiteSpace(value))
            {
                return value;
            }

            System.Console.WriteLine("  [!] This value is required.");
        }
    }

    public static string ReadOptional(string prompt, string current)
    {
        var value = ReadString($"{prompt} [{current}]");
        return string.IsNullOrWhiteSpace(value) ? current : value;
    }

    public static DateTime? ReadOptionalDate(string prompt, DateTime? current = null)
    {
        while (true)
        {
            var label = current.HasValue ? $"{prompt} [{current:yyyy-MM-dd}]" : $"{prompt}";
            var value = ReadString(label);

            if (string.IsNullOrWhiteSpace(value))
            {
                return current;
            }

            if (DateTime.TryParse(value, out var date))
            {
                return date;
            }

            System.Console.WriteLine("  [!] Not a valid date. Try e.g. 2026-08-15, or leave empty.");
        }
    }

    public static TEnum ReadEnum<TEnum>(string prompt) where TEnum : struct, Enum
    {
        while (true)
        {
            var value = ReadString($"{prompt} ({string.Join("/", Enum.GetNames<TEnum>())})");
            if (Enum.TryParse(value, ignoreCase: true, out TEnum result) && Enum.IsDefined(result))
            {
                return result;
            }

            System.Console.WriteLine("  [!] Invalid value. Try again.");
        }
    }

    public static TEnum ReadEnumOptional<TEnum>(string prompt, TEnum current) where TEnum : struct, Enum
    {
        while (true)
        {
            var value = ReadString($"{prompt} [{current}]");
            if (string.IsNullOrWhiteSpace(value))
            {
                return current;
            }

            if (Enum.TryParse(value, ignoreCase: true, out TEnum result) && Enum.IsDefined(result))
            {
                return result;
            }

            System.Console.WriteLine("  [!] Invalid value. Try again.");
        }
    }

    public static Guid ReadGuid(string prompt)
    {
        while (true)
        {
            var value = ReadString(prompt);
            if (Guid.TryParse(value, out var id))
            {
                return id;
            }

            System.Console.WriteLine("  [!] Invalid id. Copy the full id from the list above.");
        }
    }
}
