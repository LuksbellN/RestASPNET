using RestASPNET.Data.Converter.Implementations;
using RestASPNET.Data.VO;
using RestASPNET.Model;
using RestASPNET.Repository;
using RestASPNET.Repository.Generic;

namespace RestASPNET.Business.Implementations
{
    public class BookBusinessImplementation : IBookBusiness
    {
        private readonly IRepository<Book> _repository;
        private readonly BookConverter _converter;

        public BookBusinessImplementation(IRepository<Book> repository)
        {
            _repository = repository;
            _converter = new BookConverter();
        }
        public BookVO FindById(int id)
        {
            return _converter.Parse(_repository.FindById(id));
        }

        public async Task<List<BookVO>> AllAsync()
        {
            return await _converter.ParseAsync(_repository.GetAllAsync());
        }

        public BookVO Create(BookVO book)
        {
            var bookEntity = _converter.Parse(book);
            return _converter.Parse(_repository.Create(bookEntity));
        }

        public BookVO Update(BookVO book)
        {
            var bookEntity = _converter.Parse(book);
            return _converter.Parse(_repository.Update(bookEntity));
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }


    }
}
