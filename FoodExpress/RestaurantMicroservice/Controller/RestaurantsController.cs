using FoodExpress.RestaurantMicroservice.DTO;
using FoodExpress.RestaurantMicroservice.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodExpress.RestaurantMicroservice.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController : ControllerBase
    {
        private readonly IRestaurantServices _restaurantService;

        public RestaurantsController(IRestaurantServices restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RestaurantDTO>>> GetRestaurants()
        {
            var restaurants = await _restaurantService.GetAllRestaurantsAsync();
            return Ok(restaurants);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RestaurantDTO>> GetRestaurant(int id)
        {
            var restaurant = await _restaurantService.GetRestaurantByIdAsync(id);
            if (restaurant == null)
            {
                return NotFound();
            }
            return Ok(restaurant);
        }

        [HttpPost]
        public async Task<ActionResult<RestaurantDTO>> CreateRestaurant(RestaurantDTO restaurantDTO)
        {
            try
            {
                var createdRestaurant = await _restaurantService.CreateRestaurantAsync(restaurantDTO);
                return CreatedAtAction(nameof(GetRestaurant), new { id = createdRestaurant.RestaurantId }, createdRestaurant);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<RestaurantDTO>> UpdateRestaurant(int id, RestaurantDTO restaurantDTO)
        {
            try
            {
                var updatedRestaurant = await _restaurantService.UpdateRestaurantAsync(id, restaurantDTO);
                return Ok(updatedRestaurant);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRestaurant(int id)
        {
            try
            {
                await _restaurantService.DeleteRestaurantAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
