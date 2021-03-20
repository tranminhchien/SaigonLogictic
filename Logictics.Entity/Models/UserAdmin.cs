namespace Logictics.Entity.Models
{
    public class UserAdmin
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string Role { get; set; }
        public string Status { get; set; }
        public double? CreateDate { get; set; }
        public double? ModifyDate { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string FullName { get; set; }
    }
}
