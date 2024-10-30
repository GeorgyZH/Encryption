namespace Encryption
{
    internal class Program
    {

        private static readonly UInt64[] RoundConstants = new UInt64[9]
        {
            //0x0000000000000000, 0x13198A2E03707344, 0xA4093822299F31D0,
            //0x082EFA98EC4E6C89, 0x452821E638D01377, 0xBE5466CF34E90C6C,
            //0x7EF84F78FD955CB1, 0x85840851F1AC43AA, 0xC882D32F25323C54
            0x413623a282829471,
            0xd33869a36e1abc39,
            0x83e18ba853003ddf,
            0x574e097cf2a0d00d,
            0xbd3284f79c8bd5ab,
            0x9919aba72c685ea0,
            0xa2f96d51d414472d,
            0x9e727c1730373b2a,
            0xde2bc1dc5cf44846
        };

        private static readonly byte[][] rc = [
            [0x41,  0x36  ,0x23  ,0xa2  ,0x82  ,0x82  ,0x94  ,0x71],
            [0xd3,  0x38  ,0x69  ,0xa3  ,0x6e  ,0x1a  ,0xbc  ,0x39],
            [0x83,  0xe1  ,0x8b  ,0xa8  ,0x53  ,0x00  ,0x3d  ,0xdf],
            [0x57,  0x4e  ,0x09  ,0x7c  ,0xf2  ,0xa0  ,0xd0  ,0x0d],
            [0xbd,  0x32  ,0x84  ,0xf7  ,0x9c  ,0x8b  ,0xd5  ,0xab],
            [0x99,  0x19  ,0xab  ,0xa7  ,0x2c  ,0x68  ,0x5e  ,0xa0],
            [0xa2,  0xf9  ,0x6d  ,0x51  ,0xd4  ,0x14  ,0x47  ,0x2d],
            [0x9e,  0x72  ,0x7c  ,0x17  ,0x30  ,0x37  ,0x3b  ,0x2a],
            [0xde,  0x2b  ,0xc1  ,0xdc  ,0x5c  ,0xf4  ,0x48  ,0x46]
        ];

        public static byte[] sBox =
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

        public static byte[,] matrix = new byte[,]
        {
            {0x1, 0x3, 0x4, 0x5, 0x6, 0x8, 0xB, 0x7},
            {0x3, 0x1, 0x5, 0x4, 0x8, 0x6, 0x7, 0xB},
            {0x4, 0x5, 0x1, 0x3, 0xB, 0x7, 0x6, 0x8},
            {0x5, 0x4, 0x3, 0x1, 0x7, 0xB, 0x8, 0x6},
            {0x6, 0x8, 0xB, 0x7, 0x1, 0x3, 0x4, 0x5},
            {0x8, 0x6, 0x7, 0xB, 0x3, 0x1, 0x5, 0x4},
            {0xB, 0x7, 0x6, 0x8, 0x4, 0x5, 0x1, 0x3},
            {0x7, 0xB, 0x8, 0x6, 0x5, 0x4, 0x3, 0x1},
        };

        public static List<List<byte>> sBlock;
        public static List<List<byte>> matrixH;

        // заполнение sBLock листа из txt файла
        public static void fillSBlock(string path,ref List<List<byte>> result)
        {
            var t = File.ReadAllLines(path);
            result = new List<List<byte>>();
            foreach (var line in t)
            {
                var temp = line.Split('\t');
                var tListByte = new List<byte>();
                foreach (var item in temp)
                {
                    tListByte.Add(byte.Parse(item, System.Globalization.NumberStyles.HexNumber));
                }
                result.Add(tListByte);
            }
        }

        static public void printBlock(ref List<List<byte>> Block)
        {
            foreach (var item in Block)
            {
                foreach (var item2 in item)
                {
                    Console.Write(string.Format("0x{0:x2} ", item2) + " ");
                }
                Console.WriteLine();
            }
        }

        #region Нелинейное преобразование

        // нелинейное преобразование, заменяет один байт ну другой согласно SBox
        public static UInt64 nonLinearTransform(byte[] inBlock)
        {
            List<byte> outBlock = new List<byte>();
            for (int i = 0; i < inBlock.Length; i++)
            {
                outBlock.Add(sBox[inBlock[i]]);
            }
            return BitConverter.ToUInt64(outBlock.ToArray());
        }

        public static UInt64 nonLinearTransformD(byte[] inBlock, Dictionary<byte,byte> invSBox)
        {
            List<byte> outBlock = new List<byte>();
            for (int i = 0; i < inBlock.Length; i++)
            {
                outBlock.Add(invSBox[inBlock[i]]);
            }
            return BitConverter.ToUInt64(outBlock.ToArray());
        }


        #endregion

        #region Линейное преобразование

        public static byte GaloisFieldMultiply(byte a, byte b)
        {
            byte p = 0;
            byte hiBitSet;
            for (int counter = 0; counter < 8; counter++)
            {
                if ((b & 1) != 0)
                {
                    p ^= a;
                }
                // проверка что старший бит не выходит за границы
                hiBitSet = (byte)(a & 0x80);
                a <<= 1;
                // в случае выхода бита за границы применяется xor на 1D
                if (hiBitSet != 0)
                {
                    a ^= 0x1D;
                }
                b >>= 1;
            }
            return p;
        }
        // линейное преобразование
        public static UInt64 linearTransform(byte[] data)
        {
            byte[] outBlock = new byte[8];

            for (int i = 0; i < 8; i++)
            {
                byte sum = 0;
                for (int j = 0; j < 8; j++)
                {
                    sum ^= GaloisFieldMultiply(data[j], matrix[j, i]);
                }
                outBlock[i] = sum;
            }

            return BitConverter.ToUInt64(outBlock);
        }

        
        static UInt64 MatrixMultiplicationD(byte[] data)
        {

            byte[] res = new byte[8];
            int i = 0;
            byte sum = 0;

            for (var j = 0; j < 8; j++)
            {
                for (var k = 0; k < 8; k++)
                {
                    sum += (byte)(data[i] * matrix[k, j]);
                    i++;
                }
                res[j] = sum;
                sum = 0;
                i = 0;
            }

            return BitConverter.ToUInt64(res);
        }
        #endregion

        public static UInt64 RoundMethod(UInt64 State, byte[] RoundConst)
        {
            State = nonLinearTransform(BitConverter.GetBytes(State));
            Console.WriteLine("Не линейное");
            printByteArrayInHEx(BitConverter.GetBytes(State));
            Console.WriteLine();
            // линейное преобразование
            State = linearTransform(BitConverter.GetBytes(State));
            Console.WriteLine("Линейное");
            printByteArrayInHEx(BitConverter.GetBytes(State));
            Console.WriteLine();

            // применение раундового ключа
            State = KeyAdd(RoundConst, BitConverter.GetBytes(State));
            Console.WriteLine("Добавление ключа");
            printByteArrayInHEx(BitConverter.GetBytes(State));
            Console.WriteLine();

            return State;
        }

        public static UInt64 KeyAdd(byte[] a, byte[] b)
        {
            var result = new byte[8];
            for (int i = 0; i < 8; i++)
            {
                result[i] = (byte)(a[i] ^ b[i]);
            }
            return BitConverter.ToUInt64(result);
        }

        public static byte[] Encrypt(byte[] plaintext)
        {
            Console.WriteLine("Encrypt");
            UInt64 state = BitConverter.ToUInt64(plaintext);
            // применение раундового ключа
            state = KeyAdd(rc[0], BitConverter.GetBytes(state));

            printByteArrayInHEx(BitConverter.GetBytes(state));
            Console.WriteLine();            

            for (int i = 1; i <=7 ; i++)
            {
                state = RoundMethod(state, rc[i]);

                Console.WriteLine("Раундовый метод");
                printByteArrayInHEx(BitConverter.GetBytes(state));
                Console.WriteLine();
            }
            state = nonLinearTransform(BitConverter.GetBytes(state));
            state = KeyAdd(rc[8], BitConverter.GetBytes(state));

            return BitConverter.GetBytes(state);
        }

        public static byte[] Decrypt(byte[] ciphertext)
        {
            Console.WriteLine("Decrypt");
            UInt64 state = BitConverter.ToUInt64(ciphertext);
            state = KeyAdd(rc[8], BitConverter.GetBytes(state));
            Console.WriteLine("Добавление ключа");
            printByteArrayInHEx(BitConverter.GetBytes(state));
            Console.WriteLine();
            for (int i = 7; i >= 1; i--)
            {
                Console.WriteLine("Раунд: "+i);
                state = RoundMethod(state, BitConverter.GetBytes(linearTransform(rc[i])) );
                Console.WriteLine("--------------------------------------------- ");
            }
            state = nonLinearTransform(BitConverter.GetBytes(state));
            state = KeyAdd(rc[0], BitConverter.GetBytes(state));

            return BitConverter.GetBytes(state);
        }

        public static void printByteArrayInHEx(byte[] bytes)
        {
            foreach (var item in bytes)
            {
                Console.Write(String.Format("0x{0:x2} ", item) + " ");
            }
        }

        static void Main(string[] args)
        {
            // текст который нужно зашифровать
            byte[] plaintext =
            {
                0x61  ,0x62  ,0x63  ,0x64  ,0x65  ,0x66  ,0x67  ,0x68
            };

            Console.Write("Исходный: ");
            printByteArrayInHEx(plaintext);
            Console.WriteLine("\n");

            var encrypted = Encrypt(plaintext);
            var decrypted = Decrypt(encrypted);

            Console.Write("шифрование: ");
            printByteArrayInHEx(encrypted);

            Console.Write("\n\nдешифровка: ");
            printByteArrayInHEx(decrypted);
            Console.WriteLine("\n\n\n\n");
        }
    }
}
