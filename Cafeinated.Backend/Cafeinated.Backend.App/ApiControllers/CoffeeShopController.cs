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
    private readonly IUploadManager _uploadManager;

    public CoffeeShopController(IMapper mapper, IGenericRepository<CoffeeShop> coffeeShopRepo, IUploadManager uploadManager)
    {
        _mapper = mapper;
        _coffeeShopRepo = coffeeShopRepo;
        _uploadManager = uploadManager;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        base.OnActionExecuting(context);
        _coffeeShopRepo.ChainQueryable(query => 
            query.Include(c => c.PhotoPreview));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CoffeeShopDto>>> GetAll()
    {
        var coffeeShops = (await _coffeeShopRepo.GetAll()).Item;
        var response = _mapper.Map<IEnumerable<CoffeeShopDto>>(coffeeShops);

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<IEnumerable<CoffeeShopDto>>> Add([FromQuery] CoffeeShopRequestDto coffeeShopRequestDto, IFormFile file)
    {
        var coffeeShopEntity = _mapper.Map<CoffeeShop>(coffeeShopRequestDto);

        var uploadedPhotoPreview = (await _uploadManager.Upload(file)).Item;
        coffeeShopEntity.PhotoPreviewId = uploadedPhotoPreview.Id;
        coffeeShopEntity.PhotoPreview = uploadedPhotoPreview;

        await _coffeeShopRepo.Add(coffeeShopEntity);

        var response = _mapper.Map<CoffeeShopDto>(coffeeShopEntity);
        return Ok(response);
    }
}