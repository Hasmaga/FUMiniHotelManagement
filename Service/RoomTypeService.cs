using Model.Models;
using Model.RepDto;
using Model.ResDto;
using Repository;
using Repository.Interface;
using Service.Interface;

namespace Service
{
    public class RoomTypeService : IRoomTypeService
    {
        private readonly IRoomTypeRepository _roomTypeRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IHelperService _helperService;

        public RoomTypeService(IRoomTypeRepository roomTypeRepository, ICustomerRepository customerRepository, IHelperService helperService)
        {
            _roomTypeRepository = roomTypeRepository;
            _customerRepository = customerRepository;
            _helperService = helperService;
        }

        public async Task<bool> CreateRoomTypeAsync(CreateRoomTypeResDto roomType)
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
                var newRoomType = new RoomType
                {
                    RoomTypeName = roomType.RoomTypeName,
                    TypeDescription = roomType.TypeDescription,
                    TypeNote = roomType.TypeNote
                };
                return await _roomTypeRepository.CreateRoomTypeAsync(newRoomType);
            } catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<GetRoomTypeResDto>> GetAllRoomTypeAsync()
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
                var roomTypes = await _roomTypeRepository.GetAllRoomTypeAsync();
                return roomTypes.Select(roomType => new GetRoomTypeResDto
                {
                    RoomTypeId = roomType.RoomTypeId,
                    RoomTypeName = roomType.RoomTypeName,
                    TypeDescription = roomType.TypeDescription,
                    TypeNote = roomType.TypeNote
                }).ToList();
            } catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateRoomTypeAsync(UpdateRoomTypeResDto roomType)
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
                var existingRoomType = await _roomTypeRepository.GetRoomTypeByIdAsync(roomType.RoomTypeId);
                if (existingRoomType == null)
                {
                    throw new Exception("Room type not found");
                }
                existingRoomType.RoomTypeName = roomType.RoomTypeName ?? existingRoomType.RoomTypeName;
                existingRoomType.TypeDescription = roomType.TypeDescription ?? existingRoomType.TypeDescription;
                existingRoomType.TypeNote = roomType.TypeNote ?? existingRoomType.TypeNote;
                return await _roomTypeRepository.UpdateRoomTypeAsync(existingRoomType);
            } catch (Exception)
            {
                throw;
            }
        }
    }
}
