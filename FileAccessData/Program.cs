using FileAccessData.Controllers;
using FileAccessData.Models;

internal class Program
{
    private static void Main(string[] args)
    {
        var mc = new MemoryController();

        //var line5 = mc.Read(5);

        //FileLineModel modelForWrite = new FileLineModel()
        //{
        //    Id = 15,
        //    Name = "name15",
        //    Description = "description15",
        //    Active = false,
        //    CreateDate = DateTime.Now
        //};

        //mc.Write(modelForWrite);

        //mc.Delete(7);

        //FileLineModel modelForUpdate = new FileLineModel()
        //{
        //    Id = 8,
        //    Name = "nameTest",
        //    Description = "desTest",
        //    Active = true,
        //    CreateDate = DateTime.Now.AddDays(5)
        //};

        //mc.Update(modelForUpdate);

        mc.SaveChanges();
    }
}