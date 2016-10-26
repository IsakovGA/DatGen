

namespace DatGen.Dat
{
    public interface IRepository
    {
        void Init();
        IdentityInfo GetRandomName();
        string GetRandomSurname(Gender gender);
        string GetRandomPatronymic(Gender gender);
        string GetRandomUniqLogin();
        string GetRandomEmailDomain();
    }
}
