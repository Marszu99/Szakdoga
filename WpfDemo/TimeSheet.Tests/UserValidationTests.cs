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
        [TestCase("saddd s")]
        public void ValidateUserName_WhenUsernameIsNotOneWord_ReturnsErrorsString(string username)
        {
            string result = UserValidationHelper.ValidateUserName(username);

            Assert.That(result, Is.EqualTo(Resources.UsernameOneWord));
        }

        [Test]
        [TestCase("CehMarci")]
        public void ValidateUserName_WhenUsernameIsOneWord_ReturnsNull(string username)
        {
            string result = UserValidationHelper.ValidateUserName(username);

            Assert.That(result, Is.EqualTo(null));
        }

        [Test]
        [TestCase("CsehMarcell")]
        public void ValidateUserName_WhenUsernameIsExists_ReturnsErrorsString(string username)
        {
            string result = UserValidationHelper.ValidateUserName(username);

            Assert.That(result, Is.EqualTo(username + Resources.UsernameAlreadyExists));
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
        [TestCase("Password", null)]
        [TestCase("Password", "")]
        [TestCase("Password", " ")]
        [TestCase("Password", "    ")]
        public void ValidatePassword2__WhenPassword2IsNullOrWhiteSpace_ReturnsErrorsString(string password, string password2)
        {
            string result = UserValidationHelper.ValidatePassword2(password, password2);

            Assert.That(result, Is.EqualTo(Resources.Password2IsEmpty));
        }

        [Test]
        [TestCase("Password", "password")]
        public void ValidatePassword2__WhenPassword2DoesntMatchWithPassword_ReturnsErrorsString(string password, string password2)
        {
            string result = UserValidationHelper.ValidatePassword2(password, password2);

            Assert.That(result, Is.EqualTo(Resources.Password2DoesntMatch));
        }

        [Test]
        [TestCase("Password", "Password")]
        public void ValidatePassword2__WhenPassword2DoesMatchWithPassword_ReturnsNull(string password, string password2)
        {
            string result = UserValidationHelper.ValidatePassword2(password, password2);

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
        [TestCase("a@a.com")]
        public void ValidateEmail_WhenEmailIsInvalid_ReturnsErrorsString(string email)
        {
            string result = UserValidationHelper.ValidateEmail(email);

            Assert.That(result, Is.EqualTo(Resources.EmailIsInvalid));
        }

        [Test]
        [TestCase("csehmarcell@yahoo.com", null)]
        [TestCase("csehmarcell@yahoo.com", "")]
        [TestCase("csehmarcell@yahoo.com", " ")]
        [TestCase("csehmarcell@yahoo.com", "      ")]
        public void ValidateEmail2_WhenEmai2lIsNullOrWhiteSpace_ReturnsErrorsString(string email, string email2)
        {
            string result = UserValidationHelper.ValidateEmail2(email, email2);

            Assert.That(result, Is.EqualTo(Resources.Email2IsEmpty));
        }

        [Test]
        [TestCase("csehmarcell@yahoo.com", "csehlaszlo@yahoo.com")]
        public void ValidateEmail2_WhenEmai2DoesntMatchWithEmail_ReturnsErrorsString(string email, string email2)
        {
            string result = UserValidationHelper.ValidateEmail2(email, email2);

            Assert.That(result, Is.EqualTo(Resources.Email2DoesntMatch));
        }

        [Test]
        [TestCase("csehmarcell@yahoo.com", "csehmarcell@yahoo.com")]
        public void ValidateEmail2_WhenEmai2DoesMatchWithEmail_ReturnsNull(string email, string email2)
        {
            string result = UserValidationHelper.ValidateEmail2(email, email2);

            Assert.That(result, Is.EqualTo(null));
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

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("      ")]
        public void ValidateCompanyName_WhenCompanyNameIsNullOrWhiteSpace_ReturnsErrorsString(string companyName)
        {
            string result = UserValidationHelper.ValidateCompanyName(companyName);

            Assert.That(result, Is.EqualTo(Resources.CompanyNameIsEmpty));
        }

        [Test]
        [TestCase("Company")]
        public void ValidateCompanyName_WhenCompanyNamesIsLessThan10Characters_ReturnsErrorsString(string companyName)
        {
            string result = UserValidationHelper.ValidateCompanyName(companyName);

            Assert.That(result, Is.EqualTo(Resources.CompanyNameWrongLength));
        }

        [Test]
        [TestCase("CompanyVeryLoooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooongName")]
        public void ValidateCompanyName_WhenCompanyNamesIsGreaterThan60Characters_ReturnsErrorsString(string companyName)
        {
            string result = UserValidationHelper.ValidateCompanyName(companyName);

            Assert.That(result, Is.EqualTo(Resources.CompanyNameWrongLength));
        }

        [Test]
        [TestCase("CompanyName")]
        public void ValidateCompanyName_WhenCompanyNamesIsValid_ReturnsNull(string companyName)
        {
            string result = UserValidationHelper.ValidateCompanyName(companyName);

            Assert.That(result, Is.EqualTo(null));
        }

        [Test]
        [TestCase("CompanyName", null)]
        [TestCase("CompanyName", "")]
        [TestCase("CompanyName", " ")]
        [TestCase("CompanyName", "      ")]
        public void ValidateCompanyName2_WhenCompanyName2IsNullOrWhiteSpace_ReturnsErrorsString(string companyName, string companyName2)
        {
            string result = UserValidationHelper.ValidateCompanyName2(companyName, companyName2);

            Assert.That(result, Is.EqualTo(Resources.CompanyName2IsEmpty));
        }

        [Test]
        [TestCase("CompanyName", "CompanyNamee")]
        public void ValidateCompanyName2_WhenCompanyNames2DoesntMatchWithCompanyName_ReturnsErrorsString(string companyName, string companyName2)
        {
            string result = UserValidationHelper.ValidateCompanyName2(companyName, companyName2);

            Assert.That(result, Is.EqualTo(Resources.CompanyName2DoesntMatch));
        }

        [Test]
        [TestCase("CompanyName", "CompanyName")]
        public void ValidateCompanyName2_WhenCompanyNames2DoesMatchWithCompanyName_ReturnsNull(string companyName, string companyName2)
        {
            string result = UserValidationHelper.ValidateCompanyName2(companyName, companyName2);

            Assert.That(result, Is.EqualTo(null));
        }
    }
}