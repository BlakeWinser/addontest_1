using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using System;

namespace MinimalExample
{
	[Library( "panel_timerPanel" )]
	public partial class TimerPanel : WorldPanel
	{
		public float setTime;
		public bool timerIsRunning;
		//public Text
		public String text;

		public TimerPanel()
		{
			StyleSheet.Load("ui/TimerWorldPanel.scss");
			Add.Label("text");
		}

		public void DisplayTime(float timeToDisplay)
		{
			timeToDisplay += 1;

			float minutes = MathF.Floor(timeToDisplay / 60);
			float seconds = MathF.Floor(timeToDisplay % 60);

			text = string.Format("{0:00}:{1:00}", minutes, seconds);
		}
	}
}
