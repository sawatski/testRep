using System;
using Xunit;
using IIG.PasswordHashingUtils;

namespace TestPasswordHashingUtils
{
    public class Test_PasswordHashingUtils
    {
        // General tests if library is working
        [Fact]
        public void Test_GetHash_Length()
        {
            string s1 = PasswordHasher.GetHash("123");
            Assert.True(s1.Length > 0);
        }

        [Fact]
        public void Test_GetHash_HashTypeNotNull()
        {
            string s1 = PasswordHasher.GetHash("123");
            Assert.NotNull(s1);
        }
        // end
        // Pairs testing no salt
        [Fact]
        public void Test_GetHash_SamePass()
        {
            string s1 = PasswordHasher.GetHash("You");
            string s2 = PasswordHasher.GetHash("You");
            Assert.Equal(s1, s2);
        }

        [Fact]
        public void Test_GetHash_NotSamePass()
        {
            string s1 = PasswordHasher.GetHash("123");
            string s2 = PasswordHasher.GetHash("321");
            Assert.NotEqual(s1, s2);
        }

        [Fact]
        public void Test_GetHash_HashHash()
        {
            string s1 = PasswordHasher.GetHash("123");
            string s2 = PasswordHasher.GetHash("123");
            string s3 = PasswordHasher.GetHash(s1);
            string s4 = PasswordHasher.GetHash(s2);
            Assert.NotEqual(s1, s3);
            Assert.NotEqual(s2, s4);
            Assert.Equal(s3, s4);
        }

        [Fact]
        public void Test_GetHash_LongInput()
        {
            string testS = "";
            for (int i = 0; i < 100000; i++)
            {
                testS += "1";
            }
            // testS = "1111111... + 100000(1)"
            string s1 = PasswordHasher.GetHash(testS);
            string s2 = PasswordHasher.GetHash(testS);
            Assert.Equal(s1, s2);
        }
        // end
        // Pairs testing with salt
        [Fact]
        public void Test_GetHash_SameSalt()
        {
            string s1 = PasswordHasher.GetHash("123", "hey");
            string s2 = PasswordHasher.GetHash("123", "hey");
            Assert.Equal(s1, s2);
        }

        [Fact]
        public void Test_GetHash_NotSameSalt()
        {
            string s1 = PasswordHasher.GetHash("123", "hi");
            string s2 = PasswordHasher.GetHash("123", "hey");
            Assert.NotEqual(s1, s2);
        }

        [Fact]
        public void Test_GetHash_SameSaltNotPass()
        {
            string s1 = PasswordHasher.GetHash("123", "hey");
            string s2 = PasswordHasher.GetHash("321", "hey");
            Assert.NotEqual(s1, s2);
        }

        [Fact]
        public void Test_GetHash_NotSameSaltNotPass()
        {
            string s1 = PasswordHasher.GetHash("123", "hi");
            string s2 = PasswordHasher.GetHash("321", "hey");
            Assert.NotEqual(s1, s2);
        }
        [Fact]
        public void Test_GetHash_NumberSalt()
        {
            string s1 = PasswordHasher.GetHash("Lol", "222");
            string s2 = PasswordHasher.GetHash("Lol", "232");
            Assert.NotEqual(s1, s2);
        }
        // end
        // Pairs testing cyryllic hashing
        [Fact]
        public void Test_GetHash_Cyryllic()
        {
            string s1 = PasswordHasher.GetHash("Человек");
            string s2 = PasswordHasher.GetHash("Человек");
            Assert.Equal(s1, s2);
        }

        [Fact]
        public void Test_GetHash_CyryllicO()
        {
            string s1 = PasswordHasher.GetHash("ЧелОвек");
            string s2 = PasswordHasher.GetHash("ЧелOвек");
            Assert.NotEqual(s1, s2);
        }

        [Fact]
        public void Test_GetHash_CyryllicSalt()
        {
            string s1 = PasswordHasher.GetHash("Человек", "ку");
            string s2 = PasswordHasher.GetHash("Человек", "куку");
            Assert.NotEqual(s1, s2);
        }
        // end
        // Pairs testing adlerMod32

        [Fact]
        public void Test_GetHash_AdlerMedium()
        {
            string s1 = PasswordHasher.GetHash("Me", "444", 10);
            string s2 = PasswordHasher.GetHash("Me", "444", 11);
            Assert.NotEqual(s1, s2);
        }

        [Fact]
        public void Test_GetHash_AdlerSmallAndBig()
        {
            string s1 = PasswordHasher.GetHash("Me", "444", 1);
            string s2 = PasswordHasher.GetHash("Me", "444", 101);
            Assert.NotEqual(s1, s2);
        }
        // end
        // Testing specific cases
        [Fact]
        public void Test_GetHash_HashNull()
        {
            string s1 = PasswordHasher.GetHash(null);
            string s2 = PasswordHasher.GetHash(null);
            string s3 = PasswordHasher.GetHash(null, null);
            string s4 = PasswordHasher.GetHash(null, null);
            string s5 = PasswordHasher.GetHash(null, null, null);
            string s6 = PasswordHasher.GetHash(null, null, null);
            string s7 = PasswordHasher.GetHash("123");
            string s8 = PasswordHasher.GetHash("123", null, null);
            Assert.Equal(s1, s2);
            Assert.Equal(s3, s4);
            Assert.Equal(s5, s6);
            Assert.Equal(s1, s3);
            Assert.Equal(s1, s5);
            Assert.Equal(s7, s8);
            Assert.NotNull(s7);
            Assert.Null(s1);
        }

        [Fact]
        public void Test_GetHash_EmptyString()
        {
            string empty = String.Empty;
            string s1 = PasswordHasher.GetHash(empty);
            string s2 = PasswordHasher.GetHash(empty);
            Assert.Equal(s1, s2);
            Assert.NotNull(s1);
        }
        // end
    }
}