using JdCat.Basketball.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JdCat.Basketball.App.Middles
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                //var statusCode = context.Response.StatusCode;
                //if (ex is ArgumentException)
                //{
                //    statusCode = 200;
                //}
                context.Response.StatusCode = 500;
                await HandleExceptionAsync(context, 500, ex.Message, ex.StackTrace);
            }
            finally
            {
                var statusCode = context.Response.StatusCode;
                var msg = "";
                if (statusCode == 401)
                {
                    msg = "未授权";
                }
                else if (statusCode == 404)
                {
                    msg = "未找到服务";
                }
                else if (statusCode == 502)
                {
                    msg = "请求错误";
                }
                else if (statusCode >= 400)
                {
                    msg = "未知错误";
                }
                if (!string.IsNullOrWhiteSpace(msg))
                {
                    await HandleExceptionAsync(context, statusCode, msg);
                }
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, int statusCode, string msg, string trace = null)
        {
            var data = new ApiResult<string> { Code = statusCode, Message = msg };
            var result = data.ToJson();
            LogManager.GetCurrentClassLogger().Error($"系统错误：{statusCode}，{msg}{Environment.NewLine}{trace}");
            context.Response.ContentType = "application/json;charset=utf-8";
            return context.Response.WriteAsync(result);
        }
    }

}
