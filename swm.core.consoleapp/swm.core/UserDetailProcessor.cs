using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using swm.core.Contract;

namespace swm.core
{
    public class UserDetailProcessor : IUserDetailProcessor
    {
        public List<GendersPerAge> ProcessNumberofGendersPerAge(IEnumerable<UserDetail> userDetails)
        {
            var groupedDetails = userDetails.Where(ud => ud.Gender == "M" | ud.Gender == "F").GroupBy(ud => ud.Age, ud => ud.Gender, (key, g) => new { Age = key, details = g.ToList() }).OrderBy(o => o.Age);
            var genderPerAgeDetails = new List<GendersPerAge>();
            groupedDetails.ToList().ForEach(gd =>
            {
                genderPerAgeDetails.Add(new GendersPerAge()
                {
                    Age = gd.Age,
                    FemaleCout = gd.details.Count(g => g.Equals("F")),
                    MaleCount = gd.details.Count(g => g.Equals("M"))
                });
            });

            return genderPerAgeDetails;
        }

        public UserDetail ProcessUserDetailsById(IEnumerable<UserDetail> userDetails, int id)
        {
            var selectedUser = userDetails.SingleOrDefault(ud => ud.Id == id);

            return selectedUser ?? null;
        }

        public string ProcessUsersByAge(IEnumerable<UserDetail> userDetails, int age)
        {
            var selectedUsersByAge = userDetails.Where(ud => ud.Age == age).ToList();

            var firstNames = new StringBuilder();
            selectedUsersByAge.ForEach(u =>
            {
                firstNames.Append(u.First).Append(',');
            });

            return firstNames.Remove(firstNames.Length - 1, 1).ToString();
        }
    }
}
