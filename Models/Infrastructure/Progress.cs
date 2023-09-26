using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
namespace ProcsDLL.Models.Infrastructure
{
    public class Progress
    {
        public Progress()
        {
        }
        public void Add(ProgressStep step)
        {
            foreach (var s in Steps)
            {
                if (s.Status != ProgressStatus.Error)
                    s.Status = ProgressStatus.Completed;
            }
            Steps.Add(step);
        }
        private List<ProgressStep> _Steps;
        public List<ProgressStep> Steps
        {
            get
            {
                if (_Steps == null)
                    _Steps = new List<ProgressStep>();
                return _Steps;
            }
            set
            {
                _Steps = value;
            }
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table>");
            foreach (var step in Steps)
            {
                sb.Append(string.Format("<tr><td>{0}&nbsp;&nbsp;</td><td>{1}&nbsp;&nbsp;-</td><td>{2}</td></tr>",
                    step.Status == ProgressStatus.Completed ? "<img src='icons/check.ico' alt='' height='20px' />"
                    : step.Status == ProgressStatus.InProgress ? "<img src='icons/loading.gif' alt='' height='20px' />"
                    : step.Status == ProgressStatus.Error ? "<img src='icons/error.png' alt='' height='20px' />" : "UNKNOWN",
                    step.StartTime.ToString("hh:mm tt"),
                    step.Message));
            }
            sb.Append("</table>");
            return sb.ToString();
        }
        public void Dispose()
        {
            GC.Collect();
        }
    }
    public enum ProgressStatus
    {
        Started=99,
        Error,
        InProgress,
        Completed
    }
    public class ProgressStep
    {
        public ProgressStep(string msg, ProgressStatus status, string flag = "")
        {
            if (flag != "")
            {
                flag = GetHiddenField(flag);
            }
            else if (status == ProgressStatus.Error)
            {
                flag = GetHiddenField("000ABORT_CHECK000");
            }
            StartTime = DateTime.Now;
            Message = msg + flag;
            Status = status;
        }
        public string Message { get; private set; }
        public DateTime StartTime { get; private set; }
        public ProgressStatus Status { get; set; }
        string GetHiddenField(string flag)
        {
            return "<div hidden='hidden'>" + flag + "</div>";
        }
    }
}