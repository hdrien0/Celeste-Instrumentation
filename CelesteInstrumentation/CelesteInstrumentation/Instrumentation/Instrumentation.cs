using System;
using System.IO;
using System.Text;
using Celeste;
using Monocle;
using System.Xml.Serialization;

namespace Instrumentation
{
	public static class Instrumentation
	{
		public static void Initialize()
        {
			XmlSerializer serializer = new XmlSerializer(typeof(GlobalParameters));
			using (FileStream fileStream = new FileStream("InstrumentationParameters.xml", FileMode.Open))
			{
				GlobalParameters = (GlobalParameters)serializer.Deserialize(fileStream);
			}
		}

		public static void BeginSession(SessionParameters sessionParameters)
		{
			inSession = true;
			SessionParameters = sessionParameters;
			SaveData.Start(new SaveData
			{
				Name = "run",
				AssistMode = false,
				VariantMode = false,
			}, 666);
			Engine.Scene = new LevelLoader(new Celeste.Session(new AreaKey(int.Parse(SessionParameters.AreaKey), (AreaMode)SessionParameters.AreaMode), null, null)
			{
				Level = SessionParameters.Level,
				InArea = true,
				JustStarted = false
			}, null);
		}

		public static void EndSession(Player player)
		{
			if (inSession)
            {
				Socket.SendAndReceiveData(Encoding.ASCII.GetBytes("END"));
				Socket.SendAndReceiveData(player.Components.Get<PlayerStateTracker>().PlayerState.Serialize());
				Engine.Scene = new WaitingScene();
				GC.Collect();
				inSession = false;
			}
			
		}

		public static void OnTransition(Player player)
        {
			player.Components.Get<PlayerStateTracker>().PlayerState.FinishedLevelsNumber += 1;
			if (Instrumentation.SessionParameters.EndOnLevelExit)
			{
				Instrumentation.EndSession(player);
			}
		}

        public static GlobalParameters GlobalParameters;
		public static SessionParameters SessionParameters;
		public static bool inSession;
	}
}