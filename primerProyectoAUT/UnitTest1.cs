using OpenQA.Selenium.Firefox;

namespace primerProyectoAUT
{
    public class Tests
    {
        private FirefoxDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new FirefoxDriver();
        }

        [Test]
        public void Test1()
        {
            driver.Navigate().GoToUrl("https://www.google.com/");


            Assert.That(driver.Title, Does.Contain("Google"));
        }

        [TearDown] 
        public void TearDown() 
        { 
            driver?.Quit();
            driver?.Dispose();
        }
    }
}
