namespace Hall_Of_Fame.Data.Models
{

    /// <summary>
    /// Модель Person
    /// </summary>
    public class Person
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public List<Skills> Skills { get; set; } 
    }
}