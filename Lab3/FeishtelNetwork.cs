using System.Text;
using Common;

namespace Lab3;

public static class FeishtelNetwork
{
    private const int Key1 = 12345678;
    private const int Key2 = 43;
    private const int CountRounds = 8;
    private const int CountBlocks = 4;

    private const int CountBitsInChar = 8;

    public static string Encode(string value)
    {
        var blocks = new int[CountBlocks];

        //Разделяем на блоки и переводим в биты и дополняем массив пустыми значениями до кратности 4
        var valueByteList = Encoding.ASCII.GetBytes(value).ToList();
        while (valueByteList.Count % CountBlocks != 0)
        {
            valueByteList.Add(0);
        }

        var lengthBlock = valueByteList.Count / CountBlocks;

        for (int i = 0; i < blocks.Length; i++)
        {
            blocks[i] = BitConverter.ToInt32(valueByteList.GetRange(i * lengthBlock, lengthBlock).ToArray());
        }

        for (var i = 0; i < CountRounds; i++)
        {
            var v = rotl(Key1, i) ^ rotr(Key2, i);
            var function = LogicalAdditionByScalar(blocks[0], v);
            
            var blocksAfterOneRound = new int[CountBlocks];
            for (var j = 0; j < CountBlocks; j++)
            {
                blocksAfterOneRound[j] = j switch
                {
                    0 => LogicalAdditionByScalar(blocks[j + 1], function),
                    CountBlocks - 1 => blocks[0],
                    _ => blocks[j + 1]
                };
            }
            blocks = blocksAfterOneRound;
        }

        var encodeValue = new StringBuilder();
        foreach (var block in blocks)
        {
            encodeValue.Append(Encoding.ASCII.GetString(BitConverter.GetBytes(block)));
        }
        
        return encodeValue.ToString();
    }
    
    public static string Decode(string value)
    {
        var blocks = new int[CountBlocks];

        //Разделяем на блоки и переводим в биты и дополняем массив пустыми значениями до кратности 4
        var valueByteList = Encoding.ASCII.GetBytes(value).ToList();
        while (valueByteList.Count % CountBlocks != 0)
        {
            valueByteList.Add(0);
        }

        var lengthBlock = valueByteList.Count / CountBlocks;

        for (int i = 0; i < blocks.Length; i++)
        {
            blocks[i] = BitConverter.ToInt32(valueByteList.GetRange(i * lengthBlock, lengthBlock).ToArray());
        }

        for (var i = CountRounds-1; i >= 0; i--)
        {
            var v = rotl(Key1, i) ^ rotr(Key2,i);
            var function = LogicalAdditionByScalar(blocks[0], v);
            
            var blocksAfterOneRound = new int[CountBlocks];
            for (var j = 0; j < CountBlocks; j++)
            {
                blocksAfterOneRound[j] = j switch
                {
                    0 => blocks[CountBlocks-1],
                    1 => LogicalAdditionByScalar(blocks[j + 1], function),
                    _ => blocks[j - 1]
                };
            }
            blocks = blocksAfterOneRound;
        }

        var encodeValue = new StringBuilder();
        foreach (var block in blocks)
        {
            encodeValue.Append(Encoding.ASCII.GetString(BitConverter.GetBytes(block)));
        }
        
        return encodeValue.ToString();
    }

    private static int LogicalAdditionByScalar(int value1, int value2)
    {
        return value1 ^ value2;
    }

    static int rotr(this int value, int bits)
    {
        bits %= 32;
        var res = Convert.ToString((value >> bits) | (value << (32 - bits)), 2).PadLeft(32, '0');
        return Convert.ToInt32( res.Substring(res.Length - 32, 32),2);
    }
 
    static int rotl(this int value, int bits)
    {
        bits %= 32;
        var res = Convert.ToString((value << bits) | (value >> (32 - bits)), 2).PadLeft(32,'0');
        return Convert.ToInt32( res.Substring(res.Length - 32, 32), 2);
    }


}