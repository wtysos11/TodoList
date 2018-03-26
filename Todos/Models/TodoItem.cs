using System;
using System.Globalization;
using System.ComponentModel;

namespace Todos.Models
{
    class TodoItem
    {
        private string id;
        private bool isCompleted;

        public string GetId() { return id; }
        public string title { get; set; }
        public string description { get; set; }
        public DateTime time { get; set; }

        public bool completed
        {
            get { return isCompleted; }
            set { isCompleted = value; }
        }

        public int completedLine
        {
            get { if (isCompleted == true) return 1; else return 0; }
        }

        public void SetTime(string time)
        {
            DateTimeFormatInfo dateFormat = new DateTimeFormatInfo();
            dateFormat.ShortDatePattern = "yyyy/MM/dd";
            DateTime nowTime = Convert.ToDateTime(time, dateFormat);
            this.time = nowTime;
        }

        public TodoItem(string title, string description, string time)
        {
            this.id = Guid.NewGuid().ToString(); //生成id
            this.title = title;
            this.description = description;
            SetTime(time);
            this.isCompleted = false; //默认为未完成
        }
    }
}
