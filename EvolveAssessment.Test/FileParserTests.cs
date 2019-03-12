using Assessment.Web.Infrastructure;
using System;
using Xunit;

namespace EvolveAssessment.Test
{
    public class FileParserTests
    {
        [Fact]
        public void GivenFormulaOne_the_result_is_correct()
        {
            var sut = new FileParser();
            var line = "A x B / C;1;4;2";
            var result = sut.ProcessLine(line);
            Assert.True(result.Result == 2);
        }
        [Fact]
        public void GivenFormulaTwo_the_result_is_correct()
        {
            var sut = new FileParser();
            var line = "A mod B x C;20;4;2";
            var result = sut.ProcessLine(line);
            Assert.True(result.Result == 4);
        }
        [Fact]
        public void GivenFormulaThree_the_result_is_correct()
        {
            var sut = new FileParser();
            var line = "A^C - √B x C;2;16;2";
            var result = sut.ProcessLine(line);
            Assert.True(result.Result == -4);
        }
        [Fact]
        
        public void GivenInvalidFormula_Exception_is_thrown()
        {
            var sut = new FileParser();
            var line = "A - √B x C;2;16;2";
            Assert.Throws<FormatException>(()=> sut.ProcessLine(line));
        }

    }
}
