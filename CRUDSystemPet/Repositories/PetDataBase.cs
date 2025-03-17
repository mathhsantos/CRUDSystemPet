using CRUDSystemPet.Models;
using MySqlConnector;
using Dapper;
using System.Data;
using System.Globalization;
using CRUDSystemPet.Models.Enums;



namespace CRUDSystemPet.Repositories {
    class PetDataBase {

        private IDbConnection _connection;

        public PetDataBase(IDbConnection connection) {
            _connection = connection;
        }


        public void AddPet(Pet pet) {

            string sqlCommand = $"INSERT INTO Pet " +
                                    $"(Name, Type, Sex, Adress, Age, Weight, Race, CreatedAT) " +
                                $"VALUES" +
                                    $"(@Name, " +
                                    $"@Type, " +
                                    $"@Sex, " +
                                    $"@Adress, " +
                                    $"@Age, " +
                                    $"@Weight, " +
                                    $"@Race," +
                                    $"@CreatedAt);";

            _connection.Execute(sqlCommand, new {
                pet.Name,
                pet.Type,
                pet.Sex,
                pet.Adress,
                pet.Age,
                Weight = pet.Weight.ToString("F2", CultureInfo.InvariantCulture),
                pet.Race,
                CreatedAt = pet.CretedAt.ToString("yyyy-MM-dd HH:mm:ss")
            });

            string path = @"C:\Users\NOT170\Documents\projects\CRUDSystemPet\CRUDSystemPet\petsCadastrados\";

            if (!Directory.Exists(path)) {
                Directory.CreateDirectory(path);
            }


            try {
                using (StreamWriter sw = File.CreateText(path + $"{DateTime.Now.ToString("yyyyMMddTHHmm")} - {pet.Name.Trim().ToUpper().Replace(" ", "")}.txt")) {
                    sw.WriteLine($"1 - {pet.Name}");
                    sw.WriteLine($"2 - {pet.Type}");
                    sw.WriteLine($"3 - {pet.Sex.ToString()}");
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

        public void ListPets() {

            string sqlCommand = $"SELECT * FROM Pet ORDER BY Id;";

            var listPets = _connection.Query<Pet>(sqlCommand);

            foreach (Pet pet in listPets) {

                Console.WriteLine($"{pet.Id}. {pet.Name} - {pet.Type} - {pet.Sex} - ({pet.Adress}) - {pet.Age} anos - {pet.Weight.ToString("F2", CultureInfo.InvariantCulture)}Kg - {pet.Race}");
            }

            Console.WriteLine();
        }

        public Pet FindOnePet(int idPet) {

            string sqlCommand = $"SELECT * FROM Pet WHERE Id = @Id;";

            Pet? petFound = _connection.QueryFirstOrDefault<Pet>(sqlCommand, new { Id = idPet });

            if (petFound == null) {
                Console.WriteLine("Pet não encontrado!");
                return null;
            }

            return petFound;
        }

        public List<Pet> FindPets() {

            List<Pet> gambetaMonstra = null;

            List<Pet> petList = _connection.Query<Pet>("SELECT * FROM Pet;").ToList();

            if (petList.Count == 0) {
                Console.WriteLine("Cadastre pelo menos um Pet!!");
                Console.WriteLine();
                return gambetaMonstra;
            }

            string[] options = new string[3];

            Console.Write("Qual o tipo de animal? (Cachorro/Gato): ");
            string petType = Console.ReadLine();
            options[0] = petType;

            Console.WriteLine();

            Console.WriteLine("1 - Nome ou Sobrenome");
            Console.WriteLine("2 - Sexo");
            Console.WriteLine("3 - Idade");
            Console.WriteLine("4 - Peso");
            Console.WriteLine("5 - Raça");
            Console.WriteLine("6 - Endereço");
            Console.WriteLine();

            Console.Write("Qual dos criterios acima deseja escolher para a busca? ");
            int criterio1 = int.Parse(Console.ReadLine());

            switch (criterio1) {
                case 1:
                    Console.Write("Digite o nome ou sobrenome: ");
                    string name = Console.ReadLine();
                    options[1] = name;
                    break;

                case 2:
                    Console.Write("Digite o sexo (Macho/Femea): ");
                    string sex = Console.ReadLine();
                    options[1] = sex;
                    break;

                case 3:
                    Console.Write("Digite a idade: ");
                    string age = Console.ReadLine();
                    options[1] = age;
                    break;

                case 4:
                    Console.Write("Digite o peso: ");
                    string weight = Console.ReadLine();
                    options[1] = weight;
                    break;

                case 5:
                    Console.Write("Digite a Raça: ");
                    string race = Console.ReadLine();
                    options[1] = race;
                    break;

                case 6:
                    Console.Write("Digite o endereço: ");
                    string endereco = Console.ReadLine();
                    options[1] = endereco;
                    break;

                default:
                    Console.WriteLine("Número incorreto, voltando pro menu inicial!!");
                    Console.WriteLine();
                    break;
            }

            Console.Write("Deseja escolher mais um critério? (y/n): ");
            char yesOrNot = char.Parse(Console.ReadLine());

            if (yesOrNot == 'y') {
                Console.Write("Qual dos criterios acima deseja escolher para a busca? ");
                int criterio2 = int.Parse(Console.ReadLine());

                switch (criterio2) {
                    case 1:
                        Console.Write("Digite o nome ou sobrenome: ");
                        string name = Console.ReadLine();
                        options[2] = name;
                        break;

                    case 2:
                        Console.Write("Digite o sexo (Macho/Femea): ");
                        string sex = Console.ReadLine();
                        options[2] = sex;
                        break;

                    case 3:
                        Console.Write("Digite a idade: ");
                        string age = Console.ReadLine();
                        options[2] = age;
                        break;

                    case 4:
                        Console.Write("Digite o peso: ");
                        string weight = Console.ReadLine();
                        options[2] = weight;
                        break;

                    case 5:
                        Console.Write("Digite a Raça: ");
                        string race = Console.ReadLine();
                        options[2] = race;
                        break;

                    case 6:
                        Console.Write("Digite o endereço: ");
                        string endereco = Console.ReadLine();
                        options[2] = endereco;
                        break;

                    default:
                        Console.WriteLine("Número incorreto, voltando pro menu inicial!!");
                        Console.WriteLine();
                        break;
                }
            }

            List<Pet> search;
            int count = 1;

            if (!string.IsNullOrEmpty(options[2])) {
                search = petList.FindAll(x =>
                    $"{x.Name}{x.Type}{x.Sex}{x.Adress}{x.Age}{x.Weight.ToString("F2", CultureInfo.InvariantCulture)}{x.Race}".ToLower().Contains(options[0].ToLower()) &&
                    $"{x.Name}{x.Type}{x.Sex}{x.Adress}{x.Age}{x.Weight.ToString("F2", CultureInfo.InvariantCulture)}{x.Race}".ToLower().Contains(options[1].ToLower()) &&
                    $"{x.Name}{x.Type}{x.Sex}{x.Adress}{x.Age}{x.Weight.ToString("F2", CultureInfo.InvariantCulture)}{x.Race}".ToLower().Contains(options[2].ToLower())
                );
            }
            else {
                search = petList.FindAll(x =>
                    $"{x.Name}{x.Type}{x.Sex}{x.Adress}{x.Age}{x.Weight.ToString("F2", CultureInfo.InvariantCulture)}{x.Race}".ToLower().Contains(options[0].ToLower()) &&
                    $"{x.Name}{x.Type}{x.Sex}{x.Adress}{x.Age}{x.Weight.ToString("F2", CultureInfo.InvariantCulture)}{x.Race}".ToLower().Contains(options[1].ToLower()));
            }

            Console.WriteLine();
            Console.WriteLine("Pets encontrados de acordo com criterios de busca:");

            foreach (Pet pet in search) {

                Console.WriteLine($"{pet.Id}. {pet.Name} - {pet.Type} - {pet.Sex} - ({pet.Adress}) - {pet.Age} anos - {pet.Weight.ToString("F2", CultureInfo.InvariantCulture)}Kg - {pet.Race}");
            }

            Console.WriteLine();

            return search;
        }

        public void UpdatePet(Pet pet) {

            string sqlCommand = "UPDATE Pet SET Name = @Name, Adress = @Adress, Age = @Age, Weight = @Weight, Race = @Race WHERE Id = @Id;";

            _connection.Execute(sqlCommand, new {
                pet.Id,
                pet.Name,
                pet.Adress,
                pet.Age,
                pet.Weight,
                pet.Race
            });
        }

        public void RemovePet(int petiD) {

            string sqlCommand = "DELETE FROM pet WHERE id = @iD;";

            _connection.Execute(sqlCommand, new { Id = petiD });

        }
    }
}
