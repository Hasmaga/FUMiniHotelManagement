using Model.RepDto;
using Model.ResDto;
using Repository;
using Repository.Interface;
using Service.Interface;

namespace Service
{
    public class RoomInformationService : IRoomInformationService
    {
        private readonly IRoomInformationRepository _roomInformationRepository;
        private readonly IHelperService _helperService;
        private readonly ICustomerRepository _customerRepository;

        public RoomInformationService(IRoomInformationRepository roomInformationRepository, IHelperService helperService, ICustomerRepository customerRepository)
        {
            _roomInformationRepository = roomInformationRepository;
            _helperService = helperService;
            _customerRepository = customerRepository;
        }

        public async Task<bool> CreateRoomInformationAsync(CreateRoomInformationResDto room)  // By Admin
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
                var isExist = await _roomInformationRepository.GetRoomInformationByRoomNumberAsync(room.RoomNumber);
                if (isExist != null)
                {
                    throw new Exception("Room number already exist");
                }
                else
                {
                    var roomInformation = new Model.Models.RoomInformation
                    {
                        RoomNumber = room.RoomNumber,
                        RoomDetailDescription = room.RoomDetailDescription,
                        RoomMaxCapacity = room.RoomMaxCapacity,
                        RoomTypeId = room.RoomTypeId,
                        RoomPricePerDay = room.RoomPricePerDay
                    };
                    return await _roomInformationRepository.CreateRoomInformationAsync(roomInformation);
                }
            } catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteRoomInformationAsync(int roomId)
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
                var room = await _roomInformationRepository.GetRoomInformationByIdAsync(roomId);
                if (room == null)
                {
                    throw new Exception("Room not found");
                }
                else
                {
                    room.RoomStatus = 0;      
                    return await _roomInformationRepository.UpdateRoomInformationAsync(room);
                }
            } catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<GetRoomInformationResDto>> GetAllRoomInformationAsync()
        {
            try
            {
                var roomList = await _roomInformationRepository.GetAllRoomInformationAsync();
                return roomList.Select(x => new GetRoomInformationResDto
                {
                    RoomId = x.RoomId,
                    RoomNumber = x.RoomNumber,
                    RoomDetailDescription = x.RoomDetailDescription,
                    RoomMaxCapacity = x.RoomMaxCapacity,
                    RoomTypeId = x.RoomTypeId,
                    RoomStatus = x.RoomStatus,
                    RoomPricePerDay = x.RoomPricePerDay
                }).ToList();
            } catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateRoomInformationAsync(UpdateRoomInformationResDto room)
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
                var roomInformation = await _roomInformationRepository.GetRoomInformationByIdAsync(room.RoomId);
                if (roomInformation == null)
                {
                    throw new Exception("Room not found");
                }
                roomInformation.RoomNumber = room.RoomNumber ?? roomInformation.RoomNumber;
                roomInformation.RoomDetailDescription = room.RoomDetailDescription ?? roomInformation.RoomDetailDescription;
                roomInformation.RoomMaxCapacity = room.RoomMaxCapacity ?? roomInformation.RoomMaxCapacity;
                roomInformation.RoomTypeId = room.RoomTypeId ?? roomInformation.RoomTypeId;
                roomInformation.RoomPricePerDay = room.RoomPricePerDay ?? roomInformation.RoomPricePerDay;
                return await _roomInformationRepository.UpdateRoomInformationAsync(roomInformation);
            } catch (Exception)
            {
                throw;
            }
        }
    }
}
