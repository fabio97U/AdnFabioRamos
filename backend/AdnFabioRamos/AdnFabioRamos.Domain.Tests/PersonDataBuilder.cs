using System;
using AdnFabioRamos.Domain.Entities;

namespace AdnFabioRamos.Domain.Tests
{

    public class PersonDataBuilder
    {
        string FirstName;
        string LastName;
        string Email;
        DateTime DateOfBirth;       

        public Person Build(){
            Person person = new(FirstName, LastName, Email, DateOfBirth);
            person.Id = Guid.NewGuid();
            return person;
        }

        public PersonDataBuilder WithDateOfBirth(DateTime dateOfBirth)
        {
            DateOfBirth = dateOfBirth;
            return this;

        }

        public PersonDataBuilder WithName(string name)
        {
            FirstName = name;
            return this;
        }

        public PersonDataBuilder WithLastName(string last)
        {
            LastName = last;
            return this;
        }

        public PersonDataBuilder WithEmail(string email)
        {
            Email = email; 
            return this;
        }


    }
}