using AutoMapper;
using Cafeinated.Backend.App.DTOs;
using Cafeinated.Backend.Core.Entities;
using Cafeinated.Backend.Infrastructure.Repositories.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Cafeinated.Backend.App.ApiControllers;

[ApiController]
[Route("api/order")]
public class OrderController : Controller
{
    private readonly IMapper _mapper;
    private readonly IGenericRepository<Order> _orderRepo;
    private readonly IGenericRepository<CoffeeShop> _coffeeShopRepo;
    private readonly UserManager<ApplicationUser> _userManager;

    public OrderController(IMapper mapper, IGenericRepository<Order> orderRepo, IGenericRepository<CoffeeShop> coffeeShopRepo, UserManager<ApplicationUser> userManager)
    {
        _mapper = mapper;
        _orderRepo = orderRepo;
        _coffeeShopRepo = coffeeShopRepo;
        _userManager = userManager;
    }
    
    [HttpPost]
    public async Task<ActionResult<Order>> Add([FromBody] OrderRequestDto orderRequest)
    {
        var orderEntity = _mapper.Map<Order>(orderRequest);
        var addedOrder = (await _orderRepo.Add(orderEntity)).Item;
        
        var coffeeShop = (await _coffeeShopRepo.GetEntityBy(cs => cs.Id == orderRequest.CoffeeShopId)).Item;
        var orderResponse = _mapper.Map<OrderResponseDto>(addedOrder);
        orderResponse.CoffeeShopName = coffeeShop.Name;

        return Ok(orderResponse);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderResponseDto>>> GetAllByUserId([FromQuery] string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user is null)
        {
            return BadRequest("No user found with the given id.");
        }
        
        var orders = (await _orderRepo.FindBy(o => o.ApplicationUserId == userId)).Item;
        var response = new List<OrderResponseDto>();

        foreach (var order in orders)
        {
            var coffeeShop = (await _coffeeShopRepo.GetEntityBy(cs => cs.Id == order.CoffeeShopId)).Item;
            var orderResponse = _mapper.Map<OrderResponseDto>(order);
            orderResponse.CoffeeShopName = coffeeShop.Name;
            response.Add(orderResponse);
        }

        return Ok(response);
    }
}