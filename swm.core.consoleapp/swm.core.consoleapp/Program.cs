using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using swm.core.consoleapp.Model;

namespace swm.core.consoleapp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Console app starts ...");

            try
            {
                var userDetails = ExtractUserDetails(@"C:/Data/example_data.json");

                ProcessUserDetailsById(userDetails, 42);

                ProcessUsersByAge(userDetails, 23);

                ProcessNumberofGendersPerAge(userDetails);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Process user details data failed with error {ex}");
            }

            Console.WriteLine("Press any key to exit console app ...");
            Console.ReadKey();
        }

        private static List<UserDetail> ExtractUserDetails(string path)
        {
            var inputData = System.IO.File.ReadAllText(path);
            var userDetails = JsonConvert.DeserializeObject<List<UserDetail>>(inputData);
            return userDetails;
        }

        private static void ProcessNumberofGendersPerAge(IEnumerable<UserDetail> userDetails)
        {
            Console.WriteLine("The number of genders per age, displayed from youngest to oldest");
            var groupedDetails = userDetails.Where(ud => ud.Gender == "M" | ud.Gender == "F").GroupBy(ud => ud.Age, ud => ud.Gender, (key, g) => new { Age = key, details = g.ToList() }).OrderBy(o => o.Age);
            groupedDetails.ToList().ForEach(gd =>
            {
                Console.WriteLine($"Age : {gd.Age}, Female : {gd.details.Count(g => g.Equals("F"))}, Male : {gd.details.Count(g => g.Equals("M"))}");
            });
        }

        private static void ProcessUsersByAge(IEnumerable<UserDetail> userDetails, int age)
        {
            Console.WriteLine("All the users first names who are 23");
            var selectedUsersByAge = userDetails.Where(ud => ud.Age == age).ToList();
            selectedUsersByAge.ForEach(u =>
            {
                Console.WriteLine($"{u.Last}, {u.First}");
            });
        }

        private static void ProcessUserDetailsById(IEnumerable<UserDetail> userDetails, int id)
        {
            Console.WriteLine("The users full name for id=42");
            var selectedUser = userDetails.SingleOrDefault(ud => ud.Id == id);
            Console.WriteLine($"Result : {selectedUser}");
        }
    }
}
