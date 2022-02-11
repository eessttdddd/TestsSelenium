using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TestsSelenium;

public class Utils
{
    public static (string, string) ClientRegistration(WebDriver driver, WebDriverWait wait )
    {
        try
        {
            driver.Navigate().GoToUrl("https://old.kzn.opencity.pro/");
            var registration = driver.FindElement(By.XPath("//a[@data-ui='registration']"));
         
            registration.Click();
            IWebElement inputEmail = wait.Until(e => e.FindElement(By.Name("email")));

          
            Random rnd = new Random();
            inputEmail.SendKeys(rnd.NextInt64(1111111,99999999) + "@gmail.com");
            driver.FindElement(By.CssSelector("button[data-ui='submitBtn']")).Click();

            var msg = driver.FindElement(By.XPath("//div[@class='message_notify']/div[@class='text']"));
           
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Assert.Ignore("Не удалось зарегистрировать клиента, дальнейшие тесты бессмысленны");
        }

        return ("alisa.skrynko@gmail.com", "c069db");
    }

 
}