using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IReservationsService
    {
        Task<List<Reservations>> GetAllReservationsAsync();
        Task<Reservations> GetReservationByIdAsync(int id);
        Task<Reservations> CreateReservationAsync(Reservations reservation);
    }
}

