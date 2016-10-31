using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DatGen.BL;
using DatGen.Dat;

namespace DatGen.BL.Test
{
    [TestClass]
    public class ScriptGenTest
    {
        private ScriptGen _generator;

        [TestInitialize]
        public void Init() {
            IRepository repository = new RepositoryMock();
            _generator = new ScriptGen(repository);
        }

        [TestMethod]
        public void GenerateUser_NameReq()
        {

            UserEntity entity = _generator.GenerateUser();
            string name = entity.Name;

            Assert.IsNotNull(name);
        }

        [TestMethod]
        public void GenerateUser_SurnameReq()
        {

            UserEntity entity = _generator.GenerateUser();
            string surname = entity.Surname;

            Assert.IsNotNull(surname);
        }

        [TestMethod]
        public void GenerateUser_LoginReq()
        {

            UserEntity entity = _generator.GenerateUser();
            string login = entity.Login;

            Assert.IsNotNull(login);
        }

        [TestMethod]
        public void GenerateUser_PasswordReq()
        {

            UserEntity entity = _generator.GenerateUser();
            string pass = entity.Password;

            Assert.IsNotNull(pass);
        }

        [TestMethod]
        public void GenerateUser_EmailReq()
        {

            UserEntity entity = _generator.GenerateUser();
            string email = entity.Email;

            Assert.IsNotNull(email);
        }

        [TestMethod]
        public void GenerateUser_RegistrationDatePeriod()
        {

            UserEntity entity = _generator.GenerateUser();
            DateTime registrationDate = entity.RegistrationDate;
            bool isInRange = registrationDate > (new DateTime(2010, 1, 1)) && registrationDate < (new DateTime(2016, 9, 1));

            Assert.IsTrue(isInRange);
        }

        [TestMethod]
        public void GetValueLine_test()
        {
            UserEntity user = new UserEntity()
            {
                Name = "Петр",
                Surname = "Петров",
                Patronymic = "Петрович",
                Login = "petr",
                Password = "12345",
                Email = "petr@mail.ru",
                RegistrationDate = new DateTime(2016, 1, 1)
            };
            const string exp_result = @"(N'Петр', N'Петров', N'Петрович', 'petr@mail.ru', 'petr', '12345', '20160101')";

            string result = _generator.GetValueLine(user);

            Assert.AreEqual(exp_result, result);
        }

        [TestMethod]
        public void GetInsertLine_test()
        {

            const string exp_result = @"INSERT INTO BlogUsers (Name, Surname, Patronymic, Email, Login, Password, RegistrationDate) VALUES";

            string result = _generator.GetInsertLine();

            Assert.AreEqual(exp_result, result);
        }

        [TestMethod]
        public void MergeLine_test()
        {
            const string ins_line = "INSERT LINE";
            string[] valueLines = { "VALUE LINE 1", "VALUE LINE 2" };
            string exp_result = $"INSERT LINE{Environment.NewLine}VALUE LINE 1,{Environment.NewLine}VALUE LINE 2";
            string result = _generator.MergeLines(valueLines, ins_line);

            Assert.AreEqual(exp_result, result);
        }
    }
}
