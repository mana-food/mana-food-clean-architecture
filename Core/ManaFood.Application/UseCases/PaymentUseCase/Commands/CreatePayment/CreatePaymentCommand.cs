using MediatR;
using System;
using ManaFood.Application.Dtos;

namespace ManaFood.Application.UseCases.PaymentUseCase.Commands.CreatePayment
{
    public class CreatePaymentCommand : IRequest<CreatePaymentResponse>
    {
        public Guid OrderId { get; set; }
    }

}
