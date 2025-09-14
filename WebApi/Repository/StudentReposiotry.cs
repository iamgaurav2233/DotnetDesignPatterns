using Models;
namespace MyReposiotry
{
    public class StudentRepository
    {
        private List<StudentModel> list = new List<StudentModel> {
            new StudentModel{
                Name = "Gaurav",
                Id = 1
            },
            new StudentModel{
                Name = "Pratik",
                Id = 2
            }
        };

        public List<StudentModel> GetAllStudents()
        {
            return list;
        }

        public bool AddStudent(StudentModel model)
        {
            list.Add(model);
            return true;
        }

        public StudentModel GetStudentByIndex(int id)
        {
            if (id >= 0 && id <= list.Count - 1)
            {
                return list[id];
            }
            return new StudentModel();
        }

        public bool RemoveStudentById(int id)
        {
            foreach (var obj in list)
            {
                if (obj.Id == id)
                {
                    list.Remove(obj);
                    return true;
                }
            }
            return false;
        }

        public List<StudentModel> SortStudents()
        {
            List<StudentModel> listCopy = new List<StudentModel>(list);
            listCopy.Sort((a, b) => a.Id.CompareTo((b.Id)));
            return listCopy;
        }
    }
}