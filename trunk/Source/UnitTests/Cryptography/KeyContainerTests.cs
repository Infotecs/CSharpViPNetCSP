using System;
using System.Security.Cryptography;
using Infotecs.Cryptography;
using NUnit.Framework;

namespace UnitTests.Cryptography
{
    /// <summary>
    ///     Тесты на <see cref="KeyContainer" />.
    /// </summary>
    [TestFixture]
    public sealed class KeyContainerTests
    {
        private const string Container = @"DataStore\TestContainer";
        private const string ContainerPassword = "123123";

        /// <summary>
        ///     Подсчет хэша.
        /// </summary>
        [Test]
        public void ComputeHash()
        {
            byte[] data = GetRandomData();

            byte[] hash = KeyContainer.ComputeHash(data);

            const int HashLength = 32;
            Assert.AreEqual(HashLength, hash.Length);
        }

        /// <summary>
        ///     Создание ключевого контейнера.
        /// </summary>
        [Test, Ignore]
        public void CreateKeyContainer()
        {
            const string NewContainer = "Infotecs_FD80A40E-BC07-4D58-BB8E-4B6BE802CC34";
            KeyContainer.Create(NewContainer, KeyNumber.Signature);
        }

        /// <summary>
        ///     Проверка экспорта открытого ключа.
        /// </summary>
        [Test]
        public void ExportPublicKey()
        {
            using (KeyContainer keyContainer = KeyContainer.Open(Container, ContainerPassword))
            {
                byte[] key = keyContainer.ExportPublicKey();
                CollectionAssert.IsNotEmpty(key);
            }
        }

        /// <summary>
        ///     Проверка существования ключевого контейнера.
        /// </summary>
        [Test]
        public void Exist_KeyContainerAbsent_False()
        {
            bool exist = KeyContainer.Exist(Guid.NewGuid().ToString());
            Assert.IsFalse(exist);
        }

        /// <summary>
        ///     Проверка существования ключевого контейнера.
        /// </summary>
        [Test]
        public void Exist_KeyContainerExist_True()
        {
            bool exist = KeyContainer.Exist(Container);
            Assert.IsTrue(exist);
        }

        /// <summary>
        ///     Проеверка подписи хэша.
        /// </summary>
        [Test]
        public void SignHash()
        {
            byte[] data = GetRandomData();
            byte[] signature;
            byte[] hash = KeyContainer.ComputeHash(data);

            using (KeyContainer keyContainer = KeyContainer.Open(Container, ContainerPassword))
            {
                signature = keyContainer.SignHash(hash, KeyNumber.Signature);
            }

            byte[] publicKey = KeyContainer.ExportPublicKey(Container);
            bool result = KeyContainer.VerifySignature(signature, data, publicKey);

            Assert.IsTrue(result);
        }

        private static byte[] GetRandomData()
        {
            var data = new byte[10];
            new RNGCryptoServiceProvider().GetBytes(data);
            return data;
        }
    }
}
