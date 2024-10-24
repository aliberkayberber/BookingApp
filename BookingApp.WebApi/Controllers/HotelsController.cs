using BookingApp.Business.Operations.Hotel;
using BookingApp.Business.Operations.Hotel.Dtos;
using BookingApp.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly IHotelService _hotelService;

        public HotelsController(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHotel(int id)
        {
            var hotel = await _hotelService.GetHotel(id);

            if (hotel == null) 
                return NotFound();

            else return Ok(hotel);
        }

        [HttpGet]
        public async Task<IActionResult> HetHotels()
        {
            var hotels = await _hotelService.GetHotels();

            return Ok(hotels);

        }

        [HttpPatch("{id}/stars")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdjustHotelStars(int id,int changeTo)
        {
            var result = await _hotelService.AddjustHotelStars(id, changeTo);

            if (!result.IsSucceed)
                return NotFound();
            else
                return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            var result = await _hotelService.DeleteHotel(id);
            if (!result.IsSucceed)
                return NotFound(result.Message);
            else
                return Ok();

        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddHotel(AddHotelRequest request)
        {
            var addHotelDto = new AddHotelDto
            {
                Name = request.Name,
                Stars = request.Stars,
                Location = request.Location,
                AccomodationType = request.AccomodationType,
                FeatureIds = request.FeatureIds,
            };

            var result = await _hotelService.AddHotel(addHotelDto);

            if(!result.IsSucceed)
            {
                return BadRequest(result.Message);
            }
            else
                return Ok();

        }

        [HttpPut("{id}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> UpdateHotel(int id, UpdateHotelRequest request)
        {
            var updateHotelDto = new UpdateHotelDto
            {
                Id = id,
                Name = request.Name,
                Stars = request.Stars,
                Location = request.Location,
                AccomodationType = request.AccomodationType,
                FeatureIds = request.FeatureIds,
            };

            var result = await _hotelService.UpdateHotel(updateHotelDto);

            if(!result.IsSucceed)
            {
                return NotFound(result.Message);
            }
            else
            {
                return await GetHotel(id);
            }

        }

    }
}
