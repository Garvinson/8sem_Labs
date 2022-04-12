namespace RGZ;

public static class DiffiHellman
{
    private const long p = 29;
    private const long a = -1;
    private const long b = 1;
    private static readonly long[] G = {17,5}; /*{9,27} - значения по варианту не находятся на кривой*/

    /// <summary>
    /// Признак нахождения точна на кривой.
    /// </summary>
    /// <param name="point">Точка.</param>
    /// <returns>Признак нахождения точна на кривой.</returns>
    public static bool IsOnCurve(long[]? point)
    {
        if (point == null)
            return true;
        
        var x = point[0];
        var y = point[1];
        return Mod((y * y - x * x * x - a * x - b), p) == 0;
    }

    /// <summary>
    /// Вычисляет обратное число для деления, реализуется с помощью расширенного алгоритма Евклида
    /// </summary>
    /// <param name="k">Число</param>
    /// <returns>Обратное число</returns>
    public static long InverseMod(long k)
    {
        if (k < 0)
        {
            return p - InverseMod(-k);
        }

        long s = 0;
        long oldS = 1;
        long t = 1;
        long oldT = 0;
        var r = p;
        var oldR = k;

        while (r != 0)
        {
            var quotient = oldR / r;
            
            var tempR = r;
            r = oldR - quotient * r;
            oldR = tempR;
            
            var tempS = s;
            s = oldS - quotient * s;
            oldS = tempS;
            
            var tempT = t;
            t = oldT - quotient * t;
            oldT = tempT;
        }

        if (oldR != 1)
            throw new Exception($"Ошибка№1 в методе {nameof(InverseMod)} не существует обратной величины");
        
        if ( Mod(k * oldS, p) != 1)
            throw new Exception($"Ошибка№2 в методе {nameof(InverseMod)} не существует обратной величины");

        return Mod(oldS, p);
    }

    /// <summary>
    /// Сложение точек
    /// </summary>
    /// <param name="point1">Точка 1</param>
    /// <param name="point2">Точка 2</param>
    /// <returns>Результат сложения</returns>
    private static long[]? PointAdd(long[]? point1, long[]? point2)
    {
        if (!IsOnCurve(point1))
            throw new Exception($"Ошибка№1 в методе {nameof(PointAdd)} точка 1 не находится на кривой");
        
        if (!IsOnCurve(point2))
            throw new Exception($"Ошибка№2 в методе {nameof(PointAdd)} точка 2 не находится на кривой");

        if (point1 == null)
            return point2;

        if (point2 == null)
            return point1;

        var x1 = point1[0];
        var y1 = point1[1];
        
        var x2 = point2[0];
        var y2 = point2[1];

        if (x1 == x2 && y1 != y2)
            return null;

        var m = x1 == x2 
            ? (3 * x1 * x1 + a) * InverseMod(2 * y1) 
            : (y1 - y2) * InverseMod(x1 - x2);

        var x3 = m * m - x1 - x2;
        var y3 = y1 + m * (x3 - x1);

        var result = new[] { Mod(x3, p), Mod(-y3, p) };
        
        if (!IsOnCurve(result))
            throw new Exception($"Ошибка№3 в методе {nameof(PointAdd)} результат сложения точек не находится на прямой");

        return result;
    }
    
    /// <summary>
    /// Деление по модулю "Residual", тогда как обычное % - это Remainder
    /// </summary>
    /// <param name="n">Делимое</param>
    /// <param name="d">Делитель</param>
    /// <returns>Остаток деления</returns>
    private static long Mod(long n, long d)
    {
        long result = n % d;
        if (Math.Sign(result) * Math.Sign(d) < 0)
            result += d;
        return result;
    }

    /// <summary>
    /// Скалярное умножение, реализована путем удвоения сложения, алгоритм менее затратен чем обычное умножение k раз
    /// </summary>
    /// <param name="k">Скаляр.</param>
    /// <param name="point"> Точка на кривой.</param>
    /// <returns>Результат умножения</returns>
    public static long[]? ScalarMult( long k, long[]? point)
    {
        if (!IsOnCurve(point))
            throw new Exception($"Ошибка№1 в методе {nameof(ScalarMult)} точна не находится на кривой");

        long[]? result = null;
        var added = point;

        while (k != 0)
        {
            if ((k & 1) != 0)
            {
                result = PointAdd(result, added);
            }

            added = PointAdd(added, added);

            k >>= 1;
        }
        
        if (!IsOnCurve(point))
            throw new Exception($"Ошибка№2 в методе {nameof(ScalarMult)} результирующая точка сложения не находтся на прямой");
        
        return result;
    }

    /// <summary>
    /// Генерирует публичный ключ
    /// </summary>
    /// <param name="privateKey">Приватный ключ</param>
    /// <returns>Публичный ключ</returns>
    public static long[]? GetPublicKey(long privateKey)
    {
        return ScalarMult(privateKey, G);
    }
}