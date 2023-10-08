using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WebApi.Extenstions;
using WebApi.Models;
using WebApi.Modes.DTOS.Product;
using WebApi.RequestHelpers;
using WebApiProjectEnd.Repositorys.IRepositorys;


namespace WebApiProjectEnd.Endpoints
{
    public static class ProductEndpoints
    {
        public static void ConfigureProductEndpoints(this WebApplication app)
        {
            app.MapPost("/products", GetAllProduct)
                .WithName("GetProducts")
                .Accepts<ProductParams>("application/json")
                .Produces<APIResponse>(200) ;

            app.MapGet("/product/{id}", GetProduct)
                .WithName("GetProduct")
                .Produces<APIResponse>(200);

            app.MapGet("/product/accountId", GetProductByAccountId)
                .WithName("GetProductByAccountId")
                .Produces<APIResponse>(200);

            app.MapPost("/product", CreateProduct)
                .WithName("CreateProduct")
                .Accepts<ProductRequest>("multipart/form-data")
                .Produces<APIResponse>(200)
                .Produces(400);

            app.MapGet("/product/rare", GetProductRare)
                .WithName("GetProductRare")
                .Produces<APIResponse>(200);

            app.MapGet("/product/recommend", GetProductRecommend)
                .WithName("GetProductRecommend")
                .Produces<APIResponse>(200);

            app.MapPost("/product/put", UpdateProduct)
                .WithName("UpdateProduct")
                .Accepts<ProductRequest>("multipart/form-data")
                .Produces<APIResponse>(200)
                .Produces(400)
                .Produces(404);

            app.MapGet("/product/name", GetProductByNames)
                .WithName("GetProductsByName")
                .Produces<APIResponse>(200);

            app.MapPost("/product/{id}", DeleteProduct);
        }

       
        private static async Task<IResult> GetAllProduct(HttpResponse httpResponse, IProductRepository _productRepo, ProductParams productParams)
        {
            APIResponse response = new();

            var query = await _productRepo.GetAllAsync(productParams);
            var products = await PagedList<ProductDTO>.ToPagedList(query,
                          productParams.PageNumber, productParams.PageSize);

            httpResponse.AddPaginationHeader(products.MetaData);

            response.Result = products;
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }

        private static async Task<IResult> GetProductRecommend(IProductRepository _productRepo, int num)
        {
            APIResponse response = new();
            var products = await _productRepo.GetRecommendAsync(num);
            response.Result = products;
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }
        private static async Task<IResult> GetProductRare(IProductRepository _productRepo)
        {
            APIResponse response = new();
            var products = await _productRepo.GetRareAsync();
            response.Result = products;
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }
        private static async Task<IResult> GetProduct(IProductRepository _productRepo, string id)
        {
            APIResponse response = new();
            var data = await _productRepo.GetAsync(id);
            if (data == null) return Results.Ok("ไม่มีข้อมูล");
            response.Result = data;
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }

        [Authorize(Roles = "admin,seller")]
        private static async Task<IResult> CreateProduct(IMapper _mapper, IProductRepository _productRepo, IAccountRepository _accountRepo, ProductRequest model)
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
            product.ImageUrl = imageName;
            product.Created = DateTime.Now;
            await _productRepo.CreateAsync(product);
            await _productRepo.SaveAsync();
            response.Result = product;
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.Created;
            return Results.Ok(response);
        }

        [Authorize(Roles = "admin,seller")]
        private async static Task<IResult> UpdateProduct(ApplicationDbContext db ,IMapper _mapper, IProductRepository _productRepo, ProductRequest model)
        {
            APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };
            var product = await db.Products.AsNoTracking().FirstOrDefaultAsync(e => e.Id.Equals(model.Id));
            if (product == null) return Results.NotFound();
            #region จัดการรูปภาพ
            (string erorrMesage, string imageName) = await _productRepo.UploadImage(model.FormFiles);
            if (!string.IsNullOrEmpty(erorrMesage))
            {
                response.ErrorMessages.Add(erorrMesage);
                return Results.BadRequest(response);
            }

            var data = _mapper.Map<Product>(model);
            if (!string.IsNullOrEmpty(imageName))
                 data.ImageUrl = imageName;
            else data.ImageUrl = product.ImageUrl;
            #endregion

            data.LastUpdate = DateTime.Now;
            await _productRepo.UpdateAsync(data);
            await _productRepo.SaveAsync();
            response.Result = data;
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }

        [Authorize(Roles = "admin,seller")]
        private async static Task<IResult> DeleteProduct(IMapper _mapper , IProductRepository _productRepo, string id)
        {
            APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };
            var product = await _productRepo.GetAsync(id);

            if (product != null)
            {
                await _productRepo.RemoveAsync(_mapper.Map<Product>(product));
                await _productRepo.SaveAsync();
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
            var data = await _productRepo.GetAsyncByNameAsync(name);
            if (data == null) return Results.Ok("ไม่มีข้อมูล");
            response.Result = data;
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }

        private static async Task<IResult> GetProductByAccountId(IProductRepository _productRepo, string accountId)
        {
            APIResponse response = new();
            var data = await _productRepo.GetProductByAccountIdAsync();
            response.Result = data;
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }
    }
}
