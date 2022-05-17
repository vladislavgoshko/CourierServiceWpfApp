using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using System.Threading.Tasks;
using CourierServiceLibrary.Models;


namespace CourierServiceLibrary.Repository
{
    public class CourierRepository : IRepository<Courier>
    {
        private readonly string fileName;
        public CourierRepository(string fileName)
        {
            this.fileName = fileName;
        }

        public void Create(Courier entity)
        {
            List<Courier> couriersInFile = Read().ToList();
            int i = 1;
            while (couriersInFile.Any(t => t.Id == entity.Id))
                entity.Id = i++;
            couriersInFile.Add(entity);
            File.WriteAllText(fileName, JsonConvert.SerializeObject(couriersInFile));
        }

        public IEnumerable<Courier> Read()
        {
            return JsonConvert.DeserializeObject<IEnumerable<Courier>>(File.ReadAllText($@"{Directory.GetCurrentDirectory()}\Data\Couriers.json")) ?? new List<Courier>();
        }

        public void Delete(int id)
        {
            List<Courier> couriersInFile = Read().ToList();

            couriersInFile.RemoveAt(couriersInFile.FindIndex(t => t.Id == id));

            File.WriteAllText(fileName, JsonConvert.SerializeObject(couriersInFile));
        }

    }
}
