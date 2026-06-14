using BookingHotel.Application.DTOs.Room;

namespace BookingHotel.Application.Interfaces.Services;

public interface IRoomService
{
    Task<RoomResponse> CreateAsync(
        CreateRoomRequest request,
        CancellationToken cancellationToken);

    Task<RoomResponse?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task UpdateAsync(
        Guid id,
        UpdateRoomRequest request,
        CancellationToken cancellationToken);

    Task DeleteAsync(
        Guid id,
        CancellationToken cancellationToken);
}
