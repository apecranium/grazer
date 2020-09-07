using System;
using System.Collections.Generic;
using Grazer.Models;

namespace Grazer.ViewModels
{
    public class UsersViewModel : IPaginated
    {
        public List<User> Users { get; set; }
        public int Page { get; set; }
        public int Total { get; set; }
        public int Count { get; set; }
        public int PageCount => (int)Math.Ceiling(decimal.Divide(Total, Count));
    }
}
