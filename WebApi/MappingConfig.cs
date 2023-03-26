using AutoMapper;
using WebApi.Models;
using WebApi.Models.DTOS.Account;
using WebApi.Models.DTOS.Address;
using WebApi.Models.DTOS.Delivery;
using WebApi.Models.DTOS.DetailProduct;
using WebApi.Models.DTOS.EvidenceMoneyTransfer;
using WebApi.Models.DTOS.Order;
using WebApi.Models.DTOS.OrderMessage;
using WebApi.Models.DTOS.Review;
using WebApi.Models.DTOS.StatusDelivery;
using WebApi.Models.OrderAggregate;
using WebApi.Modes;
using WebApi.Modes.DTOS.Address;
using WebApi.Modes.DTOS.ImageProduct;
using WebApi.Modes.DTOS.Product;
using WebApiProjectEnd.Modes.DTOS.Accounts;

namespace WebApiProjectEnd
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<AccountRequestDTO, Account>().ConstructUsing(src => new Account()
            {
                Id = src.Id,
                FirstName = !string.IsNullOrEmpty(src.FirstName) ? new Account().FirstName : src.FirstName,
                LastName = !string.IsNullOrEmpty(src.LastName) ? new Account().LastName : src.LastName,
                Email = !string.IsNullOrEmpty(src.Email) ? new Account().Email : src.Email,
                Password = !string.IsNullOrEmpty(src.Password) ? new Account().Password : src.Password,
                PhoneNumber = !string.IsNullOrEmpty(src.PhoneNumber) ? new Account().PhoneNumber : src.PhoneNumber,
                RoleID = (int)(src.RoleID == null ? new Account().RoleID : src.RoleID)
            }).ReverseMap();
            CreateMap<Account, GoogleLoginRequestDTO>().ReverseMap();
            CreateMap<Account, AccountDTO>().ReverseMap();
            CreateMap<AccountResponse, AccountDTO>().ReverseMap();

            CreateMap<Product, ProductRequest>().ReverseMap();
            CreateMap<Product, ProductDTO>().ReverseMap();

            CreateMap<ImageProduct, ImageProductDTO>().ReverseMap();
            CreateMap<Address, CreateAddressDTO>().ReverseMap();
            CreateMap<AddressDTO, UpdateAddressDTO>().ReverseMap();
            CreateMap<UpdateAddressDTO, Address>().ReverseMap();
            CreateMap<AddressDTO, Address>().ReverseMap();

            CreateMap<DetailProduct, DetailProductDTO>().ReverseMap();

            CreateMap<Order, UpdateOrderDTO>().ReverseMap();
            CreateMap<Order, OrderDTO>().ReverseMap();

            CreateMap<EvidenceMoneyTransfer, CreateEvidenceMoneyTransferDTO>().ReverseMap();
            CreateMap<EvidenceMoneyTransfer, UpdateEvidenceMoneyTransferDTO>().ReverseMap();

            CreateMap<Delivery, DeliveryRequestDTO>().ReverseMap();

            CreateMap<StatusDelivery, StatusDeliveryDTO>().ReverseMap();
            CreateMap<Review, ReviewRequestDTO>().ReverseMap();

            CreateMap<OrderMessage , OrderMessageRequest>().ReverseMap();
        }
    }
}
