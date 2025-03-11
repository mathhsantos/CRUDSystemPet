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

            if (!Directory.Exists(path)) {
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

            }
            catch (IOException e) {
                Console.WriteLine(e.Message);
            }
        }

        static public void RemovePet(Pet pet) {
            petList.Remove(pet);

        }

        static public void ListPets() {

            int count = 1;

            foreach (Pet pet in petList) {

                Console.WriteLine($"{count}. {pet.Name} - {pet.Type} - {pet.Sex} - {pet.Adress} - {pet.Age} - {pet.Weight}Kg - {pet.Race}");

                count++;
            }
        }

        public static void FindPet(string[] arr) {

            List<Pet> search;
            int count = 1;

            if (!string.IsNullOrEmpty(arr[2])) {
                search = petList.FindAll(x =>
                    $"{x.Name}{x.Type}{x.Sex}{x.Adress}{x.Age}{x.Weight}{x.Race}".ToLower().Contains(arr[0].ToLower()) &&
                    $"{x.Name}{x.Type}{x.Sex}{x.Adress}{x.Age}{x.Weight}{x.Race}".ToLower().Contains(arr[1].ToLower()) &&
                    $"{x.Name}{x.Type}{x.Sex}{x.Adress}{x.Age}{x.Weight}{x.Race}".ToLower().Contains(arr[2].ToLower())
                );
            } else {
                search = petList.FindAll(x =>
                    $"{x.Name}{x.Type}{x.Sex}{x.Adress}{x.Age}{x.Weight}{x.Race}".ToLower().Contains(arr[0].ToLower()) &&
                    $"{x.Name}{x.Type}{x.Sex}{x.Adress}{x.Age}{x.Weight}{x.Race}".ToLower().Contains(arr[1].ToLower()));
            }

            foreach (Pet pet in search) {

                Console.WriteLine($"{count}. {pet.Name} - {pet.Type} - {pet.Sex} - {pet.Adress} - {pet.Age} - {pet.Weight}Kg - {pet.Race}");

                count++;
            }

            Console.WriteLine();
        }
    }
}
