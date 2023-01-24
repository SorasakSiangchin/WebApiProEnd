using AutoMapper;
using System.Net;
using WebApi.Models;
using WebApi.Models.DTOS.Address;
using WebApi.Repositorys.IRepositorys;
using WebApiProjectEnd.Repositorys.IRepositorys;

namespace WebApi.Endpoints
{
    public static class AddressEndpoints
    {
        public static void ConfigureAddressEndpoints(this WebApplication app)
        {
            app.MapGet("addresses", GetAllAddresses).WithName("GetAllAddresses").Produces<APIResponse>(200);
            app.MapGet("address/{id}", GetAllAddress).WithName("GetAllAddress").Produces<APIResponse>(200);
            app.MapPost("address", CreateAddress).WithName("CreateAddress").Accepts<CreateAddressDTO>("application/json").Produces<APIResponse>(200).Produces(400); ;
        }

        private static async Task<IResult> GetAllAddresses(IAddressRepository _addressRepo , string accountId)
        {
            APIResponse response = new();
            var addresses = await _addressRepo.GetAllAsync(accountId);
            response.Result = addresses;
            response.StatusCode = HttpStatusCode.OK;
            response.IsSuccess = true;
            return Results.Ok(response);
        }
        private static async Task<IResult> GetAllAddress(IAddressRepository _addressRepo, string accountId , int Id)
        {
            APIResponse response = new();
            var address = await _addressRepo.GetAsync(Id , accountId);
            if (address == null)
            {
                return Results.Ok("ไม่มีข้อมูล");
            }
            response.Result = address;
            response.StatusCode = HttpStatusCode.OK;
            response.IsSuccess = true;
            return Results.Ok(response);
        }

        private static async Task<IResult> CreateAddress(IMapper _mapper, IAddressRepository _addressRepo, IAccountRepository _accountRepo, CreateAddressDTO model)
        {
            APIResponse response = new() { IsSuccess = false , StatusCode = HttpStatusCode.BadRequest };
            var acoount = await _accountRepo.GetAsync(model.AccountID);
            if (acoount == null)
            {
                response.ErrorMessages.Add("ไม่พบผู้ใช้งานนี้");
                return Results.BadRequest(response);
            }
            var address =  _mapper.Map<Address>(model);
           await _addressRepo.CreactAsync(address);
            response.Result = address;
            response.StatusCode = HttpStatusCode.OK;
            response.IsSuccess = true;
            return Results.Ok(response);
        }
    }
}
