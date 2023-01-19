using Models;
using Common;
using AutoMapper;
using DataAccess.Data;
using DataAccess.ViewModel;
using Business.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using DataAccess;

namespace Business.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public OrderRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<OrderDTO> Create(OrderDTO objDTO)
        {
            try
            {
                var obj = _mapper.Map<OrderDTO, Order>(objDTO);
                _db.OrderHeaders.Add(obj.OrderHeader);
                await _db.SaveChangesAsync();
                foreach (var details in obj.OrderDetails)
                {
                    details.OrderHeaderId = obj.OrderHeader.Id;
                }
                await _db.OrderDetails.AddRangeAsync(obj.OrderDetails);
                await _db.SaveChangesAsync();

                return new OrderDTO
                {
                    OrderHeader = _mapper.Map<OrderHeader, OrderHeaderDTO>(obj.OrderHeader),
                    OrderDetails = _mapper.Map<IEnumerable<OrderDetail>, IEnumerable<OrderDetailDTO>>(obj.OrderDetails).ToList(),
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> Delete(int id)
        {
            var objHeader = await _db.OrderHeaders.SingleOrDefaultAsync(e => e.Id == id);
            if(objHeader != null)
            {
                var objDetail = _db.OrderDetails.Where(d => d.OrderHeaderId == id);
                _db.OrderDetails.RemoveRange(objDetail);
                _db.OrderHeaders.Remove(objHeader);

                return await _db.SaveChangesAsync();
            }

            return 0;
        }

        public async Task<OrderDTO> Get(int id)
        {
            Order order = new()
            {
                OrderHeader =  await _db.OrderHeaders.SingleOrDefaultAsync(e => e.Id == id) ?? new(),
                OrderDetails = await _db.OrderDetails.Where(e => e.OrderHeaderId == id).ToListAsync(),
            };

            if(order != null)
            {
                return _mapper.Map<Order, OrderDTO>(order);
            }

            return new();
        }

        public async Task<IEnumerable<OrderDTO>> GetAll(string? userId = null, string? status = null)
        {
            List<Order> OrderFromDb = new(0);
            IEnumerable<OrderHeader> orederHeaderList = await _db.OrderHeaders.ToListAsync();
            foreach (var header in orederHeaderList)
            {
                Order order = new()
                {
                    OrderHeader = header,
                    OrderDetails = await _db.OrderDetails.Where(d => d.OrderHeaderId != header.Id).ToListAsync(),
                };
                OrderFromDb.Add(order);
            }

            // do some filtering

            return _mapper.Map<IEnumerable<Order>, IEnumerable<OrderDTO>>(OrderFromDb);
        }

        public async Task<OrderHeaderDTO> MarkPaymentSuccessful(int id, string paymentIntentId)
        {
            var data = await _db.OrderHeaders.FindAsync(id);
            OrderHeaderDTO order = new();
            if (data != null)
            {
                if(data.Status == SD.Status_Pending)
                {
                    data.PaymentIntentId= paymentIntentId;
                    data.Status = SD.Status_Confirmed;
                    await _db.SaveChangesAsync();
                    order = _mapper.Map<OrderHeader, OrderHeaderDTO>(data);
                }
            }

            return order;
        }

        public async Task<OrderHeaderDTO> UpdateHeader(OrderHeaderDTO objDTO)
        {
            if (objDTO != null)
            {
                var orderHeaderFromDb = await _db.OrderHeaders.SingleOrDefaultAsync(o => o.Id == objDTO.Id) ?? new OrderHeader();
                orderHeaderFromDb.Name = objDTO.Name;
                orderHeaderFromDb.PhoneNumber = objDTO.PhoneNumber;
                orderHeaderFromDb.Carrier = objDTO.Carrier;
                orderHeaderFromDb.Tracking = objDTO.Tracking;
                orderHeaderFromDb.StreetAddress = objDTO.StreetAddress;
                orderHeaderFromDb.City = objDTO.City;
                orderHeaderFromDb.State = objDTO.State;
                orderHeaderFromDb.PostalCode = objDTO.PostalCode;
                orderHeaderFromDb.Status = objDTO.Status;
                await _db.SaveChangesAsync();

                return _mapper.Map<OrderHeader, OrderHeaderDTO>(orderHeaderFromDb);
            }

            return new();
        }

        public async Task<bool> UpdateOrderStatus(int orderId, string status)
        {
            var data = await _db.OrderHeaders.FindAsync(orderId);
            bool result = false;
            if (data != null)
            {
                data.Status = status;
                if (data.Status == SD.Status_Shipped)
                {
                    data.ShippingDate = DateTime.UtcNow;
                }
                result = (await _db.SaveChangesAsync()) > 0;
            }

            return result;
        }

        public async Task<OrderHeaderDTO> CancelOrder(int orderId)
        {
            var orderHeader = await _db.OrderHeaders.FindAsync(orderId);
            if (orderHeader == null)
            {
                return new();
            }

            if (orderHeader.Status == SD.Status_Pending)
            {
                orderHeader.Status = SD.Status_Cancelled;
                await _db.SaveChangesAsync();
            }

            if(orderHeader.Status == SD.Status_Confirmed)
            {
                orderHeader.Status = SD.Status_Refunded;
                await _db.SaveChangesAsync();
            }

            return _mapper.Map<OrderHeaderDTO>(orderHeader);
        }
    }
}
