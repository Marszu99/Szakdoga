using NUnit.Framework;
using System;
using TimeSheet.Model.Extension;


namespace TimeSheet.Tests
{
    [TestFixture]
    public class RecordValidationTests
    {
        [Test]
        [TestCase(null)]
        public void ValidateDate_WhenDateIsNull_ReturnsErrorsString(DateTime? date)
        {
            DateTime TaskCreationDate = DateTime.Today.AddYears(-1);
            string result = RecordValidationHelper.ValidateDate(date, TaskCreationDate);

            Assert.That(result, Is.EqualTo("Date is empty!"));
        }

        [Test]
        public void ValidateDate_WhenDateIsSetToAFuturesDate_ReturnsErrorsString()
        {
            DateTime TaskCreationDate = DateTime.Today.AddYears(-1);

            string result = RecordValidationHelper.ValidateDate(DateTime.Today.AddDays(1), TaskCreationDate);

            Assert.That(result, Is.EqualTo("Record's date can't be in the future!"));
        }

        [Test]
        public void ValidateDate_WhenDateIsSetToADateThatIsBeforeTheTaskHasBeenCreated_ReturnsErrorsString()
        {
            DateTime TaskCreationDate = DateTime.Today.AddYears(1);

            string result = RecordValidationHelper.ValidateDate(DateTime.Today, TaskCreationDate);

            Assert.That(result, Is.EqualTo("Record's date can't be earlier than the date when the task was created!"));
        }

        [Test]
        public void ValidateDate_WhenDateIsValid_ReturnsNull()
        {
            DateTime TaskCreationDate = DateTime.Today.AddYears(-1);

            string result = RecordValidationHelper.ValidateDate(DateTime.Today, TaskCreationDate);

            Assert.That(result, Is.EqualTo(null));
        }

        [Test]
        [TestCase(-11)]
        public void ValidateDuration_WhenDurationIsNegative_ReturnsErrorsMessage(int duration)
        {
            string result = RecordValidationHelper.ValidateDuration(duration);

            Assert.That(result, Is.EqualTo("Duration can't be lower than 0!"));
        }

        [Test]
        [TestCase(11)]
        public void ValidateDuration_WhenDurationIsPositive_ReturnsNull(int duration)
        {
            string result = RecordValidationHelper.ValidateDuration(duration);

            Assert.That(result, Is.EqualTo(null));
        }

        [Test]
        [TestCase(721)]
        public void ValidateDuration_WhenDurationIsHigherThan720Min_ReturnsErrorsString(int duration)
        {
            string result = RecordValidationHelper.ValidateDuration(duration);

            Assert.That(result, Is.EqualTo("Duration can't be higher than 12 hours!"));
        }

        [Test]
        [TestCase(60)]
        public void ValidateDuration_WhenDurationIsValid_ReturnsNull(int duration)
        {
            string result = RecordValidationHelper.ValidateDuration(duration);

            Assert.That(result, Is.EqualTo(null));
        }
    }
}
