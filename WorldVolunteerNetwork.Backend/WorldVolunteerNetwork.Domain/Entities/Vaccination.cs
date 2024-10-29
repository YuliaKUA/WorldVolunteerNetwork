using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldVolunteerNetwork.Domain.Entities
{
    public class Vaccination : Common.Entity
    {
        private Vaccination() { }
        public Vaccination(string name, DateTime applied)
        {
            Name = name;
            Applied = applied;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public DateTime Applied { get; private set; }

    }
}
