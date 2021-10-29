using Sandbox;
using Sandbox.UI.Construct;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Linq;

//
// You don't need to put things in a namespace, but it doesn't hurt.
//
namespace MinimalExample
{
	/// <summary>
	/// This is your game class. This is an entity that is created serverside when
	/// the game starts, and is replicated to the client. 
	/// 
	/// You can use this to create things like HUDs and declare which player class
	/// to use for spawned players.
	/// </summary>
	public partial class MinimalGame : Sandbox.Game
	{
		public MinimalGame()
		{
			if ( IsServer )
			{
				Log.Info( "My Gamemode Has Created Serverside!" );

				// Create a HUD entity. This entity is globally networked
				// and when it is created clientside it creates the actual
				// UI panels. You don't have to create your HUD via an entity,
				// this just feels like a nice neat way to do it.
				new MinimalHudEntity();
			}

			if ( IsClient )
			{
				Log.Info( "My Gamemode Has Created Clientside!" );
			}
		}

		/// <summary>
		/// A client has joined the server. Make them a pawn to play with
		/// </summary>
		public override void ClientJoined( Client client )
		{
			base.ClientJoined( client );

			var player = new MinimalPlayer( client );

			player.Respawn();
			client.Pawn = player;
		}

		[ServerCmd( "killall" )]
		public static void KillAll()
		{
			foreach ( Player player in All.OfType<Player>() )
			{
				player.TakeDamage( DamageInfo.Generic( 100f ) );
			}
		}

		[ServerCmd( "setHealth" )]
		public static void SetHealth( int health )
		{
			var caller = ConsoleSystem.Caller.Pawn;
			if ( caller == null ) return;

			caller.Health = health;
		}

		[ServerCmd( "damageTarget" )]
		public static void DamageTarget( int damage )
		{
			var caller = ConsoleSystem.Caller.Pawn;
			if ( caller == null ) return;

			var tr = Trace.Ray( caller.EyePos, caller.EyePos + caller.EyeRot.Forward * 1000 )
				.UseHitboxes()
				.Ignore( caller )
				.Run();

			if ( tr.Entity is Player victim && victim.IsValid )
			{
				victim.TakeDamage( DamageInfo.Generic( damage ) );
			}
		}

		[ServerCmd( "damageSelf" )]
		public static void DamageSelf( int damage )
		{
			var caller = ConsoleSystem.Caller.Pawn;
			if ( caller == null ) return;

			caller.TakeDamage( DamageInfo.Generic( damage ) );
			//caller.Health -= damage;
		}

		[ServerCmd( "createTestEnt" )]
		public static void CreateTestEnt()
		{
			var caller = ConsoleSystem.Caller.Pawn;
			if ( caller == null ) return;

			new TestEntity()
			{
				Position = caller.Position + caller.Rotation.Forward * 100
			};
		}

		[ServerCmd( "createBoxEnt" )]
		public static void CreateBoxEnt()
		{
			var caller = ConsoleSystem.Caller.Pawn;
			if ( caller == null ) return;

			new BoxEntity()
			{
				Position = caller.EyePos + caller.EyeRot.Forward * 50,
				Rotation = caller.EyeRot
			};
		}

		[ServerCmd( "createLightEnt" )]
		public static void CreateTestLight()
		{
			var caller = ConsoleSystem.Caller.Pawn;
			if ( caller == null ) return;

			new RLightGLight()
			{
				Position = caller.Position + caller.Rotation.Forward * 100
			};
		}

	}
}
