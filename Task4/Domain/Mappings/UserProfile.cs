using AutoMapper;
using Task4.Areas.Identity.Data;
using Task4.Domain.Entities.DTOs;

namespace Task4.Domain.Mappings;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<ApplicationUser, UserDto>()
            .ForMember(user => user.Status, opt => opt.MapFrom((src) =>
                src.LockoutEnd < DateTime.Now.ToUniversalTime() || src.LockoutEnd == null ? "Active" : "Blocked"));
    }
}