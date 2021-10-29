using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

namespace MinimalExample
{
	public class Health : Panel
	{
		private Label Vitals;

		public Health()
		{
			Vitals = Add.Label("100", "health");
		}

		public override void Tick()
		{
			var player = Local.Pawn;
			if ( player == null ) return;

			Vitals.Text = $"{player.Health.CeilToInt()}";
		}
	}
}
