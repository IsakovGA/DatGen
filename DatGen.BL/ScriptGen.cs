using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DatGen.Dat;

namespace DatGen.BL
{
    public class ScriptGen : IScriptGen
    {
        private readonly IRepository _repository;
        private Random _random = new Random();

        public ScriptGen(IRepository repository)
        {
            _repository = repository;
        }

        public string CreateScript(int entityCount)
        {

            List<UserEntity> users = new List<UserEntity>();

            for (int i = 0; i < entityCount; i++)
            {
                users.Add(GenerateUser());
            }

            IEnumerable<string> valueLines = users.Select(GetValueLine);
            string insertLine = GetInsertLine();

            string result = MergeLines(valueLines, insertLine);

            return result;
        }

        public string MergeLines(IEnumerable<string> valueLines, string insertLine)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine(insertLine);

            int i = 0;
            foreach (var valueLine in valueLines)
            {
                if (i > 0) builder.AppendLine(",");
                builder.Append(valueLine);
                i++;
            }

            return builder.ToString();
        }

        public UserEntity GenerateUser()
        {
            UserEntity entity = new UserEntity();
            IdentityInfo identityName = _repository.GetRandomName();

            entity.Name = identityName.Identity;
            entity.Surname = _repository.GetRandomSurname(identityName.Gender);
            entity.Patronymic = _repository.GetRandomPatronymic(identityName.Gender);
            entity.Login = _repository.GetRandomUniqLogin();

            string randomEmailDomain = _repository.GetRandomEmailDomain();

            entity.Email = string.Format("{0}@{1}",entity.Login,randomEmailDomain);
            entity.Password = _random.Next(1000, 10000).ToString();

            int year = _random.Next(2010, 2017);
            int month = _random.Next(1, 13);
            int day = _random.Next(1, 29);
            if (year == 2016 && month > 8) month = 8;
            entity.RegistrationDate = new DateTime(year, month, day);

            return entity;
        }

        public string GetInsertLine()
        {
            string result = @"INSERT INTO BlogUser (Name, Surname, Patronymic, Email, Login, Password, RegistrationDate)";

            return result;
        }

        public string GetValueLine(UserEntity entity)
        {
            string result = $"VALUES ('{entity.Name}', '{entity.Surname}', '{entity.Patronymic}', '{entity.Email}', '{entity.Login}', '{entity.Password}', '{entity.RegistrationDate.ToString("yyyMMdd")}')";

            return result;
        }
    }
}
