using System.Diagnostics;
using Br.Com.Company.CurrencyQuote.Common.Infraestructure.Notifications;
using Br.Com.Company.CurrencyQuote.Common.Infraestructure.Notifications.Handler;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Br.Com.Company.CurrencyQuote.Common.Infraestructure.Filters
{
    public class NotificationFilter : IResultFilter
    {
        private readonly NotifyHandler _messageHandler;

        public NotificationFilter(INotificationHandler<Notification> notifications)
        {
            _messageHandler = (NotifyHandler)notifications;
        }

        public void OnResultExecuted(ResultExecutedContext context) { }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            if (!_messageHandler.HasNotifications())
            {
                return;
            }

            var notifications = new ModelStateDictionary();
            foreach (var notification in _messageHandler.Notifications)
            {
                notifications.AddModelError(notification.Key, notification.Message);
            }

            var problemDetails = new ValidationProblemDetails(notifications)
            {
                Title = "Business rule violation",
                Status = StatusCodes.Status422UnprocessableEntity,
                Type = typeof(Notification).FullName
            };

            var traceId = Activity.Current?.Id ?? context.HttpContext.TraceIdentifier;
            problemDetails.Extensions["traceId"] = traceId;
            var requestId = context.HttpContext.GetRequestId();
            if (requestId != null)
            {
                problemDetails.Extensions["requestId"] = requestId;
            }

            var result = new BadRequestObjectResult(problemDetails);
            result.ContentTypes.Add("application/problem+json");
            result.StatusCode = StatusCodes.Status422UnprocessableEntity;

            context.Result = result;
        }
    }
}
