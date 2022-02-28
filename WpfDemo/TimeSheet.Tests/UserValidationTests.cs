using NUnit.Framework;
using TimeSheet.Model.Extension;
using TimeSheet.Resource;


namespace TimeSheet.Tests
{
    [TestFixture]
    public class UserValidationTests
    {
        //[MethodName]_[Scenario]_[ExpectedBehaviour]
        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("      ")]
        public void ValidateUserName_WhenUsernameIsNullOrWhiteSpace_ReturnsErrorsString(string username)
        {
            string result = UserValidationHelper.ValidateUserName(username);

            Assert.That(result, Is.EqualTo(Resources.UsernameIsEmpty));
        }

        [Test]
        [TestCase("asd")]
        public void ValidateUserName_WhenUsernameIsLessThan6Characters_ReturnsErrorsString(string username)
        {
            string result = UserValidationHelper.ValidateUserName(username);

            Assert.That(result, Is.EqualTo(Resources.UsernameWrongLength));
        }

        [Test]
        [TestCase("jdkajsdakjdkajdksajdlkjaskjdakdjaklsjdakldjasdjsakdjaskjdakjsdkajdskajdkasjdkasjdkasjdskajdkasjdkasjdkasjdlkasjdklsajldkajsdljasldjaldjasldkajsdlkajldjasldjs")]
        public void ValidateUserName_WhenUsernameIsGreaterThan45Characters_ReturnsErrorsString(string username)
        {
            string result = UserValidationHelper.ValidateUserName(username);

            Assert.That(result, Is.EqualTo(Resources.UsernameWrongLength));
        }

        [Test]
        [TestCase("Marcell")]
        public void ValidateUserName_WhenUsernameIsValid_ReturnsNull(string username)
        {

            string result = UserValidationHelper.ValidateUserName(username);

            Assert.That(result, Is.EqualTo(null));
        }

        [Test]
        [TestCase("saddd 23s")]
        public void ValidateUserName_WhenUsernameIsNotOneWord_ReturnsErrorsString(string username)
        {
            string result = UserValidationHelper.ValidateUserName(username);

            Assert.That(result, Is.EqualTo(Resources.UsernameOneWord));
        }

        [Test]
        [TestCase("Marszu99")]
        public void ValidateUserName_WhenUsernameIsOneWord_ReturnsNull(string username)
        {
            string result = UserValidationHelper.ValidateUserName(username);

            Assert.That(result, Is.EqualTo(null));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("      ")]
        public void ValidatePassword__WhenPasswordIsNullOrWhiteSpace_ReturnsErrorsString(string password)
        {
            string result = UserValidationHelper.ValidatePassword(password);

            Assert.That(result, Is.EqualTo(Resources.PasswordIsEmpty));
        }

        [Test]
        [TestCase("as")]
        public void ValidatePassword_WhenPasswordIsLessThan6Characters_ReturnsErrorsString(string password)
        {
            string result = UserValidationHelper.ValidatePassword(password);

            Assert.That(result, Is.EqualTo(Resources.PasswordWrongLength));
        }

        [Test]
        [TestCase("jdkajsdakjdkajdksajdlkjaskjdakdjaklsjdakldjasdjsakdjaskjdakjsdkajdskajdkasjdkasjdkasjdskajdkasjdkasjdkasjdlkasjdklsajldkajsdljasldjaldjasldkajsdlkajldjasldjs")]
        public void ValidatePassword_WhenPasswordIsGreaterThan45Characters_ReturnsErrorsString(string password)
        {
            string result = UserValidationHelper.ValidatePassword(password);

            Assert.That(result, Is.EqualTo(Resources.PasswordWrongLength));
        }

        [Test]
        [TestCase("Marcell")]
        public void ValidateUserName_WhenPasswordIsValid_ReturnsNull(string password)
        {

            string result = UserValidationHelper.ValidatePassword(password);

            Assert.That(result, Is.EqualTo(null));
        }

        [Test]
        [TestCase("saddd 23s")]
        public void ValidatePassword_WhenIsNotOneWord_ReturnsErrorsString(string password)
        {
            string result = UserValidationHelper.ValidatePassword(password);

            Assert.That(result, Is.EqualTo(Resources.PasswordOneWord));
        }

        [Test]
        [TestCase("OneWord")]
        public void ValidatePassword_WhenPasswordIsOneWord_ReturnsNull(string password)
        {
            string result = UserValidationHelper.ValidatePassword(password);

            Assert.That(result, Is.EqualTo(null));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("      ")]
        public void ValidateFirstName_WhenFirstNameIsNullOrWhiteSpace_ReturnsErrorsString(string firstname)
        {
            string result = UserValidationHelper.ValidateFirstName(firstname);

            Assert.That(result, Is.EqualTo(Resources.FirstNameIsEmpty));
        }

        [Test]
        [TestCase("as")]
        public void ValidateFirstName_WhenFirstNameIsLessThan3Characters_ReturnsErrorsString(string firstname)
        {
            string result = UserValidationHelper.ValidateFirstName(firstname);

            Assert.That(result, Is.EqualTo(Resources.FirstNameWrongLength));
        }

        [Test]
        [TestCase("jdkajsdakjdkajdksajdlkjaskjdakdjaklsjdakldjasdjsakdjaskjdakjsdkajdskajdkasjdkasjdkasjdskajdkasjdkasjdkasjdlkasjdklsajldkajsdljasldjaldjasldkajsdlkajldjasldjs")]
        public void ValidateFirstName_WhenFirstNameIsGreaterThan45Characters_ReturnsErrorsString(string firstname)
        {
            string result = UserValidationHelper.ValidateFirstName(firstname);

            Assert.That(result, Is.EqualTo(Resources.FirstNameWrongLength));
        }

        [Test]
        [TestCase("Marcell")]
        public void ValidateFirstName_WhenFirstNameIsValid_ReturnsNull(string firstname)
        {
            string result = UserValidationHelper.ValidateFirstName(firstname);

            Assert.That(result, Is.EqualTo(null));
        }

        [Test]
        [TestCase("assdadd1")]
        [TestCase("assdad!d")]
        [TestCase("assdad?d")]
        [TestCase("assdad>d")]
        [TestCase("assdad d")]
        public void ValidateFirstName_WhenFirstNameDoesNotOnlyContainsLettersOfTheAlphabet_ReturnsErrorsString(string firstname)
        {
            string result = UserValidationHelper.ValidateFirstName(firstname);

            Assert.That(result, Is.EqualTo(Resources.FirstNameNoNumbers));
        }

        [Test]
        [TestCase("Cseh")]
        public void ValidateFirstName_WhenFirstNameDoesOnlyContainsLettersOfTheAlphabet_ReturnsNull(string firstname)
        {
            string result = UserValidationHelper.ValidateFirstName(firstname);

            Assert.That(result, Is.EqualTo(null));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("      ")]
        public void ValidateLastName_WhenLastNameIsNullOrWhiteSpace_ReturnsErrorsString(string lastname)
        {
            string result = UserValidationHelper.ValidateLastName(lastname);

            Assert.That(result, Is.EqualTo(Resources.LastNameIsEmpty));
        }

        [Test]
        [TestCase("as")]
        public void ValidateLastName_WhenLastNameIsLessThan3Characters_ReturnsErrorsString(string lastname)
        {
            string result = UserValidationHelper.ValidateLastName(lastname);

            Assert.That(result, Is.EqualTo(Resources.LastNameWrongLength));
        }

        [Test]
        [TestCase("jdkajsdakjdkajdksajdlkjaskjdakdjaklsjdakldjasdjsakdjaskjdakjsdkajdskajdkasjdkasjdkasjdskajdkasjdkasjdkasjdlkasjdklsajldkajsdljasldjaldjasldkajsdlkajldjasldjs")]
        public void ValidateLastName_WhenLastNameIsGreaterThan45Characters_ReturnsErrorsString(string lastname)
        {
            string result = UserValidationHelper.ValidateLastName(lastname);

            Assert.That(result, Is.EqualTo(Resources.LastNameWrongLength));
        }

        [Test]
        [TestCase("Marcell")]
        public void ValidateLastName_WhenLastNameIsValid_ReturnsNull(string lastname)
        {
            string result = UserValidationHelper.ValidateLastName(lastname);

            Assert.That(result, Is.EqualTo(null));
        }

        [Test]
        [TestCase("assdadd1")]
        [TestCase("assdad!d")]
        [TestCase("assdad?d")]
        [TestCase("assdad>d")]
        [TestCase("assdad d")]
        public void ValidateLastName_WhenLastNameDoesNotOnlyContainsLettersOfTheAlphabet_ReturnsErrorsString(string lastname)
        {
            string result = UserValidationHelper.ValidateLastName(lastname);

            Assert.That(result, Is.EqualTo(Resources.LastNameNoNumbers));
        }

        [Test]
        [TestCase("Marcell")]
        public void ValidateLastName_WhenLastNameDoesOnlyContainsLettersOfTheAlphabet_ReturnsNull(string lastname)
        {
            string result = UserValidationHelper.ValidateLastName(lastname);

            Assert.That(result, Is.EqualTo(null));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("      ")]
        public void ValidateEmail_WhenEmailIsNullOrWhiteSpace_ReturnsErrorsString(string email)
        {
            string result = UserValidationHelper.ValidateEmail(email);

            Assert.That(result, Is.EqualTo(Resources.EmailIsEmpty));
        }

        [Test]
        [TestCase("a@a.com")]
        public void ValidateEmail_WhenEmailIsLessThan11Characters_ReturnsErrorsString(string email)
        {
            string result = UserValidationHelper.ValidateEmail(email);

            Assert.That(result, Is.EqualTo(Resources.EmailWrongLength));
        }

        [Test]
        [TestCase("jdkajsdakjdkajdksajdlkjaskjdakdjaklsjdakldjasdjsakdjaskjda@kjsdkajdskajdkasjdkasjdkasjdskajdkasjdkasjdkasjdlkasjdklsajldkajsdljasldjaldjasldkajsdlkajldjasld.com")]
        public void ValidateEmail_WhenEmailIsGreaterThan100Characters_ReturnsErrorsString(string email)
        {
            string result = UserValidationHelper.ValidateEmail(email);

            Assert.That(result, Is.EqualTo(Resources.EmailWrongLength));
        }

        [Test]
        [TestCase("csehmarcell@yahoo.com")]
        public void ValidateEmail_WhenEmailIsValid_ReturnsNull(string email)
        {
            string result = UserValidationHelper.ValidateEmail(email);

            Assert.That(result, Is.EqualTo(null));
        }

        [Test]
        [TestCase("csehmarcellyahoo.com")]
        [TestCase("csehmarcellyahoo@.com")]
        [TestCase("csehmarc@ellyahoo")]
        [TestCase("@csehmarcellyahoo.com")]

        public void ValidateEmail_WhenEmailIsInvalid_ReturnsErrorsString(string email)
        {
            string result = UserValidationHelper.ValidateEmail(email);

            Assert.That(result, Is.EqualTo(Resources.EmailIsInvalid));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("      ")]
        public void ValidateTelephone_WhenTelephoneIsNullOrWhiteSpace_ReturnsErrorsString(string telephone)
        {
            string result = UserValidationHelper.ValidateTelephone(telephone);

            Assert.That(result, Is.EqualTo(Resources.TelephoneIsEmpty));
        }

        [Test]
        [TestCase("06707774980")]
        public void ValidateTelephone_WhenTelephoneIsValid_ReturnsNull(string telephone)
        {
            string result = UserValidationHelper.ValidateTelephone(telephone);

            Assert.That(result, Is.EqualTo(null));
        }

        [Test]
        [TestCase("06707774980!")]
        [TestCase("06707774980A")]
        [TestCase("0670777449a/")]
        [TestCase("06707774980$")]
        [TestCase("06707774980+")]

        public void ValidateTelephone_WhenTelephoneDoesNotOnlyContainsNumbers_ReturnsErrorsString(string telephone)
        {
            string result = UserValidationHelper.ValidateTelephone(telephone);

            Assert.That(result, Is.EqualTo(Resources.TelephoneIsInvalid));
        }

        [Test]
        [TestCase("06707774980")]
        public void ValidateTelephone_WhenTelephoneDoesOnlyContainsNumbers_ReturnsNull(string telephone)
        {
            string result = UserValidationHelper.ValidateTelephone(telephone);

            Assert.That(result, Is.EqualTo(null));
        }


    }
}