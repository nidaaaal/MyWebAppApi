using BCrypt.Net;
using MyWebAppApi.DTOs;
using MyWebAppApi.Helper;
using MyWebAppApi.Repository.Interfaces;
using MyWebAppApi.Services.Interfaces;

namespace MyWebAppApi.Services
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepository _userRepository;
        public UserServices(IUserRepository userRepository) 
        { 
            _userRepository = userRepository;
        }

        public async Task<ApiResponse<string>> RegisterUser(RegisterRequestDto dto)
        {
            var validateUsername = InputIdentifier.Identify(dto.UserName);

            if (validateUsername == InputIdentifier.InputType.Invalid) return ApiResponseBuilder.Fail<string>("Invalid Email or Phone Number format",400);

            var passwordCheck = PasswordValidator.Validate(dto.Password);

            if (!passwordCheck.IsValid) return ApiResponseBuilder.Fail<string>(passwordCheck.ErrorMessage, 400);


           string hashedPass =  BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var Today = DateTime.Today;
            int age = Today.Year - dto.DateOfBirth.Year;

            if(dto.DateOfBirth.Month > Today.Month || (dto.DateOfBirth.Month == Today.Month && Today.Day < dto.DateOfBirth.Day))
            {
                age--;
            }

            try
            {
                int result = await _userRepository.RegisterUser(dto, hashedPass, age);

                if (result == -1)
                {
                    return ApiResponseBuilder.Fail<string>("Username already exists.", 409);

                }
                if (result == 1)
                {
                    return ApiResponseBuilder.Success<string>(null!, "User registered successfully.");
                }
                else
                {
                    return ApiResponseBuilder.Fail<string>("User registration failed.", 500);

                }
            }
            catch (Exception ex)
            {
                return ApiResponseBuilder.Fail<string>($"An error occurred: {ex.Message}", 500);

            }
        }

        public async Task<ApiResponse<string>> LoginUser(LoginRequestDto dto)
        {
            var result = await _userRepository.GetUserByUsername(dto.UserName);

            if (result == null) return ApiResponseBuilder.Fail<string>("No user exists with the username",404);

            if (!BCrypt.Net.BCrypt.Verify(dto.Password, result.HashedPassword)) return ApiResponseBuilder.Fail<string>("Invalid Credentials",401);

            return ApiResponseBuilder.Success<string>(null!, "Login Successful");
        }




    }
}
