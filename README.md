Pusher.Connections.SocketRocket
===============================

Pusher.NET implementation using SocketRocket for Xamarin.iOS.

### Motivation

We use Xamarin.iOS for developing Mural.ly iOS App, we use Pusher on our Architecture and we needed to connect on the iOS App. We took the great transport agnostic implementation from [Pusher.NET](https://github.com/digitalcreations/Pusher.NET) and the C# bindings of [SocketRocket](https://github.com/thefactory/SocketRocket-Sharp) and put together a library.

SocketRocket is a [WebSocket Implementation](https://github.com/square/SocketRocket) for iOS created by the Square Team.

#### Usage 

```csharp
var factory = new Pusher.Connections.SocketRocket.WebsocketConnectionFactory();
this.pusher = new Pusher.Pusher(factory, "you-pusher-api-key", new Options
{
    Authenticator = // any IAuthenticator if private channels needed
});

await this.pusher.ConnectAsync();
this.channel = (Channel)await pusher.SubscribeToChannelAsync("your-channel-name");

this.channel.EventEmitted += async (sender, e) =>
{
    if (e.EventName == "pusher_internal:subscription_succeeded")
    {
      Console.WriteLine(e.Data) // Get the event data
    }
};
```

#### Copyright 

This is work in progress created by [@leChantux](http://twitter.com/leChantux), [@juliracca](http://twitter.com/juliracca), [@johnnyhalife](http://twitter.com/johnnyhalife)
