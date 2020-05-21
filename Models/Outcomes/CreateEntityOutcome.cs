namespace WebsiteManager.Models.Outcomes
{
    public enum CreateEntityOutcome
    {
        Success,
        EntityAlreadyExists,
        MissingFullEntityData,
        CreateFailed
    }
}
