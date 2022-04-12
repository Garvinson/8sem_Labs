using System.Text;

namespace Common;

public static class CommonHelper
{
    /// <summary>
    /// Кодировка буквы "А"
    /// </summary>
    private const int CodeA = 1040;
    
    /// <summary>
    /// Преобразует строку из русских символов в двоичное представление
    /// </summary>
    /// <param name="value">Строка</param>
    ///     /// <param name="countBits">Количество битов в символе строки</param>
    /// <returns>Двоичное представление строки</returns>
    public static string EncodeBinString(string value, int countBits)
    {
        var binString = new StringBuilder();
        foreach (var intChar in value.Select(Convert.ToInt16))
        {
            binString.Append(Convert.ToString(intChar - (CodeA - 1), 2).PadLeft(countBits, '0'));
        }

        return binString.ToString();
    }

    /// <summary>
    /// Преобразует двоичную строку в строку русских символов
    /// </summary>
    /// <param name="value">Двоичное представление строки</param>
    /// <param name="countBits">Количество битов в символе строки</param>
    /// <returns>Строка </returns>
    public static string DecodeBinString(string value, int countBits)
    {
        var decodeString = new StringBuilder();

        var subString = value;
        for (var i = 0; i < value.Length/countBits; i++)
        {
            var oneChar = subString[..countBits];
            subString = subString.Substring(countBits, subString.Length -countBits);
            
            decodeString.Append((char)(Convert.ToInt16(oneChar, 2) + (CodeA - 1)));
        }

        return decodeString.ToString();
    }
}