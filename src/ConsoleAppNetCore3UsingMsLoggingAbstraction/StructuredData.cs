namespace ConsoleAppNetCore3UsingMsLoggingAbstraction
{
	public class StructuredData
	{
		// Only public properties get serialized and show up in the logs.
		public string PublicStringProperty { get; set; }
		public int PublicIntProperty { get; set; }

		// The following properties/fields will not show up in the logs.
		private string PrivateStringProperty { get; set; }

		public string PublicStringField;
		private string PrivateStringField;

		public StructuredData()
		{
			PublicStringProperty = "Public property value";
			PublicIntProperty = 1;
			PrivateStringProperty = "Private property value";
			PublicStringField = "Public field value";
			PrivateStringField = "Private field value";
		}
	}
}
