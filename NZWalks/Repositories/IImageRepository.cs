namespace MWalks.API.Repositories
{
    public interface IImageRepository
    {
        Task<Image> Upload(UploadImageDto dto);
        Task<bool> DeleteImage(Guid id);

    }
}
