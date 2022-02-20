using ParallelApp.Shared.Models;
using System;
using System.Collections.Generic;

namespace ParallelApp.Shared.Dto
{
    public partial class TagForUpdateDto
    {
        public TagForUpdateDto()
        {

        }

        public string Name { get; set; } = null!;
        public string Color { get; set; }


    }
}
