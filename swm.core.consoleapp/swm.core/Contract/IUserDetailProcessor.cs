using System;
using System.Collections.Generic;
using System.Text;
using swm.core.Contract;

namespace swm.core.Contract
{
    public interface IUserDetailProcessor
    {
        List<GendersPerAge> ProcessNumberofGendersPerAge(IEnumerable<UserDetail> userDetails);

        string ProcessUsersByAge(IEnumerable<UserDetail> userDetails, int age);

        UserDetail ProcessUserDetailsById(IEnumerable<UserDetail> userDetails, int id);
    }
}
