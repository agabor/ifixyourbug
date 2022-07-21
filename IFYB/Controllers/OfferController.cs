using IFYB.Models;
using Microsoft.AspNetCore.Mvc;

namespace IFYB.Controllers;

[ApiController]
[Route("api/offer")]
public class OfferController : ControllerBase
{
    OfferDto offer;
    public OfferController(OfferDto offer) {
        this.offer = offer;
    }

    [HttpGet]
    [Produces(typeof(OfferDto))]
    public IActionResult GetOffer() {
        return Ok(offer);
    }
}
