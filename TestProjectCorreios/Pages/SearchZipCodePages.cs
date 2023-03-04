using System.Text.RegularExpressions;
using NUnit.Framework;
using OpenQA.Selenium;

namespace TestProjectCorreios.Pages
{
    public class SearchZipCodePages
    {
        private readonly IWebDriver Driver;

        public SearchZipCodePages(IWebDriver driver)
        {
            Driver = driver;
        }

        public void AccessCepSearch()
        {
            IWebElement pageTitle = Driver.FindElement(By.CssSelector("#titulo_tela > h2"));
            var pageTitleText = pageTitle.Text;
            Assert.AreEqual("Busca CEP", pageTitleText, "Valor atual diferente do valor esperado");
        }

        public void InputZipCode(string zipCode)
        {
            IWebElement inputZipCode = Driver.FindElement(By.Id("endereco"));
            IWebElement btnSearch = Driver.FindElement(By.ClassName("botoes"));

            inputZipCode.SendKeys(zipCode);
            btnSearch.Click();
        }

        public void DataReturn(string street)
        {
            if (street == "Dados não encontrado")
            {
                IWebElement result = Driver.FindElement(By.XPath("//*[@id='mensagem-resultado-alerta']/h6"));
                var actualStreet = result.Text;
                Assert.AreEqual(street, actualStreet);
            }
            else if (street == "Rua Quinze de Novembro, São Paulo/SP")
            {
                IWebElement resultStreet = Driver.FindElement(By.CssSelector("#resultado-DNEC td:nth-child(1)"));
                var nameStreet = resultStreet.Text;
                var padrao = @"^([\w\s]+) -.*$";
                nameStreet = Regex.Match(nameStreet, padrao).Groups[1].Value.Trim();

                IWebElement resultPlace = Driver.FindElement(By.CssSelector("#resultado-DNEC td:nth-child(3)"));
                var namePlace = resultPlace.Text;
                var fullAddress = string.Concat(nameStreet + ", " + namePlace);
                Assert.AreEqual(street, fullAddress);
            }

            IWebElement btnNewSearch = Driver.FindElement(By.Id("btn_nbusca"));
            btnNewSearch.Click();
        }

        public void SearcOrder(string order)
        {
            IWebElement trackObject = Driver.FindElement(By.CssSelector(".campo #objetos"));
            trackObject.SendKeys(order);
                
            IWebElement btnFindOrder = Driver.FindElement(By.XPath("//*[@id='content']/div[3]/div/div[2]/div[1]/form/div[2]/button/i"));
            btnFindOrder.Click();
        }
    }
}