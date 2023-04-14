using AutoMapper;
using Bank.Data.IRepositories;
using Bank.Domain.Entities;
using Bank.Service.DTOs;
using Bank.Service.Exeptions;
using Bank.Service.Interfaces;
using System;
using System.Linq.Expressions;

namespace Bank.Service.Services;

public class UserService : IUserService
{
    private readonly IRepository<User> userRepo;
    private readonly IMapper mapper;

    public UserService(IRepository<User> userRepo, IMapper mapper)
    {
        this.userRepo = userRepo;
        this.mapper = mapper;
    }

    public async Task<UserForResultDto> AddAsync(UserForCreationDto dto)
    {
        var user = await userRepo.SelectAsync(u => u.Email.ToLower() == dto.Email.ToLower());
        if (user != null)
            throw new CustomExeption(400, "User already exists");
        var mappedUser = mapper.Map<User>(dto);
        mappedUser.CreatedAt = DateTime.UtcNow;
        var result = await userRepo.InsertAsync(mappedUser);
        await userRepo.SaveAsync();
        return mapper.Map<UserForResultDto>(result);
    }

    public async Task<bool> DeleteAsync(Expression<Func<User, bool>> predicate)
    {
        var user = await userRepo.SelectAsync(predicate);
        if (user == null)
            throw new CustomExeption(404, "User Not Found");
        await userRepo.DeleteAsync(predicate);
        await userRepo.SaveAsync();
        return true;
    }

    public async Task<IEnumerable<UserForResultDto>> GetAllAsync(Expression<Func<User, bool>> predicate = null)
    {
        var users = userRepo.SelectAllAsync();
        users = predicate != null ? users.Where(predicate) : users;
        var mappedUsers = mapper.Map<IEnumerable<UserForResultDto>>(users); 
        return mappedUsers;
    }

    public async Task<UserForResultDto> GetAsync(Expression<Func<User, bool>> predicate)
    {
        var user = await userRepo.SelectAsync(predicate);
        if (user == null)
            throw new CustomExeption(404, "User Not Found");
        var mappedUser = mapper.Map<UserForResultDto>(user);
        return mappedUser;
    }

    public async Task<UserForResultDto> UpdateAsync(Expression<Func<User, bool>> predicate, UserForCreationDto dto)
    {
        var user = await userRepo.SelectAsync(predicate);
        if (user == null)
            throw new CustomExeption(404, "User Not Found");
        user.FirstName = dto.FirstName;
        user.LastName = dto.LastName;
        user.Email = dto.Email;
        user.Password = dto.Password;
        user.DateOfBirth = dto.DateOfBirth;
        user.UpdatedAt = DateTime.UtcNow;
        
        await userRepo.UpdateAsync(user);
        await userRepo.SaveAsync();
        return mapper.Map<UserForResultDto>(user);
        
    }
}
