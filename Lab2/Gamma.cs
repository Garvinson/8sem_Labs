using System.Security.AccessControl;
using System.Text;
using Common;

namespace Lab2;

public static class Gamma
{
    /// <summary>
    /// Константы для линейного конгруэнтного датчика ПСЧ
    /// </summary>
    private const int A = 5;
    private const int C = 3;
    private const int M = 32;
    private const int T0 = 7;
    /// <summary>
    /// Длина машинного слова в битах
    /// </summary>
    private const int b = 6;

    /// <summary>
    /// Линейный конгруэнтный датчик ПСЧ
    /// </summary>
    /// <returns>Последовательность случайных чисел в двоичном виде</returns>
    private static string LinearCongruentTransducers(int count)
    {
        var stringRandomNumbers = new StringBuilder();
        var randomNumber = T0;
        
        for (var i = 0; i < count; i++)
        {
            randomNumber = (A * randomNumber + C) % M;
            stringRandomNumbers.Append(Convert.ToString(randomNumber,2).PadLeft(b,'0'));
        }

        return stringRandomNumbers.ToString();
    }
    

    /// <summary>
    /// Шифрует строку методом гаммирования
    /// </summary>
    /// <param name="value">Строка</param>
    /// <returns>Зашифорованная строка</returns>
    public static string Encode(this string? value)
    {
        var binStringEncoded = new StringBuilder();
        
        var stringRandomNumbers = LinearCongruentTransducers(value.Length);
        var binString = CommonHelper.EncodeBinString(value, b);

        for (var index = 0; index < stringRandomNumbers.Length; index++)
        {
            binStringEncoded.Append(stringRandomNumbers[index] == binString[index] ? '0' : '1');
        }

        return CommonHelper.DecodeBinString(binStringEncoded.ToString(), b);
    }
    
    /// <summary>
    /// Дешифрует строку методом гаммирования
    /// </summary>
    /// <param name="value">Зашифорованная строка</param>
    /// <returns>Дешифорованная строка</returns>
    public static string Decode(this string value)
    {
        return Encode(value);
    }
}