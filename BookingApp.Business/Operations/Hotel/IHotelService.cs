using BookingApp.Business.Operations.Hotel.Dtos;
using BookingApp.Business.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Business.Operations.Hotel
{
    public interface IHotelService
    {
        public Task<ServiceMessage> AddHotel(AddHotelDto hotel);

        public Task<HotelDto> GetHotel(int id);

        public Task<List<HotelDto>> GetHotels();

        public Task<ServiceMessage> AddjustHotelStars(int id,int changeTo);

        public Task<ServiceMessage> DeleteHotel(int id);

        public Task<ServiceMessage> UpdateHotel(UpdateHotelDto hotel);
    }
}
