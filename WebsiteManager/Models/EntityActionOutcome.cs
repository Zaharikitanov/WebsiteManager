namespace WebsiteManager.Models
{
    public enum EntityActionOutcome
    {
        Success,
        MissingFullEntityData,
        CreateFailed,
        EntityNotFound,
        UpdateFailed
    }
}
