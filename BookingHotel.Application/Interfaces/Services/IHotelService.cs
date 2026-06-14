using BookingHotel.Application.DTOs.Hotel;

namespace BookingHotel.Application.Interfaces.Services;

public interface IHotelService
{
    Task<HotelResponse> CreateAsync(
        CreateHotelRequest request,
        CancellationToken cancellationToken);

    Task<HotelResponse?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task UpdateAsync(
        Guid id,
        UpdateHotelRequest request,
        CancellationToken cancellationToken);

    Task DeleteAsync(
        Guid id,
        CancellationToken cancellationToken);
}
