// code: fatih.unal date: 2025-04-21T10:20:07
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FthAdmin.Application.Features.Servers.DTOs;
using FthAdmin.Application.Features.Servers.Commands;
using FthAdmin.Application.Features.Servers.Queries;

namespace FthAdmin.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ServersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10)
        {
            var query = new GetServerListQuery { PageIndex = pageIndex, PageSize = pageSize };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetServerByIdQuery { Id = id };
            var result = await _mediator.Send(query);
            return result is null ? NotFound() : Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,ServerManager")]
        public async Task<IActionResult> Create([FromBody] CreateServerCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }
    }
}
