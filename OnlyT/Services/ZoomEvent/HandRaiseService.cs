using System;

namespace OnlyT.Services.ZoomEvent;

public class HandRaiseService : IHandRaiseService
{
    public event EventHandler? HandEvent;

    public HandRaiseService()
    { }

    public bool HandIsRaised => false;

    public void RaiseHand()
    {
        OnHandRaiseEvent(new System.EventArgs());
    }

    private void OnHandRaiseEvent(System.EventArgs e)
    {
        HandEvent?.Invoke(this, e);
    }
}