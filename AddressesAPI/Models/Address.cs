using System;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;


namespace AddressesAPI.Models
{
    public class Address
    {
        
        private string pathFile = "addresses.txt";
        public bool VerificationNewAddress(string newAddress)
        {
            string stringVerification = @"\d{6},\s{1}\w+\s{1}\w+,\s{1}\w+\s{1}\w+,\s{1}город\s{1}\w+,\s{1}\d+";
            if (Regex.IsMatch(newAddress, stringVerification, RegexOptions.IgnoreCase))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void AddAddress(string newAddress)
        {
            newAddress = "\n" + newAddress;
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);//Add all text encodings 
            System.IO.File.AppendAllText(pathFile, newAddress, Encoding.GetEncoding(1251));
        }

        public List<string> Search(string searchString)
        {
            List<string> addresses = new List<string>();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);//Add all text encodings 
            using (FileStream fileWithAddresses = System.IO.File.Open(pathFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (BufferedStream buffer = new BufferedStream(fileWithAddresses))
            using (StreamReader fileLine = new StreamReader(buffer, Encoding.GetEncoding(1251)))
            {
                string line;
                while ((line = fileLine.ReadLine()) != null)
                {
                    if (line.Contains(searchString))
                    {
                        addresses.Add(line);
                    }
                }
            }
            return addresses;
        }
        
    }
}
