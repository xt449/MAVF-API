namespace MILAV.API.Classification
{
	public enum USClassification
	{
		Unclassified,
		Confidential,
		Secret,
		Top_Secret,
	}

	public static class USClassificationExtension
	{
		public static string ToString(this USClassification classification)
		{
			return classification.ToString().Replace('_', ' ');
		}
	}
}
