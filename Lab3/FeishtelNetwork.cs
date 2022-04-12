using System.Text;
using Common;

namespace Lab3;

public static class FeishtelNetwork
{
    private const string K1 = "0101";
    private const string K2 = "1010";
    private const int CountRounds = 8;
    private const int CountBlocks = 4;

    private const int CountBitsInChar = 8;

    public static string Encode(string value)
    {
        var blocks = new string[CountBlocks];

        //Разделяем на блоки и переводим в биты
        var subStringValue = CommonHelper.EncodeBinString(value,CountBitsInChar);
        var lengthBlock = subStringValue.Length / CountBlocks;
        for (var j = 0; j < CountBlocks; j++)
        {
            blocks[j] = subStringValue[..lengthBlock];
            subStringValue = subStringValue.Substring(lengthBlock, subStringValue.Length - lengthBlock);
        }
        
        for (var i = 0; i < CountRounds; i++)
        {
            var v = LogicalAddition(ShiftLeft(K1,i), ShiftRight(K2,i));
            var function = LogicalAddition(blocks[0], v);
            
            var blocksAfterOneRound = new string[CountBlocks];
            for (var j = 0; j < CountBlocks; j++)
            {
                blocksAfterOneRound[j] = j switch
                {
                    0 => LogicalAddition(blocks[j + 1], function),
                    CountBlocks - 1 => blocks[0],
                    _ => blocks[j + 1]
                };
            }
            blocks = blocksAfterOneRound;
        }

        var encodeValue = new StringBuilder();
        foreach (var block in blocks)
        {
            encodeValue.Append(block);
        }
        
        return encodeValue.ToString();
    }

    private static string ShiftRight(string value, int count)
    {
        var shiftValue = value;
        for (var i = 0; i < count; i++)
        {
            var bit = shiftValue.Substring(shiftValue.Length - 1, 1);
            shiftValue = shiftValue.Substring(0, shiftValue.Length-1);
            shiftValue = $"{bit}{shiftValue}";
        }

        return shiftValue;
    }
    
    private static string ShiftLeft(string value, int count)
    {
        var shiftValue = value;
        for (var i = 0; i < count; i++)
        {
            var bit = shiftValue[..1];
            shiftValue = shiftValue.Substring(1, shiftValue.Length-1);
            shiftValue = $"{shiftValue}{bit}";
        }

        return shiftValue;
    }

    private static string LogicalAddition(string value1, string value2)
    {
        if (value1.Length < value2.Length)
        {
            while (value1.Length != value2.Length)
            {
                value1 = $"0{value1}";
            }
        }
        else
        {
            while (value2.Length != value1.Length)
            {
                value2 = $"0{value2}";
            }
        }
        
        var additionValue = new StringBuilder();
        for (var i = 0; i < value2.Length; i++)
        {
            if (value1[i] == '0' && value2[i] == '0')
            {
                additionValue.Append('0');
            }
            else
            {
                additionValue.Append('1');
            }
        }
        
        return additionValue.ToString()!;
    }


}