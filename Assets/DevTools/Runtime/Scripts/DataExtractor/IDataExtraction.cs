namespace DevTools
{
	public interface IDataExtraction
	{
		TContainer CreateContainer<TContainer>() where TContainer : class, new();

		TData[] ExtractData<TData>() where TData : class, new();
	}
}