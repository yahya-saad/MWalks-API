namespace MWalks.API.Utilities
{
    public static class ImageCoverter
    {
        public static async Task<byte[]> ToByteArray(IFormFile image)
        {
            using var dataStream = new MemoryStream();
            await image.CopyToAsync(dataStream);
            return dataStream.ToArray();
        }
    }
}
