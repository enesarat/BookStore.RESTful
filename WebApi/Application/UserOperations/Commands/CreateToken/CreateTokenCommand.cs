using AutoMapper;
using Newtonsoft.Json.Linq;
using WebApi.DbOperations;
using WebApi.TokenOperations;
using WebApi.TokenOperations.Models;

namespace WebApi.Application.UserOperations.Commands.CreateToken
{
    public class CreateTokenCommand
    {
        public CreateTokenModel Model { get; set; }
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public CreateTokenCommand(IBookStoreDbContext dbContext, IMapper mapper, IConfiguration configuration)
        {
            this._context = dbContext;
            this._mapper = mapper;
            this._configuration = configuration;
        }

        public Token Handle()
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == Model.Email && u.Password == Model.Password);
            if (user is not null)
            {
                // Create Token
                TokenHandler handler = new TokenHandler(_configuration);
                Token token = handler.CreateAccessToken(user);

                user.RefreshToken = token.RefreshToken;
                user.RefreshTokenExpireDate= token.Expiration.AddMinutes(5);
                _context.SaveChanges();
                return token;
            }
            else
                throw new InvalidOperationException("Kullanıcı Adı veya Şifre Hatalı!");
        }

        public class CreateTokenModel
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }
    }
}
