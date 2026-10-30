using OpenQA.Selenium;
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

            //driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
        }

        [Test]
        [Retry(2)]
        [TestCase("student", "Password123")]
        public void validarLoginExitoso(string username, string password)
        {
            driver.Navigate().GoToUrl("https://practicetestautomation.com/practice-test-login/");

            Assert.That(driver.Title, Does.Contain("Test Login | Practice Test Automation"));

            driver.FindElement(By.Id("username")).SendKeys(username);
            driver.FindElement(By.Id("password")).SendKeys(password);
            driver.FindElement(By.Id("submit")).Click();



            Assert.That(driver.Url, Does.Contain("https://practicetestautomation.com/practice-test-login/"));

            Assert.That(driver.FindElement(By.TagName("h1")).Text, Does.Contain("Logged In Successfully"));
            Assert.That(driver.FindElement(By.CssSelector("h1.post-title")).Text, Does.Contain("Logged In Successfully"));

            driver.FindElement(By.LinkText("Log out")).Click();
            Assert.That(driver.Url, Does.Contain("https://practicetestautomation.com/practice-test-login/"));
            Assert.That(driver.FindElement(By.TagName("h1")).Text, Does.Contain("Test Login | Practice Test Automation"));
        }

        [Test]
        [Retry(3)]
        [TestCase("students", "Password123")]
        [TestCase(" ", "Password123")]
        [Category("Smoke")]
        public void ValidarLoginErroneoUsuario(string username, string password)
        {
            driver.Navigate().GoToUrl("https://practicetestautomation.com/practice-test-login/");
            Assert.That(driver.Title, Does.Contain("Test Login | Practice Test Automation"));

            driver.FindElement(By.Id("username")).SendKeys(username);
            driver.FindElement(By.Id("password")).SendKeys(password);
            driver.FindElement(By.Id("submit")).Click();

            Assert.That(driver.FindElement(By.Id("error")).Text, Does.Contain("Your username is invalid!"));
            Assert.That(driver.FindElement(By.Id("error")).GetDomProperty("textContent"), Is.EqualTo("Your username is invalid!"));
        }

        [Test]
        [Retry(2)]
        [TestCase("student", "Password")]
        [TestCase("student", "321")]
        [TestCase("student", "wrongpassword")]
        [TestCase("student", "******")]
        [Category("Smoke")]
        public void ValidarLoginErroneo(string username, string password)
        {
            driver.Navigate().GoToUrl("https://practicetestautomation.com/practice-test-login/");
            Assert.That(driver.Title, Does.Contain("Test Login | Practice Test Automation"));

            driver.FindElement(By.Id("username")).SendKeys(username);
            driver.FindElement(By.Id("password")).SendKeys(password);
            driver.FindElement(By.Id("submit")).Click();

            Assert.That(driver.FindElement(By.Id("error")).Text, Does.Contain("Your password is invalid!"));
            Assert.That(driver.FindElement(By.Id("error")).GetDomProperty("textContent"), Is.EqualTo("Your password is invalid!"));
        }

        [TearDown] 
        public void TearDown() 
        { 
            driver?.Quit();
            driver?.Dispose();
        }
    }
}
