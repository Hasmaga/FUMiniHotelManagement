using Model.Models;
using Model.RepDto;
using Model.ResDto;
using Repository.Interface;
using Service.Interface;

namespace Service
{
    public class BookingService : IBookingService
    {
        private readonly IBookingDetailRepository _bookingDetailRepository;
        private readonly IBookingReservationRepository _bookingReservationRepository;
        private readonly IRoomInformationRepository _roomInformationRepository;
        private readonly IHelperService _helperService;
        private readonly ICustomerRepository _customerRepository;

        public BookingService(IBookingDetailRepository bookingDetailRepository, IBookingReservationRepository bookingReservationRepository, IRoomInformationRepository roomInformationRepository, IHelperService helperService, ICustomerRepository customerRepository)
        {
            _bookingDetailRepository = bookingDetailRepository;
            _bookingReservationRepository = bookingReservationRepository;
            _roomInformationRepository = roomInformationRepository;
            _helperService = helperService;
            _customerRepository = customerRepository;
        }

        public async Task<bool> CreateBookingAsync(CreateBookingResDto booking) // By Admin
        {
            try
            {
                if (!_helperService.IsTokenValid())
                {
                    throw new Exception("Unauthorized");
                }
                var AccountLoggedIn = await _customerRepository.GetCustomerByIdAsync(_helperService.GetCustomerIdFromLogged()) ?? throw new Exception("Unauthorized");
                var adminAccount = await _helperService.GetAdminAccount();
                if (AccountLoggedIn.CustomerId != adminAccount.CustomerId)
                {
                    throw new Exception("Unauthorized");
                }
                var unavailableRooms = new List<int>();
                // Check Room Availability
                foreach (var room in booking.RoomIds)
                {
                    var isAvailable = await _bookingDetailRepository.IsRoomAvailableAsync(room, booking.StartDate, booking.EndDate);
                    if (!isAvailable)
                    {
                        unavailableRooms.Add(room);
                    }
                }
                if (unavailableRooms.Count > 0)
                {
                    throw new Exception($"Room(s) {string.Join(", ", unavailableRooms)} is/are not available for the selected date range.");
                }
                // Create Booking
                var newBooking = new BookingReservation
                {
                    // BookingDate is DateOnly
                    BookingDate = DateOnly.FromDateTime(DateTime.Now),
                    CustomerId = booking.CustomerId,
                    BookingStatus = 1,
                };

                await _bookingReservationRepository.CreateBookingReservationAsync(newBooking);

                decimal totalPrice = 0;

                foreach (var room in booking.RoomIds)
                {                    
                    var days = (int)(booking.EndDate.ToDateTime(new TimeOnly(0)) - booking.StartDate.ToDateTime(new TimeOnly(0))).TotalDays;
                    var newBookingDetail = new BookingDetail
                    {
                        BookingReservationId = newBooking.BookingReservationId,
                        RoomId = room,
                        StartDate = booking.StartDate,
                        EndDate = booking.EndDate,
                        ActualPrice = (await _roomInformationRepository.GetRoomInformationByIdAsync(room) ?? new RoomInformation()).RoomPricePerDay * days,
                    };
                    totalPrice += newBookingDetail.ActualPrice ?? 0;
                    await _bookingDetailRepository.CreateBookingDetailAsync(newBookingDetail);                    
                }
                newBooking.TotalPrice = totalPrice;
                await _bookingReservationRepository.UpdateBookingReservationAsync(newBooking);
                return true;                
            } catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> CreateBookingDetailAsync(CreateBookingDetailResDto create)
        {
            try
            {
                var isAvailable = await _bookingDetailRepository.IsRoomAvailableAsync(create.RoomId, create.StartDate, create.EndDate);
                if (!isAvailable)
                {
                    throw new Exception("Room is not available for the selected date range.");
                }
                var newBookingDetail = new BookingDetail
                {
                    BookingReservationId = create.BookingReservationId,
                    RoomId = create.RoomId,
                    StartDate = create.StartDate,
                    EndDate = create.EndDate,
                    ActualPrice = (await _roomInformationRepository.GetRoomInformationByIdAsync(create.RoomId) ?? new RoomInformation()).RoomPricePerDay * (int)(create.EndDate.ToDateTime(new TimeOnly(0)) - create.StartDate.ToDateTime(new TimeOnly(0))).TotalDays,
                };
                return await _bookingDetailRepository.CreateBookingDetailAsync(newBookingDetail);
            } catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateBoookingDetailAsync(UpdateBookingDetailResDto update) // By Admin
        {
            try
            {
                if (!_helperService.IsTokenValid())
                {
                    throw new Exception("Unauthorized");
                }
                var AccountLoggedIn = await _customerRepository.GetCustomerByIdAsync(_helperService.GetCustomerIdFromLogged()) ?? throw new Exception("Unauthorized");
                var adminAccount = await _helperService.GetAdminAccount();
                if (AccountLoggedIn.CustomerId != adminAccount.CustomerId)
                {
                    throw new Exception("Unauthorized");
                }
                var isExist = await _bookingDetailRepository.GetBookingDetailByBoookingReservationAndRoomIdAsync(update.BookingReservationId, update.RoomId) ?? throw new Exception("Booking Detail not found.");
                isExist.StartDate = update.StartDate ?? isExist.StartDate;
                isExist.EndDate = update.EndDate ?? isExist.EndDate;
                isExist.ActualPrice = (await _roomInformationRepository.GetRoomInformationByIdAsync(update.RoomId) ?? new RoomInformation()).RoomPricePerDay * (int)(isExist.EndDate.ToDateTime(new TimeOnly(0)) - isExist.StartDate.ToDateTime(new TimeOnly(0))).TotalDays;
                return await _bookingDetailRepository.UpdateBookingDetailAsync(isExist);
            } catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteBookingDetailAsync(DeleteBookingDetailResDto delete) // By Admin
        {
            try
            {
                if (!_helperService.IsTokenValid())
                {
                    throw new Exception("Unauthorized");
                }
                var AccountLoggedIn = await _customerRepository.GetCustomerByIdAsync(_helperService.GetCustomerIdFromLogged()) ?? throw new Exception("Unauthorized");
                var adminAccount = await _helperService.GetAdminAccount();
                if (AccountLoggedIn.CustomerId != adminAccount.CustomerId)
                {
                    throw new Exception("Unauthorized");
                }
                var booking = await _bookingDetailRepository.GetBookingDetailByBoookingReservationAndRoomIdAsync(delete.BookingReservationId, delete.RoomId) ?? throw new Exception("Booking Detail not found.");
                return await _bookingDetailRepository.DeleteBookingDetailAsync(booking);
            } catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<GetBookingDetailResDto>> GetAllBookingDetailByReservationIdAsync(int reservationId) // By Admin  
        {
            try
            {
                if (!_helperService.IsTokenValid())
                {
                    throw new Exception("Unauthorized");
                }
                var AccountLoggedIn = await _customerRepository.GetCustomerByIdAsync(_helperService.GetCustomerIdFromLogged()) ?? throw new Exception("Unauthorized");
                var adminAccount = await _helperService.GetAdminAccount();
                if (AccountLoggedIn.CustomerId != adminAccount.CustomerId)
                {
                    throw new Exception("Unauthorized");
                }
                var bookingDetails = await _bookingDetailRepository.GetAllBookingDetailByReservationIdAsync(reservationId);
                var result = new List<GetBookingDetailResDto>();
                foreach (var booking in bookingDetails)
                {
                    result.Add(new GetBookingDetailResDto
                    {                        
                        RoomNumber = (await _roomInformationRepository.GetRoomInformationByIdAsync(booking.RoomId) ?? new RoomInformation()).RoomNumber,
                        StartDate = booking.StartDate,
                        EndDate = booking.EndDate,
                        ActualPrice = booking.ActualPrice ?? new decimal(),
                    });
                }
                return result;
            } catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<GetBookingReservationResDto>> GetListBookingReservationAsync() // By Admin
        {
            try
            {
                if (!_helperService.IsTokenValid())
                {
                    throw new Exception("Unauthorized");
                }
                var AccountLoggedIn = await _customerRepository.GetCustomerByIdAsync(_helperService.GetCustomerIdFromLogged()) ?? throw new Exception("Unauthorized");
                var adminAccount = await _helperService.GetAdminAccount();
                if (AccountLoggedIn.CustomerId != adminAccount.CustomerId)
                {
                    throw new Exception("Unauthorized");
                }
                var bookingReservations = await _bookingReservationRepository.GetAllBookingReservationAsync();
                return bookingReservations.Select(x => new GetBookingReservationResDto
                {
                    BookingReservationId = x.BookingReservationId,
                    BookingDate = x.BookingDate ?? new DateOnly(),
                    TotalPrice = x.TotalPrice ?? new decimal(),
                    CustomerId = x.CustomerId,
                    BookingStatus = x.BookingStatus ?? new byte(),
                }).ToList();
            } catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateBookingReservationStatusAsync(UpdateBookingReservationStatusResDto update)  // By Admin
        {
            try
            {
                if (!_helperService.IsTokenValid())
                {
                    throw new Exception("Unauthorized");
                }
                var AccountLoggedIn = await _customerRepository.GetCustomerByIdAsync(_helperService.GetCustomerIdFromLogged()) ?? throw new Exception("Unauthorized");
                var adminAccount = await _helperService.GetAdminAccount();
                if (AccountLoggedIn.CustomerId != adminAccount.CustomerId)
                {
                    throw new Exception("Unauthorized");
                }
                var booking = await _bookingReservationRepository.GetBookingReservationByIdAsync(update.BookingReservationId) ?? throw new Exception("Booking Reservation not found.");
                booking.BookingStatus = update.BookingStatus;
                return await _bookingReservationRepository.UpdateBookingReservationAsync(booking);
            } catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> CreateBookingByCustomerAsync(CreateBookingForCustomerResDto booking)  // By Customer
        {
            try
            {
                if (!_helperService.IsTokenValid())
                {
                    throw new Exception("Unauthorized");
                }
                var AccountLoggedIn = await _customerRepository.GetCustomerByIdAsync(_helperService.GetCustomerIdFromLogged()) ?? throw new Exception("Unauthorized");
                var unavailableRooms = new List<int>();
                // Check Room Availability
                foreach (var room in booking.RoomIds)
                {
                    var isAvailable = await _bookingDetailRepository.IsRoomAvailableAsync(room, DateOnly.FromDateTime(booking.StartDate), DateOnly.FromDateTime(booking.EndDate));
                    if (!isAvailable)
                    {
                        unavailableRooms.Add(room);
                    }
                }
                if (unavailableRooms.Count > 0)
                {
                    throw new Exception($"Room(s) {string.Join(", ", unavailableRooms)} is/are not available for the selected date range.");
                }
                // Create Booking
                var newBooking = new BookingReservation
                {
                    // BookingDate is DateOnly
                    BookingDate = DateOnly.FromDateTime(DateTime.Now),
                    CustomerId = AccountLoggedIn.CustomerId,
                    BookingStatus = 1,
                };

                await _bookingReservationRepository.CreateBookingReservationAsync(newBooking);

                decimal totalPrice = 0;

                foreach (var room in booking.RoomIds)
                {
                    var days = (int)(booking.EndDate - booking.StartDate).TotalDays;
                    var newBookingDetail = new BookingDetail
                    {
                        BookingReservationId = newBooking.BookingReservationId,
                        RoomId = room,
                        StartDate = DateOnly.FromDateTime(booking.StartDate),
                        EndDate = DateOnly.FromDateTime(booking.EndDate),
                        ActualPrice = (await _roomInformationRepository.GetRoomInformationByIdAsync(room) ?? new RoomInformation()).RoomPricePerDay * days,
                    };
                    totalPrice += newBookingDetail.ActualPrice ?? 0;
                    await _bookingDetailRepository.CreateBookingDetailAsync(newBookingDetail);
                }
                newBooking.TotalPrice = totalPrice;
                await _bookingReservationRepository.UpdateBookingReservationAsync(newBooking);
                return true;
            } catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<GetBookingReservationResDto>> GetListBookingReservationByCustomerAsync()
        {
            try
            {
                if (!_helperService.IsTokenValid())
                {
                    throw new Exception("Unauthorized");
                }
                var AccountLoggedIn = await _customerRepository.GetCustomerByIdAsync(_helperService.GetCustomerIdFromLogged()) ?? throw new Exception("Unauthorized");
                var listBookingReservations = await _bookingReservationRepository.GetListBookingReservationByCustomerIdAsync(AccountLoggedIn.CustomerId);
                return listBookingReservations.Select(x => new GetBookingReservationResDto
                {
                    BookingReservationId = x.BookingReservationId,
                    BookingDate = x.BookingDate ?? new DateOnly(),
                    TotalPrice = x.TotalPrice ?? new decimal(),
                    CustomerId = x.CustomerId,
                    BookingStatus = x.BookingStatus ?? new byte(),
                }).ToList();                
            } catch (Exception)
            {
                throw;
            }
        }
    }
}
