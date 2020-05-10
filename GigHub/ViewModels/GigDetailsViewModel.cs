using GigHub.Dtos;

namespace GigHub.ViewModels
{
    public class GigDetailsViewModel
    {
        public GigDto Gig { get; set; }
        public bool IsFollowing { get; set; }
        public bool IsAttending { get; set; }
        public string FollowingButtonText => IsFollowing ? "Following" : "Follow";
    }
}