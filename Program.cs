using System.Text;
using System.Xml;

namespace Encryption
{
    internal class Program
    {
        public static void printByteArrayInHEx(byte[] bytes)
        {
            foreach (var item in bytes)
            {
                Console.Write(String.Format("0x{0:x2} ", item) + " ");
            }
        }

        public static void printByteToChar(byte[] bytes)
        {
            for (int i = 0; i < bytes.Length; i++)
            {
                Console.Write((char)bytes[i]+" ");
            }
        }

        //0xa7d3e671d0ac4d79, 0x3ac991fc1e4754bd, 0x8ca57afb63b8ddd4,
        //    0xe5b3c5bea9880ca2, 0x39df29da2ba8cb4c, 0x4b22aa244170a6f9,
        //    0x5ae2b0367de433ff, 0x6020088b5eab7f78, 0x7c2c57d2dc6d7e0d

        private static readonly UInt64[] RoundConstants = new UInt64[9]
        {
            0x807441d6770beade,
            0xadf5f72430ab7828,
            0x324629ba140c3982,
            0xe7416d14a8bd26bd,
            0x3df17e33e28b5c61,
            0x79f39fc4d0fa11d1,
            0x3d1b8ac1733f0414,
            0xb91b034456a5bf2e,
            0x3377494c15c7b14b
        };

        public static byte[] sBlock =
        {
            0xA7,  0xD3,  0xE6,  0x71,  0xD0,  0xAC,  0x4D, 0x79,
            0x3A,  0xC9,  0x91,  0xFC,  0x1E,  0x47,  0x54, 0xBD,
            0x8C,  0xA5,  0x7A,  0xFB,  0x63,  0xB8,  0xDD, 0xD4,
            0xE5,  0xB3,  0xC5,  0xBE,  0xA9,  0x88,  0x0C, 0xA2,
            0x39,  0xDF,  0x29,  0xDA,  0x2B,  0xA8,  0xCB, 0x4C,
            0x4B,  0x22,  0xAA,  0x24,  0x41,  0x70,  0xA6, 0xF9,
            0x5A,  0xE2,  0xB0,  0x36,  0x7D,  0xE4,  0x33, 0xFF,
            0x60,  0x20,  0x08,  0x8B,  0x5E,  0xAB,  0x7F, 0x78,
            0x7C,  0x2C,  0x57,  0xD2,  0xDC,  0x6D,  0x7E, 0x0D,
            0x53,  0x94,  0xC3,  0x28,  0x27,  0x06,  0x5F, 0xAD,
            0x67,  0x5C,  0x55,  0x48,  0x0E,  0x52,  0xEA, 0x42,
            0x5B,  0x5D,  0x30,  0x58,  0x51,  0x59,  0x3C, 0x4E,
            0x38,  0x8A,  0x72,  0x14,  0xE7,  0xC6,  0xDE, 0x50,
            0x8E,  0x92,  0xD1,  0x77,  0x93,  0x45,  0x9A, 0xCE,
            0x2D,  0x03,  0x62,  0xB6,  0xB9,  0xBF,  0x96, 0x6B,
            0x3F,  0x07,  0x12,  0xAE,  0x40,  0x34,  0x46, 0x3E,
            0xDB,  0xCF,  0xEC,  0xCC,  0xC1,  0xA1,  0xC0, 0xD6,
            0x1D,  0xF4,  0x61,  0x3B,  0x10,  0xD8,  0x68, 0xA0,
            0xB1,  0x0A,  0x69,  0x6C,  0x49,  0xFA,  0x76, 0xC4,
            0x9E,  0x9B,  0x6E,  0x99,  0xC2,  0xB7,  0x98, 0xBC,
            0x8F,  0x85,  0x1F,  0xB4,  0xF8,  0x11,  0x2E, 0x00,
            0x25,  0x1C,  0x2A,  0x3D,  0x05,  0x4F,  0x7B, 0xB2,
            0x32,  0x90,  0xAF,  0x19,  0xA3,  0xF7,  0x73, 0x9D,
            0x15,  0x74,  0xEE,  0xCA,  0x9F,  0x0F,  0x1B, 0x75,
            0x86,  0x84,  0x9C,  0x4A,  0x97,  0x1A,  0x65, 0xF6,
            0xED,  0x09,  0xBB,  0x26,  0x83,  0xEB,  0x6F, 0x81,
            0x04,  0x6A,  0x43,  0x01,  0x17,  0xE1,  0x87, 0xF5,
            0x8D,  0xE3,  0x23,  0x80,  0x44,  0x16,  0x66, 0x21,
            0xFE,  0xD5,  0x31,  0xD9,  0x35,  0x18,  0x02, 0x64,
            0xF2,  0xF1,  0x56,  0xCD,  0x82,  0xC8,  0xBA, 0xF0,
            0xEF,  0xE9,  0xE8,  0xFD,  0x89,  0xD7,  0xC7, 0xB5,
            0xA4,  0x2F,  0x95,  0x13,  0x0B,  0xF3,  0xE0, 0x37
        };

        public static byte[][] matrixH = new byte[][]
        {
            [0x1, 0x3, 0x4, 0x5, 0x6, 0x8, 0xB, 0x7],
            [0x3, 0x1, 0x5, 0x4, 0x8, 0x6, 0x7, 0xB],
            [0x4, 0x5, 0x1, 0x3, 0xB, 0x7, 0x6, 0x8],
            [0x5, 0x4, 0x3, 0x1, 0x7, 0xB, 0x8, 0x6],
            [0x6, 0x8, 0xB, 0x7, 0x1, 0x3, 0x4, 0x5],
            [0x8, 0x6, 0x7, 0xB, 0x3, 0x1, 0x5, 0x4],
            [0xB, 0x7, 0x6, 0x8, 0x4, 0x5, 0x1, 0x3],
            [0x7, 0xB, 0x8, 0x6, 0x5, 0x4, 0x3, 0x1],
        };

        static byte[] nonLinearTransform(byte[] a)
        {
            // Нелинейный слой (S-блок)
            byte[] result = new byte[8];
            for (int i = 0; i < 8; i++)
            {
                result[i] = sBlock[a[i]];
            }
            Console.WriteLine("Не линейное");
            printByteArrayInHEx(result);
            Console.WriteLine();
            return result;
        }

        static byte[] linearTransform(byte[] a)
        {
            // Линейный слой
            byte[] result = new byte[8];
            for (int i = 0; i < 8; i++)
            {
                result[i] = GaloisFieldMultiplyBytesArray(a, matrixH[i]);
            }
            Console.WriteLine("Линейное");
            printByteArrayInHEx(result);
            Console.WriteLine();
            return result;
        }

        static byte[] KeyAdd(byte[] k, byte[] a)
        {
            // Побитовое сложение ключа и данных
            byte[] result = new byte[8];
            for (int i = 0; i < 8; i++)
            {
                result[i] = (byte)(k[i] ^ a[i]);
            }
            Console.WriteLine("Добавление ключа");
            printByteArrayInHEx(result);
            Console.WriteLine();
            return result;
        }

        static byte[] RoundMethod(byte[] k, byte[] a)
        {
            // Раундовая функция
            return KeyAdd(k, linearTransform(nonLinearTransform(a)));
        }

        static byte[] Feil(byte[] k, byte[] constR)
        {
            byte[] L = new byte[8];
            byte[] R = new byte[8];
            Array.Copy(k, 0, L, 0, 8);
            Array.Copy(k, 8, R, 0, 8);

            byte[] temp = RoundMethod(constR, R);
            byte[] newL = XOR(L, temp);

            byte[] result = new byte[16];
            Array.Copy(R, 0, result, 0, 8);
            Array.Copy(newL, 0, result, 8, 8);

            return result;
        }

        static byte[][] KeySchedule(byte[] keyT)
        {
            List<byte[]> aRes = new List<byte[]>();
            for (int i = 0; i < 9; i++)
            {
                byte[] data = Feil(keyT, BitConverter.GetBytes(RoundConstants[i]));
                byte[] key = new byte[8];
                Array.Copy(data, 8, key, 0, 8);
                aRes.Add(key);
                keyT = data;
                //aRes.Add(BitConverter.GetBytes(RoundConstants[i]));
            }
            return [.. aRes];
        }
        
        static byte GaloisFieldMultiplyBytesArray(byte[] a, byte[] b)
        {
            byte res = 0;
            for (int i = 0; i < 8; i++)
            {
                res ^= GaloisFieldMultiply(a[i], b[i]);
            }
            return res;
        }

        static byte GaloisFieldMultiply(byte a, byte b)
        {
            byte p = 0;
            for (int counter = 0; counter < 8; counter++)
            {
                if ((b & 1) != 0)
                {
                    p ^= a;
                }
                bool hiBitSet = (a & 0x80) != 0;
                a <<= 1;
                if (hiBitSet)
                {
                    a ^= 0x1D; // x^8 + x^4 + x^3 + x^2 + 1
                }
                b >>= 1;
            }
            return p;
        }

        static byte[] XOR(byte[] a, byte[] b)
        {
            byte[] result = new byte[a.Length];
            for (int i = 0; i < a.Length; i++)
            {
                result[i] = (byte)(a[i] ^ b[i]);
            }
            return result;
        }

        static byte[] Encrypt(byte[][] RoundKey, byte[] Text)
        {
            Console.WriteLine($"Encrypt");
            byte[] st = KeyAdd(RoundKey[0], Text);
            Console.WriteLine("Добавление раундового ключа");
            Console.WriteLine($"state: {BitConverter.ToUInt64(Text)}");
            Console.WriteLine($"rkey: {BitConverter.ToUInt64(RoundKey[0])}");
            printByteArrayInHEx(st);
            Console.WriteLine();
            for (int i = 1; i <= 7; i++)
            {
                st = RoundMethod(RoundKey[i], st);
                Console.WriteLine("Раундовый метод");
                printByteArrayInHEx(st);
                Console.WriteLine();
            }
            return KeyAdd(RoundKey[8], nonLinearTransform(st));
        }
        static byte[] Decrypt(byte[][] RoundKey, byte[] ChipherText)
        {
            Console.WriteLine($"Decrypt");
            byte[] st = KeyAdd(RoundKey[8], ChipherText);
            Console.WriteLine("Добавление раундового ключа");
            printByteArrayInHEx(st);
            Console.WriteLine();
            for (int i = 7; i >= 1; i--)
            {
                byte[] temp = RoundMethod(linearTransform(RoundKey[i]), st);
                st = temp;
                Console.WriteLine("Раундовый метод");
                printByteArrayInHEx(st);
                Console.WriteLine();
            }
            return KeyAdd(RoundKey[0], nonLinearTransform(st));
        }

        static void Main(string[] args)
        {
            string text = "qwertyuiopasdfgh";
            Console.WriteLine($"Ключ: {text}");
            char[] symbols = [.. text];
            if(symbols.Length > 16)
            {
                throw new Exception();
            }
            var tc = new ASCIIEncoding();
            var tb = tc.GetBytes(symbols);

            byte[] keyT = tb;//new byte[16]; // 128 бит
            //keyT[0] = 0x80; // Большой порядок (big-endian)

            // Генерация раундовых ключей
            byte[][] keys = KeySchedule(keyT);
            Console.WriteLine("Раунодовые ключи с расширением");
            foreach (var key in keys)
            {
                Console.WriteLine(BitConverter.ToInt64(key)); 
                Console.WriteLine();
            }
            // текст который нужно зашифровать
            byte[] plaintext = new byte[8] { (byte)'a', (byte)'b', (byte)'c', (byte)'d', (byte)'e', (byte)'f', (byte)'g', (byte)'h' };
            Console.WriteLine($"Текст для шифрования: ");
            printByteArrayInHEx(plaintext);
            Console.WriteLine();

            Console.Write("Исходный: ");
            //printByteArrayInHEx(plaintext);
            printByteToChar(plaintext);
            Console.WriteLine("\n");

            var encrypted = Encrypt(keys, plaintext);
            var decrypted = Decrypt(keys, encrypted);

            Console.Write("шифрование: ");
            //printByteArrayInHEx(encrypted);
            printByteToChar(encrypted);

            Console.Write("\n\nдешифровка: ");
            //printByteArrayInHEx(decrypted);
            printByteToChar(decrypted);
            Console.WriteLine("\n\n\n\n");
        }
    }
}
