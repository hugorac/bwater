using Microsoft.EntityFrameworkCore;
using HotelBookingApi.Models;

namespace HotelBookingApi.Data {
    public class ApiContext : DbContext {
        public DbSet<HotelBooking> Bookings{ get; set; }
        //Klare Erklärung, warum ich den Base Konstruktor brauche
        public ApiContext(DbContextOptions<ApiContext> options ) : base(options) {
                
        }
    }
}
