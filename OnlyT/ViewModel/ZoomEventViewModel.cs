using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using OnlyT.Services.Options;
using OnlyT.Services.ZoomEvent;
using OnlyT.ViewModel.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlyT.ViewModel
{
    public class ZoomEventViewModel : ObservableObject
    {
        private readonly IOptionsService _optionsService;
        private readonly IZoomEventService _zoomEventService;

        public bool ApplicationClosing { get; private set; }

        public ZoomEventViewModel(IOptionsService optionsService, IZoomEventService zoomEventService)
        {
            _optionsService = optionsService;
            _zoomEventService = zoomEventService;

            _zoomEventService.ZoomEvent += ZoomEventHandler;

            WeakReferenceMessenger.Default.Register<ShutDownMessage>(this, OnShutDown);
        }

        private void ZoomEventHandler(object? sender, System.EventArgs e)
        {
            WeakReferenceMessenger.Default.Send(new ZoomEventMessage());
        }

        private void OnShutDown(object recipient, ShutDownMessage obj)
        {
            ApplicationClosing = true;
        }
    }
}
