using System;
using Celeste;
using Monocle;
using System.IO;
using System.Xml.Serialization;

namespace Instrumentation
{
	public class WaitingScene : Scene
	{

		public override void Begin()
		{
			base.Add(new HudRenderer());
			new FadeWipe(this, true, null);
			RunThread.Start(new Action(this.GetLoadInfo), "Instrumentation session initialization", true);
		}

		private void GetLoadInfo()
		{
			string sessionParameters = Socket.SendAndReceiveMessage("LOADINFO");
			XmlSerializer serializer = new XmlSerializer(typeof(SessionParameters));
			Instrumentation.BeginSession((SessionParameters)serializer.Deserialize(new StringReader(sessionParameters)));
		}

	}
}
