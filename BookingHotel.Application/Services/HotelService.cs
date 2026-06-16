using BookingHotel.Application.DTOs.Hotel;
using BookingHotel.Application.Interfaces.Repositories;
using BookingHotel.Application.Interfaces.Services;
using BookingHotel.Domain.Entities;

namespace BookingHotel.Application.Services;

public sealed class HotelService : IHotelService
{
    private readonly IHotelRepository _hotelRepository;
    private readonly IUnitOfWork _unitOfWork;

    public HotelService(
        IHotelRepository hotelRepository,
        IUnitOfWork unitOfWork)
    {
        _hotelRepository = hotelRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<HotelResponse> CreateAsync(
        CreateHotelRequest request,
        CancellationToken cancellationToken)
    {
        var hotel = new Hotel(
            request.Name,
            request.City,
            request.Address,
            request.Description);

        await _hotelRepository.AddAsync(hotel, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return MapToResponse(hotel);
    }

    public async Task<HotelResponse?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        var hotel = await _hotelRepository.GetByIdAsync(id, cancellationToken);

        return hotel is null ? null : MapToResponse(hotel);
    }

    public async Task UpdateAsync(
        Guid id,
        UpdateHotelRequest request,
        CancellationToken cancellationToken)
    {
        var hotel = await _hotelRepository.GetByIdAsync(id, cancellationToken);

        if (hotel is null)
            throw new KeyNotFoundException("Hotel was not found.");

        hotel.Update(
            request.Name,
            request.City,
            request.Address,
            request.Description);

        _hotelRepository.Update(hotel);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        var hotel = await _hotelRepository.GetByIdAsync(id, cancellationToken);

        if (hotel is null)
            throw new KeyNotFoundException("Hotel was not found.");

        _hotelRepository.Delete(hotel);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    private static HotelResponse MapToResponse(Hotel hotel)
    {
        return new HotelResponse(
            hotel.Id,
            hotel.Name,
            hotel.City,
            hotel.Address,
            hotel.Description,
            hotel.CreatedAtUtc,
            hotel.UpdatedAtUtc);
    }
}  
