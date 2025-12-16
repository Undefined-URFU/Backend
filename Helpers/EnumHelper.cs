using System.ComponentModel;
using System.Reflection;

namespace CosmeticsRecommendationSystem.Helpers;

public static class EnumHelper
{
    /// <summary>
    ///     Преобразует строку в значение enum, игнорируя регистр
    /// </summary>
    /// <typeparam name="TEnum">Тип enum</typeparam>
    /// <param name="value">Строковое значение</param>
    /// <returns>Значение enum</returns>
    public static TEnum Parse<TEnum>(string value) where TEnum : struct, Enum
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Value cannot be null or empty", nameof(value));

        // Пробуем стандартный парсинг с игнорированием регистра
        if (Enum.TryParse<TEnum>(value, true, out var result))
        {
            return result;
        }

        // Если стандартный парсинг не сработал, ищем по Description attribute
        var enumType = typeof(TEnum);
        foreach (var field in enumType.GetFields(BindingFlags.Public | BindingFlags.Static))
        {
            var descriptionAttribute = field.GetCustomAttribute<DescriptionAttribute>();
            if (descriptionAttribute != null &&
                string.Equals(descriptionAttribute.Description, value, StringComparison.OrdinalIgnoreCase))
            {
                return (TEnum)field.GetValue(null)!;
            }

            // Также проверяем само имя поля
            if (string.Equals(field.Name, value, StringComparison.OrdinalIgnoreCase))
            {
                return (TEnum)field.GetValue(null)!;
            }
        }

        throw new ArgumentException($"Value '{value}' is not a valid value for enum {enumType.Name}");
    }

    /// <summary>
    ///     Безопасно преобразует строку в значение enum, возвращает null при неудаче
    /// </summary>
    public static TEnum? TryParse<TEnum>(string value) where TEnum : struct, Enum
    {
        if (string.IsNullOrWhiteSpace(value))
            return null;

        try
        {
            return Parse<TEnum>(value);
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    ///     Преобразует строку в значение enum с использованием fallback значения при неудаче
    /// </summary>
    public static TEnum ParseOrDefault<TEnum>(string value, TEnum defaultValue) where TEnum : struct, Enum
    {
        return TryParse<TEnum>(value) ?? defaultValue;
    }

    /// <summary>
    ///     Получает Description attribute значения enum или его имя
    /// </summary>
    public static string GetDescription<TEnum>(TEnum value) where TEnum : struct, Enum
    {
        var field = value.GetType().GetField(value.ToString());
        var descriptionAttribute = field?.GetCustomAttribute<DescriptionAttribute>();
        return descriptionAttribute?.Description ?? value.ToString();
    }

    /// <summary>
    ///     Получает все значения enum с их описаниями
    /// </summary>
    public static Dictionary<TEnum, string> GetDescriptions<TEnum>() where TEnum : struct, Enum
    {
        var result = new Dictionary<TEnum, string>();
        var enumType = typeof(TEnum);

        foreach (TEnum value in Enum.GetValues(enumType))
        {
            result.Add(value, GetDescription(value));
        }

        return result;
    }
}
