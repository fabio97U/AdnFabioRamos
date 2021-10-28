using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AdnFabioRamos.Domain.Services;
using MediatR;

namespace AdnFabioRamos.Application.Person.Command
{
    public class CreatePersonCommandHandler : AsyncRequestHandler<CreatePersonCommand>
    {
        readonly PersonService _personService;

        public CreatePersonCommandHandler(PersonService personService)
        {
            _personService = personService;

        }

        protected override async Task Handle(CreatePersonCommand request, CancellationToken cancellationToken)
        {
            await _personService.RegisterPerson(new Domain.Entities.Person
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                DateOfBirth = request.DateOfBirth
            });
        }
    }
}
