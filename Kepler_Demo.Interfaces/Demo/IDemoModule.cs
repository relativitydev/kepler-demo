using Relativity.Kepler.Services;

namespace Kepler_Demo.Interfaces.Demo
{
	/// <summary>
	/// Demo Module Interface.
	/// </summary>
	[ServiceModule("Demo Module")]
	[RoutePrefix("Demo", VersioningStrategy.Namespace)]
	public interface IDemoModule
	{
	}
}