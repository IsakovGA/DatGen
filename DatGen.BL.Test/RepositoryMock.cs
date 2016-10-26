using DatGen.Dat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatGen.BL.Test
{
    class RepositoryMock : IRepository
    {
        public string GetRandomEmailDomain()
        {
            return "mail.ru";
        }

        public IdentityInfo GetRandomName()
        {
            return new IdentityInfo { Identity = "Иван", Gender = Gender.Male };
        }

        public string GetRandomPatronymic(Gender gender)
        {
            return "Иванович";
        }

        public string GetRandomSurname(Gender gender)
        {
            return "Иванович";
        }

        public string GetRandomUniqLogin()
        {
            return "ivan";
        }

        public void Init()
        {
            
        }
    }
}
