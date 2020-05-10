using GigHub.Validation;
using System;
using System.ComponentModel.DataAnnotations;

namespace GigHub.Dtos
{
    public class GigDto
    {
        private DateTime _dateTime;
        private string _date;
        private string _time;

        public int Id { get; set; }

        [Required]
        [FutureDate]
        public string Date
        {
            get => _dateTime == default ? string.Empty : _dateTime.ToString("d MMM yyyy");
            set => _date = value;
        }

        [Required]
        [Time]
        public string Time
        {
            get => _dateTime == default ? string.Empty : _dateTime.ToString("HH:mm");
            set => _time = value;
        }

        public string DateAndTime => $"{Date} {Time}";

        public DateTime DateTime
        {
            get
            {
                if (_dateTime != default)
                    return _dateTime;

                DateTime.TryParse($"{_date} {_time}", out _dateTime);

                return _dateTime;
            }

            set { _dateTime = value; }
        }

        [Required] [StringLength(255)] public string Venue { get; set; }

        public ArtistDto Artist { get; set; }

        public string ArtistId { get; set; }

        public GenreDto Genre { get; set; }

        [Required] [Display(Name = "Genre")] public int GenreId { get; set; }

        public bool Active { get; set; }

        public string Month => _dateTime != default
            ? DateTime.ToString("MMM")
            : string.Empty;

        public string Day => _dateTime != default
            ? DateTime.ToString("d ")
            : string.Empty;

        public string Year => _dateTime != default
            ? DateTime.ToString("yyyy")
            : string.Empty;

        public bool IsGoing { get; set; }

        public string AttendingButtonText => IsGoing ? "Going" : "Going?";
    }
}