using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace Folio_Liquidacion_API.Data.Security
{
    public class Encryption
    {
        public static string encryptionSHA1(string str)
        {
            ///Encripta el texto escrito en un hash SHA1
            SHA1CryptoServiceProvider oSHA1 = new SHA1CryptoServiceProvider();
            byte[] btCadena = System.Text.Encoding.UTF8.GetBytes(str.Trim());
            byte[] btResultado = oSHA1.ComputeHash(btCadena);
            oSHA1.Clear();
            return Convert.ToBase64String(btResultado).ToString().Trim();
        }
    }
}