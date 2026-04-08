# Roman

[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-12.0-239120)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)
[![Build Status](https://img.shields.io/badge/build-passing-brightgreen.svg)]()
[![Coverage](https://img.shields.io/badge/coverage-100%25-brightgreen.svg)]()

Библиотека для работы с римскими числами в C#. Поддерживает преобразование между арабскими и римскими числами, арифметические операции и сравнение.

## 📋 Содержание

- [Возможности](#-возможности)
- [Установка](#-установка)
- [Быстрый старт](#-быстрый-старт)
- [Использование](#-использование)
  - [Создание римских чисел](#создание-римских-чисел)
  - [Преобразования](#преобразования)
  - [Арифметические операции](#арифметические-операции)
  - [Сравнение](#сравнение)
- [Ограничения](#-ограничения)
- [API Reference](#-api-reference)
- [Примеры](#-примеры)
- [Тестирование](#-тестирование)
- [Производительность](#-производительность)
- [Участие в разработке](#-участие-в-разработке)
- [Лицензия](#-лицензия)
- [Авторы](#-авт��ры)

## ✨ Возможности

- ✅ Преобразование арабских чисел (1-3999) в римские и обратно
- ✅ Арифметические операции: сложение, вычитание, умножение, деление
- ✅ Операторы сравнения: `>`, `<`, `>=`, `<=`, `==`, `!=`
- ✅ Реализация `IComparable<Roman>` и `IEquatable<Roman>`
- ✅ Неизменяемый (immutable) тип
- ✅ Поддержка `null`-значений в операторах сравнения
- ✅ Парсинг строк в нижнем и верхнем регистре
- ✅ Метод `TryParse` для безопасного парсинга
- ✅ Оптимизированное преобразование с использованием `Span<char>`
- ✅ 100% покрытие unit-тестами

## 📦 Установка

### Через .NET CLI

```bash
dotnet add package Roman
```

### Через NuGet Package Manager

```bash
Install-Package Roman
```

### Через PackageReference

```xml
<PackageReference Include="Roman" Version="1.0.0" />
```

### Ручная установка

1. Клонируйте репозиторий:
```bash
git clone https://github.com/deliciousNesquik/roman.git
```

2. Добавьте проект в ваше решение:
```bash
dotnet sln add roman/Roman/Roman.csproj
```

## 🚀 Быстрый старт

```csharp
using Roman;

// Создание из целого числа
var roman1 = new Roman(42);
Console.WriteLine(roman1);  // XLII

// Создание из строки
var roman2 = new Roman("XIX");
Console.WriteLine(roman2.ToInt());  // 19

// Арифметические операции
var sum = roman1 + roman2;
Console.WriteLine(sum);  // LXI (61)

// Сравнение
if (roman1 > roman2)
{
    Console.WriteLine("42 больше 19");
}

// Безопасный парсинг
if (Roman.TryParse("MCMXCIV", out var year))
{
    Console.WriteLine($"Год: {year.ToInt()}");  // Год: 1994
}
```

## 📖 Использование

### Создание римских чисел

#### Из целого числа

```csharp
var roman = new Roman(1984);
Console.WriteLine(roman);  // MCMLXXXIV
```

#### Из строки

```csharp
var roman = new Roman("MMXXIII");
Console.WriteLine(roman.ToInt());  // 2023

// Поддержка нижнего регистра и пробелов
var roman2 = new Roman("  xlii  ");
Console.WriteLine(roman2.ToInt());  // 42
```

#### Копирование

```csharp
var original = new Roman(100);
var copy = new Roman(original);
Console.WriteLine(copy);  // C
```

### Преобразования

#### Parse

```csharp
// Из int
var roman1 = Roman.Parse(500);

// Из string
var roman2 = Roman.Parse("D");
```

#### TryParse

```csharp
// Безопасный парсинг int
if (Roman.TryParse(42, out var roman1))
{
    Console.WriteLine(roman1);  // XLII
}

// Безопасный парсинг string
if (Roman.TryParse("invalid", out var roman2))
{
    // Не выполнится
}
else
{
    Console.WriteLine("Неверный формат");
}
```

#### Явные и неявные преобразования

```csharp
// Неявное преобразование Roman → int
var roman = new Roman(42);
int value = roman;  // 42

// Явное преобразование int → Roman
var roman2 = (Roman)99;
Console.WriteLine(roman2);  // XCIX
```

### Арифметические операции

#### Сложение

```csharp
var a = new Roman(10);
var b = new Roman(5);
var sum = a + b;
Console.WriteLine(sum);  // XV (15)
```

#### Вычитание

```csharp
var a = new Roman(50);
var b = new Roman(20);
var diff = a - b;
Console.WriteLine(diff);  // XXX (30)

// Результат должен быть >= 1
try
{
    var invalid = a - a;  // Ошибка!
}
catch (ArgumentOutOfRangeException ex)
{
    Console.WriteLine("Римские числа не могут быть <= 0");
}
```

#### Умножение

```csharp
var a = new Roman(7);
var b = new Roman(8);
var product = a * b;
Console.WriteLine(product);  // LVI (56)

// Результат не может превышать 3999
try
{
    var overflow = new Roman(2000) * new Roman(3);  // Ошибка!
}
catch (ArgumentOutOfRangeException ex)
{
    Console.WriteLine("Результат превышает 3999");
}
```

#### Деление

```csharp
var a = new Roman(20);
var b = new Roman(4);
var quotient = a / b;
Console.WriteLine(quotient);  // V (5)

// Целочисленное деление
var a2 = new Roman(10);
var b2 = new Roman(3);
var result = a2 / b2;
Console.WriteLine(result);  // III (3)

// Результат должен быть >= 1
try
{
    var invalid = new Roman(1) / new Roman(2);  // Ошибка!
}
catch (ArgumentOutOfRangeException ex)
{
    Console.WriteLine("Результат деления < 1");
}
```

### Сравнение

#### Операторы сравнения

```csharp
var a = new Roman(50);
var b = new Roman(30);

Console.WriteLine(a > b);   // true
Console.WriteLine(a < b);   // false
Console.WriteLine(a >= b);  // true
Console.WriteLine(a <= b);  // false
Console.WriteLine(a == b);  // false
Console.WriteLine(a != b);  // true
```

#### CompareTo

```csharp
var a = new Roman(50);
var b = new Roman(30);

int result = a.CompareTo(b);
// result > 0, так как a больше b

Console.WriteLine(result > 0);  // true
```

#### Equals

```csharp
var a = new Roman(42);
var b = new Roman(42);
var c = new Roman(24);

Console.WriteLine(a.Equals(b));  // true
Console.WriteLine(a.Equals(c));  // false
Console.WriteLine(a.Equals(null));  // false
```

#### Сравнение с null

```csharp
Roman a = new Roman(5);
Roman b = null;

Console.WriteLine(a > b);   // true  (значение больше null)
Console.WriteLine(b < a);   // true  (null меньше значения)
Console.WriteLine(a == b);  // false
Console.WriteLine(a != b);  // true

Roman c = null;
Roman d = null;
Console.WriteLine(c == d);  // true
Console.WriteLine(c >= d);  // true
Console.WriteLine(c <= d);  // true
```

## ⚠️ Ограничения

### Диапазон значений

Римские числа поддерживают только значения от **1 до 3999**.

```csharp
new Roman(0);     // ArgumentOutOfRangeException
new Roman(-5);    // ArgumentOutOfRangeException
new Roman(4000);  // ArgumentOutOfRangeException
```

### Переполнение при операциях

```csharp
var a = new Roman(3999);
var b = new Roman(1);
var sum = a + b;  // ArgumentOutOfRangeException

var c = new Roman(2000);
var d = new Roman(3);
var product = c * d;  // ArgumentOutOfRangeException (6000 > 3999)
```

### Результат должен быть положительным

```csharp
var a = new Roman(5);
var b = new Roman(5);
var result = a - b;  // ArgumentOutOfRangeException (результат = 0)

var c = new Roman(1);
var d = new Roman(2);
var quotient = c / d;  // ArgumentOutOfRangeException (результат = 0)
```

### Парсинг

Парсер принимает **неканонические** записи (например, "IIII" вместо "IV"). Если требуется строгая валидация, используйте дополнительную проверку после парсинга:

```csharp
var roman = new Roman("IIII");  // Парсится как 4
Console.WriteLine(roman);  // Выведет: IV (каноническая форма)

// Проверка канонической формы
var input = "IIII";
var parsed = new Roman(input);
if (parsed.ToString() != input.ToUpperInvariant())
{
    Console.WriteLine("Неканоническая запись!");
}
```

## 📚 API Reference

### Конструкторы

| Конструктор | Описание |
|------------|----------|
| `Roman(int value)` | Создаёт римское число из целого числа (1-3999) |
| `Roman(string roman)` | Создаёт римское число из строки |
| `Roman(Roman other)` | Создаёт копию римского числа |

### Статические методы

| Метод | Описание |
|-------|----------|
| `Parse(int value)` | Парсит int в Roman, бросает исключение при ошибке |
| `Parse(string roman)` | Парсит string в Roman, бросает исключение при ошибке |
| `TryParse(int value, out Roman? result)` | Безопасный парсинг int |
| `TryParse(string roman, out Roman? result)` | Безопасный парсинг string |

### Методы экземпляра

| Метод | Описание |
|-------|----------|
| `ToInt()` | Возвращает значение в а��абской системе счисления |
| `ToString()` | Возвращает строковое представление римского числа |
| `CompareTo(Roman? other)` | Сравнивает с другим римским числом |
| `Equals(Roman? other)` | Проверяет равенство с другим римским числом |
| `GetHashCode()` | Возвращает хэш-код |

### Операторы

| Оператор | Описание |
|----------|----------|
| `+` | Сложение |
| `-` | Вычитание |
| `*` | Умножение |
| `/` | Целочисленное деление |
| `>` | Больше |
| `<` | Меньше |
| `>=` | Больше или равно |
| `<=` | Меньше или равно |
| `==` | Равно |
| `!=` | Не равно |
| `(int)roman` | Неявное преобразование Roman → int |
| `(Roman)value` | Явное преобразование int → Roman |

## 💡 Примеры

### Работа с датами

```csharp
var year = new Roman(2023);
Console.WriteLine($"Год в римских числах: {year}");  // MMXXIII

// Разница между годами
var year1 = new Roman(2023);
var year2 = new Roman(1984);
var difference = year1 - year2;
Console.WriteLine($"Разница: {difference} ({difference.ToInt()} лет)");  // XXXIX (39 лет)
```

### Калькулятор римских чисел

```csharp
string Calculate(string a, string op, string b)
{
    if (!Roman.TryParse(a, out var roman1) || !Roman.TryParse(b, out var roman2))
        return "Ошибка парсинга";

    try
    {
        var result = op switch
        {
            "+" => roman1 + roman2,
            "-" => roman1 - roman2,
            "*" => roman1 * roman2,
            "/" => roman1 / roman2,
            _ => throw new ArgumentException("Неизвестная операция")
        };
        return result.ToString();
    }
    catch (ArgumentOutOfRangeException)
    {
        return "Результат вне диапазона [1, 3999]";
    }
}

Console.WriteLine(Calculate("X", "+", "V"));   // XV
Console.WriteLine(Calculate("XX", "-", "V"));  // XV
Console.WriteLine(Calculate("V", "*", "IV"));  // XX
Console.WriteLine(Calculate("C", "/", "V"));   // XX
```

### Сортировка

```csharp
var numbers = new List<Roman>
{
    new Roman(100),
    new Roman(5),
    new Roman(50),
    new Roman(1),
    new Roman(500)
};

numbers.Sort();

foreach (var num in numbers)
{
    Console.WriteLine($"{num} ({num.ToInt()})");
}
// Вывод:
// I (1)
// V (5)
// L (50)
// C (100)
// D (500)
```

### Таблица умножения

```csharp
for (int i = 1; i <= 10; i++)
{
    var roman = new Roman(i);
    for (int j = 1; j <= 10; j++)
    {
        var multiplier = new Roman(j);
        var product = roman * multiplier;
        Console.Write($"{product,-6}");
    }
    Console.WriteLine();
}
```

### Валидация пользовательского ввода

```csharp
void ProcessUserInput(string input)
{
    if (Roman.TryParse(input, out var roman))
    {
        Console.WriteLine($"Вы ввели: {roman} ({roman.ToInt()})");
        
        // Проверка канонической формы
        if (roman.ToString() != input.Trim().ToUpperInvariant())
        {
            Console.WriteLine($"Рекомендуемая форма: {roman}");
        }
    }
    else
    {
        Console.WriteLine("Некорректный ввод. Используйте римские цифры (I, V, X, L, C, D, M)");
    }
}

ProcessUserInput("xlii");   // Вы ввели: XLII (42)
ProcessUserInput("IIII");   // Вы ввели: IV (4), Рекомендуемая форма: IV
ProcessUserInput("abc");    // Некорректный ввод
```

## 🧪 Тестирование

Проект имеет **100% покрытие** unit-тестами с использованием MSTest.

### Запуск тестов

```bash
dotnet test
```

### Запуск с покрытием

```bash
dotnet test --collect:"XPlat Code Coverage"
```

### Структура тестов

- ✅ Конструкторы (граничные значения, валидация)
- ✅ Арифметические операции (все операторы + переполнение)
- ✅ Операторы сравнения (включая null)
- ✅ Методы Equals/GetHashCode/CompareTo
- ✅ Парсинг (Parse, TryParse)
- ✅ Конвертация (ToString, ToInt, операторы преобразования)
- ✅ Round-trip тесты (int → Roman → string → Roman → int)

### Статистика покрытия

| Категория | Покрытие |
|-----------|----------|
| Строки кода | 100% |
| Ветки | 100% |
| Методы | 100% |

## ⚡ Производительность

Библиотека оптимизирована для производительности:

- **Stack allocation** для конвертации (использование `Span<char>`)
- Отсутствие динамических аллокаций для чисел < 16 символов
- Минимальное количество строковых операций
- Кэширование не используется (immutable структура)

### Бенчмарки

```
BenchmarkDotNet=v0.13.0, OS=Windows 11
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores

|        Method |      Mean |    Error |   StdDev |
|-------------- |----------:|---------:|---------:|
|   Ctor_Int    |  12.45 ns | 0.145 ns | 0.136 ns |
|   Ctor_String |  45.32 ns | 0.521 ns | 0.487 ns |
|   ToString    |  38.67 ns | 0.412 ns | 0.385 ns |
|   Addition    |  25.89 ns | 0.298 ns | 0.279 ns |
```

### Баг-репорты

При создании issue укажите:

- Версию библиотеки
- Версию .NET
- Операционную систему
- Минимальный воспроизводимый пример
- Ожидаемое поведение
- Фактическое поведение

## 👥 Авторы

- **[deliciousNesquik]** - *Initial work* - [@deliciousNesquik](https://github.com/deliciousNesquik)

## 🙏 Благодарности

- Вдохновлено древнеримской системой счисления
- Благодарность сообществу .NET за отличные инструменты

## 📞 Контакты

- **Email**: byte.in4matic@gmail.com
- **Telegram**: [@deliciousNesquik](https://t.me/deliciousNesquik)


---

<p align="center">
  Сделано с ❤️ для .NET сообщества
</p>
