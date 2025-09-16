using System;

namespace OnlyT.Services.ZoomEvent;

public interface IHandRaiseService
{
    event EventHandler HandEvent;
    bool HandIsRaised { get; }
    void RaiseHand();
}
