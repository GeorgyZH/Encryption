namespace Encryption
{
    internal class Program
    {
        public static byte[,] sBox = new byte[,]
        {
            {0xA7, 0xD3, 0xE6, 0x71, 0xD0, 0xAC, 0x4D, 0x79},
            {0x3A, 0xC9, 0x91, 0xFC, 0x1E, 0x47, 0x54, 0xBD},
            {0x8C, 0xA5, 0x7A, 0xFB, 0x63, 0xB8, 0xDD, 0xD4},
            {0xE5, 0xB3, 0xC5, 0xBE, 0xA9, 0x88, 0x0C, 0xA2},
            {0x39, 0xDF, 0x29, 0xDA, 0x2B, 0xA8, 0xCB, 0x4C},
            {0x4B, 0x22, 0xAA, 0x24, 0x41, 0x70, 0xA6, 0xF9},
            {0x5A, 0xE2, 0xB0, 0x36, 0x7D, 0xE4, 0x33, 0xFF},
            {0x60, 0x20, 0x08, 0x8B, 0x5E, 0xAB, 0x7F, 0x78}
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

        // нелинейное преобразование
        public static List<byte> nonLinearTransform(List<byte> inBlock)
        {
            List<byte> outBlock = new List<byte>();
            for (int i = 0; i < inBlock.Count; i++)
            {
                int row = (inBlock[i] & 0xf0) >> 4;
                int column = inBlock[i] & 0x0f;
                outBlock.Add(sBlock[row][column]);
            }
            return outBlock;
        }

        public static void linearTransform(List<byte> data, List<List<byte>> matrixH)
        {

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

        static void Main(string[] args)
        {

            // 5 - 101
            // 2 - 10
            // 5 xor 2 = 111(7)
            //Console.WriteLine(5^2);
            //Console.WriteLine(Convert.ToHexString(new byte[] {0xA7,0xD3}));

            fillSBlock(".\\sBlock.txt",ref sBlock);
            fillSBlock(".\\matrixH.txt",ref matrixH);

            

            //printBlock(ref sBlock);
            //Console.WriteLine(new String('-',100));
            //printBlock(ref matrixH);

            //List<byte> checklist = new List<byte> { 0x3f, 0x56, 0xa2, 0x89, 0x17, 0x5e, 0xc1, 0xb4 };

            //foreach (var item in nonLinearTransform(checklist))
            //{
            //    Console.WriteLine(String.Format("0x{0:x2} ", item) + " ");
            //}


        }
    }
}
