using BusinessObject.IService;
using BusinessObject.Model.Request;
using BusinessObject.Model.Response;
using BusinessObject.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HELLWASHER_Controller.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClothUnitController : ControllerBase
    {
        private readonly IClothUnitService _clothUnitService;
        public ClothUnitController(IClothUnitService clothUnitService)
        {
            _clothUnitService = clothUnitService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateClothUnit(CreateClothUnitDTO newClothUnit)
        {
            var result = await _clothUnitService.CreateClothUnit(newClothUnit);
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
        public async Task<IActionResult> GetClothUnit(int page = 1, int pageSize = 10,
            string search = "", string sort = "")
        {
            var result = await _clothUnitService.GetAllClothUnit(page, pageSize, search, sort);
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
        public async Task<IActionResult> DeleteClothUnit(int id)
        {
            var result = await _clothUnitService.DeleteClothUnit(id);
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
        public async Task<IActionResult> UpdateClothUnit(int id, ResponseClothUnitDTO newClothUnit)
        {
            var result = await _clothUnitService.UpdateClothUnit(id, newClothUnit);
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
