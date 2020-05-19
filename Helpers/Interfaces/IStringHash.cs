namespace WebsiteManager.Helpers.Interfaces
{
    public interface IStringHash
    {
        string ComputeSha256Hash(string rawData);
    }
}