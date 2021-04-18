using System;
using System.Globalization;
using System.Numerics;
using System.Text;

namespace LabRSA.Common
{
    public class RSA
    {
        public long N { get; }
        public long M { get; }
        public long D { get; }
        public long E { get; }
        
        public RSA(long p, long q)
        {

            if (!IsTheNumberSimple(p) && !IsTheNumberSimple(q))
                throw new ArgumentException("Предоставленные числа не являются простыми!");
            
            N = p * q;
            M = (p - 1) * (q - 1);
            D = Get_D(M);
            E = Get_E(D, M);
            
        }
        
        public string Encrypt(string input)
        {
            var encoded = new StringBuilder();
            
            foreach (var character in input)
            {
                var bigIntCharWrap = new BigInteger(character);
                var subResult = BigInteger.Pow(bigIntCharWrap, (int) E);
                
                var bigIntNWrap = new BigInteger(N);
 
                var result = subResult % bigIntNWrap;

                var encodedChar = Encoding.Unicode.GetString(result.ToByteArray());
                
                encoded.Append(encodedChar);
            }
            
            return encoded.ToString();
        }

        public string Decrypt(string input)
        {
            var decoded = new StringBuilder();
            
            foreach (var character in input)
            {
                var bigIntCharWrap = new BigInteger(character);
                var subResult = BigInteger.Pow(bigIntCharWrap, (int) D);
 
                var bigIntNWrap = new BigInteger(N);
 
                var result = subResult % bigIntNWrap;

                var decodedChar = (char)(long)result;
                
                decoded.Append(decodedChar);
            }
            
            return decoded.ToString();
        }
        
        
        private long Get_D(long m)
        {
            long d = m - 1;
 
            for (long i = 2; i <= m; i++)
                
                if ( (m % i) + (d % i) == 0) //если имеют общие делители
                {
                    d--;
                    i = 1;
                }
 
            return d;
        }
        
        private long Get_E(long d, long m)
        {
            long e = 10;
 
            while (true)
            {
                if ((e * d) % m == 1)
                    break;
                else
                    e++;
            }
 
            return e;
        }
        
        private bool IsTheNumberSimple(long n)
        {
            // частные случаи
            
            if (n < 2)
                return false;

            if (n == 2)
                return true;
            
            for (long i = 2; i < n; i++)
                if (n % i == 0)
                    return false;

            return true;
        }

    }
}