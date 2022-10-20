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
        private ApiUser _user;

        public AuthManager(IMapper mapper, UserManager<ApiUser> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<bool> Login(LoginDto loginDto)
        {

            _user = await _userManager.FindByEmailAsync(loginDto.Email);
            bool isValidUser = await _userManager.CheckPasswordAsync(_user, loginDto.Password);

            if (_user == null || isValidUser == false)
            {
                return null;
            }
            
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

