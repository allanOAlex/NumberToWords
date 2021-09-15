using Exercice01.Core;
using System;
using System.Numerics;
using Xunit;

namespace Exercise01.Tests
{
	public class NumberToTextTests
	{
        private string testNumber;
        private string result = "";
        

        [Fact]
		public void Returns_Number_Description()
		{
            testNumber = "9902";
            result = testNumber.ToWords();
            Assert.Equal("Nine thousand , Nine hundred and Two", result);
        }


        [Theory]
        [InlineData("18456002032011000007", "Eighteen quintillion , Four hundred and Fifty Six quadrillion , Two trillion , Thirty Two billion , Eleven million and Seven")]
        [InlineData("100000450000000", "Hundred trillion , Four hundred and Fifty million")]
        [InlineData("1000000000", "One billion")]
        [InlineData("99", "Ninety Nine")]
        [InlineData("20500000", "Twenty million , Five hundred thousand")]
        public void ToWords(string number, string expected)
        {
            testNumber = number;
            result = testNumber.ToWords();
            Assert.Equal(expected, result);
        }


    }
}
