using Microsoft.EntityFrameworkCore;
using OwodaApp.Data;
using OwodaApp.Models;

namespace OwodaApp.Repository
{

public class MemberRep : IRegister
    {
    private readonly OwodaAppContext _context;

    public MemberRep(OwodaAppContext owodaAppContext)
        {
        _context = owodaAppContext;
        }
        
    public async Task<int> Create(Member member)
    {
            member.RegDate = DateTime.UtcNow.AddHours(1);
            _context.Add(member);
           return await _context.SaveChangesAsync();
            
    }

    public Task Delete()
        {
            throw new NotImplementedException();
        }

    public Task Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Task Read()
        {
            throw new NotImplementedException();
        }

    public Task Read(int id)
    {
        throw new NotImplementedException();
    }

    public Task ReadAll()
    {
        throw new NotImplementedException();
    }

    public Task Update()
        {
            throw new NotImplementedException();
        }

    public async Task<int> Update(Member member)
    {
            _context.Update(member);
            return await _context.SaveChangesAsync();
     }
}
}
