using AutoMapper;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WebApi.Extenstions;
using WebApi.Models;
using WebApi.Modes;
using WebApi.Repositorys.IRepositorys;
using WebApi.RequestHelpers;
using WebApiProjectEnd.Modes.DTOS.Accounts;
using WebApiProjectEnd.Repositorys.IRepositorys;

namespace WebApiProjectEnd.Repositorys
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IUploadFileRepository _uploadFile;
        private readonly ICartRepository _cartRepo;
        private string secretKey;
        public AccountRepository(
            ApplicationDbContext db, 
            IMapper mapper, 
            IConfiguration configuration, 
            IUploadFileRepository uploadFile, 
            ICartRepository cartRepo )
        {
            _db = db;
            _mapper = mapper;
            _configuration = configuration;
            _uploadFile = uploadFile;
            _cartRepo = cartRepo;
            secretKey = _configuration.GetValue<string>("ApiSettings:Secret");
        }
        
        public async Task<ICollection<AccountDTO>> GetAllAsync(AccountParams accountParams)
        {
            return _db.Accounts
                .Include(e => e.Role)
                .SearchName(accountParams.SearchName)
                .SearchEmail(accountParams.SearchEmail)
                .SearchPhoneNumber(accountParams.SearchPhoneNumber)
                .FilterStatus(accountParams.Status)
                .ProjectAccountToAccountDTO(_db)
                .ToListAsync()
                .GetAwaiter()
                .GetResult();
        }

        public async Task<Account> GetAsync(string id, bool tracked = true)
        {
            IQueryable<Account> query = _db.Accounts.Include(e => e.Role);
            if (!tracked) query = query.AsNoTracking();
            return await query.FirstOrDefaultAsync(e => e.Id.Equals(id));
        }

        public bool IsUniqueEmail(string email)
        {
            var account = _db.Accounts.FirstOrDefault(x => x.Email.Equals(email));
            if (account == null) return true;
            return false;
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        {
            Account account = await CheckEmail(loginRequestDTO.Email);
            if (account == null || !VerifyPassword(account.Password, loginRequestDTO.Password)) return null;
            var accountCart = await _cartRepo.GetCartByAccountIdAsync(account.Id);
            LoginResponseDTO loginResponseDTO = new()
            {
                Account = _mapper.Map<AccountDTO>(AccountResponse.FromAccount(account)),
                Token = CreateToken(account),
                Cart = accountCart
            };
            return loginResponseDTO;
        }

        public async Task<LoginResponseDTO> LoginGoogle(LoginRequestDTO loginRequestDTO)
        {
            Account account = await CheckEmail(loginRequestDTO.Email);
            if (account == null || !VerifyPassword(account.Password, loginRequestDTO.Password)) return null;
            var accountCart = await _cartRepo.GetCartByAccountIdAsync(account.Id);

            LoginResponseDTO loginResponseDTO = new()
            {
                Account = _mapper.Map<AccountDTO>(account),
                Token = CreateToken(account),
                Cart = accountCart
            };

            return loginResponseDTO;
        }

        public async Task<Account> CheckEmail(string email)
        {
            var account = await _db.Accounts.AsNoTracking().Include(x => x.Role).SingleOrDefaultAsync(x => x.Email == email);
            if (account == null) return null;
            return account;
        }

        public async Task<AccountDTO> Register(Account account)
        {
            account.Id = GenerateID();
            var realPassword = account.Password;
            account.Password = CreateHashPassword(account.Password);
            await _db.Accounts.AddAsync(account);
            await _db.SaveChangesAsync();
            account.Password = realPassword;
            return _mapper.Map<AccountDTO>(account);
        }
        public async Task<AccountDTO> UpdatePassword(Account account, string passwordNew)
        {
            account.Password = CreateHashPassword(passwordNew);
            _db.Update(account);
            await _db.SaveChangesAsync();
            return _mapper.Map<AccountDTO>(account);
        }

        public async Task<(string errorMessage, string imageName)> UploadImage(IFormFileCollection formFiles)
        {
            var errorMessage = string.Empty;
            var imageName = string.Empty;

            if (_uploadFile.IsUpload(formFiles))
            {
                errorMessage = _uploadFile.Validation(formFiles);
                if (string.IsNullOrEmpty(errorMessage)) imageName = (await _uploadFile.UploadFile(formFiles))[0];
            }
            return (errorMessage, imageName);
        }

        private string CreateHashPassword(string password)
        {
            byte[] salt = new byte[128 / 8];
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetNonZeroBytes(salt);
            }
            var hashed = HashPassword(password, salt);
            var hpw = $"{Convert.ToBase64String(salt)}.{hashed}";
            return hpw;
        }

        private string HashPassword(string password, Byte[] salt)
        {
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
              password: password,
              salt: salt,
              prf: KeyDerivationPrf.HMACSHA256,
              iterationCount: 100000,
              numBytesRequested: 256 / 8));
            return hashed;
        }

        private string GenerateID() => Guid.NewGuid().ToString("N");

        private bool VerifyPassword(string saltAndHashFromDB, string password)
        {
            // ทำการแยกส่วนเป็น 2 สว่น เป็นอเร
            var parts = saltAndHashFromDB.Split('.', 2);
            if (parts.Length != 2) return false;
            // ไปเอาเกลือมา
            // Convert.FromBase64String ให้กลับเหมือนเดิมปกติมันเป็นไบต์
            var salt = Convert.FromBase64String(parts[0]);
            var passwordHash = parts[1];

            string hashed = HashPassword(password, salt);

            return hashed == passwordHash;
        }

        public Account GetInfo(string accessToken)
        {
            //as JwtSecurityToken แปลงค่า Token (ถอดรหัส)
            var token = new JwtSecurityTokenHandler().ReadToken(accessToken) as JwtSecurityToken;
            var id = token.Claims.First(claim => claim.Type == "Id").Value;
            var roleName = token.Claims.First(claim => claim.Type == "Role").Value;
            var email = token.Claims.First(claim => claim.Type == "Email").Value;
            var account = new Account
            {
                Id = id,
                Email = email,
                Role = new Role
                {
                    Name = roleName
                }
            };
            return account;
        }

        public async Task UpdateAsync(Account account)
        {
            _db.Update(account);
            await _db.SaveChangesAsync();
        }

        private string CreateToken(Account account)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier , account.Id),
                        new Claim(ClaimTypes.Name ,account.Email  ),
                        new Claim(ClaimTypes.Role , account.Role.Name)
                        ,
                    }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
