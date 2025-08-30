    namespace ManaFood.Application.Dtos;

    public record OrderDto : BaseDto
    {
        public required int OrderStatus { get; init; }
        public required double TotalAmount { get; init; }
        public required int PaymentMethod { get; init; }
        public required List<ProductOrderDto> Products { get; set; } = [];
    }