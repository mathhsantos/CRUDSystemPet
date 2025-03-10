using CRUDSystemPet.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDSystemPet.Entities {

    class Pet {

        string padrao = "NÃO INFORMADO";
        public string? Name { get; private set; }
        public EPetType Type { get; private set; }
        public EPetSex Sex { get; private set; }
        public string Adress { get; private set; }
        public int  Age { get; private set; }
        public double Weight { get; private set; }
        public string? Race { get; private set; }

        public Pet(string? name, EPetType type, EPetSex sex, string? adress, int age, double weight, string? race) {
            Name = name;
            Type = type;
            Sex = sex;
            Adress = adress;
            Age = age;
            Weight = weight;
            Race = race;
        }

        public void AlterPet(string? name, EPetType type, EPetSex sex, string? adress, int age, double weight, string? race) {
            Name = name;
            Type = type;
            Sex = sex;
            Adress = adress;
            Age = age;
            Weight = weight;
            Race = race;
        }
    }
}
