using System;
using System.Text;

namespace rc4
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            string str = "Hello World faruk";
            string key = "key!!'!'^";

            byte[] res = Encrypt(Encoding.ASCII.GetBytes(key),Encoding.ASCII.GetBytes(str));
            
            string result = Encoding.ASCII.GetString(res);
            Console.WriteLine(result);
            Console.WriteLine("-----");

            byte[] decRes = Decrypt(Encoding.ASCII.GetBytes(key),res);

            string decResult = Encoding.ASCII.GetString(decRes);

            Console.WriteLine(decResult);

            Console.ReadLine();
        }

        public static byte[] Encrypt(byte[] pwd, byte[] data) {
            int a, i, j, k, tmp;
            int[] key, box;
            byte[] cipher;

            key = new int[256];
            box = new int[256];
            cipher = new byte[data.Length];

            for (i = 0; i < 256; i++) {
                key[i] = pwd[i % pwd.Length];
                box[i] = i;
            }
            for (j = i = 0; i < 256; i++) {
                j = (j + box[i] + key[i]) % 256;
                tmp = box[i];
                box[i] = box[j];
                box[j] = tmp;
            }
            for (a = j = i = 0; i < data.Length; i++) {
                a++;
                a %= 256;
                j += box[a];
                j %= 256;
                tmp = box[a];
                box[a] = box[j];
                box[j] = tmp;
                k = box[((box[a] + box[j]) % 256)];
                cipher[i] = (byte)(data[i] ^ k);
            }
            return cipher;
        }

        public static byte[] Decrypt(byte[] pwd, byte[] data) {
            return Encrypt(pwd, data);
        }


    }
}
