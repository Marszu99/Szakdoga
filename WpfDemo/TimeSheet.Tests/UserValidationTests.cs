using NUnit.Framework;
using TimeSheet.Model.Extension;


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

            Assert.That(result, Is.EqualTo("Username is empty!"));
        }

        [Test]
        [TestCase("asd")]
        public void ValidateUserName_WhenUsernameIsLessThan6Characters_ReturnsErrorsString(string username)
        {
            string result = UserValidationHelper.ValidateUserName(username);

            Assert.That(result, Is.EqualTo("Username have to reach minimum 6 characters and also can't be more than 45!"));
        }

        [Test]
        [TestCase("jdkajsdakjdkajdksajdlkjaskjdakdjaklsjdakldjasdjsakdjaskjdakjsdkajdskajdkasjdkasjdkasjdskajdkasjdkasjdkasjdlkasjdklsajldkajsdljasldjaldjasldkajsdlkajldjasldjs")]
        public void ValidateUserName_WhenUsernameIsGreaterThan45Characters_ReturnsErrorsString(string username)
        {
            string result = UserValidationHelper.ValidateUserName(username);

            Assert.That(result, Is.EqualTo("Username have to reach minimum 6 characters and also can't be more than 45!"));
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

            Assert.That(result, Is.EqualTo("Username needs to be one word!"));
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

            Assert.That(result, Is.EqualTo("Password is empty!"));
        }

        [Test]
        [TestCase("as")]
        public void ValidatePassword_WhenPasswordIsLessThan6Characters_ReturnsErrorsString(string password)
        {
            string result = UserValidationHelper.ValidatePassword(password);

            Assert.That(result, Is.EqualTo("Password have to reach minimum 6 characters and also can't be more than 45!"));
        }

        [Test]
        [TestCase("jdkajsdakjdkajdksajdlkjaskjdakdjaklsjdakldjasdjsakdjaskjdakjsdkajdskajdkasjdkasjdkasjdskajdkasjdkasjdkasjdlkasjdklsajldkajsdljasldjaldjasldkajsdlkajldjasldjs")]
        public void ValidatePassword_WhenPasswordIsGreaterThan45Characters_ReturnsErrorsString(string password)
        {
            string result = UserValidationHelper.ValidatePassword(password);

            Assert.That(result, Is.EqualTo("Password have to reach minimum 6 characters and also can't be more than 45!"));
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

            Assert.That(result, Is.EqualTo("Password needs to be one word!"));
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

            Assert.That(result, Is.EqualTo("FirstName is empty!"));
        }

        [Test]
        [TestCase("as")]
        public void ValidateFirstName_WhenFirstNameIsLessThan3Characters_ReturnsErrorsString(string firstname)
        {
            string result = UserValidationHelper.ValidateFirstName(firstname);

            Assert.That(result, Is.EqualTo("FirstName have to reach minimum 3 characters and also can't be more than 45!"));
        }

        [Test]
        [TestCase("jdkajsdakjdkajdksajdlkjaskjdakdjaklsjdakldjasdjsakdjaskjdakjsdkajdskajdkasjdkasjdkasjdskajdkasjdkasjdkasjdlkasjdklsajldkajsdljasldjaldjasldkajsdlkajldjasldjs")]
        public void ValidateFirstName_WhenFirstNameIsGreaterThan45Characters_ReturnsErrorsString(string firstname)
        {
            string result = UserValidationHelper.ValidateFirstName(firstname);

            Assert.That(result, Is.EqualTo("FirstName have to reach minimum 3 characters and also can't be more than 45!"));
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

            Assert.That(result, Is.EqualTo("FirstName needs to be one word that contains only letters of the alphabet!"));
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

            Assert.That(result, Is.EqualTo("LastName is empty!"));
        }

        [Test]
        [TestCase("as")]
        public void ValidateLastName_WhenLastNameIsLessThan3Characters_ReturnsErrorsString(string lastname)
        {
            string result = UserValidationHelper.ValidateLastName(lastname);

            Assert.That(result, Is.EqualTo("LastName have to reach minimum 3 characters and also can't be more than 45!"));
        }

        [Test]
        [TestCase("jdkajsdakjdkajdksajdlkjaskjdakdjaklsjdakldjasdjsakdjaskjdakjsdkajdskajdkasjdkasjdkasjdskajdkasjdkasjdkasjdlkasjdklsajldkajsdljasldjaldjasldkajsdlkajldjasldjs")]
        public void ValidateLastName_WhenLastNameIsGreaterThan45Characters_ReturnsErrorsString(string lastname)
        {
            string result = UserValidationHelper.ValidateLastName(lastname);

            Assert.That(result, Is.EqualTo("LastName have to reach minimum 3 characters and also can't be more than 45!"));
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

            Assert.That(result, Is.EqualTo("LastName needs to be one word that contains only letters of the alphabet!"));
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

            Assert.That(result, Is.EqualTo("Email is empty!"));
        }

        [Test]
        [TestCase("a@a.com")]
        public void ValidateEmail_WhenEmailIsLessThan11Characters_ReturnsErrorsString(string email)
        {
            string result = UserValidationHelper.ValidateEmail(email);

            Assert.That(result, Is.EqualTo("Email have to reach minimum 11 characters and also can't be more than 100!"));
        }

        [Test]
        [TestCase("jdkajsdakjdkajdksajdlkjaskjdakdjaklsjdakldjasdjsakdjaskjda@kjsdkajdskajdkasjdkasjdkasjdskajdkasjdkasjdkasjdlkasjdklsajldkajsdljasldjaldjasldkajsdlkajldjasld.com")]
        public void ValidateEmail_WhenEmailIsGreaterThan100Characters_ReturnsErrorsString(string email)
        {
            string result = UserValidationHelper.ValidateEmail(email);

            Assert.That(result, Is.EqualTo("Email have to reach minimum 11 characters and also can't be more than 100!"));
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

            Assert.That(result, Is.EqualTo("Invalid email!"));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("      ")]
        public void ValidateTelephone_WhenTelephoneIsNullOrWhiteSpace_ReturnsErrorsString(string telephone)
        {
            string result = UserValidationHelper.ValidateTelephone(telephone);

            Assert.That(result, Is.EqualTo("Telephone is empty!"));
        }

        [Test]
        [TestCase("12")]
        public void ValidateTelephone_WhenTelephoneIsLessTahn10Characters_ReturnsErrorsString(string telephone)
        {
            string result = UserValidationHelper.ValidateTelephone(telephone);

            Assert.That(result, Is.EqualTo("Telephone number needs to be greater than 10 and less than 14!"));
        }

        [Test]
        [TestCase("1234567891011121314")]
        public void ValidateTelephone_WhenTelephoneIsGreaterThan14Characters_ReturnsErrorsString(string telephone)
        {
            string result = UserValidationHelper.ValidateTelephone(telephone);

            Assert.That(result, Is.EqualTo("Telephone number needs to be greater than 10 and less than 14!"));
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

            Assert.That(result, Is.EqualTo("Invalid telephone number!"));
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