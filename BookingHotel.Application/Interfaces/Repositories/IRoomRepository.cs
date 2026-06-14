using BookingHotel.Domain.Entities;

namespace BookingHotel.Application.Interfaces.Repositories;

public interface IRoomRepository
{
    Task<Room?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken);

    Task<bool> ExistsInHotelAsync(
        Guid hotelId,
        string roomNumber,
        CancellationToken cancellationToken);

    Task AddAsync(Room room, CancellationToken cancellationToken);

    void Update(Room room);

    void Delete(Room room);

    //Task SaveChangesAsync(CancellationToken cancellationToken);
}