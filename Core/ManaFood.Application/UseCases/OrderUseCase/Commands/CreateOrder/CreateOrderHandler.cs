using AutoMapper;
using ManaFood.Application.Dtos;
using ManaFood.Domain.Entities;
using ManaFood.Application.Interfaces;
using ManaFood.Domain.Enums;
using MediatR;

namespace ManaFood.Application.UseCases.OrderUseCase.Commands.CreateOrder;

public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, OrderDto>
{
    private readonly IOrderRepository _repository;
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateOrderHandler(IOrderRepository repository, IProductRepository productRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _repository = repository;
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<OrderDto> Handle(CreateOrderCommand request,
        CancellationToken cancellationToken)
    {

        var productIds = request.Products.Select(p => p.ProductId).ToList();
        var products = await _productRepository.GetByIds(productIds, cancellationToken);

        var orderProducts = request.Products.Select(requestProduct =>
        {
            var product = products.FirstOrDefault(p => p.Id == requestProduct.ProductId);
            if (product == null)
                throw new ArgumentException($"Produto {requestProduct.ProductId} não encontrado");

            return new OrderProduct
            {
                ProductId = product.Id,
                Product = product,
                Quantity = requestProduct.Quantity
            };
        }).ToList();

        var order = new Order
        {
            PaymentMethod = (PaymentMethod)request.PaymentMethod,
            OrderConfirmationTime = null,
            Products = orderProducts
        };

        order.CalculateTotal();

        await _repository.Create(order, cancellationToken);
        await _unitOfWork.Commit(cancellationToken);

        return _mapper.Map<OrderDto>(order);
    }
}