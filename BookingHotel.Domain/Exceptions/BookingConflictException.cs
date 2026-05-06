namespace BookingHotel.Domain.Exceptions;

public sealed class BookingConflictException : DomainException
{
    public BookingConflictException()
        : base("The selected room is already booked for the requested dates.")
    {
    }
}