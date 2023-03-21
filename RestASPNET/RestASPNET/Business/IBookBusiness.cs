using RestASPNET.Model;

namespace RestASPNET.Business
{
    public interface IBookBusiness
    {
        Book FindById(int id);
        Task<List<Book>> AllAsync();
        Book Create(Book book);
        Book Update(Book book);
        void Delete(int id);
    }
}
