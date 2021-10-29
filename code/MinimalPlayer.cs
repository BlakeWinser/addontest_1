using Sandbox;
using System;
using System.Linq;

namespace MinimalExample
{
	partial class MinimalPlayer : Player
	{
		private DamageInfo lastDamage;
		public Clothing.Container Clothing = new();

		public TraceResult hit;
		public Entity grabbedEntity;
		public Transform entityGrabPos;

		public MinimalPlayer()
		{

		}

		public MinimalPlayer( Client cl ) : this()
		{
			Clothing.LoadFromClient( cl );
		}

		public override void Respawn()
		{
			SetModel( "models/citizen/citizen.vmdl" );

			//
			// Use WalkController for movement (you can make your own PlayerController for 100% control)
			//
			Controller = new WalkController();

			//
			// Use StandardPlayerAnimator  (you can make your own PlayerAnimator for 100% control)
			//
			Animator = new StandardPlayerAnimator();

			//
			// Use ThirdPersonCamera (you can make your own Camera for 100% control)
			//
			Camera = new FirstPersonCamera();

			EnableAllCollisions = true;
			EnableDrawing = true;
			EnableHideInFirstPerson = true;
			EnableShadowInFirstPerson = true;
			Health = 100;

			Clothing.DressEntity( this );

			base.Respawn();
		}

		/// <summary>
		/// Called every tick, clientside and serverside.
		/// </summary>
		public override void Simulate( Client cl )
		{
			base.Simulate( cl );

			Clothing.LoadFromClient( cl );

			//
			// If you have active children (like a weapon etc) you should call this to  
			// simulate those too.
			//
			SimulateActiveChild( cl, ActiveChild );

			//
			// Use command (Default Bind: E) 
			//
			TickPlayerUse();

			if ( Input.Pressed( InputButton.View ) )
			{
				if ( Camera is ThirdPersonCamera )
				{
					Camera = new FirstPersonCamera();
				}
				else
				{
					Camera = new ThirdPersonCamera();
				}
			}

			//
			// If we're running serverside and Attack1 was just pressed, trigger command.
			//
			if ( IsServer && Input.Pressed( InputButton.Attack1 ) )
			{
				Log.Info( "> Left Click Pressed" );
			}

			if ( IsServer && Input.Down(InputButton.Attack1) )
			{
				//Log.Info( "> Left Click Held" );
				PhysicsObjectDrag();
			}

			if ( IsServer && Input.Pressed( InputButton.Attack2 ) )
			{
				Log.Info( "Right Click Pressed" );
				Log.Info(grabbedEntity.Transform.Position);
			}
		}

		public override void OnKilled()
		{
			base.OnKilled();

			BecomeRagdollOnClient( Velocity, lastDamage.Flags, lastDamage.Position, lastDamage.Force, GetHitboxBone( lastDamage.HitboxIndex ) );

			EnableDrawing = false;
		}

		public static void DamageTarget( int damage )
		{
			var caller = ConsoleSystem.Caller.Pawn;
			if ( caller == null ) return;

			caller.TakeDamage( DamageInfo.Generic( damage ) );
			caller.Health -= damage;
		}

		public void PhysicsObjectDrag()
		{
			hit = Trace.Ray( EyePos, EyePos + EyeRot.Forward * 2000 )
			.Ignore(this)
			.Run();

			if (!hit.Entity.IsValid) return;
			if (hit.Entity.IsWorld) return;

			if (hit.Hit)
			{
				DebugOverlay.Sphere( hit.EndPos, 2.0f, Color.Red, duration: 10.0f );
				Log.Info(hit.Entity.Name);

				grabbedEntity = hit.Entity;
				grabbedEntity.Velocity = 15 * (hit.EndPos - grabbedEntity.Transform.Position);
			}
		}
	}
}
