﻿using AutoMapper;
using WebApi.Modes;
using WebApi.Modes.DTOS.ImageProduct;
using WebApi.Modes.DTOS.Product;
using WebApiProjectEnd.Modes;
using WebApiProjectEnd.Modes.DTOS.Accounts;

namespace WebApiProjectEnd
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<AccountRequestDTO , Account>().ConstructUsing(src =>  new Account(){ 
                Id = src.Id ,
                FirstName = !string.IsNullOrEmpty(src.FirstName) ? new Account().FirstName : src.FirstName ,
                LastName = !string.IsNullOrEmpty(src.LastName) ? new Account().LastName : src.LastName,
                Email = !string.IsNullOrEmpty(src.Email) ? new Account().Email : src.Email,
                Password = !string.IsNullOrEmpty(src.Password) ? new Account().Password : src.Password,
                PhoneNumber = !string.IsNullOrEmpty(src.PhoneNumber) ? new Account().PhoneNumber : src.PhoneNumber,
                RoleID = (int)(src.RoleID == null ? new Account().RoleID : src.RoleID)
            }).ReverseMap();
            CreateMap<Account, AccountDTO>().ReverseMap();
            CreateMap<AccountResponse, AccountDTO>().ReverseMap();
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<ImageProduct, ImageProductDTO>().ReverseMap();
        }
    }
}