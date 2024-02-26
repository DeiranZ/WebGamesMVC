using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebGames.MVC.Models;

namespace WebGames.MVC.Extensions
{
    public static class ControllerExtensions
    {
        public static void SetNotification(this Controller controller, string type, string title, string message)
        {
            var notification = new Notification(type, title, message);
            controller.TempData["Notification"] = JsonConvert.SerializeObject(notification);
        }
    }
}
