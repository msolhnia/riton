using application.Contract.Api.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static application.Contract.Api.Interface.ITTMSService;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TTMSController : ControllerBase
    {
        private readonly ITTMSService _ttmsService;

        public TTMSController(ITTMSService ttmsService)
        {
            _ttmsService = ttmsService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadExcelFile(IFormFile excelFile)
        {
            try
            {
                await _ttmsService.UploadFileAsync(excelFile);
                return Ok("File uploaded successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }

    }
}
