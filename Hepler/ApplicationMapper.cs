using AutoMapper;
using ReponsitoryPatternExample.Data;
using ReponsitoryPatternExample.Model;
using System.Runtime;

namespace ReponsitoryPatternExample.Hepler
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            //map 2 chiều qua lại book và bookmodel
            CreateMap<Book, BookModel>().ReverseMap();
            CreateMap<Author, AuthorModel>().ReverseMap();
        }

    }
}
