using RestASPNET.Data.Converter.Contract;
using RestASPNET.Data.VO;
using RestASPNET.Model;

namespace RestASPNET.Data.Converter.Implementations
{
    public class BookConverter : IParser<Book, BookVO>, IParser<BookVO, Book>
    {
        public BookVO Parse(Book origin)
        {
            if (origin == null) return null;
            return new BookVO
            {
                Id = origin.Id,
                Title = origin.Title,
                Author = origin.Author,
                Launch_Date = origin.Launch_Date,
                Price = origin.Price
            };
        }
        public Book Parse(BookVO origin)
        {
            if (origin == null) return null;
            return new Book
            {
                Id = origin.Id,
                Title = origin.Title,
                Author = origin.Author,
                Launch_Date = origin.Launch_Date,
                Price = origin.Price
            };
        }

        public List<BookVO> Parse(List<Book> origin)
        {
            if (origin == null) return null;
            return origin.Select(b => Parse(b)).ToList();
        }
        public List<Book> Parse(List<BookVO> origin)
        {
            if (origin == null) return null;
            return origin.Select(b => Parse(b)).ToList();
        }

        public async Task<List<BookVO>> ParseAsync(Task<List<Book>> origin)
        {
            if (origin == null) return null;
            return origin.Result.Select(b => Parse(b)).ToList();
        }
        public async Task<List<Book>> ParseAsync(Task<List<BookVO>> origin)
        {
            if (origin == null) return null;
            return origin.Result.Select(b => Parse(b)).ToList();
        }
    }
}
