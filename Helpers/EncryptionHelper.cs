using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    public class EncryptionHelper
    {
        public static string EncryptPassword(string password)
        {
            using (DESCryptoServiceProvider _Des = new DESCryptoServiceProvider())
            {
                byte[] _StringToBeEncrypted = Encoding.UTF8.GetBytes(password);
                byte[] _Key = Encoding.UTF8.GetBytes("37908865-F1F1-43CF-AC2A-DF6B849AEB2D");
                byte[] _IV = Encoding.UTF8.GetBytes("FDDB62A8-BA35-4EEC-8797-2712331D1195");
                _Des.Key = _Key;
                _Des.IV = _IV;

                using (MemoryStream _MS = new MemoryStream())
                {
                    using (CryptoStream _CS = new CryptoStream(_MS, _Des.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        _CS.Write(_StringToBeEncrypted, 0, _StringToBeEncrypted.Length);
                        _CS.FlushFinalBlock();
                        return Convert.ToBase64String(_MS.ToArray());
                    }
                }
            }
        }
    }
}
