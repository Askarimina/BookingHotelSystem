using BookingHotel.Domain.Constants;

namespace BookingHotel.Domain.Entities;

public sealed class Booking : BaseEntity
{
    private Booking()
    {
    }

    public Booking(
        Guid roomId,
        string userId,
        DateTime checkInDate,
        DateTime checkOutDate)
    {
        RoomId = roomId;
        SetUserId(userId);
        SetDates(checkInDate, checkOutDate);

        Status = BookingStatuses.Confirmed;
    }

    public Guid RoomId { get; private set; }

    public Room? Room { get; private set; }

    public string UserId { get; private set; } = string.Empty;

    public DateTime CheckInDate { get; private set; }

    public DateTime CheckOutDate { get; private set; }

    public string Status { get; private set; } = BookingStatuses.Confirmed;

    public void Cancel()
    {
        if (Status == BookingStatuses.Cancelled)
            throw new InvalidOperationException("Booking is already cancelled.");

        Status = BookingStatuses.Cancelled;
        MarkAsUpdated();
    }

    public bool OverlapsWith(DateTime newStart, DateTime newEnd)
    {
        return newStart < CheckOutDate && newEnd > CheckInDate;
    }

    private void SetUserId(string userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
            throw new ArgumentException("User id is required.", nameof(userId));

        UserId = userId;
    }

    private void SetDates(DateTime checkInDate, DateTime checkOutDate)
    {
        if (checkInDate.Date < DateTime.UtcNow.Date)
            throw new ArgumentException("Check-in date cannot be in the past.", nameof(checkInDate));

        if (checkOutDate.Date <= checkInDate.Date)
            throw new ArgumentException("Check-out date must be after check-in date.", nameof(checkOutDate));

        CheckInDate = checkInDate.Date;
        CheckOutDate = checkOutDate.Date;
    }
}