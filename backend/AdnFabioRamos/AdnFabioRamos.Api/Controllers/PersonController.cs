using System;
using System.Threading.Tasks;
using AdnFabioRamos.Application.Person.Command;
using AdnFabioRamos.Application.Person.Queries;
using AdnFabioRamos.Domain.Entities;
using AdnFabioRamos.Domain.Ports;
using AdnFabioRamos.Domain.Services;
using AdnFabioRamos.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AdnFabioRamos.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {

        private readonly IMediator _Mediator;       

        public PersonController(IMediator mediator)
        {
            _Mediator = mediator;           
        }


        [HttpGet("{id}")]
        public async Task<PersonDto> GetPerson(Guid id) => await _Mediator.Send(new PersonQuery(id));

        [HttpPost]
        public async Task NewPerson(CreatePersonCommand person) => await _Mediator.Send(person);

    }
}