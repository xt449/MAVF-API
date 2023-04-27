namespace MILAV.API.Classification
{
    public enum NATOClassification
    {
        UNCLASSIFIED,
        RESTRICTED,
        CONFIDENTIAL,
        SECRET,
        COSMIC_TOP_SECRET,
    }

    public static class NATOClassificationExtension
    {
        public static string ToString(this NATOClassification classification)
        {
            return classification.ToString().Replace('_', ' ');
        }
    }
}
