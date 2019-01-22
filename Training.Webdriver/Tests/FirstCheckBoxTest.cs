using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Webdriver.Tests
{
    using NUnit.Framework;
    using NUnit.Framework.Internal;

    using Training.Webdriver.Common;

    [TestFixture]
    public class FirstCheckBoxTest : TestBase
    {
        private const int idCheckbox = 1;

        [Test]
        public void TestFirstCheckbox()
        {
            var mainPage = new MainPage(this.Driver)
                .GoToCheckboxPage()
                .SelectCheckBOx(idCheckbox, out var previous)
                .CheckIfCheckboxChangedStatusAfterClick(idCheckbox, previous);
        }
    }
}
