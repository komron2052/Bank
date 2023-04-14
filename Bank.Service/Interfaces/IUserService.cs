using Bank.Domain.Entities;
using Bank.Service.DTOs;
using System.Linq.Expressions;

namespace Bank.Service.Interfaces;

public interface IUserService
{
    Task<UserForResultDto> AddAsync(UserForCreationDto dto);
    Task<UserForResultDto> UpdateAsync(Expression<Func<User, bool>> predicate, UserForCreationDto dto);
    Task<bool> DeleteAsync(Expression<Func<User, bool>> predicate);
    Task<UserForResultDto> GetAsync(Expression<Func<User, bool>> predicate);
    Task<IEnumerable<UserForResultDto>> GetAllAsync(Expression<Func<User, bool>> predicate = null);
}
