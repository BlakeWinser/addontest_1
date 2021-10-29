using Sandbox;
using Sandbox.UI.Construct;
using System;

namespace MinimalExample
{
	[Library( "ent_testEntity" )]
	public partial class TestEntity : Prop, IUse
	{
		bool spinning = true;

		public override void Spawn()
		{
			base.Spawn();

			SetModel( "models/heart_collectible/heart_in_love_original.vmdl_c" );
			SetupPhysicsFromModel( PhysicsMotionType.Static, false );

			GlowState = GlowStates.GlowStateOn;
			GlowDistanceStart = 0;
			GlowDistanceEnd = 1000;
			GlowColor = new Color( 1.0f, 1.0f, 1.0f, 1.0f );
			GlowActive = true;

			EnableTouch = true;
			EnableTouchPersists = true;

			CollisionGroup = CollisionGroup.Trigger;
		}

		[Event( "server.tick" )]
		public void RotationTick()
		{
			if ( spinning )
			{
				Rotation = Rotation.RotateAroundAxis( Vector3.Up, 1f );
			}
		}

		public bool OnUse( Entity user )
		{
			if ( user is not Player player ) return false;
			//player.Health = Math.Clamp(player.Health + 50f, 0f, 100f);
			//Delete();
			Log.Info( "Box Deleted" );
			Log.Info( spinning );

			spinning = !spinning;

			//return True to infinitely use health box.
			return false;
		}

		public bool IsUsable( Entity user )
		{
			return user is Player player; //&& player.Health < 100;
		}

		public override void StartTouch( Entity user )
		{
			if ( user is Player player && user.Health < 100f)
			{
				Log.Info( "TOUCHING TEH FUCKING BOX" );
				user.Health = Math.Clamp( user.Health + 50f, 0f, 100f );
				Delete();
			}
		}
	}
}
