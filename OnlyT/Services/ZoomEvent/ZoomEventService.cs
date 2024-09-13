using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace OnlyT.Services.ZoomEvent;

public class ZoomEventService : IZoomEventService
{
    public event EventHandler? ZoomEvent;

    public ZoomEventService()
    { }

    public bool HandIsRaised => false;

    public void RaiseHand()
    {
        OnZoomEvent(new System.EventArgs());
    }

    private void OnZoomEvent(System.EventArgs e)
    {
        ZoomEvent?.Invoke(this, e);
    }
}