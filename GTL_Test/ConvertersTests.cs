using GTL_Application.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Xunit;

namespace GTL_Test
{
    [ExcludeFromCodeCoverage]
    public class ConvertersTests
    {
        readonly ButtonVisibilityConverter buttonVisibilityConverter;
        readonly MultibindConverter multibindConverter;
        public ConvertersTests()
        {
            buttonVisibilityConverter = new ButtonVisibilityConverter();
            multibindConverter = new MultibindConverter();
        }

        [Theory]
        [InlineData("MeMbeR")]
        [InlineData("member")]
        [InlineData("MEMBER")]
        public void TC016_ButtonVisibilityConverter_Visible_Passes(string input)
        {
            string result = (string) buttonVisibilityConverter.Convert(input, null, null, null);

            Assert.Equal("Visible", result);
        }

        [Theory]
        [InlineData("asdasd")]
        [InlineData("spaghetti")]
        [InlineData("00")]
        public void TC016_ButtonVisibilityConverter_Hidden_Passes(string input)
        {
            string result = (string)buttonVisibilityConverter.Convert(input, null, null, null);

            Assert.Equal("Hidden", result);
        }

        [Fact]
        public void TC017_MultibindConverter_Convert_Passes()
        {
            object[] input = new object[3] { 1, 2, 3};

            object[] result = (object[]) multibindConverter.Convert(input, null, null, null);

            Assert.Equal(input, result);
        }
    }
}
