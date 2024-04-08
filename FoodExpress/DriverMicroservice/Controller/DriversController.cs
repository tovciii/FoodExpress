using FoodExpress.DriverMicroservice.Models;
using FoodExpress.DriverMicroservice.Services;
using FoodExpress.UserMicroservice.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodExpress.DriverMicroservice.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriversController : ControllerBase
    {
        private readonly IDriverServices _driverServices;

        public DriversController(IDriverServices driverServices)
        {
            _driverServices = driverServices;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Driver>>> GetDrivers()
        {
            try
            {
                var driver = await _driverServices.GetAllDriversAsync();
                return Ok(driver);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching driver.");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Driver>> GetDriver(int id)
        {
            try
            {
                var driver = await _driverServices.GetDriverByIdAsync(id);

                if (driver == null)
                {
                    return NotFound();
                }

                return driver;
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching the driver.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Driver>> AddDriver(Driver driver)
        {
            try
            {
                var newDriver = await _driverServices.AddDriverAsync(driver);
                return CreatedAtAction(nameof(GetDriver), new { id = newDriver.DriverId }, newDriver);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while adding the driver.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDriver(int id, Driver driver)
        {
            if (id != driver.DriverId)
            {
                return BadRequest();
            }

            try
            {
                await _driverServices.UpdateDriverAsync(driver);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the driver.");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDriver(int id)
        {
            try
            {
                var result = await _driverServices.DeleteDriverAsync(id);
                if (!result)
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while deleting the driver.");
            }

            return NoContent();
        }
    }
}
