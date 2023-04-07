using FileAccessData.Controllers;
using FileAccessData.Models;

namespace FileAccessDataTests
{
    public class Tests
    {
        [Test]
        public void Test_1_Read()
        {
            var mc = new MemoryController();

            var line5 = mc.Read(5);
            var expectId = 5;
            var resSC = mc.SaveChanges();

            Assert.That(expectId, Is.EqualTo(line5.Id));
            Assert.That(resSC, Is.EqualTo(true));
        }

        [Test]
        public void Test_2_Write()
        {
            var mc = new MemoryController();

            FileLineModel modelForWrite = new FileLineModel()
            {
                Id = 16,
                Name = "name16",
                Description = "description16",
                Active = false,
                CreateDate = DateTime.Now
            };

            var resW = mc.Write(modelForWrite);
            var resSC = mc.SaveChanges();

            Assert.That(resW, Is.EqualTo(true));
            Assert.That(resSC, Is.EqualTo(true));
        }

        [Test]
        public void Test_3_Update()
        {
            var mc = new MemoryController();

            FileLineModel modelForWrite = new FileLineModel()
            {
                Id = 16,
                Name = "test16",
                Description = "test16",
                Active = false,
                CreateDate = DateTime.Now
            };

            var resW = mc.Update(modelForWrite);
            var resSC = mc.SaveChanges();

            Assert.That(resW, Is.EqualTo(true));
            Assert.That(resSC, Is.EqualTo(true));
        }

        [Test]
        public void Test_4_Delete()
        {
            var mc = new MemoryController();

            var resD = mc.Delete(16);
            var resSC = mc.SaveChanges();

            Assert.That(resD, Is.EqualTo(true));
            Assert.That(resSC, Is.EqualTo(true));
        }
    }
}