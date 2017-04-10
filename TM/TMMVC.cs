using System.Web.Mvc;
namespace TM.Message
{
    public static class Messaging
    {
        public static void success(this Controller controller, string message)
        {
            controller.TempData["MsgSuccess"] = message;
        }
        public static void info(this Controller controller, string message)
        {
            controller.TempData["MsgInfo"] = message;
        }
        public static void warning(this Controller controller, string message)
        {
            controller.TempData["MsgWarning"] = message;
        }
        public static void danger(this Controller controller, string message)
        {
            controller.TempData["MsgDanger"] = message;
        }
    }

}
