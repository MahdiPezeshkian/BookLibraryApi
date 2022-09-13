using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookLibraryApi.Models;

namespace BookLibraryApi.Dtos
{
    public class BookWithCountPropDto
    {
        public BookOutputDto book { get; set; }
        public int count { get; set; }
    }
}
