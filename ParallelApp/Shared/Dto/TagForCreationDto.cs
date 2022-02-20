﻿using ParallelApp.Shared.Models;
using System;
using System.Collections.Generic;

namespace ParallelApp.Shared.Dto
{
    public partial class TagForCreationDto
    {
        public TagForCreationDto()
        {

        }

        public string Name { get; set; } = null!;
        public int SchoolId { get; set; }
        public string Type { get; set; } = null!;
        public string Color { get; set; }


    }
}
