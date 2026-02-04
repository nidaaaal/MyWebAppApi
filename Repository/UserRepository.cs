using Microsoft.Data.SqlClient;
using MyApp.Models;
using MyWebAppApi.DTOs;
using MyWebAppApi.Repository.Interfaces;
using System.Net;

namespace MyWebAppApi.Repository
{
    public class UserRepository : BaseRepository,IUserRepository
    {
        public UserRepository(IConfiguration configuration) : base(configuration) { }

        public async Task<int> RegisterUser(RegisterRequestDto dto,string hashedpass,int age)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@username",dto.UserName),
                new SqlParameter("@hashed_password",hashedpass),
                new SqlParameter("@first_name",dto.FirstName),
                new SqlParameter("@last_name",dto.LastName),
                new SqlParameter("@display_name",string.IsNullOrEmpty(dto.DisplayName) ? (object)DBNull.Value : dto.DisplayName),
                new SqlParameter("@date_of_birth",dto.DateOfBirth),
                new SqlParameter("@age",age),
                new SqlParameter("@gender",dto.Gender),
                new SqlParameter("@address",dto.Address),
                new SqlParameter("@city",dto.City),
                new SqlParameter("@state",dto.State),
                new SqlParameter("@zipcode",dto.ZipCode),
                new SqlParameter("@phone",dto.Phone),
                new SqlParameter("@mobile", string.IsNullOrEmpty(dto.Mobile) ? (object)DBNull.Value : dto.Phone)
            };

            SqlDataReader reader = await ExecuteSp("auth.register_user", parameters);

            if(await reader.ReadAsync())
            {
                int ResultCode = Convert.ToInt32(reader["ResultCode"]);
                string Message = Convert.ToString(reader["Message"]) ?? "";

                return ResultCode;
            }

            return 0;

        }

        public async Task<Credential?> GetUserByUsername(string username)
        {
            string sql = "SELECT id,username,hashed_password FROM auth.credentials WHERE username = @username;";
            await using var conn = GetConnection();

            await using SqlCommand cmd = new SqlCommand(sql, conn);

            Credential credential = new Credential();

            cmd.Parameters.AddWithValue("@username", username);

            await conn.OpenAsync();



            await using var read = await cmd.ExecuteReaderAsync();

            if (!await read.ReadAsync()) return null;
         
                credential.Id = Convert.ToInt32(read["id"]);
                credential.UserName = Convert.ToString(read["username"]) ?? "";
                credential.HashedPassword = Convert.ToString(read["hashed_password"]) ?? "";


            return credential;

        }

        public async Task SaveLogin(int id)
        {
           await using var conn = GetConnection();

            DateTime now = DateTime.Now;

            string sql = "UPDATE auth.credentials SET login_at = @now WHERE id = @id;";

            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                await conn.OpenAsync();

                cmd.Parameters.AddWithValue("@now", now);
                cmd.Parameters.AddWithValue("@id", id);

                await cmd.ExecuteNonQueryAsync();

            }
        }
    }
}
