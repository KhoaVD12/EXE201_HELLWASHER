using BusinessObject.IService;
using BusinessObject.Model.Request;
using BusinessObject.Model.Response;
using BusinessObject.Service;
using Microsoft.AspNetCore.Mvc;

namespace HELLWASHER_Controller.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryDTO newCategory)
        {
            var result = await _categoryService.CreateCategory(newCategory);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetCategory(int page = 1, int pageSize = 10,
            string search = "", string sort = "")
        {
            var result = await _categoryService.GetAllCategory(page, pageSize, search, sort);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var result = await _categoryService.DeleteCategory(id);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, ResponseCategoryDTO newCategory)
        {
            var result = await _categoryService.UpdateCategory(id, newCategory);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }

    }
}
