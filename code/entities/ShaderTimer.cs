using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using System;

namespace MinimalExample
{
	[Library( "ent_ShaderEntity", Description = "Basic Min/Sec Timer" )]
	//[Hammer.EditorModel("models/citizen_props/crate01.vmdl")]
	public partial class ShaderTimer : ModelEntity
	{
		[Net]
		public float TimeRemaining {get; set;}
		//public float timeRemaining = 100f;
		public bool timerIsRunning = true;

		public override void Spawn()
		{
			base.Spawn();
			//SetModel("models/citizen_props/crate01.vmdl");
			timerIsRunning = true;
			TimeRemaining = 1000;
		}

		[Event.Tick]
		void Tick()
		{
			if ( IsServer )
			{
				if ( timerIsRunning )
				{
					if ( TimeRemaining > 0 )
					{
						TimeRemaining -= Time.Delta;										
					}
					else
					{
						TimeRemaining = 0;
						timerIsRunning = false;
					}
				}
			}

			if ( IsClient ) 
			{
				//Log.Info(TimeRemaining);	
				this.SceneObject.SetValue( "shader_numbervalue", TimeRemaining );
			}
		}
	}
}
