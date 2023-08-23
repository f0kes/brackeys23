using GameState;

namespace Services.Light
{
	public class AmbientLightSource : LightSource
	{
		protected override void Start()
		{
			GameManager.Instance.GetService<ILightService>().RegisterAmbientLightSource(this);
		}
	}
}