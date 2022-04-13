using AutoMapper;
using Customer.API.Models;
using EventBus.Messages.Events;

namespace Customer.API.Mapper;

public class CustomerProfile:Profile
{
    public CustomerProfile()
    {
        CreateMap<CreateCustomerModel, Entities.Customer>().ForMember(x => x.Id, opt => opt.Ignore());

        CreateMap<UpdateCustomerModel, Entities.Customer>();
        CreateMap<UpdateCustomerEvent, Entities.Customer>().ReverseMap();
    }
}