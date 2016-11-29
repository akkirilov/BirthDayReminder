using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirthDayReminderApp
{
    class BirthDayReminder
    {
        public static string AddPerson()
        {
            Console.Write("Enter name: ");
            string personName = Console.ReadLine();
            DateTime personBirth;
            try
            {
                Console.Write("Enter birth date: ");
                DateTime.TryParse(Console.ReadLine(), out personBirth);
            }
            catch (Exception)
            {
                throw;
            }
            string person = personBirth.ToString("dd-MM-yyyy") + "*" + personName;
            return person; 
        }

        public static int Age(string DateOfBirth)
        {
            DateTime birthDate = DateTime.Parse(DateOfBirth);
            int age = DateTime.Today.Date >= birthDate.AddYears(DateTime.Today.Year - birthDate.Year).Date ?
                DateTime.Today.Year - birthDate.Year : DateTime.Today.Year - birthDate.Year - 1;
            return age;
        }

        public static string Days(string DateOfBirth)
        {
            DateTime birthDate = DateTime.Parse(DateOfBirth);
            string days = DateTime.Today.Date >= birthDate.AddYears(DateTime.Today.Year - birthDate.Year).Date ?
                (birthDate.AddYears(DateTime.Today.Year - birthDate.Year).Date - DateTime.Today.AddYears(-1).Date).ToString("dd") :
                (DateTime.Today.Date - birthDate.AddYears(DateTime.Today.Year - birthDate.Year).Date).ToString("dd");
            return days;
        }

        public static string BirthDay(string DateOfBirth)
        {
            DateTime birthDate = DateTime.Parse(DateOfBirth);
            DateTime newBirthDate = DateTime.Today.Date <= birthDate.AddYears(DateTime.Today.Year - birthDate.Year).Date ?
                birthDate.AddYears(DateTime.Today.Year - birthDate.Year) :
                birthDate.AddYears(DateTime.Today.Year - birthDate.Year + 1);
            string bd = newBirthDate.ToString("dd-MM-yyyy");
            return bd;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello Nasko!");
            Console.WriteLine();
            string path = System.IO.Directory.GetCurrentDirectory() + "\\file.txt";
            List<string> persons = System.IO.File.ReadAllLines(path).ToList();
            string line = "======================================================================";
            {
                string todayDay = DateTime.Today.DayOfWeek.ToString();
                string empty = new string(' ', (16 - todayDay.Length) / 2);
                Console.WriteLine(line);
                if (DateTime.Today.Date == DateTime.Parse(BirthDay("23/12/1987")))
                {
                    Console.WriteLine("||                |                                |                ||");
                    Console.WriteLine("||   " + DateTime.Today.ToString("dd-MM-yyyy") + "   |      Happy Birthdey Nasko!     |"
                        + empty + todayDay + (todayDay.Length % 2 == 1 ? empty + " " : empty) + "||");
                    Console.WriteLine("||                |                                |                ||");
                }
                else if (DateTime.Today.Date == DateTime.Parse(BirthDay("25/12/0001")))
                {
                    Console.WriteLine("||                |                                |                ||");
                    Console.WriteLine("||   " + DateTime.Today.ToString("dd-MM-yyyy") + "   |        Happy Christmas!        |"
                        + empty + todayDay + (todayDay.Length % 2 == 1 ? empty + " " : empty) + "||");
                    Console.WriteLine("||                |                                |                ||");
                }
                else if (DateTime.Today.Date == DateTime.Parse(BirthDay("31/12/0001")))
                {
                    Console.WriteLine("||                |                                |                ||");
                    Console.WriteLine("||   " + DateTime.Today.ToString("dd-MM-yyyy") + "   |        Happy New Year!         |"
                        + empty + todayDay + (todayDay.Length % 2 == 1 ? empty + " " : empty) + "||");
                    Console.WriteLine("||                |                                |                ||");
                }
                else
                {
                    Console.WriteLine("||                | Days before your BirthDay:  {0} |                ||", Days("23/12/1987"));
                    Console.WriteLine("||   " + DateTime.Today.ToString("dd-MM-yyyy") + "   | Days before best Christmas: {0} |"
                        + empty + todayDay + (todayDay.Length % 2 == 1 ? empty + " " : empty) + "||", Days("25/12/0001"));
                    Console.WriteLine("||                | Days before happy NewYear:  {0} |                ||", Days("31/12/0001"));
                }
            
                Console.WriteLine(line);
                Console.WriteLine("||===========    R    E    M    I    N    D    E    R    ===========||");
                List<DateTime> orderByDate = new List<DateTime>();
                for (int i = 0; i < persons.Count; i++)
                {
                    string[] t = persons[i].Split('*');
                    if (int.Parse(Days(t[0])) < 25 || DateTime.Parse(BirthDay(t[0])) == DateTime.Today)
                    {
                        orderByDate.Insert(0, DateTime.Parse(BirthDay(t[0])));
                    }
                }
                orderByDate.Sort();
                for (int i = 0; i < orderByDate.Count; i++)
                {
                    for (int j = 0; j < persons.Count; j++)
                    {
                        string[] t = persons[j].Split('*');
                        if (orderByDate[i].Date == DateTime.Parse(BirthDay(t[0])).Date 
                            && orderByDate[i] != DateTime.Parse(BirthDay("23-12-1987")))
                        {
                            string lineReminder = "|| " + (int.Parse(Days(t[0])) < 25 ? Days(t[0]) + " days before " : "Today is ") 
                                + t[1]  + "'s Birthday - " + BirthDay(t[0]);
                            Console.WriteLine("||" + new string(' ', (line.Length - 4)) + "||");
                            Console.WriteLine(lineReminder + new string(' ', (line.Length - lineReminder.Length - 2)) + "||");
                        }
                    }
                }
                Console.WriteLine(line);
            }
            Console.WriteLine("||=====================    M    E    N    U    =====================||");
            Console.WriteLine("||" + new string(' ', (line.Length - 4)) + "||");
            Console.WriteLine("|| Add person        a      Find person    f       View by near   l ||");
            Console.WriteLine("|| Delete person     d      Find by day    s       View by name   n ||");
            Console.WriteLine("|| Change birthdate  b      Find by month  m       View by birth  o ||");
            Console.WriteLine("|| Change name       c      Find by year   y       Return         r ||");
            Console.WriteLine(line);
            Console.WriteLine();

            string command = "";
            Console.Write("Enter command... ");
            command = Console.ReadLine().ToLower();

            while (command != "r")
            {
                if (command == "a")
                {
                    string temp = AddPerson();
                    string[] tNew = temp.Split('*');
                    bool unSucesfull = false;
                    for (int i = 0; i < persons.Count; i++)
                    {
                        string[] t = persons[i].Split('*');
                        if (t[1] == tNew[1])
                        {
                            unSucesfull = true;
                        }
                    }
                    if (unSucesfull == false && tNew[0] != "1.1.0001 г. 00:00:00 ч.")
                    {
                        persons.Add(temp);
                        System.IO.File.AppendAllText(path, temp + Environment.NewLine);
                        Console.WriteLine("Sucesfull add new person!");
                        Console.WriteLine();
                    }
                    else if (unSucesfull == false && tNew[0] == "1.1.0001 г. 00:00:00 ч.")
                    {
                        Console.WriteLine("Invalid date of birth!");
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine("This name allready exist in the database!");
                        Console.WriteLine();
                    }
                }

                else if (command == "l")
                {
                    List<DateTime> orderByDate = new List<DateTime>();
                    for (int i = 0; i < persons.Count; i++)
                    {
                        string[] t = persons[i].Split('*');
                        orderByDate.Insert(0, DateTime.Parse(BirthDay(t[0])));
                    }
                    orderByDate.Sort();
                    for (int i = 0; i < persons.Count; i++)
                    {
                        string temp = "";
                        for (int j = 0; j < persons.Count; j++)
                        {
                            string[] t = persons[j].Split('*');
                            if (orderByDate[i].Date == DateTime.Parse(BirthDay(t[0])).Date)
                            {
                                temp = t[1];
                                break;
                            }
                        }
                        Console.WriteLine(orderByDate[i].ToString("dd-MM-yyyy") + " " + temp);
                    }
                    Console.WriteLine();
                }

                else if (command == "n")
                {
                    List<string> orderByName = new List<string>();
                    for (int i = 0; i < persons.Count; i++)
                    {
                        string[] t = persons[i].Split('*');
                        orderByName.Insert(0, t[1]);
                    }
                    orderByName.Sort();
                    for (int i = 0; i < persons.Count; i++)
                    {
                        string temp = "";
                        for (int j = 0; j < persons.Count; j++)
                        {
                            string[] t = persons[j].Split('*');
                            if (orderByName[i] == t[1])
                            {
                                temp = DateTime.Parse(t[0]).ToString("dd-MM-yyyy");
                                break;
                            }
                        }
                        Console.WriteLine(temp + " " + orderByName[i]);
                    }
                    Console.WriteLine();
                }

                else if (command == "o")
                {
                    List<DateTime> orderByDate = new List<DateTime>();
                    for (int i = 0; i < persons.Count; i++)
                    {
                        string[] t = persons[i].Split('*');
                        orderByDate.Insert(0, DateTime.Parse(t[0]).Date);
                    }
                    orderByDate.Sort();
                    for (int i = 0; i < persons.Count; i++)
                    {
                        string temp = "";
                        for (int j = 0; j < persons.Count; j++)
                        {
                            string[] t = persons[j].Split('*');
                            if (orderByDate[i].Date == DateTime.Parse(t[0]).Date)
                            {
                                temp = t[1];
                                break;
                            }
                        }
                        Console.WriteLine(orderByDate[i].ToString("dd-MM-yyyy") + " " + temp);
                    }
                    Console.WriteLine();
                }

                else if (command == "s")
                {
                    Console.Write("Enter day number... ");
                    bool sucesfull = false;
                    string temp = Console.ReadLine();
                    for (int i = 0; i < persons.Count; i++)
                    {
                        string[] t = persons[i].Split('*');
                        string[] day = t[0].Split('-');
                        if (temp == day[0] || "0" + temp == day[0])
                        {
                            Console.WriteLine(t[0] + " " + t[1]);
                            sucesfull = true;
                        }
                    }
                    if (sucesfull == false)
                    {
                        Console.WriteLine("There are no person born in this day in your list!");
                    }
                    Console.WriteLine();
                    Console.WriteLine();
                }

                else if (command == "m")
                {
                    Console.Write("Enter month number... ");
                    bool sucesfull = false;
                    string temp = Console.ReadLine();
                    for (int i = 0; i < persons.Count; i++)
                    {
                        string[] t = persons[i].Split('*');
                        string[] month = t[0].Split('-');
                        if (temp == month[1] || "0" + temp == month[1])
                        {
                            Console.WriteLine(t[0] + " " + t[1]);
                            sucesfull = true;
                        }
                    }
                    if (sucesfull == false)
                    {
                        Console.WriteLine("There are no person born at this month in your list!");
                    }
                    Console.WriteLine();
                }

                else if (command == "y")
                {
                    Console.Write("Enter year... ");
                    bool sucesfull = false;
                    string temp = Console.ReadLine();
                    for (int i = 0; i < persons.Count; i++)
                    {
                        string[] t = persons[i].Split('*');
                        string[] year = t[0].Split('-');
                        if (temp == year[2] || "19" + temp == year[2] || "20" + temp == year[2])
                        {
                            Console.WriteLine(t[0] + " " + t[1]);
                            sucesfull = true;
                        }
                    }
                    if (sucesfull == false)
                    {
                        Console.WriteLine("There are no person born at this year in your list!");
                    }
                    Console.WriteLine();
                }

                else if (command == "c")
                {
                    Console.Write("Enter old name: ");
                    string tempO = Console.ReadLine();
                    Console.Write("Enter new name: ");
                    string tempN = Console.ReadLine();
                    bool sucesfull = false;
                    for (int i = 0; i < persons.Count; i++)
                    {
                        string[] t = persons[i].Split('*');
                        if (t[1] == tempO)
                        {
                            persons.RemoveAt(i);
                            persons.Insert(i, t[0] + "*" + tempN);
                            System.IO.File.WriteAllLines(path, persons);
                            Console.WriteLine("The change is sucesfull!");
                            Console.WriteLine();
                            sucesfull = true;
                            break;
                        }
                        if (sucesfull == false && i == persons.Count - 1)
                        {
                            Console.WriteLine("There are no person with this name!");
                            Console.WriteLine();
                        }
                    }
                }

                else if (command == "b")
                {
                    Console.Write("Enter name: ");
                    string nameForChange = Console.ReadLine();
                    Console.Write("Enter new date: ");
                    DateTime newDate;
                    if (DateTime.TryParse(Console.ReadLine(), out newDate))
                    {
                        bool sucesfull = false;
                        for (int i = 0; i < persons.Count; i++)
                        {
                            string[] t = persons[i].Split('*');
                            if (t[1] == nameForChange)
                            {
                                if (DateTime.Parse(t[0]) == newDate)
                                {
                                    Console.WriteLine("Entered date is the same!");
                                    Console.WriteLine();
                                    break;
                                }
                                else
                                {
                                    persons.RemoveAt(i);
                                    persons.Insert(i, newDate.ToString("dd-MM-yyyy") + "*" + t[1]);
                                    System.IO.File.WriteAllLines(path, persons);
                                    Console.WriteLine("The change is sucesfull!");
                                    Console.WriteLine();
                                    sucesfull = true;
                                    break;
                                }
                            }
                            else if (sucesfull == false && i == persons.Count - 1)
                            {
                                Console.WriteLine("There are no person with this name!");
                                Console.WriteLine();
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid date of birth!");
                    }
                }

                else if (command == "d")
                {
                    Console.Write("Enter name: ");
                    string tempO = Console.ReadLine();
                    bool sucesfull = false;
                    for (int i = 0; i < persons.Count; i++)
                    {
                        string[] t = persons[i].Split('*');
                        if (t[1] == tempO)
                        {
                            persons.RemoveAt(i);
                            System.IO.File.WriteAllLines(path, persons);
                            Console.WriteLine("Sucesfully remove person!");
                            Console.WriteLine();
                            sucesfull = true;
                            break;
                        }
                        else if (sucesfull == false && i == persons.Count - 1)
                        {
                            Console.WriteLine("There are no person with this name!");
                            Console.WriteLine();
                        }
                    }
                }

                else if (command == "f")
                {
                    Console.Write("Enter name:    ");
                    string tempO = Console.ReadLine();
                    bool sucesfull = false;
                    for (int i = 0; i < persons.Count; i++)
                    {
                        string[] t = persons[i].Split('*');
                        if (t[1] == tempO)
                        {
                            Console.WriteLine("BirthDay:      {0}", BirthDay(t[0]));
                            Console.WriteLine("Days left:     {0}", Days(t[0]));
                            Console.WriteLine("Age:           {0}", Age(t[0]));
                            Console.WriteLine("Date of bitrh: {0}", DateTime.Parse(t[0]).ToString("dd-MM-yyyy"));
                            Console.WriteLine();
                            sucesfull = true;
                            break;
                        }
                        else if (sucesfull == false && i == persons.Count - 1)
                        {
                            Console.WriteLine("There are no person with this name!");
                            Console.WriteLine();
                        }
                    }
                }

                Console.Write("Enter command... ");
                command = Console.ReadLine().ToLower();
            }

            Console.WriteLine();
            Console.WriteLine("GoodBye Nasko!");
            Console.WriteLine();
        }
    }
}