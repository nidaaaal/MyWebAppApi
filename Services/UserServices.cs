using BCrypt.Net;
using Microsoft.AspNetCore.Http.HttpResults;
using MyWebApp.Models;
using MyWebAppApi.DTOs;
using MyWebAppApi.Helper;
using MyWebAppApi.Repository.Interfaces;
using MyWebAppApi.Services.Interfaces;

namespace MyWebAppApi.Services
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepository _userRepository;
        private readonly IWebHostEnvironment _env;

        public UserServices(IUserRepository userRepository,IWebHostEnvironment hostEnvironment) 
        { 
            _userRepository = userRepository;
            _env = hostEnvironment;
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
            if (age < 13)
            {
                return ApiResponseBuilder.Fail<string>("Underage!", 500);
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

        public async Task<ApiResponse<int>> LoginUser(LoginRequestDto dto)
        {
            var result = await _userRepository.GetUserByUsername(dto.UserName);

            if (result == null) return ApiResponseBuilder.Fail<int>("No user exists with the username",404);

            if (!BCrypt.Net.BCrypt.Verify(dto.Password, result.HashedPassword)) return ApiResponseBuilder.Fail<int>("Invalid Credentials",401);

            await _userRepository.SaveLogin(result.Id);

            return ApiResponseBuilder.Success<int>(result.Id, "Login Successful");
        }
        public async Task<ApiResponse<Users?>> GetUserProfile(int id)
        {
            var result = await _userRepository.GetUserProfile(id);
            if (result == null) return ApiResponseBuilder.Fail<Users?>("User not found", 404);
            return ApiResponseBuilder.Success<Users?>(result, "User profile retrieved successfully");
        }

        public async Task<ApiResponse<string>> UpdateUserProfile(int id, UpdateProfileDto updateProfileDto)
        {
            var Today = DateTime.Today;
            int age = Today.Year - updateProfileDto.DateOfBirth.Year;

            if (updateProfileDto.DateOfBirth.Month > Today.Month || (updateProfileDto.DateOfBirth.Month == Today.Month && Today.Day < updateProfileDto.DateOfBirth.Day))
            {
                age--;
            }

            var result = await _userRepository.UpdateUserProfile(id, updateProfileDto,age);

            if(result.ResultCode == 1)
            {
                return ApiResponseBuilder.Success<string>(null!,result.Message ?? "Profile Updated Sucessfully");
            }
            if(result.ResultCode == -1)
            {
                return ApiResponseBuilder.Fail<string>(result.Message ?? "user notfound",404);
            }

            return ApiResponseBuilder.Fail<string>(result.Message ?? "server down", 500);
        }


        public async Task<ApiResponse<string>>ChangePassword(int id, string oldpassword, string password)
        {
            if(oldpassword == password) return ApiResponseBuilder.Fail<string>("New password cannot be the same as the old password.", 400);

            var currentPassword = await _userRepository.GetPasswordById(id);

            if (currentPassword == null) return ApiResponseBuilder.Fail<string>("User Notfound !",404);

            if (!BCrypt.Net.BCrypt.Verify(oldpassword, currentPassword)) return ApiResponseBuilder.Fail<string>("You Entered a wrong Password!" ,401);

            string hashedNewPassword = BCrypt.Net.BCrypt.HashPassword(password);

            var response = await _userRepository.SavePassword(id, hashedNewPassword);


            if (!response) return ApiResponseBuilder.Fail<string>( "password changing failed" ,500);

            return ApiResponseBuilder.Success<string>(null!,"Password Changed successfully");

        }

        public async Task<ApiResponse<string>> UpdateImage(int id, IFormFile file)
        {
            byte[] bytes;

            using (var ms = new MemoryStream())
            {
                await file.CopyToAsync(ms);
                bytes = ms.ToArray();

            }

            string folder = Path.Combine(_env.WebRootPath, "uploads", "users", id.ToString());

            Directory.CreateDirectory(folder);

            var existingFiles = Directory.GetFiles(folder);

            foreach (var files in existingFiles)
            {
                File.Delete(files);
            }

            string ext = Path.GetExtension(file.FileName).ToLower();
            string fileName = "profile" + ext;

            string fullPath = Path.Combine(folder, fileName);

            using var stram = new FileStream(fullPath, FileMode.Create);
            await file.CopyToAsync(stram);

            string relativePath = $"/uploads/users/{id}/{fileName}";


            var response = await _userRepository.UploadImage(id, bytes, relativePath);

            if (!response) return ApiResponseBuilder.Fail<string>("Uploadig Image Failed!",500);

            return ApiResponseBuilder.Success<string>(null!, "Profile Updated Successfully");
        }
    }
}
