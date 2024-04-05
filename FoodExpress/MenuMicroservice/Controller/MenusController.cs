using FoodExpress.MenuMicroservice.DTO;
using FoodExpress.MenuMicroservice.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodExpress.MenuMicroservice.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenusController : ControllerBase
    {
        private readonly IMenuServices _menuService;

        public MenusController(IMenuServices menuService)
        {
            _menuService = menuService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MenuDTO>>> GetMenus()
        {
            var menus = await _menuService.GetAllMenusAsync();
            return Ok(menus);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MenuDTO>> GetMenu(int id)
        {
            var menu = await _menuService.GetMenuByIdAsync(id);
            if (menu == null)
            {
                return NotFound();
            }
            return Ok(menu);
        }

        [HttpPost("{restaurantId}")]
        public async Task<ActionResult<MenuDTO>> CreateMenu(int restaurantId, MenuDTO menuDTO)
        {
            try
            {
                var createdMenu = await _menuService.CreateMenuAsync(restaurantId, menuDTO);
                return CreatedAtAction(nameof(GetMenu), new { id = createdMenu.MenuId }, createdMenu);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<MenuDTO>> UpdateMenu(int id, MenuDTO menuDTO)
        {
            try
            {
                await _menuService.UpdateMenuAsync(id, menuDTO);
                return Ok(menuDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMenu(int id)
        {
            try
            {
                await _menuService.DeleteMenuAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
