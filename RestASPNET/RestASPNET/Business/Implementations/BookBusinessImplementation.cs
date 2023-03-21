using RestASPNET.Model;
using RestASPNET.Repository;
using RestASPNET.Repository.Generic;

namespace RestASPNET.Business.Implementations
{
    public class BookBusinessImplementation : IBookBusiness
    {
        private readonly IRepository<Book> _repository;

        public BookBusinessImplementation(IRepository<Book> repository)
        {
            _repository = repository;
        }
        public Book FindById(int id)
        {
            return _repository.FindById(id);
        }

        public async Task<List<Book>> AllAsync()
        {
            return await _repository.GetAllAsync();
        }
        public Book Create(Book book)
        {
            return _repository.Create(book);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public Book Update(Book book)
        {
            return _repository.Update(book);
        }
    }
}
