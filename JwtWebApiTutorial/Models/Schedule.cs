namespace JwtWebApiTutorial.Models
{
    public class Schedule
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Start_checkin_at { get; set; }
        public DateTime End_checkin_at { get; set; }
        public DateTime Start_checkout_at { get; set; }
        public DateTime End_checkout_at { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
