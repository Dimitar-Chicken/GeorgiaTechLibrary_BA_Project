using GTL_Application.Interfaces;
using GTL_Application.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Security;
using System.Text;
using Xunit;

namespace GTL_Test
{
    [ExcludeFromCodeCoverage]
    public class DataAccessTests
    {
        readonly DataAccess dataAccess;
        public DataAccessTests()
        {
            dataAccess = new DataAccess();
        }

        [Theory]
        [InlineData("test")]
        [InlineData("222")]
        [InlineData("---")]
        public void TC009_DataAccess_ConvertToSecureString_Passes(string input)
        {
            Assert.IsType<SecureString>(dataAccess.ConvertToSecureString(input));
        }

        [Fact]
        public void TC010_DataAccess_ConvertSecureStringToString_Passes()
        {
            SecureString input = new NetworkCredential("", "51").SecurePassword;
            Assert.IsType<string>(DataAccess.ConvertSecureStringToString(input));
        }
    }
}
