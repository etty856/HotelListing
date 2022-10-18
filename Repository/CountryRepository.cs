using HotelListing.Contracts;
using HotelListing.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.Repository
{
    public class CountryRepository : GenericRepository<Country>, ICountriesRepository
    {
        private readonly HotelListingDbContext _context;

        public CountryRepository(HotelListingDbContext context) : base(context)
        {
           _context = context;
        }
        public async Task<Country> GetDetails(int id)
        {
            //using FirstOrDefault method will return null in case he wouldn't find the given id in the country table
            return await _context.Countries.Include(q => q.Hotels).FirstOrDefaultAsync(q => q.Id == id);
        }
    }
}
