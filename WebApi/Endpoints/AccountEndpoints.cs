using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using System.Net;
using WebApi.Extenstions;
using WebApi.Models;
using WebApi.Models.DTOS.Account;
using WebApi.Modes;
using WebApi.Repositorys.IRepositorys;
using WebApi.RequestHelpers;
using WebApiProjectEnd.Modes.DTOS.Accounts;
using WebApiProjectEnd.Repositorys.IRepositorys;

namespace WebApiProjectEnd.Endpoints
{
    public static class AccountEndpoints
    {
        public static void ConfigureAccountEndpoints(this WebApplication app)
        {
            app.MapPost("/accounts", GetAllAccount).WithName("GetAccounts").Accepts<AccountParams>("application/json").Produces<APIResponse>(200).RequireAuthorization();
            app.MapGet("/account/info", Info).WithName("Info").Produces<APIResponse>(200).Produces(401);
            app.MapGet("/roles", GetAllRole).WithName("GetAllRoles").Produces<APIResponse>(200);
            app.MapGet("/account/{id}", GetAccount).WithName("GetAccount").Produces<APIResponse>(200);
            app.MapPost("/register", Register).WithName("Register").Accepts<AccountRequestDTO>("multipart/form-data").Produces<APIResponse>(200).Produces(400); ;
            app.MapPost("/login", Login).WithName("Login").Accepts<LoginRequestDTO>("multipart/form-data").Produces<APIResponse>(200).Produces(400); ;
            app.MapPost("/googleLogin", GoogleLogin).WithName("GoogleLogin").Accepts<GoogleLoginRequestDTO>("application/json").Produces<APIResponse>(200).Produces(400); ;
            app.MapPost("/account/put", UpdateAccount).WithName("UpdateAccount").Accepts<AccountRequestDTO>("multipart/form-data").Produces<APIResponse>(200).Produces(400); ;
            app.MapPost("/account/password/put", UpdateAccountPassword).WithName("UpdateAccountPassword").Accepts<AccountRequestDTO>("multipart/form-data").Produces<APIResponse>(200).Produces(400); ;
        }

        private async static Task<IResult> GetAllAccount(HttpResponse httpResponse, IAccountRepository _accountRepo, AccountParams accountParams)
        {
            APIResponse response = new();
            var query = _accountRepo.GetAllAsync(accountParams).GetAwaiter().GetResult();
            var accounts = await PagedList<AccountDTO>.ToPagedList(query,
                          accountParams.PageNumber, accountParams.PageSize);
            httpResponse.AddPaginationHeader(accounts.MetaData);
            response.Result = accounts;
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }
        private async static Task<IResult> GetAllRole(IRoleRepository _roleRepo)
        {
            APIResponse response = new();
            response.Result = await _roleRepo.GetAllAsync();
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }
        private async static Task<IResult> GetAccount(IAccountRepository _accountRepo, string id)
        {
            APIResponse response = new();
            var data = await _accountRepo.GetAsync(id);
            if (data == null) return Results.Ok("ไม่มีข้อมูล");
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            response.Result = AccountResponse.FromAccount(data);
            return Results.Ok(response);
        }
        private async static Task<IResult> Register(IMapper _mapper, IAccountRepository _accountRepo, AccountRequestDTO model)
        {
            APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };
            (string erorrMesage, string imageName) = await _accountRepo.UploadImage(model.FormFiles);
            if (!string.IsNullOrEmpty(erorrMesage))
            {
                response.ErrorMessages.Add(erorrMesage);
                return Results.Ok(response);
            }
            bool ifEmailisUnique = _accountRepo.IsUniqueEmail(model.Email);
            if (!ifEmailisUnique)
            {
                response.ErrorMessages.Add("อีเมลซ้ำ");
                return Results.Ok(response);
            }

            var account = _mapper.Map<Account>(model);
            account.ImageUrl = imageName;
            account.Status = true;
            var register = await _accountRepo.Register(account);

            if (register == null || string.IsNullOrEmpty(register.Email)) return Results.BadRequest(response);
            response.Result = register;
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }
        private async static Task<IResult> Login(IAccountRepository _accountRepo, LoginRequestDTO model)
        {
            APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };
            var login = await _accountRepo.Login(model);
            if (login == null)
            {
                response.ErrorMessages.Add("อีเมลหรือรหัสผ่านไม่ถูกต้อง");
                return Results.Ok(response);
            };

            if (login.Account.Status == false)
            {
                response.ErrorMessages.Add("บัญชีของคุณถูกระงับการใช้งาน");
                return Results.Ok(response);
            };

            response.Result = login;
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }
        private async static Task<IResult> GoogleLogin(IMapper _mapper, IAccountRepository _accountRepo, GoogleLoginRequestDTO model)
        {
            APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };
            var result = await _accountRepo.CheckEmail(model.Email);
            var check = result == null;
            AccountDTO register = null;

            if (check)
            {
                var account = _mapper.Map<Account>(model);
                if (account?.RoleID == 0) account.RoleID = 1;
                account.Status = true;
                account.LoginBy = "google";
                register = await _accountRepo.Register(account);
            };

            var login = await _accountRepo.LoginGoogle(new LoginRequestDTO
            {
                Email = !check ? model.Email : register.Email,
                Password = !check ? model.Password : register.Password
            });

            if (login == null)
            {
                response.ErrorMessages.Add("อีเมลหรือรหัสผ่านไม่ถูกต้อง");
                return Results.Ok(response);
            }
            response.Result = login;
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }
        private async static Task<IResult> Info(IAccountRepository _accountRepo, HttpContext httpContext)
        {
            APIResponse response = new();
            var accessToken = await httpContext.GetTokenAsync("access_token");
            if (accessToken == null)
            {
                response.StatusCode = HttpStatusCode.Unauthorized;
                return Results.Ok(response);
            }
            var accounts = _accountRepo.GetInfo(accessToken);
            response.Result = accounts;
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }
        private static async Task<IResult> UpdateAccount(IMapper _mapper, IAccountRepository _accountRepo, AccountRequestDTO model)
        {
            APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };
            var account = await _accountRepo.GetAsync(model.Id);
            if (account == null) return Results.NotFound();
            _mapper.Map(model, account); // แทนค่า
            #region จัดการรูปภาพ
            (string erorrMesage, string imageName) = await _accountRepo.UploadImage(model.FormFiles);
            if (!string.IsNullOrEmpty(erorrMesage))
            {
                response.ErrorMessages.Add(erorrMesage);
                return Results.BadRequest(response);
            }
            if (!string.IsNullOrEmpty(imageName))
            {
                await _accountRepo.DeleteImage(account.ImageUrl);
                account.ImageUrl = imageName;
            }
            #endregion

            await _accountRepo.UpdateAsync(account);
            var result = await _accountRepo.GetAsync(account.Id, tracked: false);
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            response.Result = AccountResponse.FromAccount(result);
            return Results.Ok(response);
        }
        private static async Task<IResult> UpdateAccountPassword(IMapper _mapper, IAccountRepository _accountRepo, AccountRequestDTO model)
        {
            APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };
            var account = await _accountRepo.GetAsync(model.Id);
            if (account == null) return Results.NotFound();
            _mapper.Map(model, account); // แทนค่า
            await _accountRepo.UpdatePassword(account, model.PasswordNew);
            response.Result = AccountResponse.FromAccount(await _accountRepo.GetAsync(account.Id, tracked: false));
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }
    }
}
