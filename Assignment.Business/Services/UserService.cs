using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Assignment.Helpers;
using Assignment.UserData.Models;
using Assignment.UserData.Reposistories;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Assignment.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Appsettings _appsettings;

        public UserService(IUnitOfWork unitOfWork,IOptions<Appsettings> appsetting)
        {
            _unitOfWork = unitOfWork;
            _appsettings = appsetting.Value;
        }
        public async Task<User> AddUser(User user)
        {
            _unitOfWork.UserRepository.Add(user);
            await _unitOfWork.SaveChanges();
            return user;
        }

        public bool ExistUser(string email, string name)
        {
            return _unitOfWork.UserRepository.CheckExistUser(email, name);
        }

        public async Task<User> UpdateUser(User user)
        {
            var currentMember = await _unitOfWork.UserRepository.Get(user.Id);
            currentMember.Name = user.Name;
            currentMember.MobileNumber = user.MobileNumber;
            currentMember.Gender = user.Gender;
            currentMember.Dob = user.Dob;
            currentMember.EmailOptIn = user.EmailOptIn;
            await _unitOfWork.SaveChanges();
            return currentMember;
        }

        public async Task<User> Authenticate(string email, string password)
        {
            var member = await _unitOfWork.UserRepository.FindByEmailAndPassword(email, password);
            if (member == null)
                return null;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appsettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, member.Email)
                }),
                Expires = DateTime.UtcNow.AddDays(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            member.Token = tokenHandler.WriteToken(token);
            member.Password = null;
            return member;
        }

        public async Task<User> GetUserById(int id)
        {
            var currentUser = await _unitOfWork.UserRepository.Get(id);
            currentUser.Password = null;
            return currentUser;
        }
    }
}
