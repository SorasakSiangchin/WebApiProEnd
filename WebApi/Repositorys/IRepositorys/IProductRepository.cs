﻿using WebApi.Repositorys.IRepositorys;
using WebApiProjectEnd.Modes;

namespace WebApiProjectEnd.Repositorys.IRepositorys
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<ICollection<Product>> GetAsyncByName(string name);
        Task<(string errorMessage, string imageName)> UploadImage(IFormFileCollection formFiles);
        Task DeleteImage(string fileName);

    }
}