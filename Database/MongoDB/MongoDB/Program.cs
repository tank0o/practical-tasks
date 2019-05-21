using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace MongoDB
{
    class Program
    {

        static string connectionString = "mongodb://localhost";
        static MongoClient client = new MongoClient(connectionString);
        static IMongoDatabase database = client.GetDatabase("Test");
        class Student
        {
            [BsonId]
            public ObjectId Id { get; set; }
            [BsonIgnoreIfNull]
            public string Name { get; set; }
            [BsonIgnoreIfNull]
            public Group Group { get; set; }
            public MongoDBRef Ref;
            public ObjectId idGroup;

            static public string Print(Student s)
            {
                return "name-" + s.Name + " ; " + "Group-" + s.Group?.Name;
            }
            static public string Print(BsonDocument doc)
            {
                Student s = BsonSerializer.Deserialize<Student>(doc);
                return "name-" + s.Name + " ; " + "Group-" + s.Group?.Name;
            }
        }

        class Group
        {
            [BsonId]
            public ObjectId Id { get; set; }
            public string Name { get; set; }

            static public string Print(Group s)
            {
                return "name-" + s.Name;
            }
            static public string Print(BsonDocument doc)
            {
                Group group = BsonSerializer.Deserialize<Group>(doc);
                return "name-" + group.Name;
            }
        }

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1)Просмотр студентов");
                Console.WriteLine("2)Просмотр групп");
                Console.WriteLine("3)Создать студента");
                Console.WriteLine("4)Создать группу");
                Console.WriteLine("5)Редактировать студента");
                Console.WriteLine("6)Редактировать группу");
                Console.WriteLine("7)Удалить студента");
                Console.WriteLine("8)Удалить группу");
                Console.WriteLine("9)Связи");
                int menu = Convert.ToInt16(Console.ReadLine());

                switch (menu)
                {
                    case 1:
                        {
                            Console.WriteLine("Параметр поиска (для вывода всех студентов оставить пустым):");
                            string name = Console.ReadLine();
                            List<BsonDocument> students = FindStudents(name);
                            foreach (var doc in students)
                            {
                                Console.WriteLine(Student.Print(doc));
                            }
                            break;
                        }
                    case 2:
                        {
                            List<BsonDocument> group = FindGroups();
                            foreach (var doc in group)
                            {
                                Console.WriteLine(Group.Print(doc));
                            }
                            break;
                        }
                    case 3:
                        {
                            Console.WriteLine("Введите имя студента");
                            string name = Console.ReadLine();
                            int groupId;
                            List<BsonDocument> groups = FindGroups();
                            do
                            {
                                Console.WriteLine("Выберите группу:");
                                int i = 0;

                                foreach (var doc in groups)
                                {
                                    Console.WriteLine(i++ + ":" + Group.Print(doc));
                                }
                                Console.Write("ГРуппа");
                                groupId = Convert.ToInt16(Console.ReadLine());
                            } while (groupId >= groups.Count || groupId < 0);

                            Student s = new Student { Name = name, Group = BsonSerializer.Deserialize<Group>(groups[groupId]), Ref = new MongoDBRef("NewCollection2", groups[groupId]["_id"]), idGroup = groups[groupId]["_id"].AsObjectId };
                            AddStudent(s).GetAwaiter().GetResult();
                            break;
                        }
                    case 4:
                        {
                            Console.WriteLine("Введите название группы");
                            string name = Console.ReadLine();

                            AddGroup(name).GetAwaiter().GetResult();
                            break;
                        }
                    case 5:
                        {
                            int studentId;
                            List<BsonDocument> students = FindStudents("");
                            do
                            {
                                Console.WriteLine("Выберите студента:");
                                int i = 0;

                                foreach (var doc in students)
                                {
                                    Console.WriteLine(i++ + ":" + Student.Print(doc));
                                }
                                Console.Write("Студент:");
                                studentId = Convert.ToInt16(Console.ReadLine());
                            } while (studentId >= students.Count || studentId < 0);

                            Console.WriteLine("Введите имя студента");
                            string name = Console.ReadLine();
                            int groupId;
                            List<BsonDocument> groups = FindGroups();
                            do
                            {
                                Console.WriteLine("Выберите группу:");
                                int i = 0;

                                foreach (var doc in groups)
                                {
                                    Console.WriteLine(i++ + ":" + Group.Print(doc));
                                }
                                Console.Write("ГРуппа");
                                groupId = Convert.ToInt16(Console.ReadLine());
                            } while (groupId >= groups.Count || groupId < 0);

                            Student s = new Student { Name = name, Group = BsonSerializer.Deserialize<Group>(groups[groupId]), Ref = new MongoDBRef("NewCollection2", groups[groupId]["_id"]), idGroup = groups[groupId]["_id"].AsObjectId };

                            UpdateStudent(students[studentId]["_id"].AsObjectId, s);

                            break;
                        }
                    case 6:
                        {
                            int groupId;
                            List<BsonDocument> groups = FindGroups();
                            do
                            {
                                Console.WriteLine("Выберите группу:");
                                int i = 0;

                                foreach (var doc in groups)
                                {
                                    Console.WriteLine(i++ + ":" + Group.Print(doc));
                                }
                                Console.Write("ГРуппа");
                                groupId = Convert.ToInt16(Console.ReadLine());
                            } while (groupId >= groups.Count || groupId < 0);

                            Console.WriteLine("Введите новое название группы");
                            string name = Console.ReadLine();

                            UpdateGroup(groups[groupId]["_id"].AsObjectId, new Group { Name = name });
                            break;
                        }
                    case 7:
                        {
                            int studentId;
                            List<BsonDocument> students = FindStudents("");
                            do
                            {
                                Console.WriteLine("Выберите студента:");
                                int i = 0;

                                foreach (var doc in students)
                                {
                                    Console.WriteLine(i++ + ":" + Student.Print(doc));
                                }
                                Console.Write("Студент:");
                                studentId = Convert.ToInt16(Console.ReadLine());
                            } while (studentId >= students.Count || studentId < 0);

                            DeleteStudent(students[studentId]["_id"].AsObjectId);

                            break;
                        }
                    case 8:
                        {
                            int groupId;
                            List<BsonDocument> groups = FindGroups();
                            do
                            {
                                Console.WriteLine("Выберите группу:");
                                int i = 0;

                                foreach (var doc in groups)
                                {
                                    Console.WriteLine(i++ + ":" + Group.Print(doc));
                                }
                                Console.Write("ГРуппа");
                                groupId = Convert.ToInt16(Console.ReadLine());
                            } while (groupId >= groups.Count || groupId < 0);


                            DeleteGroup(groups[groupId]["_id"].AsObjectId);
                            break;
                        }
                    case 9:
                        {
                            int studentId;
                            List<BsonDocument> students = FindStudents("");
                            do
                            {
                                Console.WriteLine("Выберите студента:");
                                int i = 0;

                                foreach (var doc in students)
                                {
                                    Console.WriteLine(i++ + ":" + Student.Print(doc));
                                }
                                Console.Write("Студент:");
                                studentId = Convert.ToInt16(Console.ReadLine());
                            } while (studentId >= students.Count || studentId < 0);
                            Student s = BsonSerializer.Deserialize<Student>(students[studentId]);

                            Console.WriteLine("DBRef");
                            Console.WriteLine(s.Ref.ToBsonDocument());
                            Console.WriteLine(FindGroups(s.Ref.Id)[0]);

                            Console.WriteLine("Вложенный документ");
                            Console.WriteLine(s.Group.ToBsonDocument());

                            Console.WriteLine("поиск по id документа");
                            Console.WriteLine(FindGroups(s.idGroup)[0]);

                            break;
                        }

                    default:
                        {
                            break;
                        }
                }
                Console.ReadLine();
            }

        }

        private static async Task AddStudent(Student student)
        {
            var collection = database.GetCollection<Student>("NewCollection");
            //BsonDocument doc = student.ToBsonDocument();
            await collection.InsertOneAsync(student);
        }

        private static async Task AddGroup(string name)
        {
            var collection = database.GetCollection<Group>("NewCollection2");
            Group group = new Group { Name = name };
            // BsonDocument doc = group.ToBsonDocument();
            await collection.InsertOneAsync(group);
        }

        private static List<BsonDocument> FindStudents(string name = null, Group group = null)
        {
            var collection = database.GetCollection<BsonDocument>("NewCollection");
            var builder = Builders<BsonDocument>.Filter;
            FilterDefinition<BsonDocument> filter;
            if (group == null)
                filter = builder.Gte("Name", name);
            else
                filter = builder.Gte("Name", name) & builder.Eq("Group", group);

            var students = collection.Find(filter).ToList();
            return students;
        }

        private static List<BsonDocument> FindGroups()
        {
            var collection = database.GetCollection<BsonDocument>("NewCollection2");
            var builder = Builders<BsonDocument>.Filter;
            var filter = new BsonDocument();
            var groups = collection.Find(filter).ToList();
            return groups;
        }
        private static List<BsonDocument> FindGroups(string _id)
        {
            var collection = database.GetCollection<BsonDocument>("NewCollection2");
            var builder = Builders<BsonDocument>.Filter;
            FilterDefinition<BsonDocument> filter = builder.Eq("_id", _id);
            var groups = collection.Find(filter).ToList();
            return groups;
        }
        private static List<BsonDocument> FindGroups(BsonValue _id)
        {
            var collection = database.GetCollection<BsonDocument>("NewCollection2");
            var builder = Builders<BsonDocument>.Filter;
            FilterDefinition<BsonDocument> filter = builder.Eq("_id", _id);
            var groups = collection.Find(filter).ToList();
            return groups;
        }

        private static async Task UpdateStudent(ObjectId id, Student newStudent)
        {
            var collection = database.GetCollection<Student>("NewCollection");
            newStudent.Id = id;
            var result = await collection.ReplaceOneAsync(new BsonDocument("_id", id), newStudent);
            Console.WriteLine("Найдено по соответствию: {0}; обновлено: {1}",
                result.MatchedCount, result.ModifiedCount);
        }

        private static async Task UpdateGroup(ObjectId id, Group newGroup)
        {
            var collection = database.GetCollection<Group>("NewCollection2");
            newGroup.Id = id;
            var result = await collection.ReplaceOneAsync(new BsonDocument("_id", id), newGroup);
            Console.WriteLine("Найдено по соответствию: {0}; обновлено: {1}",
                result.MatchedCount, result.ModifiedCount);
        }

        private static async Task DeleteStudent(ObjectId id)
        {
            var collection = database.GetCollection<BsonDocument>("NewCollection");

            var filter = Builders<BsonDocument>.Filter.Eq("_id", id);
            await collection.DeleteOneAsync(filter);

            var student = await collection.Find(new BsonDocument()).ToListAsync();
        }

        private static async Task DeleteGroup(ObjectId id)
        {
            var collection = database.GetCollection<BsonDocument>("NewCollection2");

            var filter = Builders<BsonDocument>.Filter.Eq("_id", id);
            await collection.DeleteOneAsync(filter);

            var group = await collection.Find(new BsonDocument()).ToListAsync();
        }
    }
}
