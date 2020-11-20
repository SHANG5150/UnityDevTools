namespace DevTools
{
	public partial class DataExtractor
	{
		public delegate void Fill<TContainer, Data>(TContainer container, Data[] data);

		private IDataExtraction processer;

		public DataExtractor(IDataExtraction processer)
		{
			this.processer = processer;
		}

		public TContainer Extract<TContainer, TData>(Fill<TContainer, TData> fillMethod)
			where TContainer : class, new()
			where TData : class, new()
		{
			TContainer container = processer.CreateContainer<TContainer>();
			TData[] data = processer.ExtractData<TData>();

			fillMethod.Invoke(container, data);

			return container;
		}
	}
}