using BookingSystemModel.Models;

namespace BookingSystem.DAL.Interfaces
{
    public interface IMemberDAL
    {
        Task UpsertManyMembersAsync(List<MemberModel> membersList);
        Task<MemberModel> GetMember_ByIdAsync(int memberId);
        Task<bool> UpdateMemberInfoAsync(MemberModel member);
    }
}
