using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace DataLibrary
{
    public class Hashing
    {
        public static void Main(String[] args)
        {
            Console.WriteLine("Enter a string to hash:");
            string input = Console.ReadLine();
            Console.WriteLine("SHA256 hash of " + input + " is:");
            Console.WriteLine(Hash(input));
        }
        public static String Hash(String password)
        {
           
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                 // Convert byte array to a string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i<bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                    return builder.ToString();
            }
        }
    }
}
