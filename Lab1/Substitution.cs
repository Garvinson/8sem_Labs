using System.Text;

namespace Lab1;

/// <summary>
/// Класс реализующий метод подстановки 
/// </summary>
public static class Substitution
{
    /// <summary>
    /// Cловарь для подстановки
    /// </summary>
    private static readonly char[][] Dictionary = new char[][]
    {
        new [] { 'A', 'Z' },
        new [] { 'B', ' ' },
        new [] { 'C', '.' },
        new [] { 'D', 'X' },
        new [] { 'E', 'Y' },
        new [] { 'F', ',' },
        new [] { 'G', '!' },
        new [] { 'H', 'S' },
        new [] { 'I', 'T' },
        new [] { 'J', ':' },
        new [] { 'K', ';' },
        new [] { 'L', 'Q' },
        new [] { 'M', 'R' },
        new [] { 'N', '?' },
        new [] { 'O', '-' },
        new [] { 'P', 'U' },
        new [] { 'Q', 'V' },
        new [] { 'R', 'W' },
        new [] { 'S', 'L' },
        new [] { 'T', 'M' },
        new [] { 'U', 'N' },
        new [] { 'V', 'O' },
        new [] { 'W', 'P' },
        new [] { 'X', 'A' },
        new [] { 'Y', 'B' },
        new [] { 'Z', 'C' },
        new [] { ' ', 'D' },
        new [] { '.', 'E' },
        new [] { ',', 'F' },
        new [] { '!', 'G' },
        new [] { ':', 'H' },
        new [] { ';', 'I' },
        new [] { '?', 'J' },
        new [] { '-', 'K' }
    };      
    
    /// <summary>
    /// Шифрует строку методом подстановки
    /// </summary>
    /// <param name="value">Строка</param>
    /// <returns>Зашифрованная строка</returns>
    public static string Encode(this string? value)
    {
        var encodeValue = new StringBuilder();
        foreach (var valueChar in value)
        {
            encodeValue.Append(Dictionary.FirstOrDefault(x => x [0] == valueChar)![1]);
        }

        return encodeValue.ToString();
    }

    /// <summary>
    ///  Дешифрует методом подстановки
    /// </summary>
    /// <param name="value">Строка</param>
    /// <returns>Дешифрованая строка</returns>
    public static string Decode(this string value)
    {
        var encodeValue = new StringBuilder();
        foreach (var valueChar in value)
        {
            encodeValue.Append(Dictionary.FirstOrDefault(x => x [1] == valueChar)![0]);
        }

        return encodeValue.ToString();
    }
}