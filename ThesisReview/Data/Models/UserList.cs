namespace ThesisReview.Data.Models
{
  public class UserList
  {
    public string UserListId { get; set; }
    public ApplicationUser ApplicationUser { get; set; }
    public string Mail { get; set; }
    public string Fullname { get; set; }
    public string Department { get; set; }
    public string Title { get; set; }
  }
}
