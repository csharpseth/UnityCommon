namespace MooshieGames.Common
{
	public class ServiceLocatorSceneBootstrapper : ServiceLocatorBootstrapper
	{
		protected override void Bootstrap() => Container.ConfigureForScene();
	}
}