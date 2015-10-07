using NUnit.Framework;

namespace FluentInterface.Examples.Tests
{
    [TestFixture]
    public class StringParserUsingLinqTests
    {
        [Test]
        public void ParseString_WithSpacedString_ReturnsParsedString()
        {
            //ARRANGE
            const string str = "  B a l t i c - A m a d e u s  ";

            //ACT
            //ASSERT
            Assert.That(StringParserUsingLinq.ParseString(str), Is.EqualTo("BALTICAMADEUS"));
        }

        [Test]
        public void ParseStringNonFluent_WithSpacedString_ReturnsParsedString()
        {
            //ARRANGE
            const string str = "  B a l t i c - A m a d e u s  ";

            //ACT
            //ASSERT
            Assert.That(StringParserUsingLinq.ParseStringNonFluent(str), Is.EqualTo("BALTICAMADEUS"));
        }
    }
}