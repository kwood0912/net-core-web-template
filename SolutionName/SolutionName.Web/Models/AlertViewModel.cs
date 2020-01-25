using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SolutionName.Web.Models
{
    [Serializable]
    public class AlertViewModel
    {
        public AlertViewModel() { }
        public AlertViewModel(AlertType alertType, string title, string message)
        {
            Title = title;
            Message = message;
            AlertType = alertType;
        }
        public string Title { get; set; }
        public string Message { get; set; }
        public AlertType AlertType { get; set; }
    }

    public enum AlertType
    {
        Info,
        Success,
        Warning,
        Error
    }
}
