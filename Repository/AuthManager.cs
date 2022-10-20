using AutoMapper;
using HotelListing.Contracts;
using HotelListing.Data;
using HotelListing.Models.User;
using Microsoft.AspNetCore.Identity;

namespace HotelListing.Repository
{
    public class AuthManager : IAuthManager
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApiUser> _userManager;

        public AuthManager(IMapper mapper, UserManager<ApiUser> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        public Task<bool> Login(LoginDto loginDto)
        {
            var user=_userManager.FindByEmailAsync(loginDto.Email);
            var validPassword = _userManager.ValidatePasswordAsync(loginDto.Password);
        }

        public async Task<IEnumerable<IdentityError>> Register(ApiUserDto userDto)
        {
            var user = _mapper.Map<ApiUser>(userDto);
            user.UserName = userDto.Email;
            var result = await _userManager.CreateAsync(user, userDto.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
            }
            return result.Errors;
        }
    }
}

