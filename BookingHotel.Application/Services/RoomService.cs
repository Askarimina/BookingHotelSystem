using BookingHotel.Application.DTOs.Room;
using BookingHotel.Application.Interfaces.Repositories;
using BookingHotel.Application.Interfaces.Services;
using BookingHotel.Domain.Entities;

namespace BookingHotel.Application.Services;

public sealed class RoomService : IRoomService
{
    private readonly IRoomRepository _roomRepository;
    private readonly IHotelRepository _hotelRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RoomService(
        IRoomRepository roomRepository,
        IHotelRepository hotelRepository,
        IUnitOfWork unitOfWork)
    {
        _roomRepository = roomRepository;
        _hotelRepository = hotelRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<RoomResponse> CreateAsync(
        CreateRoomRequest request,
        CancellationToken cancellationToken)
    {
        var hotelExists = await _hotelRepository.ExistsAsync(
            request.HotelId,
            cancellationToken);

        if (!hotelExists)
            throw new KeyNotFoundException("Hotel was not found.");

        var roomAlreadyExists = await _roomRepository.ExistsInHotelAsync(
            request.HotelId,
            request.RoomNumber,
            cancellationToken);

        if (roomAlreadyExists)
            throw new InvalidOperationException("Room number already exists in this hotel.");

        var room = new Room(
            request.HotelId,
            request.RoomNumber,
            request.Capacity,
            request.PricePerNight,
            request.Description);

        await _roomRepository.AddAsync(room, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return MapToResponse(room);
    }

    public async Task<RoomResponse?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        var room = await _roomRepository.GetByIdAsync(id, cancellationToken);

        return room is null ? null : MapToResponse(room);
    }

    public async Task UpdateAsync(
        Guid id,
        UpdateRoomRequest request,
        CancellationToken cancellationToken)
    {
        var room = await _roomRepository.GetByIdAsync(id, cancellationToken);

        if (room is null)
            throw new KeyNotFoundException("Room was not found.");

        var roomNumberExists = await _roomRepository.ExistsInHotelAsync(
            room.HotelId,
            request.RoomNumber,
            cancellationToken);

        if (roomNumberExists && room.RoomNumber != request.RoomNumber)
            throw new InvalidOperationException("Room number already exists in this hotel.");

        room.Update(
            request.RoomNumber,
            request.Capacity,
            request.PricePerNight,
            request.Description);

        _roomRepository.Update(room);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        var room = await _roomRepository.GetByIdAsync(id, cancellationToken);

        if (room is null)
            throw new KeyNotFoundException("Room was not found.");

        _roomRepository.Delete(room);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    private static RoomResponse MapToResponse(Room room)
    {
        return new RoomResponse(
            room.Id,
            room.HotelId,
            room.RoomNumber,
            room.Capacity,
            room.PricePerNight,
            room.Description,
            room.CreatedAtUtc,
            room.UpdatedAtUtc);
    }
}
