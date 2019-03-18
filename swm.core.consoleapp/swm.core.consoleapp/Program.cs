using System;
using System.Collections.Generic;
using Newtonsoft.Json;


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

        private static List<swm.core.Contract.UserDetail> ExtractUserDetails(string path)
        {
            var inputData = System.IO.File.ReadAllText(path);
            var userDetails = JsonConvert.DeserializeObject<List<swm.core.Contract.UserDetail>>(inputData);
            return userDetails;
        }

        private static void ProcessNumberofGendersPerAge(IEnumerable<swm.core.Contract.UserDetail> userDetails)
        {
            var gendersPerAgeDetails = new UserDetailProcessor().ProcessNumberofGendersPerAge(userDetails);
            gendersPerAgeDetails.ForEach(gd =>
            {
                Console.WriteLine($"Age : {gd.Age}, Female : {gd.FemaleCout}, Male : {gd.MaleCount}");
            });
        }

        private static void ProcessUsersByAge(IEnumerable<swm.core.Contract.UserDetail> userDetails, int age)
        {
            Console.WriteLine($"All the users first names who are {age}");
            var selectedUserDetail = new UserDetailProcessor().ProcessUsersByAge(userDetails, age);
            Console.WriteLine(selectedUserDetail);
        }

        private static void ProcessUserDetailsById(IEnumerable<swm.core.Contract.UserDetail> userDetails, int id)
        {
            Console.WriteLine($"The users full name for id={id}");
            var selectedUserDetail = new UserDetailProcessor().ProcessUserDetailsById(userDetails, id);
            Console.WriteLine(selectedUserDetail != null
                ? $"Result : {selectedUserDetail.First} {selectedUserDetail.Last}"
                : "Result : Not found");
        }
    }
}
