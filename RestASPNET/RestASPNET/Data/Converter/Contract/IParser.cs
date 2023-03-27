using RestASPNET.Data.VO;
using RestASPNET.Model;

namespace RestASPNET.Data.Converter.Contract
{
    public interface IParser<O, D>
    {
        D Parse(O origin);
        List<D> Parse(List<O> origin);
        Task<List<D>> ParseAsync(Task<List<O>> origin);
    }
}
