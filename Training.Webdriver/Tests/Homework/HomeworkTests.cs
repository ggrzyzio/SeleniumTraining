using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Webdriver.Tests.Homework
{
    using System.Threading;

    using AutoFixture;

    using NUnit.Framework;
    using NUnit.Framework.Internal;

    using Training.Webdriver.Common;
    using Training.Webdriver.Models;
    using Training.Webdriver.Pages.AutomationPractices;

    [TestFixture]
    public class HomeworkTests : TestBase
    {
        private static readonly Fixture Fixture = new Fixture();

        [Test]
        public void CreateUser()
        {
            var userBefore = Fixture.Build<User>()
                .With(x => x.Email, Helper.GenerateEmail())
                .With(x => x.FirsName, Helper.GenerateStringNoDigits())
                .With(x => x.LastName, Helper.GenerateStringNoDigits())
                .With(x => x.City, Helper.GenerateStringNoDigits())
                .With(x=>x.PostCode, Helper.GeneratePostCode())
                .Create();

            var mainPage = new MainPage(this.Driver);

            mainPage.GoToMainPage()
                .ClickSignInButton()
                .FillEmailInputToCreateAccountAndSubmit(userBefore.Email)
                .FillUserData(userBefore)
                .ClickLogout()
                .LogInWithCredentials(userBefore.Email, userBefore.Password)
                .CheckUserLoggedName($"{userBefore.FirsName} {userBefore.LastName}");
                
            Thread.Sleep(10000);
        }
    }
}
