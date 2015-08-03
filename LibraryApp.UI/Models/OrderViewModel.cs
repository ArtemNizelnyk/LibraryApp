using LibraryApp.BLL.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryApp.UI.Models
{
    public class OrderViewModel
    {
        [System.Web.Mvc.HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        [System.Web.Mvc.HiddenInput(DisplayValue = false)]
        public int UserId { get; set; }
        [System.Web.Mvc.HiddenInput(DisplayValue = false)]
        public int BookId { get; set; }
        public string BookName { get; set; }
        public DateTime? Date { get; set; }
    }
}