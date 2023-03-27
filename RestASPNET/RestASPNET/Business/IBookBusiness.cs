using RestASPNET.Data.VO;

namespace RestASPNET.Business
{
    public interface IBookBusiness
    {
        BookVO FindById(int id);
        Task<List<BookVO>> AllAsync();
        BookVO Create(BookVO book);
        BookVO Update(BookVO book);
        void Delete(int id);
    }
}
