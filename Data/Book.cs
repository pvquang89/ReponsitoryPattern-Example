using ReponsitoryPatternExample.Model;
using System.Text.Json.Serialization;

namespace ReponsitoryPatternExample.Data
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }

        //FK
        public int AuthorId { get; set; }


        [JsonIgnore]
        // Navigation property
        public Author Author { get; set; }
    }
}
