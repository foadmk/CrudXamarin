using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinFinal
{
    public class TaskModel
    {
        public TaskModel()
        {
            status = "pending";
        }
        public string _id { get; set; }
        public string name { get; set; }
        public DateTime created_date { get; set; }
        public string status { get; set; }
    }
}
