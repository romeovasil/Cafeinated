using AutoMapper;
using Cafeinated.Backend.App.ApiControllers;
using Cafeinated.Backend.App.DTOs;
using Cafeinated.Backend.App.Mapper;
using Cafeinated.Backend.Core.Entities;
using Cafeinated.Backend.Infrastructure.Repositories;
using Cafeinated.Backend.Tests.Mocks;
using FactoryBot;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xunit.Abstractions;

namespace Cafeinated.Backend.Tests;

public class CoffeeTypeRepositoryTests : TestingBase
{
    private readonly IMapper _mapper;
    private readonly IEnumerable<CoffeeType> _testData = _getTestData();

    public CoffeeTypeRepositoryTests(ITestOutputHelper outputHelper) : base(outputHelper)
    {
        var mapConfig = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
        _mapper = mapConfig.CreateMapper();
    }

    [Fact]
    public async Task Get_ReturnsAllExamples()
    {
        var coffeeTypeRepository = new CoffeeTypeRepository(new TestAppDBContext<CoffeeType>(_testData).Object);
        var controller = new CoffeeTypeController(_mapper, coffeeTypeRepository);

        var actionResult = await controller.GetAll();
        var returnedResult = actionResult.Result as OkObjectResult;

        Assert.NotNull(returnedResult);
        var items = Assert.IsAssignableFrom<IEnumerable<CoffeeTypeResponseDto>>(returnedResult!.Value);
        Assert.Equal(StatusCodes.Status200OK, returnedResult.StatusCode);

        foreach (var data in _testData)
        {
            var mappedData = _mapper.Map<CoffeeTypeResponseDto>(data);
            var itemsData = items.First(i => i.Id == mappedData.Id);
            Assert.NotNull(itemsData);
            Assert.Equal(mappedData.Name, itemsData.Name);
            Assert.Equal(mappedData.PricePerUnit, itemsData.PricePerUnit);
        }
    }

    [Fact]
    public async Task Add_ReturnsCreatedObject()
    {
        var coffeeTypeRepository = new CoffeeTypeRepository(new TestAppDBContext<CoffeeType>(_testData).Object);
        var controller = new CoffeeTypeController(_mapper, coffeeTypeRepository);

        var model = new CoffeeTypeRequestDto
        {
            Name = "cappuccino",
            PricePerUnit = 2.5f,
            CoffeeShopId = Guid.NewGuid().ToString()
        };

        var added = await controller.Add(model);

        var result = added.Result as OkObjectResult;
        Assert.NotNull(result);


        var entity = result!.Value as CoffeeTypeResponseDto;
        Assert.NotNull(entity);
        Assert.NotNull(entity!.Id);
        Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
        Assert.Equal(model.Name, entity.Name);
        Assert.Equal(model.PricePerUnit, entity.PricePerUnit);
    }

    [Fact]
    public async Task GetById_ReturnsOkObjectResult()
    {
        var coffeeTypeRepository = new CoffeeTypeRepository(new TestAppDBContext<CoffeeType>(_testData).Object);
        var controller = new CoffeeTypeController(_mapper, coffeeTypeRepository);
        var firstObj = coffeeTypeRepository.DbSet.First();
        var actionResult = await controller.GetById(firstObj.Id);
        var returnedResult = actionResult.Result as OkObjectResult;

        Assert.NotNull(returnedResult);
        Assert.Equal(StatusCodes.Status200OK, returnedResult!.StatusCode);
        
        var entity = returnedResult.Value as CoffeeTypeResponseDto;
        Assert.Equal(firstObj.Id, entity!.Id);
        Assert.Equal(firstObj.Name, entity.Name);
        Assert.Equal(firstObj.PricePerUnit, entity.PricePerUnit);
    }

    [Fact]
    public async Task GetById_ReturnsNotFound()
    {
        var coffeeTypeRepository = new CoffeeTypeRepository(new TestAppDBContext<CoffeeType>(_testData).Object);
        var controller = new CoffeeTypeController(_mapper, coffeeTypeRepository);
        var id = Guid.NewGuid().ToString();
        var actionResult = await controller.GetById(id);
        var returnedResult = actionResult.Result as NotFoundResult;

        Assert.NotNull(returnedResult);
        Assert.Equal(StatusCodes.Status404NotFound, returnedResult!.StatusCode);
    }

    [Fact]
    public async Task Delete_ReturnsNotFound()
    {
        var coffeeTypeRepository = new CoffeeTypeRepository(new TestAppDBContext<CoffeeType>(_testData).Object);
        var controller = new CoffeeTypeController(_mapper, coffeeTypeRepository);
        var deleteObj = coffeeTypeRepository.DbSet.First();
        
        var actionResult = await controller.Delete(deleteObj.Id);
        var deleteResult = actionResult.Result as OkObjectResult;
        var actionResult1 = await controller.GetById(deleteObj.Id);
        var getResult = actionResult1.Result as NotFoundResult;

        Assert.NotNull(deleteResult);
        Assert.NotNull(getResult);
        Assert.Equal(StatusCodes.Status404NotFound, getResult!.StatusCode);
        
        var entity = deleteResult!.Value as CoffeeTypeResponseDto;
        var mappedObj = _mapper.Map<CoffeeTypeResponseDto>(deleteObj);
        
        Assert.Equal(mappedObj.Id, entity!.Id);
        Assert.Equal(mappedObj.Name, entity.Name);
        Assert.Equal(mappedObj.PricePerUnit, entity.PricePerUnit);
    }

    private static IEnumerable<CoffeeType> _getTestData()
    {
        Bot.DefineAuto<CoffeeType>();

        return Bot.BuildSequence<CoffeeType>().Take(10).ToList();
    }
}