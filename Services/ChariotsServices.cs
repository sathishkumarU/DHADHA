using FirstControllerProject.Models;

namespace FirstControllerProject.Services
{
    public class ChariotsServices : ICommonServicescs<Chariot>
    {
        public List<Chariot> _Chariot;
        private int nextId;
        public ChariotsServices()
        {
            _Chariot  = new List<Chariot>
            {
                new Chariot { ChariotAutoId = 1, MemberName = "Sathish", Amount = 21, CreatedBy = "3" },
                new Chariot { ChariotAutoId = 2, MemberName = "Saravana", Amount = 22, CreatedBy = "2"}
            }; 
        }
        public IEnumerable<Chariot> GetAll() => _Chariot;
        public Chariot GetById(int id)
        {
            return _Chariot.Where(x => x.ChariotAutoId == id).FirstOrDefault();
        }
        public Chariot Edit(int id)
        {
            return _Chariot.Where(x => x.ChariotAutoId == id).FirstOrDefault();
        }
        public Chariot Update(Chariot Chariotupdate)
        {
            Chariot updatedChariot = new Chariot();
            if (updatedChariot != null)
            {
                updatedChariot = _Chariot.Where(x => x.ChariotAutoId == Chariotupdate.ChariotAutoId).First();
                //updatedStudent.Age = Studentupdate.Age;
                //updatedStudent.Name = Studentupdate.Name;
                //updatedStudent.Email = Studentupdate.Email;
            }
            return updatedChariot;
        }
        public IEnumerable<Chariot> Add(IEnumerable<Chariot> Chariot)
        {
            try
            {
                if (Chariot != null && Chariot.Any())
                {
                    foreach (var stude in Chariot)
                    {
                        if (!string.IsNullOrWhiteSpace(stude.MemberName)) // skip empty rows
                        {
                            stude.ChariotAutoId = nextId;
                        }
                        nextId++;
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Add: {ex.Message}");
                throw;
            }

            _Chariot.AddRange(Chariot);
            return _Chariot;
        }
        public bool Delete(int id)
        {
            var student = _Chariot.FirstOrDefault(s => s.ChariotAutoId == id);
            if (student != null)
            {
                _Chariot.Remove(student);
                return true; // deleted successfully
            }
            return false; // not found
        }
        public IEnumerable<Chariot> GetPageSized(int pageSize = 5, int pageCount = 1)
        {
            List<Chariot> curStudnet = new List<Chariot>();
            curStudnet = _Chariot.OrderBy(x => x.ChariotAutoId).Skip((pageCount - 1) * pageSize).Take(pageSize).ToList();
            return curStudnet;
        }
    }
}
