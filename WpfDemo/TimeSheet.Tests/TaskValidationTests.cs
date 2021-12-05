using NUnit.Framework;
using System;
using TimeSheet.Model.Extension;

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

            Assert.That(result, Is.EqualTo("Title is empty!"));
        }

        [Test]
        [TestCase("jdkajsdakjdkajdksajdlkjaskjdakdjaklsjdakldjasdjsakdjaskjdakjsdkajdskajdkasjdkasjdkasjdskajdkas")]
        public void ValidateTitle_WhenTitleIsGreaterThan45Characters_ReturnsErrorsString(string title)
        {
            string result = TaskValidationHelper.ValidateTitle(title);

            Assert.That(result, Is.EqualTo("Title can't be more than 45 characters!"));
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

            Assert.That(result, Is.EqualTo("Deadline is empty!"));
        }

        [Test]
        public void ValidateDeadline_WhenDeadlineIsSetForPastTodaysDate_ReturnsErrorsString()
        {
            string result = TaskValidationHelper.ValidateDeadline(DateTime.Today.AddDays(-14));

            Assert.That(result, Is.EqualTo("Deadline has to be atleast more than 24 hours away from the current time!"));
        }

        [Test]
        public void ValidateDeadline_WhenDeadlineIsodaysDate_ReturnsErrorsString()
        {
            string result = TaskValidationHelper.ValidateDeadline(DateTime.Today);

            Assert.That(result, Is.EqualTo("Deadline has to be atleast more than 24 hours away from the current time!"));
        }

        [Test]
        public void ValidateDeadline_WhenDeadlineIsSetForAFutureDate_ReturnsNull()
        {
            string result = TaskValidationHelper.ValidateDeadline(DateTime.Today.AddDays(14));

            Assert.That(result, Is.EqualTo(null));
        }
    }
}