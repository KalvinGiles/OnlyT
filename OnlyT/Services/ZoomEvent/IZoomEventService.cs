using System;

namespace OnlyT.Services.ZoomEvent;

public interface IZoomEventService
{
    event EventHandler ZoomEvent;
    bool HandIsRaised { get; }
    void RaiseHand();
}
