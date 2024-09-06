namespace DAL.Enums
{
    public enum OrderProcessingState
    {
        Registered,
        Accepted,
        Sent,
        Delivered
    }

    public enum OrderCompletionState
    {
        Completed,
        Revoked
    }
}