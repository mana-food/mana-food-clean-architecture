using AutoMapper;
using ManaFood.Application.Dtos;
using ManaFood.Application.UseCases.PaymentUseCase.Commands.CreatePayment;

namespace ManaFood.Application.Mappings
{
    public class PaymentMapper : Profile
    {
        public PaymentMapper()
        {
            CreateMap<CreatePaymentRequest, CreatePaymentCommand>();
        }
    }

}