using AutoMapper;
using Cafeinated.Backend.App.DTOs;
using Cafeinated.Backend.Core.Entities;
using Cafeinated.Backend.Infrastructure.Repositories.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace Cafeinated.Backend.App.ApiControllers;

[ApiController]
[Route("api/coffee-shop")]
public class CoffeeShopController : Controller
{
    private readonly IMapper _mapper;
    private readonly IGenericRepository<CoffeeShop> _coffeeShopRepo;
    private readonly IGenericRepository<CoffeeType> _coffeeTypeRepo;
    private readonly IUploadManager _uploadManager;

    public CoffeeShopController(IMapper mapper, IGenericRepository<CoffeeShop> coffeeShopRepo, IUploadManager uploadManager,
        IGenericRepository<CoffeeType> coffeeTypeRepo)
    {
        _mapper = mapper;
        _coffeeShopRepo = coffeeShopRepo;
        _uploadManager = uploadManager;
        _coffeeTypeRepo = coffeeTypeRepo;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        base.OnActionExecuting(context);
        _coffeeShopRepo.ChainQueryable(query => 
            query.Include(c => c.PhotoPreview));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CoffeeShopResponseDto>>> GetAll()
    {
        var coffeeShops = (await _coffeeShopRepo.GetAll()).Item;
        var response = _mapper.Map<IEnumerable<CoffeeShopResponseDto>>(coffeeShops);

        foreach (var coffeeShop in response)
        {
            var coffeeTypes = (await _coffeeTypeRepo.FindBy(ct => ct.CoffeeShopId == coffeeShop.Id)).Item;
            var coffeeList = _mapper.Map<IEnumerable<CoffeeTypeResponseDto>>(coffeeTypes);
            coffeeShop.CoffeeList = coffeeList;
        }

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<CoffeeShopResponseDto>> Add([FromQuery] CoffeeShopRequestDto coffeeShopRequestDto, IFormFile file)
    {
        var coffeeShopEntity = _mapper.Map<CoffeeShop>(coffeeShopRequestDto);

        var uploadedPhotoPreview = (await _uploadManager.Upload(file)).Item;
        coffeeShopEntity.PhotoPreviewId = uploadedPhotoPreview.Id;
        coffeeShopEntity.PhotoPreview = uploadedPhotoPreview;

        await _coffeeShopRepo.Add(coffeeShopEntity);

        var response = _mapper.Map<CoffeeShopResponseDto>(coffeeShopEntity);
        return Ok(response);
    }
}