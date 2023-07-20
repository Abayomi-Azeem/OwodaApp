using OwodaApp.Models;

namespace OwodaApp.Repository
{
    public interface IPayment
    {
        Task<int> Create(Member member);
        Task Read(int id);

        Task ReadAll();
        
    }

    public interface IRegister : IPayment
    {
        Task<int> Update(Member member);
        Task Delete(int id);
    }
}
