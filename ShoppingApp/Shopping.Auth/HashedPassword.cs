using System;

namespace Utility.Authentication
{
    public class HashedPassword
    {
        public byte[] Salt { get; }
        public byte[] Hash { get; }

        public HashedPassword(byte[] salt, byte[] hash)
        {
            Salt = salt ?? throw new ArgumentNullException(nameof(salt));
            Hash = hash ?? throw new ArgumentNullException(nameof(hash));
        }
    }
}
