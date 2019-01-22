using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Webdriver.Tests
{
    using NUnit.Framework;

    using Training.Webdriver.Common;

    [TestFixture]
    public class SecondCheckBoxTest : TestBase
    {
        private const int idCheckbox = 0;

        [Test]
        public void TestSecondCheckbox()
        {
            var mainPage = new MainPage(this.Driver)
                .GoToCheckboxPage()
                .SelectCheckBOx(idCheckbox, out var previous)
                .CheckIfCheckboxChangedStatusAfterClick(idCheckbox, previous);
        }
    }
}
