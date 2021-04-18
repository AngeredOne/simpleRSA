using System;
using System.IO;
using LabRSA.Common;

namespace LabRSA
{
    class Program
    {
        static void Main(string[] args)
        {
            
            if (args.Length != 4)
            {
                Console.WriteLine($"Неверное количество аргументов: получено {args.Length} из 4!");
                return;
            }
            
            var inputFileName = args[0];
            var outputFileName = args[1];

            long p, q;

            if( !long.TryParse(args[2], out p) || !long.TryParse(args[3], out q))
            {
                Console.WriteLine($"Невозможно распознать переданные числа!");
                return;  
            }
            
            
            var rsa = new RSA(p, q);
            
            Console.WriteLine($"Ключи: {rsa.E} {rsa.N}");
            
            var input = File.ReadAllText(inputFileName);
            
            Console.WriteLine("Входной текст:");
            Console.WriteLine(input);
            Console.WriteLine();

            var encrypted = rsa.Encrypt(input);
            
            Console.WriteLine("Зашифрованный текст");
            Console.WriteLine(encrypted);
            Console.WriteLine();
            
            var decrypted = rsa.Decrypt(encrypted);

            Console.WriteLine("Расшифрованный текст");
            Console.WriteLine(decrypted);
            Console.WriteLine();
            
            File.WriteAllText(outputFileName, decrypted);

            Console.WriteLine("Расшифрованное сообщение записанно в файл!");

        }
    }
}