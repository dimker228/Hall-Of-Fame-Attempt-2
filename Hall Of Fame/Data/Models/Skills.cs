namespace Hall_Of_Fame.Data.Models
{
    /// <summary>
    /// Модель Skills
    /// </summary>

    public class Skills
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte Level { get; set; }
        public long PersonId { get; set; }

    }
}