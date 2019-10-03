using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JdCat.Basketball.Common;
using JdCat.Basketball.IService;
using JdCat.Basketball.Model.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace JdCat.Basketball.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilController : ControllerBase
    {
        /// <summary>
        /// 生成小程序二维码
        /// </summary>
        /// <returns></returns>
        [HttpGet("qrcode")]
        public async Task<ActionResult<ApiResult<string>>> QrCode([FromQuery]string scene, [FromQuery]string page, [FromServices]IUtilService service)
        {
            var url = await service.GetWxQrCodeAsync(WeixinHelper.Weixin.AppId, WeixinHelper.Weixin.Secret, scene, page);
            return new ApiResult<string> { Result = url };
        }

    }
}