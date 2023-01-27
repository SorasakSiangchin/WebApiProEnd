using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
            app.MapPost("address", CreateAddress).WithName("CreateAddress").Accepts<CreateAddressDTO>("application/json").Produces<APIResponse>(200).Produces(400); 
            app.MapPut("address", UpdateAddress).WithName("UpdateAddress").Accepts<UpdateAddressDTO>("application/json").Produces<APIResponse>(200).Produces(400);
        
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
        private static async Task<IResult> GetAllAddress(IAddressRepository _addressRepo , int Id)
        {
            APIResponse response = new();
            var address = await _addressRepo.GetAsync(Id);
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
            var address = _mapper.Map<Address>(model);
            var result = await _addressRepo.GetAllAsync(address.AccountID);
            if (result?.Count == 0) address.Status = true;
            await _addressRepo.CreactAsync(address);
            response.Result = address;
            response.StatusCode = HttpStatusCode.OK;
            response.IsSuccess = true;
            return Results.Ok(response);
        }

        private static async Task<IResult> UpdateAddress(IMapper _mapper, IAddressRepository _addressRepo, IAccountRepository _accountRepo, UpdateAddressDTO model)
        {
            APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };
            var address = await _addressRepo.GetAsync(model.Id);
            if (address == null)
            {
                response.ErrorMessages.Add("ไม่พบที่อยู่ปัจจุบัน");
                return Results.BadRequest(response);
            }
            _mapper.Map(model , address);
            await _addressRepo.UpdateAsync(_mapper.Map<Address>(address));
            response.Result = address;
            response.StatusCode = HttpStatusCode.OK;
            response.IsSuccess = true;
            return Results.Ok(response);
        }

    }
}
