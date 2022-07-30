using System.Security.Cryptography;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Task4.Areas.Identity.Data;
using Task4.Domain.Entities.DTOs;
using Task4.Domain.Interfaces;
using Wiry.Base32;

namespace Task4.Services;

public class AdminService : IAdminService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    
    public AdminService(UserManager<ApplicationUser> userManager, ApplicationDbContext context,
        IMapper mapper)
    {
        _context = context;
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UserDto>> GetUsers()
    {
        var users = await _userManager.Users.Select(user => _mapper.Map<UserDto>(user)).ToListAsync();
        return users;
    }

    public async Task ChangeUserStatus(int[] userIds, bool isActive)
    {
        var users = await _context.Users.Where(u => userIds.Contains(u.Id)).ToListAsync();
        if (isActive)
        {
            UnBlockUsers(users);
        }
        else
        {
            BlockUsers(users);
        }
        await _context.SaveChangesAsync();
    }

    private void BlockUsers(List<ApplicationUser> users)
    {
        users.ForEach(u =>
        {
            u.LockoutEnd = DateTime.MaxValue;
            u.SecurityStamp = GenerateSecurityStamp();
        });
    }

    private void UnBlockUsers(List<ApplicationUser> users)
    {
        users.ForEach(u => { u.LockoutEnd = null; });
    }

    private string GenerateSecurityStamp()
    {
        byte[] bytes = new byte[20];
        RandomNumberGenerator.Fill(bytes);
        return Base32Encoding.Standard.GetString(bytes);
    }

    public async Task DeleteUsers(int[] userIds)
    {
        _context.Users.RemoveRange(_context.Users.Where(u => userIds.Contains(u.Id)));
        await _context.SaveChangesAsync();
    }
}