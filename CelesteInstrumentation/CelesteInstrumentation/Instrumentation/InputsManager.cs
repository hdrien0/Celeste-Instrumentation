using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using Celeste;

namespace Instrumentation
{
	public static class InputsManager
	{
		public static void ScheduleKey(Keys key)
		{
			InputsManager.NextKeysDown.Add((int)key);
		}

		public static bool Pressed(Keys key)
		{
			return InputsManager.KeysDown.Contains((int)key) && !InputsManager.PreviousKeysDown.Contains((int)key);
		}

		public static void Update()
		{
			InputsManager.PreviousKeysDown = new List<int>(InputsManager.KeysDown);
			InputsManager.KeysDown = new List<int>(InputsManager.NextKeysDown);
			InputsManager.NextKeysDown = new List<int>();
		}

		public static bool Check(Keys key)
		{
			return InputsManager.KeysDown.Contains((int)key);
		}

		public static bool Released(Keys key)
		{
			return !InputsManager.KeysDown.Contains((int)key) && InputsManager.PreviousKeysDown.Contains((int)key);
		}

		public static void ScheduleKey(int key)
		{
			InputsManager.NextKeysDown.Add(key);
		}

		public static void ScheduleKeys(int[] keys)
		{
			for (int i = 0; i < keys.Length; i++)
			{
				InputsManager.ScheduleKey(keys[i]);
			}
		}

		public static void Initialize()
		{
			InputsManager.KeysDown = new List<int>();
			InputsManager.PreviousKeysDown = new List<int>();
			InputsManager.NextKeysDown = new List<int>();
		}

		public static int[] ByteToKeys(byte inputByte)
		{
			List<int> positions = new List<int>();
			for (int i = 0; i < 8; i++)
			{
				if (((int)inputByte & 1 << i) != 0)
				{
					positions.Add((int)InputsManager.keyMap[i]);
				}
			}
			return positions.ToArray();
		}

		public static Keys[] keyMap = new Keys[]
		{
			Settings.Instance.Right.Keyboard[0],
			Settings.Instance.Left.Keyboard[0],
			Settings.Instance.Up.Keyboard[0],
			Settings.Instance.Down.Keyboard[0],
			Settings.Instance.Jump.Keyboard[0],
			Settings.Instance.Dash.Keyboard[0],
			Settings.Instance.Grab.Keyboard[0]
		};

		public static List<int> KeysDown = new List<int>();

		public static List<int> PreviousKeysDown = new List<int>();

		public static List<int> NextKeysDown = new List<int>();
	}
}
