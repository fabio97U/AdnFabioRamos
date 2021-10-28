using System;
using System.Threading.Tasks;
using AdnFabioRamos.Domain.Entities;
using AdnFabioRamos.Domain.Exceptions;
using AdnFabioRamos.Domain.Ports;

namespace AdnFabioRamos.Domain.Services
{
    [DomainService]
    public class PersonService : IDisposable
    {
        readonly IGenericRepository<Person> _repository;
        public PersonService(IGenericRepository<Person> repository)
        {
            _repository = repository ?? throw new ArgumentNullException("No person repo available");
        }

        public async Task<Person> RegisterPerson(Person person){
            if (!IsUnderAge(person.DateOfBirth)){
                return await _repository.AddAsync(person);
            }
            throw new UnderAgeException("The person you're trying to register is not of 18 year or older");
        }

        public async Task<Person> FindPerson(Guid id) {
            return await _repository.GetByIdAsync(id);
        }
        
        bool IsUnderAge(DateTime dateOfBirth){
            return ((DateTime.Now.Subtract(dateOfBirth).Days / 365) < 18) ? true : false;            
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        protected virtual void Dispose(bool disposing)
        {
            this._repository.Dispose();
        }
    }
}