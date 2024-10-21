using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PriceChecker.Helpers;
using PriceChecker.Iterfaces;
using PriceChecker.Models;
using PriceCheckerTesting.TestHelpers;
using Xunit;

namespace PriceCheckerTesting.Tests
{
    public class MailSenderTests
    {

        [Fact]
        public void MailWasSent()
        {
            ISecretGetter secretGetter = new EnvironmentSecretGetter(new TestConsoleLogger());
            Config testConfg = new Config(new List<string> { "jankovdamian@gmail.com" }, secretGetter.GetPublicKey(),
                secretGetter.GetSecretKey(), Enumerable.Empty<ItemWebResource>());

            var sender = new MailSender(testConfg, new TestConsoleLogger());
            Assert.True(sender.SendMail("A test from testing module", "A test from testing module"));    
        }

        [Fact]
        public void MailWasNotSent()
        {
            ISecretGetter secretGetter = new TestSecretGetter();
            Config testConfg = new Config(new List<string> { "jankovdamian@gmail.com" }, secretGetter.GetPublicKey(),
                secretGetter.GetSecretKey(), Enumerable.Empty<ItemWebResource>());

            var sender = new MailSender(testConfg, new TestConsoleLogger());
            Assert.False(sender.SendMail("A test from testing module", "A test from testing module"));
        }
    }
}
