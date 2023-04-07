using FileAccessData.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FileAccessData.Controllers
{
    public class FileController
    {
        private const string path = "file.txt";

        protected List<FileLineModel> ReadFileLines()
        {
            List<FileLineModel> list = new List<FileLineModel>();

            var lines = File.ReadLines(path);

            foreach (var line in lines)
            {
                if (!string.IsNullOrEmpty(line))
                {
                    var data = new FileLineModel();

                    var fileParams = line.Split('|');

                    data.Id = Convert.ToInt32(fileParams[0]);
                    data.Name = fileParams[1];
                    data.Description = fileParams[2];
                    data.Active = Convert.ToBoolean(fileParams[3]);
                    data.CreateDate = Convert.ToDateTime(fileParams[4]);

                    list.Add(data);
                }
            }
            return list;
        }
        protected void WriteFileLines(List<FileLineModel> data)
        {
            StreamWriter sw = new StreamWriter(path);
            foreach (var item in data)
            {
                sw.WriteLine($"{item.Id}|{item.Name}|{item.Description}|{item.Active.ToString()}|{item.CreateDate.ToString()}");
            }
            sw.Close();
        }
    }
}
