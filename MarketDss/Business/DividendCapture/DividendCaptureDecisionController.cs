using MarketDss.Business.Securities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MarketDss.Business.DividendCapture
{
    [Route("Dividend")]
    public class DividendCaptureDecisionController : Controller
    {
        private readonly DividendCaptureDecisionService _dividendCaptureDecisionService;

        public DividendCaptureDecisionController(DividendCaptureDecisionService dividendCaptureDecisionService)
        {
            _dividendCaptureDecisionService = dividendCaptureDecisionService;
        }

        [HttpGet("Securities")]
        public async Task<IActionResult> GetAllSecuritiesAsync()
        {
            var securities = await _dividendCaptureDecisionService.GetAllSecutiesAsync().ConfigureAwait(false);
            return Json(securities);
        }
        
        [HttpGet("Securities/{securityId}")]
        [ProducesResponseType(typeof(Security), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetSecurityAsync(int securityId)
        {
            var security = await _dividendCaptureDecisionService.GetSecurityAsync(securityId).ConfigureAwait(false);
            if(security == null)
            {
                return NotFound();
            }
            return Json(security);
        }

        [HttpPost("RunBatch")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> RunBatchAsync()
        {
            await _dividendCaptureDecisionService.RunBatchAsync().ConfigureAwait(false);
            return Ok();
        }
    }
}
