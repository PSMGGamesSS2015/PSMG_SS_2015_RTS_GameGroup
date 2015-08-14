namespace Assets.Scripts.Types
{
    /// <summary>
    /// The Job enum lists the jobs available for an imp.
    /// An imp must always have a job.
    /// The default setting is 'Unemployed'.
    /// </summary>

    public enum ImpType
    {
        Spearman,
        Coward,
        LadderCarrier,
        Blaster,
        Firebug,
        Schwarzenegger,

        Unemployed // Default
    }
}