using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PoseidonApi.Controllers;

public class CustomActionFilter : IActionFilter
{

    private readonly ILogger<CustomActionFilter> _logger;

    public CustomActionFilter(ILogger<CustomActionFilter> logger)
    {
        _logger = logger;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        var actionDescriptor = ((ControllerBase)context.Controller).ControllerContext.ActionDescriptor;

        Console.WriteLine("--------------------------------------------------------------------------------------");
        Console.WriteLine("------------- " + actionDescriptor.ActionName + " start at: " + DateTimeOffset.UtcNow + " --------------------");
        Console.WriteLine("--------------------------------------------------------------------------------------");
        _logger.LogInformation(1, " -> Start " + actionDescriptor.ActionName + " action in " + actionDescriptor.ControllerName + " controller");
        
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        var actionDescriptor = ((ControllerBase)context.Controller).ControllerContext.ActionDescriptor;
        
        Console.WriteLine("--------------------------------------------------------------------------------------");
        Console.WriteLine("------------- " + actionDescriptor.ActionName + " end at: " + DateTimeOffset.UtcNow + " --------------------");
        Console.WriteLine("--------------------------------------------------------------------------------------");
    }
}