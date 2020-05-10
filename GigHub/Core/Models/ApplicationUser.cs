using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GigHub.Core.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public ICollection<Gig> Gigs { get; set; }
        public ICollection<Following> Followers { get; set; }
        public ICollection<Following> Followees { get; set; }
        public ICollection<UserNotification> UserNotifications { get; set; }

        public ApplicationUser()
        {
            Gigs = new Collection<Gig>();
            Followees = new Collection<Following>();
            Followees = new Collection<Following>();
            UserNotifications = new Collection<UserNotification>();
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public void Notify(Notification notification)
        {
            UserNotifications.Add(new UserNotification
            {
                UserId = Id,
                Notification = notification
            });
        }

        public void ChangeFollowing(string artistId)
        {
            if (IsFollowing(artistId))
                UnFollow(artistId);
            else
                Follow(artistId);
        }

        //It is better to have this in a repository. 
        public bool IsFollowing(string artistId)
        {
            return Followees.Any(f => f.FolloweeId == artistId);
        }

        private void Follow(string artistId)
        {
            Followees.Add(new Following
            {
                FollowerId = Id,
                FolloweeId = artistId
            });
        }

        private void UnFollow(string artistId)
        {
            var following = Followees.First(f => f.FolloweeId == artistId);

            Followees.Remove(following);
        }

        //It is better to have this in a repository. 
        public bool IsAttending(int gigId)
        {
            return Gigs.Any(g => g.Id == gigId);
        }

        public void Attending(Gig gig)
        {
            Gigs.Add(gig);
        }

        public void NotAttending(Gig gig)
        {
            Gigs.Remove(gig);
        }
    }
}