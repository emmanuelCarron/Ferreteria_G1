using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FerreteriaNetCore.Models.DTOs.ResponseDTO
{
    public class PsearchDTO
    {

        public long Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Quantity { get; set; }
    }
}
