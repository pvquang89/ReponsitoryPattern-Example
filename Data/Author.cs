using ReponsitoryPatternExample.Model;
using System.Text.Json.Serialization;

namespace ReponsitoryPatternExample.Data
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }


        [JsonIgnore]
        // Navigation property
        public ICollection<Book> Books { get; set; }
    }
}
