using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDSystemPet.Entities {
    class Adress {
        public int HouseNumber { get; private set; }
        public string City { get; private set; }
        public string Street { get; private set; }

        public Adress(int houseNumber, string city, string street) {
            HouseNumber = houseNumber;
            City = city;
            Street = street;
        }
    }
}
