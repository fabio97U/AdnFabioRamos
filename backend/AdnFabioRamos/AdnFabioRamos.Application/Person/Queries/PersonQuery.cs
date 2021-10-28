using System;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace AdnFabioRamos.Application.Person.Queries
{
    public record PersonQuery([Required] Guid Id) : IRequest<PersonDto>;

}
