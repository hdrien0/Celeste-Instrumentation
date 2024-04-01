using System;
using Celeste;
using Microsoft.Xna.Framework;
using Monocle;

namespace Instrumentation
{
	public class PlayerStateTracker : Component
	{
		public override void Update()
		{
			Level level = base.Entity.SceneAs<Level>();

			if ( this.PlayerState.SecondsElapsed >= Instrumentation.SessionParameters.TimeoutSeconds)
			{
				Instrumentation.EndSession(this.Player);
				return;
			}
			this.PlayerState.CanDash = (this.Player.Dashes != 0);
			this.PlayerState.XVelocity = this.Player.Speed.X;
			this.PlayerState.YVelocity = this.Player.Speed.Y;
			this.PlayerState.OnGround = this.Player.OnGround(1);
			this.PlayerState.Stamina = this.Player.Stamina;
			this.PlayerState.DistanceToObjective = Vector2.Distance(this.Player.Position, this.objective);
			this.PlayerState.AngleToObjective = (this.objective - this.Player.Position).Angle() + (float) Math.PI;
			this.PlayerState.LevelDiagonalLength = (float)Math.Pow((double)(level.Bounds.Width * level.Bounds.Width + level.Bounds.Height * level.Bounds.Height), 0.5);

			for (int i = 0; i < this.PlayerState.Distances.Length; i++)
			{
				ValueTuple<int, int> valueTuple = this.Raycast(this.RaycastDirections[i], level);
				this.PlayerState.Distances[i] = valueTuple.Item1;
				this.PlayerState.DetectedTypes[i] = valueTuple.Item2;
			}

			if (Instrumentation.inSession)
            {
				InputsManager.ScheduleKeys(InputsManager.ByteToKeys(Socket.SendAndReceiveData(this.PlayerState.Serialize())[0]));
			}
			
			base.Update();
			this.FramesElapsed += 1f;
			this.PlayerState.SecondsElapsed = this.FramesElapsed / 60f;
		}

		public override void DebugRender(Camera camera)
		{
			for (int i = 0; i < this.PlayerState.Distances.Length; i++)
			{
				Draw.Line(base.Entity.Position, base.Entity.Position + this.RaycastDirections[i] * (float)this.PlayerState.Distances[i], PlayerStateTracker.RayColors[this.PlayerState.DetectedTypes[i]]);
			}
			string text = ((int)this.Player.Position.X).ToString() + ", " + ((int)this.Player.Position.Y).ToString();
			Draw.Text(Draw.DefaultFont, text, this.Player.Position, Color.White);
			base.DebugRender(camera);
		}

		public PlayerStateTracker(Player player) : base(true, true)
		{
			this.PlayerState = new PlayerState();
			this.objective = new Vector2(Instrumentation.SessionParameters.ObjectiveXCoordinate, Instrumentation.SessionParameters.ObjectiveYCoordinate);
			this.PlayerState.InitialDistanceToObjective = Vector2.Distance(player.Position, this.objective);
			this.Player = player;
			this.FramesElapsed = 0f;
		}

		public ValueTuple<int, int> Raycast(Vector2 directionUnitVector, Level level)
		{
			int num = 1;
			Vector2 value = base.Entity.Position - Vector2.UnitY;
			for (;;)
			{
				Vector2 vector = value + directionUnitVector * (float)num;
				if (!level.Bounds.Contains((int)vector.X, (int)vector.Y))
				{
					break;
				}
				if (base.Scene.CollideCheck<Solid>(vector))
				{
					goto IL_93;
				}
				if (base.Scene.CollideCheck<Spikes>(vector))
				{
					goto IL_97;
				}
				num++;
			}
			int item;
			if (!level.Session.MapData.CanTransitionTo(level, value + directionUnitVector * (float)(num + 5)))
			{
				item = 0;
				goto IL_99;
			}
			item = 1;
			goto IL_99;
			IL_93:
			item = 2;
			goto IL_99;
			IL_97:
			item = 3;
			IL_99:
			return new ValueTuple<int, int>(num, item);
		}

		public Vector2[] RaycastDirections = new Vector2[]
		{
			new Vector2(1f, 0f).SafeNormalize(),
			new Vector2(1f, -1f).SafeNormalize(),
			new Vector2(0f, -1f).SafeNormalize(),
			new Vector2(-1f, -1f).SafeNormalize(),
			new Vector2(-1f, 0f).SafeNormalize(),
			new Vector2(-1f, 1f).SafeNormalize(),
			new Vector2(0f, 1f).SafeNormalize(),
			new Vector2(1f, 1f).SafeNormalize()
		};

		public static Color[] RayColors = new Color[]
		{
			Color.Purple,
			Color.Green,
			Color.Orange,
			Color.Red
		};

		public PlayerState PlayerState;

		public Player Player;

		public float FramesElapsed;

		private Vector2 objective;
	}
}
