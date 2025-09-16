using System.Net;
using OnlyT.WebServer.Models;
using OnlyT.WebServer.Throttling;
using OnlyT.Services.Options;
using OnlyT.Services.ZoomEvent;

namespace OnlyT.WebServer.Controllers;

internal sealed class HandRaiseEventApiController : BaseApiController
{
    private readonly IOptionsService _optionsService;
    private readonly IHandRaiseService _handRaiseEventService;
    private readonly ApiThrottler _apiThrottler;

    public HandRaiseEventApiController(
        IOptionsService optionsService,
        IHandRaiseService handRaiseEventService,
        ApiThrottler apiThrottler )
    {
        _optionsService = optionsService;
        _handRaiseEventService = handRaiseEventService;
        _apiThrottler = apiThrottler;
    }

    public void Handler(HttpListenerRequest request, HttpListenerResponse response)
    {
        CheckMethodPost(request);
        CheckSegmentLength(request, 4);

        _apiThrottler.CheckRateLimit(ApiRequestType.ZoomEvent, request);

        var responseData = new ZoomEventResponseData();

        if (!_handRaiseEventService.HandIsRaised)
        {
            responseData.Success = true;
            _handRaiseEventService.RaiseHand();
        }
        else
        {
            responseData.Success = false;
        }

        WriteResponse(response, responseData);
    }
}
