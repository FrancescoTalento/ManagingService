namespace CRUUDTest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            int input1 = 5;
            int input2 = 10;
            int expected = 15;

            MyMath mm = new MyMath();

            int actural = mm.Sum(input1,input2);

            Assert.Equal(expected, actural);

        }
    }
}