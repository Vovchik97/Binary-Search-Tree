/* Разработать программу, которая содержит текущую информацию о
заявлениях на приобретение туристических путевок. 
Каждое заявление содержит: 
- название страны посещения; 
- время года; 
- фамилию и инициалы отдыхающего; 
- желаемую дату отправления; 
- сроки пребывания; 
- уровень обслуживания. 
Программа должна обеспечивать: 
- хранение всех заявлений в виде двоичного дерева; 
- добавление и удаление заявлений; 
- вывод заявлений по фамилии и инициалам отдыхающих с их
последующим удалением; 
- вывод всех заявок. 
Программа должна обеспечивать диалог с помощью меню и контроль
ошибок при вводе. */


using System;
using System.Text;

namespace TourismApplication
{
    class Program
    {
        class Application
        {
            public string Country { get; set; }
            public string Season { get; set; }
            public string Name { get; set; }
            public DateTime DepartureDate { get; set; }
            public int Duration { get; set; }
            public string ServiceLevel { get; set; }
            public Application Left { get; set; }
            public Application Right { get; set; }
        }

        class ApplicationTree
        {
            private Application root;

            public void Add(Application application)
            {
                if (root == null)
                {
                    root = application;
                }
                else
                {
                    AddRecursive(root, application);
                }
            }

            private void AddRecursive(Application current, Application application)
            {
                if (application.Name.CompareTo(current.Name) < 0)
                {
                    if (current.Left == null)
                    {
                        current.Left = application;
                    }
                    else
                    {
                        AddRecursive(current.Left, application);
                    }
                }
                else
                {
                    if (current.Right == null)
                    {
                        current.Right = application;
                    }
                    else
                    {
                        AddRecursive(current.Right, application);
                    }
                }
            }

            public void Remove(string name)
            {
                root = RemoveRecursive(root, name);
            }

            private Application RemoveRecursive(Application current, string name)
            {
                if (current == null)
                {
                    return null;
                }

                if (name.CompareTo(current.Name) < 0)
                {
                    current.Left = RemoveRecursive(current.Left, name);
                }
                else if (name.CompareTo(current.Name) > 0)
                {
                    current.Right = RemoveRecursive(current.Right, name);
                }
                else
                {
                    if (current.Left == null)
                    {
                        return current.Right;
                    }
                    else if (current.Right == null)
                    {
                        return current.Left;
                    }

                    current.Name = FindMinName(current.Right);
                    current.Right = RemoveRecursive(current.Right, current.Name);
                }

                return current;
            }

            private string FindMinName(Application current)
            {
                while (current.Left != null)
                {
                    current = current.Left;
                }

                return current.Name;
            }

            public void PrintAll()
            {
                PrintRecursive(root);
            }

            private void PrintRecursive(Application current)
            {
                if (current != null)
                {
                    PrintRecursive(current.Left);
                    Console.WriteLine("Country: {0}, Season: {1}, Name: {2}, Departure Date: {3}, Duration: {4}, Service Level: {5}",
                        current.Country, current.Season, current.Name, current.DepartureDate.ToString("dd/MM/yyyy"), current.Duration, current.ServiceLevel);
                    PrintRecursive(current.Right);
                }
            }

            public void PrintAndRemoveByName(string name)
            {
                PrintAndRemoveByNameRecursive(root, name);
            }

            private void PrintAndRemoveByNameRecursive(Application current, string name)
            {
                if (current != null)
                {
                    PrintAndRemoveByNameRecursive(current.Left, name);
                    if (current.Name == name)
                    {
                        Console.WriteLine("Country: {0}, Season: {1}, Name: {2}, Departure Date: {3}, Duration: {4}, Service Level: {5}",
                            current.Country, current.Season, current.Name, current.DepartureDate.ToString("dd/MM/yyyy"), current.Duration, current.ServiceLevel);
                        root = RemoveRecursive(root, current.Name);
                    }
                    PrintAndRemoveByNameRecursive(current.Right, name);
                }
            }
        }

        static void Main(string[] args)
        {
            System.Console.OutputEncoding = System.Text.Encoding.UTF8;
            System.Console.InputEncoding = Encoding.GetEncoding(1251);
            ApplicationTree applicationTree = new ApplicationTree();

            while (true)
            {
                Console.WriteLine("Меню:");
                Console.WriteLine("1. Добавить заявление");
                Console.WriteLine("2. Удалить заявление по имени");
                Console.WriteLine("3. Вывести все заявления");
                Console.WriteLine("4. Вывести заявления по имени и удалить");
                Console.WriteLine("5. Выход");

                Console.Write("Выберите действие (1-5): ");
                string choice = Console.ReadLine();

                Console.WriteLine();

                if (choice == "1")
                {
                    Application application = new Application();

                    Console.Write("Введите название страны посещения: ");
                    application.Country = Console.ReadLine();

                    Console.Write("Введите время года: ");
                    application.Season = Console.ReadLine();

                    Console.Write("Введите фамилию и инициалы отдыхающего: ");
                    application.Name = Console.ReadLine();

                    Console.Write("Введите желаемую дату отправления (в формате дд.мм.гггг): ");
                    string departureDateStr = Console.ReadLine();
                    application.DepartureDate = DateTime.ParseExact(departureDateStr, "dd.MM.yyyy", null);

                    Console.Write("Введите сроки пребывания: ");
                    application.Duration = int.Parse(Console.ReadLine());

                    Console.Write("Введите уровень обслуживания: ");
                    application.ServiceLevel = Console.ReadLine();

                    applicationTree.Add(application);

                    Console.WriteLine("Заявление успешно добавлено!");
                }
                else if (choice == "2")
                {
                    Console.Write("Введите фамилию и инициалы отдыхающего для удаления: ");
                    string name = Console.ReadLine();
                    applicationTree.Remove(name);
                    Console.WriteLine("Заявление успешно удалено!");
                }
                else if (choice == "3")
                {
                    Console.WriteLine("Все заявления:");
                    applicationTree.PrintAll();
                }
                else if (choice == "4")
                {
                    Console.Write("Введите фамилию и инициалы отдыхающего для вывода и удаления: ");
                    string name = Console.ReadLine();
                    Console.WriteLine("Заявление(я) с указанным именем:");
                    applicationTree.PrintAndRemoveByName(name);
                }
                else if (choice == "5")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Неверный выбор. Пожалуйста, выберите действие от 1 до 5.");
                }

                Console.WriteLine();
            }
        }
    }
}