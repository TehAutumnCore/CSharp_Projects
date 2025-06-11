using Microsoft.EntityFrameworkCore;
using HotelBookingAPI.Models;

namespace HotelBookingAPI.Data;

public class APIContext : DbContext
{
    public DbSet<HotelBooking> Bookings { get; set; }

    public APIContext(DbContextOptions<APIContext> options) : base(options)
    {
        
    }
}