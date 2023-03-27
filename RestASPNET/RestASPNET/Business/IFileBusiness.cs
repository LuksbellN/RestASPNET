using RestASPNET.Data.VO;

namespace RestASPNET.Business
{
    public interface IFileBusiness
    {
        byte[] GetFile(string fileName);
        Task<FileDetailVO> SaveFileToDisk(IFormFile file);
        Task<List<FileDetailVO>> SaveFilesToDisk(IList<IFormFile> file);
    }
}
