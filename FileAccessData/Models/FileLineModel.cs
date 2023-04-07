using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileAccessData.Models
{
    public class FileLineModel
    {
        private int? Operation { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public DateTime CreateDate { get; set; }

        public FileLineModel() { }
        public FileLineModel(int _id)
        {
            Id = _id;
            Name = $"name{_id}";
            Description = $"description{_id}";
            Active = true;
            CreateDate = DateTime.Now;
        }

        public void SetOperation(int operation)
        {
            Operation = operation;
        }
        public int? GetOperation()
        {
            return Operation;
        }
    }
}
