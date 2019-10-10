using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Assignment.UserData.Models;
using Assignment.UserData.Reposistories;
using Microsoft.IdentityModel.Tokens;

namespace Assignment.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<User> AddUser(User user)
        {
            _unitOfWork.UserRepository.Add(user);
            await _unitOfWork.SaveChanges();
            return user;
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
            // Generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("THIS_IS_JWT_SECRET_KEY");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                        new Claim(ClaimTypes.Email, member.Email)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            try
            {
                var token = tokenHandler.CreateToken(tokenDescriptor);
                member.Token = tokenHandler.WriteToken(token);

                // Remove password before returning
                member.Password = null;
                return member;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _unitOfWork.UserRepository.GetAll();
        }

        public async Task<User> GetUserById(int id)
        {
            var currentUser = await _unitOfWork.UserRepository.Get(id);
            currentUser.Password = null;
            return currentUser;
        }
    }
}
