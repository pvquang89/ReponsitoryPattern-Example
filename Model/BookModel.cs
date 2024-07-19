namespace ReponsitoryPatternExample.Model
{
    public class BookModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        //FK
        public int AuthorId { get; set; }

    }
}
