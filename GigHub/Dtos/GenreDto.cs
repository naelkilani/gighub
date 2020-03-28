using System.ComponentModel.DataAnnotations;

namespace GigHub.Dtos
{
    public class GenreDto
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(255)]
        public string Name { get; set; }
    }
}