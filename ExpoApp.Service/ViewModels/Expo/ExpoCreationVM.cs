using System;

namespace ExpoApp.Service.ViewModels.Expo
{
    public class ExpoCreationVM
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateOfOpening { get; set; }
        public DateTime DateOfClosing { get; set; }
        public string WorkHoursOpening { get; set; }
        public string WorkHoursClosing { get; set; }
        public string Organizer { get; set; }
        public int IndustryId { get; set; }
    }
}
