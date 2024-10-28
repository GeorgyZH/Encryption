namespace Encryption
{
    internal class Program
    {

        private static readonly UInt64[] RoundConstants = new UInt64[9]
        {
            0xa7d3e671d0ac4d79, 0x3ac991fc1e4754bd, 0x8ca57afb63b8ddd4,
            0xe5b3c5bea9880ca2, 0x39df29da2ba8cb4c, 0x4b22aa244170a6f9,
            0x5ae2b0367de433ff, 0x6020088b5eab7f78, 0x7c2c57d2dc6d7e0d
        };

        public static byte[] inv_sBlock = new byte[256];

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


        public static byte[,] MatrixH = new byte[,]
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

        //public static List<List<byte>> sBlock;
        //public static List<List<byte>> matrixH;

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

        

        #endregion

        #region Линейное преобразование


        #endregion

        public static byte[] Encrypt(byte[] plaintext)
        {
            throw new NotImplementedException();
        }

        public static byte[] Decrypt(byte[] ciphertext, Dictionary<byte,byte> invSBox)
        {
            throw new NotImplementedException();
        }

        public static void printByteArrayInHEx(byte[] bytes)
        {
            foreach (var item in bytes)
            {
                Console.Write(String.Format("0x{0:x2} ", item) + " ");
            }
        }

        public static byte[][] keySchedule(byte[] masterKey)
        {
            byte[][] roundKeys = new byte[9][];
            byte[] K0 = new byte[8];
            byte[] K1 = new byte[8];
            Array.Copy(masterKey, 0, K0, 0, 8);
            Array.Copy(masterKey, 8, K1, 0, 8);

            for (int i = 0; i < 9; i++)
            {
                byte[] temp = new byte[8];
                for (int j = 0; j < 8; j++)
                {
                    byte rconByte = (byte)((RoundConstants[i] >> (8 * (7 - j))) & 0xFF);
                    temp[j] = (byte)(K0[j] ^ sBlock[K1[j]] ^ rconByte);
                }
                roundKeys[i] = temp;
                K0 = K1;
                K1 = temp;
            }

            return roundKeys;
        }

        // Функция добавления раундового ключа
        public static byte[] AddRoundKey(byte[] state, byte[] roundKey)
        {
            byte[] newState = new byte[state.Length];
            for (int i = 0; i < state.Length; i++)
            {
                newState[i] = (byte)(state[i] ^ roundKey[i]);
            }
            return newState;
        }

        // Функция замены байтов с помощью S-блока
        public static byte[] SubstituteBytes(byte[] state, byte[] sBlock)
        {
            byte[] newState = new byte[state.Length];
            for (int i = 0; i < state.Length; i++)
            {
                newState[i] = sBlock[state[i]];
            }
            return newState;
        }

        // Линейное преобразование (Mixing Transformation)
        public static byte[] LinearTransform(byte[] state)
        {
            byte[] newState = new byte[8];
            for (int i = 0; i < 8; i++)
            {
                byte temp = 0;
                for (int j = 0; j < 8; j++)
                {
                    temp ^= Gmul(state[j], MatrixH[i,j]);
                }
                newState[i] = temp;
            }
            return newState;
        }

        // Обратное линейное преобразование
        public static byte[] InverseLinearTransform(byte[] state)
        {
            byte[] newState = new byte[8];
            for (int i = 0; i < 8; i++)
            {
                byte temp = 0;
                for (int j = 0; j < 8; j++)
                {
                    temp ^= Gmul(state[j], MatrixH[i,j]);
                }
                newState[i] = temp;
            }
            return newState;
        }

        // Умножение в поле Галуа GF(2^8)
        public static byte Gmul(byte a, byte b)
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
                    a ^= 0x1D; // Ирредуцируемый полином x^8 + x^4 + x^3 + x^2 + 1
                }
                b >>= 1;
            }
            return p;
        }

        public static byte[] EncryptBlock(byte[] inputBlock, byte[][] roundKeys)
        {
            byte[] state = AddRoundKey(inputBlock, roundKeys[0]);
            for (int i = 1; i <= 8; i++)
            {
                state = SubstituteBytes(state, sBlock);
                if (i != 8)
                {
                    state = LinearTransform(state);
                }
                state = AddRoundKey(state, roundKeys[i]);
            }
            return state;
        }

        public static byte[] DecryptBlock(byte[] cipherBlock, byte[][] roundKeys)
        {
            byte[] state = AddRoundKey(cipherBlock, roundKeys[8]);
            for (int i = 7; i >= 0; i--)
            {
                state = SubstituteBytes(state, inv_sBlock);
                if (i != 0)
                {
                    state = InverseLinearTransform(state);
                }
                state = AddRoundKey(state, roundKeys[i]);
            }
            return state;
        }

        static void Main(string[] args)
        {
            for (int i = 0; i < 256; i++)
            {
                inv_sBlock[sBlock[i]] = (byte)i;
            }

            // ключ шифрования
            byte[] masterKey = new byte[16] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F };

            
            // текст который нужно зашифровать
            byte[] plaintext = { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07 };

            // раундовые ключи
            byte[][] roundKeys = keySchedule(masterKey);

            Console.Write("Исходный: ");
            printByteArrayInHEx(plaintext);
            Console.WriteLine("\n");

            var encrypted = EncryptBlock(plaintext, roundKeys);
            var decrypted = DecryptBlock(encrypted, roundKeys);

            Console.Write("шифрование: ");
            printByteArrayInHEx(encrypted);

            Console.Write("\n\nдешифровка: ");
            printByteArrayInHEx(decrypted);
            Console.WriteLine("\n\n\n\n");
        }
    }
}
