using System;
using System.Text;

namespace rc4
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            string str = "Hello World faruk";
            //text from wikipedia
            str = "Wired Equivalent Privacy (WEP) is a security algorithm for IEEE 802.11 wireless networks. Introduced as part of the original 802.11 standard ratified in 1997, its intention was to provide data confidentiality comparable to that of a traditional wired network.[1] WEP, recognizable by its key of 10 or 26 hexadecimal digits (40 or 104 bits), was at one time widely in use and was often the first security choice presented to users by router configuration tools";
            string key = "key!!'!'^";
            //test with a 
            byte[] res = Rc4(Encoding.ASCII.GetBytes(str),Encoding.ASCII.GetBytes(key));
            string result = Encoding.ASCII.GetString(res);
            Console.WriteLine(result);
            Console.WriteLine("-----");

            /*
                ???8T??sh?mB???
                ???,???MC??[?NDK???\K??b?IK???-r{$MT??I??3?`?Yf?KgJT???`??\?NY???R???????K?e@/G????O?????
                ?y&(p???/?w??-?l"?[?(?????i!B[/"??J???zJ??*O??Xb??Y??I?t??i8?_-?p3??{?G*)s?%?ugf`?f*???c???"\]%;!c2R?Cw??g???a???aH}	??QQ?3j?;N???????VM?*?wL????~???-.?}w??o???z?c??.m
                ??}K???YT???????i3???s<?S?????t??v??Zwp,t?:3R??g??pN??X???-????????
                ;O??*"P!?????S2?is???~????|?~rA?f???D????Z?`?oFy?x|?>?? ????bv??3_
            */


            //decrypt
            byte[] decRes = Rc4(res,Encoding.ASCII.GetBytes(key));
            string decResult = Encoding.ASCII.GetString(decRes);
            Console.WriteLine(decResult);

            /*
                    Wired Equivalent Privacy (WEP) is a security algorithm for IEEE 802.11 wireless networks. Introduced as part of the original 802.11 standard ratified in 1997, its intention was to provide data confidentiality comparable to that of a traditional wired network.[1] WEP, recognizable by its key of 10 or 26 hexadecimal digits (40 or 104 bits), was at one time widely in use and was often the first security choice presented to users by router configuration tools
            */

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


        //Rc4 method is used for both encrytion and decryption
        //if the inputs are plaintext + key => result equals cipher
        //if the inputs are cipher + key = > result equals plaintext
        public static byte[] Rc4(byte[] text, byte[] key)
        {
            int i =0;
            int j= 0;
            int temp =0 ;
            byte[] result = new byte[text.Length];

            //initialize arrays
            //Step 1
            int[] keyArr = new int[256];
            int[] S = new int[256];

            for(i=0; i< 256;i++)
            {
                S[i]= i;
                keyArr[i] = key[i % key.Length];
            }
            //Step2 KSA = Key Scheduling Algorithm
            j=0;
            for (i = 0; i < 256; i++) 
            {
                j = (j + S[i] + keyArr[i]) % 256;
                //swapping
                temp = S[i];
                S[i] = S[j];
                S[j] = temp;
            }   

            //Step3 PRGA = Pseudo Random Generation Algorithm
            int k = 0;
            i=0;
            j=0;
            for (int x =0 ;x < text.Length; x++)
             {    
                i = (i +1) % 256;
                j = (j + S[i]) % 256;
                //swapping
                temp = S[i];
                S[i] = S[j];
                S[j] = temp;

                k = S[((S[i] + S[j]) % 256)];
                //XOR operation
                result[x] = (byte)(text[x] ^ k);
            }

            return result;

        } 

    }
}
