using System;
using Pusher;
using Pusher.Events;
using System.Threading.Tasks;
using SocketRocket;
using MonoTouch.Foundation;

namespace Pusher.Connections.SocketRocket
{
	public class WebSocket : IDisposable, IConnection
	{

		private SRWebSocket socket;

		public WebSocket (Uri endpoint)
		{
			this.socket = new SRWebSocket(new NSUrl(endpoint.ToString()));
			this.socket.MessageReceived += this.OnMessageReceived;
		}

		public event EventHandler<EventArgs> OnClose;

		public event EventHandler<EventArgs> OnOpen;

		public event EventHandler<DataReceivedEventArgs> OnData;

		protected void OnMessageReceived(object sender, SRMessageReceivedEventArgs e) 
		{
			if (this.OnData == null)
				return;

			this.OnData (this, new DataReceivedEventArgs { TextData = e.Message.ToString() });
		}

		public void Close ()
		{
			socket.Close ();

			socket.Closed += ((object sender, SRClosedEventArgs e) => {
				if(this.OnClose == null) return;
				this.OnClose(this, e);
			});
		}

		public Task Open ()
		{
			return Task.Factory.StartNew(() => {
				socket.Open ();

				socket.Opened += ((object sender, EventArgs e) => {
					if(this.OnOpen == null) return;
					this.OnOpen(this, e);
				});
			});
		}

		public Task SendMessage (string data)
		{
			return Task.Factory.StartNew(() => socket.Send (new NSString (data)));
		}

		public void Dispose ()
		{
            this.socket.Dispose();
		}
	}
}

