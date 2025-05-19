using BookingSystem.BAL.Interfaces;
using BookingSystem.DAL.Interfaces;
using BookingSystemModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.BAL.Services
{
    public class MemberBAL : IMemberBAL
    {
        private readonly IMemberDAL _memberDAL;

        public MemberBAL(IMemberDAL memberDAL)
        {
            _memberDAL = memberDAL;
        }

        public async Task UpsertManyMembersAsync(List<MemberModel> membersList)
        {
            try
            {
                await _memberDAL.UpsertManyMembersAsync(membersList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
