using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.ViewModel
{
    public class MesajModel
    {
        public int mesajId { get; set; }
        public string mesajText { get; set; }
        public int bulkMesaj { get; set; }
        public int grupId { get; set; }
        public int kimdenId { get; set; }
        public int kimeId { get; set; }
        public string kullaniciAd { get; internal set; }
    }
}