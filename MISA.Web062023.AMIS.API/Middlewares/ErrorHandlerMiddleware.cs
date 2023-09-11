using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MISA.Web062023.AMIS.Domain;
using System.Net;
using System.Resources;
using System.Text.Json;
using MISA.Web062023.AMIS.Domain.Resources;

namespace MISA.Web062023.AMIS.API
{
    /// <summary>
    /// Middleware bắt và xử lý lỗi toàn cục
    /// </summary>
    /// Created by: Nguyễn Thanh Lâm (14/08/2023)
    public class ErrorHandlerMiddleware
    {
        #region Properties
        private readonly RequestDelegate _requestDelegate;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        /// <summary>
        /// The .ctor.
        /// </summary>
        /// <param name="requestDelegate">The request delegate.</param>
        /// <param name="logger">The logger.</param>
        /// Created by: Nguyễn Thanh Lâm (14/08/2023)
        public ErrorHandlerMiddleware(RequestDelegate requestDelegate, ILogger<ErrorHandlerMiddleware> logger)
        {
            _requestDelegate = requestDelegate;
            _logger = logger;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Middleware nếu không có lỗi thì chuyển đến Middleware khác, nếu có lỗi thì xử lý
        /// </summary>
        /// <param name="context">Đối tượng HttpContext</param>
        /// <returns></returns>
        /// Created by: Nguyễn Thanh Lâm (14/08/2023)
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _requestDelegate(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        /// Created by: Nguyễn Thanh Lâm (14/08/2023)
        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            context.Response.ContentType = "application/json";
            var resourceManager = Domain.Resources.Exception.Exception.ResourceManager;
            switch (ex)
            {
                // Trường hợp lỗi không tìm thấy dữ liệu
                case NotFoundException:
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    await context.Response.WriteAsync(
                        text: new BaseException()
                        {
                            ErrorCode = ((NotFoundException)ex).ErrorCode,
                            UserMessage = ((NotFoundException)ex).Message ?? resourceManager.GetString("NotFoundException"),
                            DevMessage = ex.Message,
                            TraceId = context.TraceIdentifier,
                            MoreInfo = ex.HelpLink
                        }.ToString() ?? ""
                    );
                    break;
                // Trường hợp lỗi xung đột request
                case ConflictException:
                    context.Response.StatusCode = StatusCodes.Status409Conflict;
                    await context.Response.WriteAsync(
                        text: new BaseException()
                        {
                            ErrorCode = ((ConflictException)ex).ErrorCode,
                            UserMessage = ((ConflictException)ex).Message ?? resourceManager.GetString("ConflictException"),
                            DevMessage = ex.Message,
                            TraceId = context.TraceIdentifier,
                            MoreInfo = ex.HelpLink
                        }.ToString() ?? ""
                    );
                    break;
                // Lỗi nhập liệu từ người dùng
                case BadRequestException:
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsync(
                        text: new BaseException()
                        {
                            ErrorCode = ((BadRequestException)ex).ErrorCode,
                            UserMessage = ((BadRequestException)ex).UserMsg ?? resourceManager.GetString("BadRequestException"),
                            DevMessage = ex.Message,
                            TraceId = context.TraceIdentifier,
                            MoreInfo = ex.HelpLink
                        }.ToString() ?? ""
                    );
                    break;
                // Lỗi mặc định
                default:
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    await context.Response.WriteAsync(
                       text: new BaseException()
                       {
                           ErrorCode = StatusCodes.Status500InternalServerError,
                           UserMessage = resourceManager.GetString("Server"),
                           DevMessage = ex.Message,
                           TraceId = context.TraceIdentifier,
                           MoreInfo = ex.HelpLink
                       }.ToString() ?? ""
                   );
                    break;
            }
        }
        #endregion
    }
}