using System;
using Pusher;

namespace Pusher.Connections.SocketRocket
{
	public class WebsocketConnectionFactory : IConnectionFactory
	{
		public IConnection Create(Uri endpoint) 
		{
			return new WebSocket(endpoint);
		}
	}
}

