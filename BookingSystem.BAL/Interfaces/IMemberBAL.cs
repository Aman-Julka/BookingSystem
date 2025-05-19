using BookingSystemModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.BAL.Interfaces
{
    public interface IMemberBAL
    {
        Task UpsertManyMembersAsync(List<MemberModel> membersList);
    }
}
