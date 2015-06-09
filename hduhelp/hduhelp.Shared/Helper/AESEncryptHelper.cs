using System;
using System.Collections.Generic;
using System.Text;
using System.Security;
using Windows.System.Profile;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage.Streams;

namespace hduhelp.Helper
{
    public class AesEcryptHelper
    {
        private string _msg = null;
        public AesEcryptHelper(string msg)
        {
            this._msg = msg;
        }
        public string Encrypt()
        {
            var msgObj = SymmetricKeyAlgorithmProvider.OpenAlgorithm(SymmetricAlgorithmNames.AesCbcPkcs7);
            var buffMsg = CryptographicBuffer.ConvertStringToBinary(_msg, BinaryStringEncoding.Utf8);
            var iv = CryptographicBuffer.CreateFromByteArray(new byte[16]);
            var key = msgObj.CreateSymmetricKey(this.Sha256("U1MjU1M0FDOUZ.Qz"));
            return CryptographicBuffer.EncodeToBase64String(CryptographicEngine.Encrypt(key, buffMsg, iv)) + "\n";
        }

        private IBuffer Sha256(string key)
        {
            var buffKey = CryptographicBuffer.ConvertStringToBinary(key, BinaryStringEncoding.Utf8);
            return HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Sha256).HashData(buffKey);
        }
    }
}
