using NUnit.Framework;
using System;
using TimeSheet.DataAccess;
using TimeSheet.Model;
using TimeSheet.Model.Extension;
using TimeSheet.Resource;


namespace TimeSheet.Tests
{
    [TestFixture]
    public class RecordValidationTests
    {
        [Test]
        [TestCase(null)]
        public void ValidateTask_WhenTaskIsNotChosenForRecord_ReturnsErrorsString(Task task)
        {
            string result = RecordValidationHelper.ValidateTask(task);

            Assert.That(result, Is.EqualTo(Resources.NeedToChooseTaskForRecord));
        }

        /*[Test]
        [TestCase(new TaskLogic().GetTaskByID(1))]
        public void ValidateTask_WhenTaskIsNotChosenForRecord_ReturnsNull(Task task)
        {
            string result = RecordValidationHelper.ValidateTask(task);

            Assert.That(result, Is.EqualTo(null));
        }*/

        [Test]
        [TestCase(null)]
        public void ValidateDate_WhenDateIsNull_ReturnsErrorsString(DateTime? date)
        {
            DateTime TaskCreationDate = DateTime.Today.AddYears(-1);
            string result = RecordValidationHelper.ValidateDate(date, TaskCreationDate);

            Assert.That(result, Is.EqualTo(Resources.DateIsEmpty));
        }

        [Test]
        public void ValidateDate_WhenDateIsSetToAFuturesDate_ReturnsErrorsString()
        {
            DateTime TaskCreationDate = DateTime.Today.AddYears(-1);

            string result = RecordValidationHelper.ValidateDate(DateTime.Today.AddDays(1), TaskCreationDate);

            Assert.That(result, Is.EqualTo(Resources.DateCantBeInFuture));
        }

        [Test]
        public void ValidateDate_WhenDateIsSetToADateThatIsBeforeTheTaskHasBeenCreated_ReturnsErrorsString()
        {
            DateTime TaskCreationDate = DateTime.Today.AddYears(1);

            string result = RecordValidationHelper.ValidateDate(DateTime.Today, TaskCreationDate);

            Assert.That(result, Is.EqualTo(Resources.DateCantBePastTheTaskCreationDate));
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

            Assert.That(result, Is.EqualTo(Resources.DurationCantBeLowerZero));
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

            Assert.That(result, Is.EqualTo(Resources.DurationCantBeHigher12Hours));
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
