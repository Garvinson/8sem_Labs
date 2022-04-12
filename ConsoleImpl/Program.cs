using System.Security.Cryptography;
using Lab1;
using Lab2;
using Lab3;
using RGZ;

const string menu = "Консольный интерфейс для Лабораторных работ по МиАЗИ \n" +
                    "1 - Лабоработрная работа №1 \n" +
                    "2 - Лабоработрная работа №2 \n" +
                    "3 - Лабоработрная работа №3 \n" +
                    "8 - РГЗ \n" +
                    "9 - Очистить консоль \n" +
                    "0 - Выход";

Console.WriteLine(menu);

var inputCommand = 1;
while (inputCommand != 0)
{
    inputCommand = Convert.ToInt32(Console.ReadLine());
    switch (inputCommand)
    {
        case 1:
        {
            //пример для шифрования ABCDEFGHIJKLMNOPQRSTUVWXYZ
            Console.WriteLine("Шифрование данных методами подстановки");
            Console.WriteLine("Введите строку для шифрования > ");
            
            var str = Console.ReadLine();
            var strEncode = Substitution.Encode(str);
            
            Console.WriteLine($"Зашифрованая строка: {strEncode}");
            Console.WriteLine($"Дешифрованная строка: {Substitution.Decode(strEncode)}");
            break;
        }
        case 2:
        {
            //пример для шифрования АБВабв
            Console.WriteLine("Шифрование данных методами гаммирования");
            Console.WriteLine("Введите строку для шифрования > ");
            
            var str = Console.ReadLine();
            var strEncode = Gamma.Encode(str);
            
            Console.WriteLine($"Зашифрованая строка: {strEncode}");
            Console.WriteLine($"Дешифрованная строка: {Gamma.Decode(strEncode)}");
            break;
        }
        case 3:
        {
            //пример для шифрования АБВабв
            Console.WriteLine("Шифрование данных методом Сети Фейштеля(реализовано только шифрование возвращяется в виде битов.\n" +
                              "Проблема в том что если зашифровать 'абв' и 'йуц' получим одинаковый шифр не понятно как расшифровавать\n" +
                              "возможно неправильно понял метод");
            Console.WriteLine("Введите строку для шифрования > ");
            
            var str = Console.ReadLine();
            var strEncode = FeishtelNetwork.Encode(str);
            
            Console.WriteLine($"Зашифрованая строка: {strEncode}");
            break;
        }
        case 8:
        {
            long privateKey1 = 4;
            long privateKey2 = 17;
            
            try
            {
                Console.WriteLine("\nПриватный ключ 1: " + privateKey1);
            
                var publickey1 = DiffiHellman.GetPublicKey(privateKey1);
                Console.WriteLine($"Публичный ключ 1: {publickey1[0]} {publickey1[1]}");

                Console.WriteLine("\nПриватный ключ 2: " + privateKey2);
                var publickey2 = DiffiHellman.GetPublicKey(privateKey2);
                Console.WriteLine($"Публичный ключ 2: {publickey2[0]} {publickey2[1]}");


                var s1 = DiffiHellman.ScalarMult(privateKey1, publickey2);
                var s2 = DiffiHellman.ScalarMult(privateKey2, publickey1);
                Console.WriteLine($"\nОбщий секретный ключ 1: {s1[0]} {s1[1]}");
                Console.WriteLine($"Общий секретный ключ 2: {s2[0]} {s2[1]}");
            }
            catch (Exception e)
            {
                Console.WriteLine("Не удалось сформировать ключи" + e);
                throw;
            }
            break;
        }
        case 9:
        {
            Console.Clear();
            Console.WriteLine(menu);
            break;
        }
        case 0:
        {
            Console.WriteLine("Выход из программы...");
            break;
        }
        default:
        {
            Console.WriteLine("Неизвестная команда");
            break;
        }

    }
}
