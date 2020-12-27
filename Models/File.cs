using System;
using System.Collections.Generic;

namespace Unsplash.Models
{
    public class File
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Path { get; set; }
        public DateTime? Uploaded { get; set; }
        public string Label { get; set; }
    }
}