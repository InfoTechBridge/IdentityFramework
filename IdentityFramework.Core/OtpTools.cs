using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace IdentityFramework.Core
{
    public enum OtpAlgorithmEnum
    {
        Md5 = 1,
        Sha1 = 2,
        Des = 3

    }

    public enum OtpValueTypeEnum
    {
        Raw = 1,
        HexaDecimal = 2,
        Numerical = 3

    }

    internal class OtpTools
    {
        private OtpTools()
        {
        }

        public static string GenerateChallenge()
        {
            return ByteArrayToHexString(GenRandom(10));
        }

        public static string GenerateChallenge(int charCount)
        {
            int byteCount = (int)Math.Ceiling((decimal)charCount / 2);
            return ByteArrayToHexString(GenRandom(byteCount));
        }

        public static string GenerateChallenge(bool numerical)
        {
            if (numerical)
                return GenRandomNumber();
            else
                return GenerateChallenge();
        }

        public static string GenerateChallenge(bool numerical, int length)
        {
            if (numerical)
                return GenRandomNumber(length);
            else
                return GenerateChallenge(length);
        }

        public static byte[] GenerateOtp(string appKey, string userKey, string challenge, OtpAlgorithmEnum AuthAlgorithm)
        {
            byte[] uKey = Encoding.UTF8.GetBytes(userKey);
            byte[] aKey = Encoding.UTF8.GetBytes(appKey);
            byte[] chall = Encoding.UTF8.GetBytes(challenge);

            byte[] inBuf = new byte[uKey.Length + aKey.Length + chall.Length];

            int j = 0;
            for (int i = 0; i < uKey.Length; i++)
                inBuf[j++] = uKey[i];

            for (int i = 0; i < aKey.Length; i++)
                inBuf[j++] = aKey[i];

            for (int i = 0; i < chall.Length; i++)
                inBuf[j++] = chall[i];

            byte[] hashedvalue = null;
            switch (AuthAlgorithm)
            {
                case OtpAlgorithmEnum.Md5:
                    System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
                    hashedvalue = md5.ComputeHash(inBuf);
                    break;
                case OtpAlgorithmEnum.Sha1:
                    System.Security.Cryptography.SHA1 sha1 = System.Security.Cryptography.SHA1.Create();
                    hashedvalue = sha1.ComputeHash(inBuf);
                    break;
                case OtpAlgorithmEnum.Des:
                    int lenAdj = inBuf.Length % 8;
                    if (lenAdj != 0)
                        lenAdj = 8 - lenAdj;
                    //int byteLen = 
                    byte[] temp = new byte[inBuf.Length + lenAdj];
                    Array.Copy(inBuf, temp, inBuf.Length);
                    for (int i = 0; i < lenAdj; i++)
                        temp[inBuf.Length + i] = 0x00;

                    hashedvalue = Encrypt3DES(new MemoryStream(temp), uKey, new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 }).ToArray();
                    break;
            }
            return hashedvalue;
        }
        public static byte[] Signe(string appKey, string userKey, string data, OtpAlgorithmEnum AuthAlgorithm)
        {
            byte[] uKey = Encoding.UTF8.GetBytes(userKey);
            byte[] aKey = Encoding.UTF8.GetBytes(appKey);
            byte[] chall = Encoding.UTF8.GetBytes(data);

            byte[] inBuf = new byte[aKey.Length + uKey.Length + chall.Length];

            int j = 0;
            for (int i = 0; i < uKey.Length; i++)
                inBuf[j++] = uKey[i];

            for (int i = 0; i < aKey.Length; i++)
                inBuf[j++] = aKey[i];

            for (int i = 0; i < chall.Length; i++)
                inBuf[j++] = chall[i];

            byte[] hashedvalue = null;
            switch (AuthAlgorithm)
            {
                case OtpAlgorithmEnum.Md5:
                    System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
                    hashedvalue = md5.ComputeHash(inBuf);
                    break;
                case OtpAlgorithmEnum.Sha1:
                    System.Security.Cryptography.SHA1 sha1 = System.Security.Cryptography.SHA1.Create();
                    hashedvalue = sha1.ComputeHash(inBuf);
                    break;
                case OtpAlgorithmEnum.Des:
                    break;
            }
            return hashedvalue;
        }

        public static byte[] GenRandom()
        {
            return GenRandom(20); //byte[64];
        }
        public static byte[] GenRandom(int length)
        {
            // Create a byte array to hold the random value.
            byte[] randomNumber = new byte[length]; //byte[64];

            // Fill the array with a random value.
            //RNGCryptoServiceProvider.Create().GetBytes(randomNumber);
            RNGCryptoServiceProvider.Create().GetNonZeroBytes(randomNumber);

            return randomNumber;
        }
        public static string GenRandomNumber()
        {
            return GenRandomNumber(10);
        }
        public static string GenRandomNumber(int digitCount)
        {
            //int byteCount = (int)Math.Ceiling((decimal)digitCount / 3) + 1;
            int byteCount = digitCount;
            byte[] randomNumber = GenRandom(byteCount);

            string ret = ByteArrayToDecimalString(randomNumber);
            if (ret.Length > digitCount)
                return ret.Substring(0, digitCount);
            else
                return ret;
        }
        public static void GenRandomNumber(out int result)
        {
            const int byteCount = sizeof(int);
            byte[] randomNumber = GenRandom(byteCount);

            // Convert the byte to an integer value to make the modulus operation easier.
            result = BitConverter.ToInt32(randomNumber, 0);
        }
        public static void GenRandomNumber(out long result)
        {
            const int byteCount = sizeof(long);
            byte[] randomNumber = GenRandom(byteCount);

            // Convert the byte to an integer value to make the modulus operation easier.
            result = BitConverter.ToInt64(randomNumber, 0);
        }
        public static string GetOtpTime()
        {

            DateTime date = DateTime.Now;
            /*
            if (date.Second < 30)
                date = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, 00, 00);
            else
                date = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, 30, 00);
            return date.ToString("yyyyMMddHHmmssff");
            */

            return date.ToString("yyyyMMddHHmm0000");
            //return date.ToString("0000000000000000"); // GSM Simulation returns this value

        }
        public static short FindAlgMaxLenght(OtpAlgorithmEnum alg, OtpValueTypeEnum valueType)
        {
            short length = (short)0;
            switch (alg)
            {
                case OtpAlgorithmEnum.Md5:
                    length = (short)16;
                    break;
                case OtpAlgorithmEnum.Sha1:
                    length = (short)20;
                    break;
                case OtpAlgorithmEnum.Des:
                    length = (short)8;
                    break;
            }
            switch (valueType)
            {
                case OtpValueTypeEnum.Raw:
                    break;
                case OtpValueTypeEnum.HexaDecimal:
                    length *= (short)2;
                    break;
                case OtpValueTypeEnum.Numerical:
                    length *= (short)3;
                    break;
            }
            return length;
        }

        public static string ByteArrayToHexString(byte[] arr)
        {
            return BitConverter.ToString(arr).Replace("-", "");
        }
        public static string ByteArrayToDecimalString(byte[] arr)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in arr)
                sb.Append(b);
            return sb.ToString();
        }
        public static byte[] HexStringToByteArray(string hex)
        {
            int r = hex.Length % 2;
            if (r != 0) // 
                hex = "0" + hex;

            int count = hex.Length / 2;
            byte[] b = new byte[count];
            for (int i = 0; i < count; i++)
                b[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);

            return b;
            //byte x = 0xab;
            //byte y = Convert.ToByte("ab", 16);
        }

        public static MemoryStream Encrypt3DES(Stream stream, byte[] key, byte[] IV)//TripleDES
        {
            stream.Position = 0;

            // Create a MemoryStream.
            MemoryStream mStream = new MemoryStream();

            // Create a CryptoStream using the MemoryStream 
            // and the passed key and initialization vector (IV).
            CryptoStream cStream = new CryptoStream(mStream,
                new TripleDESCryptoServiceProvider().CreateEncryptor(key, IV),
                CryptoStreamMode.Write);

            // Write the byte array to the crypto stream and flush it.
            CopyStream(stream, cStream);
            cStream.FlushFinalBlock();

            // Get an array of bytes from the 
            // MemoryStream that holds the 
            // encrypted data.
            //byte[] ret = mStream.ToArray();

            // Return the encrypted buffer.
            //return ret;
            stream.Position = 0;
            mStream.Position = 0;
            return mStream;
        }
        public static void CopyStream(Stream input, Stream output)
        {
            const int blockSize = 64 * 1024;    // 64k
            // copy data block by block
            byte[] buffer = new byte[blockSize];
            int bytesRead = input.Read(buffer, 0, blockSize);
            while (bytesRead > 0)
            {
                output.Write(buffer, 0, bytesRead);
                bytesRead = input.Read(buffer, 0, blockSize);
            }
        }
    }
}
