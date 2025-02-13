using UnityEngine;

namespace MooshieGames.Common
{
	public class ServiceLocatorGlobalBootstrapper : ServiceLocatorBootstrapper
	{
		[SerializeField] private bool dontDestroyOnLoad = true;

		protected override void Bootstrap() => Container.ConfigureAsGlobal(dontDestroyOnLoad);
	}
}