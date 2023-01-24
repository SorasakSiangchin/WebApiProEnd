﻿using AutoMapper;
using Azure;
using FluentValidation;
using System.Net;
using System.Text.Json;
using WebApi.Extenstions;
using WebApi.Models;
using WebApi.Modes;
using WebApi.Modes.DTOS.Product;
using WebApi.RequestHelpers;
using WebApiProjectEnd.Repositorys.IRepositorys;


namespace WebApiProjectEnd.Endpoints
{
    public static class ProductEndpoints
    {
        public static void ConfigureProductEndpoints(this WebApplication app)
        {
            app.MapPost("/products", GetAllProduct).WithName("GetProducts").Accepts<PaginationParams>("application/json").Produces<APIResponse>(200);
            app.MapGet("/product/{id}", GetProduct).WithName("GetProduct").Produces<APIResponse>(200);
            app.MapPost("/product", CreateProduct).WithName("CreateProduct").Accepts<ProductDTO>("multipart/form-data").Produces<APIResponse>(200).Produces(400);
            app.MapGet("/product/rare", GetProductRare).WithName("GetProductRare").Produces<APIResponse>(200);
            app.MapPut("/product", UpdateProduct).WithName("UpdateProduct").Accepts<ProductDTO>("multipart/form-data").Produces<APIResponse>(200).Produces(400).Produces(404);
            app.MapGet("/product/name", GetProductByNames).WithName("GetProductsByName").Produces<APIResponse>(200);
            app.MapDelete("/product/{id}", DeleteProduct);
        }

        private static async Task<IResult> GetAllProduct(HttpResponse httpResponse, IProductRepository _productRepo , ProductParams productParams)
        {
            APIResponse response = new();

            var query = await _productRepo.GetAllAsync(productParams);
            var products = await PagedList<Product>.ToPagedList(query,
                          productParams.PageNumber, productParams.PageSize);

            httpResponse.AddPaginationHeader(products.MetaData);

            response.Result = products.Select(ProductResponse.FromProduct);
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }

        private static async Task<IResult> GetProductRare(IProductRepository _productRepo)
        {
            APIResponse response = new();
            var products = await _productRepo.GetRareAsync();
            response.Result = products.Select(ProductResponse.FromProduct);
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }

        private static async Task<IResult> GetProduct(IProductRepository _productRepo , string id)
        {
            APIResponse response = new();
            var data =  await _productRepo.GetAsync(id);
            if (data == null)
            {
                return Results.Ok("ไม่มีข้อมูล");
            }
            response.Result = ProductResponse.FromProduct(data);
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }

        private static async Task<IResult> CreateProduct(IMapper _mapper, IProductRepository _productRepo, IAccountRepository _accountRepo , ProductDTO model)
        {
            APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };

            var acoount = await _accountRepo.GetAsync(model.AccountID);

            if (acoount == null)
            {
                response.ErrorMessages.Add("ไม่พบผู้ใช้งานนี้");
                return Results.BadRequest(response);
            }

            (string erorrMesage, string imageName) = await _productRepo.UploadImage(model.FormFiles);
            if (!string.IsNullOrEmpty(erorrMesage))
            {
                response.ErrorMessages.Add(erorrMesage);
                return Results.BadRequest(response);
            }
            
            var product = _mapper.Map<Product>(model);
            product.ImageUrl = imageName ;
            await _productRepo.CreactAsync(product);
            response.Result = product;
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.Created;
            return Results.Ok(response);
        }

        private async static Task<IResult> UpdateProduct(IMapper _mapper, IProductRepository _productRepo , ProductDTO model)
        {
            APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };
            var product = await _productRepo.GetAsync(model.Id , tracked:false);
            if (product == null) return Results.NotFound();
            #region จัดการรูปภาพ
            (string erorrMesage, string imageName) = await _productRepo.UploadImage(model.FormFiles);
            if (!string.IsNullOrEmpty(erorrMesage))
            {
                response.ErrorMessages.Add(erorrMesage);
                return Results.BadRequest(response);
            }
            if (!string.IsNullOrEmpty(imageName))
            {
                await _productRepo.DeleteImage(product.ImageUrl);
            }
            #endregion
            var data = _mapper.Map<Product>(model);
            data.ImageUrl = imageName;
            data.LastUpdate = DateTime.Now;
            await _productRepo.UpdateAsync(data);
            response.Result = data;
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }

        private async static Task<IResult> DeleteProduct(IProductRepository _productRepo, string id)
        {
            APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };
            var product = await _productRepo.GetAsync(id);
            if (product != null)
            {
                await _productRepo.RemoveAsync(product);
                await _productRepo.DeleteImage(product.ImageUrl);
                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.NoContent;
                return Results.Ok(response);
            }
            else
            {
                response.ErrorMessages.Add("รหัสไม่ถูกต้อง");
                return Results.BadRequest(response);
            }
        }
        private static async Task<IResult> GetProductByNames(IProductRepository _productRepo, string name)
        {
            APIResponse response = new();
            var data = await _productRepo.GetAsyncByName(name);
            if (data == null) return Results.Ok("ไม่มีข้อมูล");
            response.Result = data.Select(ProductResponse.FromProduct);
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }

      
    }
}
