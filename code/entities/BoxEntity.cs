using Sandbox;
using Sandbox.UI.Construct;
using System;

namespace MinimalExample
{
	[Library( "ent_boxEntity" )]
	public partial class BoxEntity : Prop
	{
		public override void Spawn()
		{
			base.Spawn();

			SetModel( "models/citizen_props/crate01.vmdl" );
			SetupPhysicsFromModel( PhysicsMotionType.Static, false );

			EnableTouch = true;
			EnableTouchPersists = true;

			PhysicsEnabled = true;
			MoveType = MoveType.Physics;
		}
	}
}
