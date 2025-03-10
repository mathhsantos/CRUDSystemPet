using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CRUDSystemPet.Entities {
    static class PetDataBase {
        static private List<Pet> petList = new List<Pet>();


        static public void AddPet(Pet pet) {
            petList.Add(pet);

            string path = @"C:\Users\NOT170\Documents\projects\CRUDSystemPet\CRUDSystemPet\petsCadastrados\";

            if(!Directory.Exists(path)) {
                Directory.CreateDirectory(path);
            }
                

            try {
                using (StreamWriter sw = File.CreateText(path + $"{DateTime.Now.ToString("yyyyMMddTHHmm")} - {pet.Name.Trim().ToUpper().Replace(" ", "")}.txt")) {
                    sw.WriteLine($"1 - {pet.Name}");
                    sw.WriteLine($"2 - {pet.Type}");
                    sw.WriteLine($"3 - {pet.Sex}");
                    sw.WriteLine($"4 - {pet.Adress}");
                    sw.WriteLine($"5 - {pet.Age}"); ;
                    sw.WriteLine($"6 - {pet.Weight.ToString("F2", CultureInfo.InvariantCulture)}Kg");
                    sw.WriteLine($"7 - {pet.Race}");
                }

            } catch (IOException e ) {
                Console.WriteLine(e.Message);
            }
        }

        static public void RemovePet(Pet pet) {
            petList.Remove(pet);

        }

        static public void ListPets() {

            int count = 1;

            foreach(Pet pet in petList) {

                Console.WriteLine($"{count}. {pet.Name} - {pet.Type} - {pet.Sex} - {pet.Adress} - {pet.Age} - {pet.Weight}Kg - {pet.Race}");

                count++;
            }

            
        }
    }
}
