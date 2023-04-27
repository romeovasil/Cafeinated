using AutoMapper;
using Cafeinated.Backend.App.DTOs;
using Cafeinated.Backend.Core.Entities;
using Cafeinated.Backend.Infrastructure.Repositories.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Cafeinated.Backend.App.ApiControllers;


[ApiController]
[Route("api/coffee-type")]
public class CoffeeTypeController : Controller
{
    private readonly IMapper _mapper;
    private readonly IGenericRepository<CoffeeType> _coffeeTypeRepo;

    public CoffeeTypeController(IMapper mapper, IGenericRepository<CoffeeType> coffeeTypeRepo)
    {
        _mapper = mapper;
        _coffeeTypeRepo = coffeeTypeRepo;
    }

    [HttpPost]
    public async Task<ActionResult<CoffeeTypeResponseDto>> Add([FromBody] CoffeeTypeRequestDto coffeeTypeRequestDto)
    {
        var coffeeTypeEntity = _mapper.Map<CoffeeType>(coffeeTypeRequestDto);
        var addedCoffeeType = (await _coffeeTypeRepo.Add(coffeeTypeEntity)).Item;
        var response = _mapper.Map<CoffeeTypeResponseDto>(addedCoffeeType);

        return Ok(response);
    }

    [HttpDelete("delete/{id}")]
    public async Task<ActionResult<CoffeeTypeResponseDto>> Delete([FromRoute] string id)
    {
        var deletedCoffeeType = (await _coffeeTypeRepo.Delete(id)).Item;
        var response = _mapper.Map<CoffeeTypeResponseDto>(deletedCoffeeType);
        return Ok(response);
    }
}