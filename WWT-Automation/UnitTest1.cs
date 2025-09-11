namespace WWT_Automation
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            TestContext.Progress.WriteLine("Setup method for test");
        }

        [Test]
        public void Test1()
        {
            TestContext.Progress.WriteLine("This is Test1");
            Assert.Pass();
        }
    }
}