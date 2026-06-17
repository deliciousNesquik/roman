namespace RomanNumerals;

/// <summary>Режим разбора строкового римского числа.</summary>
public enum RomanStyle
{
    /// <summary>
    ///     Лояльный разбор: принимает неканонические формы (например, "IIII" = 4).
    ///     Поведение по умолчанию.
    /// </summary>
    Lenient = 0,

    /// <summary>
    ///     Строгий разбор: принимает только каноническую запись
    ///     (например, "IV", но не "IIII").
    /// </summary>
    Strict
}
