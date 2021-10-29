using Sandbox;
using System;
using System.Linq;

namespace MinimalExample
{
	[Library( "ent_RLGLEntity" )]
	public partial class RLightGLight : Prop
	{
		bool spinning = true;

		public override void Spawn()
		{
			base.Spawn();

			SetModel( "models/citizen_props/crate01.vmdl" );
			SetupPhysicsFromModel( PhysicsMotionType.Static, false );

			EnableTouch = true;
			EnableTouchPersists = true;
		}

		[Event( "server.tick" )]
		public void RotationTick()
		{
			if (spinning)
			{
				Rotation = Rotation.Slerp(Rotation.From(0,0,0), Rotation.From(0,180,0), (MathF.Sin(Time.Now) + 1.0f) / 2.0f);
			}
		}
	}
}
