using Xunit;

namespace CursoOnline.DominioTest
{
    public class UnitTest1
    {
        [Fact(DisplayName = "Test2")]
        public void Test1()
        {
            var variavel1 = 1;
            var variavel2 = 2;

            Assert.NotEqual(variavel1, variavel2);
        }
    }
}