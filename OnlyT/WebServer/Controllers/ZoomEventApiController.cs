using System.Net;
using OnlyT.WebServer.Models;
using OnlyT.WebServer.Throttling;
using OnlyT.Services.Options;
using OnlyT.Services.ZoomEvent;

namespace OnlyT.WebServer.Controllers;

internal sealed class ZoomEventApiController : BaseApiController
{
    private readonly IOptionsService _optionsService;
    private readonly IZoomEventService _zoomEventService;
    private readonly ApiThrottler _apiThrottler;

    public ZoomEventApiController(
        IOptionsService optionsService,
        IZoomEventService zoomEventService,
        ApiThrottler apiThrottler )
    {
        _optionsService = optionsService;
        _zoomEventService = zoomEventService;
        _apiThrottler = apiThrottler;
    }

    public void Handler(HttpListenerRequest request, HttpListenerResponse response)
    {
        CheckMethodPost(request);
        CheckSegmentLength(request, 4);

        _apiThrottler.CheckRateLimit(ApiRequestType.ZoomEvent, request);

        var responseData = new ZoomEventResponseData();

        if (!_zoomEventService.HandIsRaised)
        {
            responseData.Success = true;
            _zoomEventService.RaiseHand();
        }
        else
        {
            responseData.Success = false;
        }

        WriteResponse(response, responseData);
    }
}
