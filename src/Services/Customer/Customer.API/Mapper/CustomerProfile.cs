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
        //CreateMap<UpdateCustomerEvent, Entities.Customer>().ForMember(cust => cust.Id,
        //    opts => opts.MapFrom(custEvent => custEvent.CustomerId));
        CreateMap<Entities.Customer, UpdateCustomerEvent>().ForMember(evt => evt.CustomerId,
            opt => opt.MapFrom(c => c.Id));
    }
}