using System;
using System.Collections.Generic;

namespace BLL
{
    public class Drink
    {
        public string Name { get; set; }

        public string Category { get; set; }

        public bool IsAlcoholic { get; set; }

        public string Instructions { get; set; }
        public List<Ingredient> Ingredients { get; set; }
    }
}