using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelListing.Data;
using HotelListing.Models.Country;
using AutoMapper;
using HotelListing.Contracts;

namespace HotelListing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICountriesRepository _countriesRepository;

        public CountriesController(IMapper mapper, ICountriesRepository countriesRepository)
        {
            _mapper = mapper;
            _countriesRepository = countriesRepository;
        }

        // GET: api/Countries
        //Getting all country from the database
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetCountryDto>>> GetCountries()
        {
            var countries = await _countriesRepository.GetAllAsync();
            var records=_mapper.Map<List<GetCountryDto>>(countries);
            return Ok(records);
        }

        // GET: api/Countries/{id}
        //example: api/Countries/2
        //Getting a specified country by its Id from the database
        [HttpGet("{id}")]
        public async Task<ActionResult<CountryDto>> GetCountry(int id)
        {
            var country = await _countriesRepository.GetDetails(id);

            if (country == null)
            {
                return NotFound();
            }

            var countryDto=_mapper.Map<CountryDto>(country);

            return Ok(countryDto);
        }

        // PUT: api/Countries/{id}
        //example: api/Countries/2
        //Updating a country in the database
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCountry(int id, UpdateCountryDto updateCountry)
        {
            if (id != updateCountry.Id)
            {
                return BadRequest("Invalid record Id");
            }

            var country = await _countriesRepository.GetAsync(id);

            if (country == null)
            {
                return NotFound("No such id exists");
            }

            _mapper.Map(updateCountry, country);
            try
            {
                await _countriesRepository.UpdateAsync(country);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CountryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            //this is a good response, I updated the record,  but I dont have anything to return back
            return NoContent();
        }

        // POST: api/Countries
        //Adding a country to the database
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Country>> PostCountry(CreateCountryDto createCountry)
        {             
            var country=_mapper.Map<Country>(createCountry);

            await _countriesRepository.AddAsync(country);

            return CreatedAtAction("GetCountry", new { id = country.Id }, country);
        }

        // DELETE: api/Countries/{id}
        //Deleting a specified country from the database by its Id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            var country = await _countriesRepository.GetAsync(id);
            if (country == null)
            {
                return NotFound("No such id exists");
            }

            await _countriesRepository.DeleteAsync(id);

            return NoContent();
        }

        //A helping method for knowing if a country with the given Id existss 
        private async Task<bool> CountryExists(int id)
        {
            return await _countriesRepository.Exists(id);
        }
    }
}