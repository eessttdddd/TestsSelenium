using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace TestsSelenium
{
    public class Tests
    {
        private WebDriver driver;
        private WebDriverWait wait;
        private string login, password;
        private bool isAuth = false;
        const string Url = "https://old.kzn.opencity.pro/";
        
        [OneTimeSetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
             wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
             (login, password) = Utils.ClientRegistration(driver, wait);
            
        }

        [OneTimeTearDown]
        public void TearDown()
        {
           
            driver.Quit();
        }

        [Test]
        public void  CheckAuthTesting()
        {
            Authorization(Url);
            Assert.AreEqual("https://old.kzn.opencity.pro/cabinet/", driver.Url, "Не перешли в личный кабинет");
            
        }
        [Test]
        public void CheckMyProfileTesting()
        {
            if (!isAuth) Authorization(Url);
            driver.FindElement(By.CssSelector("a[class='btn_edit_profile itemMenu'")).Click();
            IWebElement lastname = wait.Until(e => e.FindElement(By.CssSelector("input[data-ui='lastname']")));
            lastname.Clear();
            lastname.SendKeys("Botnikova");
            IWebElement name = wait.Until(e => e.FindElement(By.CssSelector("input[data-ui='name']")));
            name.Clear();
            name.SendKeys("Sonya");
            IWebElement patronymic = wait.Until(e => e.FindElement(By.CssSelector("input[data-ui='patronymic']")));
            patronymic.Clear();
            patronymic.SendKeys("Sergeevna");
            IWebElement phone = wait.Until(e => e.FindElement(By.CssSelector("input[data-ui='phone']")));
            phone.Clear();
            phone.SendKeys("3958736983"); 
            IWebElement submit = wait.Until(e => e.FindElement(By.CssSelector("button.inputSubmit")));
            submit.Click();
      
            
            Assert.AreEqual("https://old.kzn.opencity.pro/cabinet/myprofile", driver.Url, "Не открылась страница редактирования профиля");
          
        }

       
        private void Authorization(string url)
        {
            driver.Navigate().GoToUrl(url);
            driver.Manage().Window.Maximize();
            var auth = driver.FindElement(By.XPath("//a[@data-ui='auth']"));
            auth.Click();
            IWebElement inputEmail = wait.Until(e => e.FindElement(By.Name("username")));
            inputEmail.SendKeys(login);
            driver.FindElement(By.Name("password")).SendKeys(password);
            driver.FindElement(By.CssSelector("button.inputSubmit")).Click();
        }
        
     
    }
}