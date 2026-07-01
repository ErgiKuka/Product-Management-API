using AutoMapper;
using Product_Management_API.Data.Entities;
using Product_Management_API.DTO.Customer;
using Product_Management_API.UOW;

namespace Product_Management_API.Services.CustomerServ
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CustomerService> _logger;
        private readonly IMapper _mapper;

        public CustomerService(IUnitOfWork unitOfWork,
                              ILogger<CustomerService> logger,
                              IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<CustomerResponseDto> AddCustomerAsync(CustomerCreateDto dto)
        {
            var customer = new Customer
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Address = dto.Address,
                PhoneNumber = dto.PhoneNumber
            };

            await _unitOfWork.Customer.AddAsync(customer);
            await _unitOfWork.CompleteAsync();

            _logger.LogInformation("Customer with ID {CustomerId} added successfully.", customer.CustomerId);
            return _mapper.Map<CustomerResponseDto>(customer);
        }

        public async Task<IEnumerable<CustomerResponseDto>> GetAllCustomersAsync()
        {
            var customers = await _unitOfWork.Customer.GetAllCustomersAsync();

            _logger.LogInformation("Retrieved {CustomerCount} customers from the database.", customers.Count());

            return _mapper.Map<IEnumerable<CustomerResponseDto>>(customers).ToList();
        }

        public async Task<CustomerResponseDto?> GetCustomerByIdAsync(int id)
        {
            var customer = await _unitOfWork.Customer.GetByIdAsync(id);

            if (customer == null)
            {
                throw new KeyNotFoundException($"Customer with ID {id} not found.");
            }
            _logger.LogInformation("Retrieved customer with ID {CustomerId} from the database.", id);

            return _mapper.Map<CustomerResponseDto>(customer);
        }

        public async Task DeleteCustomerAsync(int id)
        {
            var customer = await _unitOfWork.Customer.GetByIdAsync(id);
            if (customer == null)
            {
                throw new KeyNotFoundException($"Customer with ID {id} not found.");
            }
            _unitOfWork.Customer.Delete(customer);
            await _unitOfWork.CompleteAsync();
            _logger.LogInformation("Customer with ID {CustomerId} deleted successfully.", id);
        }

        public async Task UpdateCustomerAsync(CustomerUpdateDto dto, int id)
        {
            var customer =await _unitOfWork.Customer.GetByIdAsync(id);

            if(customer == null)
            {
                throw new KeyNotFoundException($"Customer with ID {id} not found.");
            }

            customer.FirstName = dto.FirstName;
            customer.LastName = dto.LastName;
            customer.Email = dto.Email;
            customer.Address = dto.Address;
            customer.PhoneNumber = dto.PhoneNumber;

            _unitOfWork.Customer.Update(customer);
            await _unitOfWork.CompleteAsync();

            _logger.LogInformation("Customer with ID {CustomerId} updated successfully.", id);
        }
    }
}
