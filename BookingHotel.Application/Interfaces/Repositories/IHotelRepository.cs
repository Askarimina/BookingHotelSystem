using BookingHotel.Domain.Entities;

namespace BookingHotel.Application.Interfaces.Repositories;

public interface IHotelRepository
{
    Task<Hotel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken);

    Task AddAsync(Hotel hotel, CancellationToken cancellationToken);

    void Update(Hotel hotel);

    void Delete(Hotel hotel);

    //Task SaveChangesAsync(CancellationToken cancellationToken);
}