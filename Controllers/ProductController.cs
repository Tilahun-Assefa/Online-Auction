using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineAuction.Dtos.ProductDtos;
using OnlineAuction.Features.Products.Commands;
using OnlineAuction.Features.Products.Queries;
using OnlineAuction.Models;

namespace OnlineAuction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/Product        
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            // return list of products            
            return Ok(await _mediator.Send(new GetAllProductsQuery()));
        }

        // GET: api/Product/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var serviceResponse = await _mediator.Send(new GetProductByIdQuery() { Id = id });

            if (serviceResponse.Success == false)
            {
                return NotFound();
            }
            return Ok(serviceResponse);
        }

        // POST: api/Product
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult<ServiceResponse<GetProductDto>>> PostProduct(CreateProductCommand command)
        {
            try
            {
                var serviceResponse = await _mediator.Send(command);
                return CreatedAtAction("GetProduct", new { id = serviceResponse.Data.Id }, serviceResponse.Data);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // PUT: api/Product/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, UpdateProductCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            try
            {
                await _mediator.Send(command);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        // DELETE: api/Product/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                await _mediator.Send(new DeleteProductCommand() { Id = id });
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound();
            }
            
        }
    }
}