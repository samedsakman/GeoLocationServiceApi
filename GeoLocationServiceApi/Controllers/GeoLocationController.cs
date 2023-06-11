using GeoLocationServiceApi.ViewModels;
using MaxMind.GeoIP2;
using Microsoft.AspNetCore.Mvc;

namespace GeoLocationServiceApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GeoLocationController : ControllerBase
{
    [HttpPost, Route("locate")]
    public IActionResult GeoLocate([FromBody] string ipAddress)
    {
        using var data = new DatabaseReader("Tools/GeoLite2-City.mmdb");
        try
        {
            var result = data.City(ipAddress);
            var viewModel = new CityVm()
            {
                CityName = result.City.Name,
                CountryName = result.Country.Name,
                CountryIsoCode = result.Country.IsoCode
            };
            return Ok(viewModel);
        }
        catch (Exception e)
        {
            return BadRequest("Wrong Ip Address");
        }
        
    }
}