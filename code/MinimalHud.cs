using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

//
// You don't need to put things in a namespace, but it doesn't hurt.
//
namespace MinimalExample
{
	/// <summary>
	/// This is the HUD entity. It creates a RootPanel clientside, which can be accessed
	/// via RootPanel on this entity, or Local.Hud.
	/// </summary>
	public partial class MinimalHudEntity : Sandbox.HudEntity<RootPanel>
	{
		public MinimalHudEntity()
		{
			if ( IsClient )
			{
				RootPanel.SetTemplate("/minimalhud.html");

				RootPanel.AddChild<Health>();
				RootPanel.AddChild<Crosshair>();
				RootPanel.AddChild<Scoreboard<ScoreboardEntry>>();
			}
		}		
	}
}
