using NUnit.Framework;
using System;
using TimeSheet.Model.Extension;
using TimeSheet.Resource;


namespace TimeSheet.Tests
{
    [TestFixture]
    public class TaskValidationTests
    {
        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("     ")]
        public void ValidateTitle_WhenTitleIsNullOrWhiteSpace_ReturnsErrorsString(string title)
        {
            string result = TaskValidationHelper.ValidateTitle(title);

            Assert.That(result, Is.EqualTo(Resources.TitleIsEmpty));
        }

        [Test]
        [TestCase("jdkajsdakjdkajdksajdlkjaskjdakdjaklsjdakldjasdjsakdjaskjdakjsdkajdskajdkasjdkasjdkasjdskajdkas")]
        public void ValidateTitle_WhenTitleIsGreaterThan45Characters_ReturnsErrorsString(string title)
        {
            string result = TaskValidationHelper.ValidateTitle(title);

            Assert.That(result, Is.EqualTo(Resources.TitleWrongLength));
        }

        [Test]
        [TestCase("Programozás")]
        public void ValidateTitle_WhenTitleIsValid_ReturnsNull(string title)
        {
            string result = TaskValidationHelper.ValidateTitle(title);

            Assert.That(result, Is.EqualTo(null));
        }

        [Test]
        [TestCase(null)]
        public void ValidateDeadline_WhenDeadlineIsNull_ReturnsErrorsString(DateTime? deadline)
        {
            string result = TaskValidationHelper.ValidateDeadline(deadline);

            Assert.That(result, Is.EqualTo(Resources.DeadlineIsEmpty));
        }

        [Test]
        public void ValidateDeadline_WhenDeadlineIsSetForPastTodaysDate_ReturnsErrorsString()
        {
            string result = TaskValidationHelper.ValidateDeadline(DateTime.Today.AddDays(-14));

            Assert.That(result, Is.EqualTo(Resources.DeadlineMoreThan24Hours));
        }

        [Test]
        public void ValidateDeadline_WhenDeadlineIsodaysDate_ReturnsErrorsString()
        {
            string result = TaskValidationHelper.ValidateDeadline(DateTime.Today);

            Assert.That(result, Is.EqualTo(Resources.DeadlineMoreThan24Hours));
        }

        [Test]
        public void ValidateDeadline_WhenDeadlineIsSetForAFutureDate_ReturnsNull()
        {
            string result = TaskValidationHelper.ValidateDeadline(DateTime.Today.AddDays(14));

            Assert.That(result, Is.EqualTo(null));
        }
    }
}