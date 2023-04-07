using FileAccessData.Enums;
using FileAccessData.Models;
using Microsoft.Extensions.Caching.Memory;

namespace FileAccessData.Controllers
{
    public class MemoryController : FileController
    {
        public MemoryController()
        {
            _cache.Clear();
            cacheIds.Clear();

            var fileLines = ReadFileLines();

            Set(fileLines);

            foreach (var line in fileLines)
            {
                cacheIds.Add(line.Id);
            }
        }

        public FileLineModel Read(int id)
        {
            return Get(id);
        }

        public bool Write(FileLineModel model)
        {
            try
            {
                FileLineModel cacheEntry;
                if (!_cache.TryGetValue(model.Id, out cacheEntry!))
                {
                    model.SetOperation((int)Operation.write);
                    _cache.Set(model.Id, model);
                    cacheIds.Add(model.Id);
                    return true;
                }
                else
                {
                    Console.WriteLine($"Id {model.Id.ToString()} exist is cache");
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                FileLineModel cacheEntry;
                if (!_cache.TryGetValue(id, out cacheEntry!))
                {
                    Console.WriteLine($"Id {id.ToString()} not exist is cache");
                    return false;
                }
                else
                {
                    var modelDelele = new FileLineModel();
                    modelDelele.Id = id;
                    modelDelele.SetOperation((int)Operation.delete);

                    _cache.Remove(id);
                    _cache.Set(modelDelele.Id, modelDelele);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(FileLineModel model)
        {
            try
            {
                FileLineModel cacheEntry;
                if (!_cache.TryGetValue(model.Id, out cacheEntry!))
                {
                    model.SetOperation((int)Operation.write);
                    _cache.Set(model.Id, model);
                    return true;
                }
                else
                {
                    model.SetOperation((int)Operation.update);
                    _cache.Remove(model.Id);
                    _cache.Set(model.Id, model);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool SaveChanges()
        {
            try
            {
                var fileLines = ReadFileLines();
                var cacheLines = GetForChanges();

                foreach (var cacheLine in cacheLines)
                {
                    //Добавление Проверить нет ли такой записи в файле
                    if (cacheLine.GetOperation() == (int)Operation.write)
                    {
                        var fileLine = fileLines.FirstOrDefault(p => p.Id == cacheLine.Id);
                        if (fileLine == null)
                        {
                            fileLines.Add(cacheLine);
                        }
                        else
                        {
                            Console.WriteLine($"Запись с идентификатором {cacheLine.Id} уже существует!");
                        }
                    }

                    //Изменение Проверить есть ли такая запись в файле
                    if (cacheLine.GetOperation() == (int)Operation.update)
                    {
                        var fileLine = fileLines.FirstOrDefault(p => p.Id == cacheLine.Id);
                        if (fileLine != null)
                        {
                            fileLines.Remove(fileLine);
                            fileLines.Add(cacheLine);
                        }
                        else
                        {
                            Console.WriteLine($"Запись с идентификатором {cacheLine.Id} удалена, невозможно ее изменить!");
                        }
                    }

                    //Удаление Проверить есть ли такая запись в файле
                    if (cacheLine.GetOperation() == (int)Operation.delete)
                    {
                        var fileLine = fileLines.FirstOrDefault(p => p.Id == cacheLine.Id);
                        if (fileLine != null)
                        {
                            fileLines.Remove(fileLine);
                        }
                        else
                        {
                            Console.WriteLine($"Запись с идентификатором {cacheLine.Id} была удалена ранее!");
                        }
                    }
                }

                WriteFileLines(fileLines);
                new MemoryController();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #region Private methods

        private MemoryCache _cache = new MemoryCache(new MemoryCacheOptions());

        private List<int> cacheIds = new List<int>();

        private void Set(List<FileLineModel> model)
        {
            foreach (var item in model)
            {
                if (!Set(item))
                {
                    Update(item);
                }
            }
        }

        private bool Set(FileLineModel model)
        {
            FileLineModel cacheEntry = null;
            if (!_cache.TryGetValue(model.Id, out cacheEntry!))
            {
                _cache.Set(model.Id, model);
                return true;

            }
            else
            {
                return false;
            }
        }

        private FileLineModel Get(object key)
        {
            FileLineModel cacheEntry = null;
            if (!_cache.TryGetValue(key, out cacheEntry!)) // Ищем ключ в кэше.
            {
                return cacheEntry;
            }
            return cacheEntry!;
        }

        private List<FileLineModel> GetForChanges()
        {
            List<FileLineModel> result = new List<FileLineModel>();

            foreach (var cacheId in cacheIds)
            {
                var line = Get(cacheId);
                if (line.GetOperation() != null)
                {
                    result.Add(line);
                }

            }

            return result;
        }

        #endregion
    }
}
