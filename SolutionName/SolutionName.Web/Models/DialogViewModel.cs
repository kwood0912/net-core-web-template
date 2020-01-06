using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SolutionName.Web.Models
{
    [Serializable]
    public class DialogViewModel
    {
        public DialogViewModel() { }
        public DialogViewModel(DialogType dialogType, string title, string message)
        {
            Title = title;
            Message = message;
            DialogType = dialogType;
        }
        public string Title { get; set; }
        public string Message { get; set; }
        public DialogType DialogType { get; set; }
    }

    public enum DialogType
    {
        Info,
        Success,
        Warning,
        Error
    }
}
