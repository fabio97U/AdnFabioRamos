using System;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace AdnFabioRamos.Application.Person.Command {
    public record CreatePersonCommand(
        [Required, MaxLength(64)] string FirstName,
        [Required, MaxLength(64)] string LastName,
        [Required, MaxLength(128), EmailAddress] string Email,
        [Required] DateTime DateOfBirth
    ) : IRequest;

}