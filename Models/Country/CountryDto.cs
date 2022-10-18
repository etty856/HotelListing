using HotelListing.Models.Hotel;

namespace HotelListing.Models.Country
{
    public class CountryDto
    {
        public int Id { get; set; }
        public int Name { get; set; }
        public int ShortName { get; set; }
        public List<HotelDto> Hotels { get; set; }
    }
}
