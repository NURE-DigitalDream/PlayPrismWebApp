﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlayPrism.BLL.Abstractions.Interface;
using PlayPrism.Contracts.V1.Requests.ProductCatalogueRequests;
using PlayPrism.Contracts.V1.Responses.ProductCatalogueResponses;
using PlayPrism.Core.Domain;
using PlayPrism.Core.Models;

namespace PlayPrism.API.Controllers.V1;

/// <inheritdoc />
[Route("api/[controller]")]
public class CatalogueController : ControllerBase
{
    private readonly ICatalogueService _catalogueService;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="CatalogueController"/> class.
    /// </summary>
    /// <param name="catalogueService">The product service.</param>
    /// <param name="mapper">The automapper service.</param>
    public CatalogueController(
        ICatalogueService catalogueService,
        IMapper mapper)
    {
        _catalogueService = catalogueService;
        _mapper = mapper;
    }

    /// <summary>
    /// Loads product for catalogue.
    /// </summary>
    /// <param name="category">The category name.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    [HttpGet]
    [Route("loadProducts")]
    public async Task<IActionResult> LoadProducts(string category, CancellationToken cancellationToken)
    {
        try
        {
            var res = await _catalogueService
                .GetProductsByFiltersWithPaginationAsync(
                    new List<Filter>(),
                    new PageInfo {Number = 1, Size = 20},
                    cancellationToken);

            var response = _mapper
                .Map<IList<Product>, List<GetProductsResponse>>(res);

            return Ok(response);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return NotFound();
        }
    }

    /// <summary>
    /// The index action.
    /// </summary>
    /// <param name="productsRequest">The product request.</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>
    /// A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.
    /// The task result contains the <see cref="IActionResult"/>.
    /// </returns>
    /// <response code="200">Products</response>
    /// <response code="400">Bad request</response>
    [ProducesResponseType(typeof(IList<GetProductsResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet]
    [Route("productsFilterPage")]
    public async Task<IActionResult> GetProductsByFiltersWithPaginationAsync(
        [FromQuery] GetProductsRequest productsRequest,
        CancellationToken cancellationToken)
    {
        try
        {
            var res = await _catalogueService
                .GetProductsByFiltersWithPaginationAsync(
                    productsRequest.Filters,
                    productsRequest.PageInfo,
                    cancellationToken);

            var response = _mapper.Map<List<GetProductsResponse>>(res);

            return Ok(response);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return NotFound();
        }
    }

    /// <summary>
    /// Retrieves filters for selected category of products.
    /// </summary>
    /// <param name="category">The category name string.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    [HttpGet]
    [Route("getFilters")]
    public async Task<IActionResult> GetFilterForCategory(string category, CancellationToken cancellationToken)
    {
        try
        {
            var res = await _catalogueService
                .GetFilterForCategoryAsync(category, cancellationToken: cancellationToken);
            return Ok(res);
        }
        catch (Exception ex)
        {
            return BadRequest();
        }
    }
}