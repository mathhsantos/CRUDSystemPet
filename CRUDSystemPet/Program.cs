using CRUDSystemPet.Models;
using CRUDSystemPet.Models.Enums;
using CRUDSystemPet.Repositories;
using System.Data;
using MySqlConnector;
using System.Globalization;


namespace CRUDSystemPet {
    internal class Program {
        static void Main(string[] args) {

            string? stringConnection = Environment.GetEnvironmentVariable("StringConnectorMySQL", EnvironmentVariableTarget.User);

            using IDbConnection db = new MySqlConnection(stringConnection);

            PetDataBase petDataBase = new PetDataBase(db);

            string pathForm = @"C:\Users\NOT170\Documents\projects\CRUDSystemPet\CRUDSystemPet\formulario.txt";

            int? option = null;

            while (option != 6) {
                Console.WriteLine();
                Console.WriteLine("1. Cadastrar um novo pet");
                Console.WriteLine("2. Alterar os dados do pet cadastrado");
                Console.WriteLine("3. Deletar um pet cadastrado");
                Console.WriteLine("4. Listar todos os pets cadastrados");
                Console.WriteLine("5. Listar pets por algum critério (idade, nome, raça)");
                Console.WriteLine("6. Sair");
                Console.WriteLine();

                Console.Write("Escolha uma opção: ");
                string input = Console.ReadLine();

                if (int.TryParse(input, out int tempOption)) {
                    option = tempOption;

                } else {
                    Console.WriteLine("Apenas números são aceitos");
                    continue;
                }

                Console.WriteLine();

                if (option <= 0) {
                    Console.WriteLine("Digite um número maior que 0");
                    Console.WriteLine();
                    continue;
                }

                switch (option) {

                    case 1:

                        string[] responses = new string[6];
                        string adress = null;

                        try {
                            using (StreamReader sr = File.OpenText(pathForm)) {

                                int cont = 0;

                                while (!sr.EndOfStream) {
                                    string question = sr.ReadLine() + " ";


                                    Console.Write(question + "");

                                    if (question[0] == '4') {
                                        Console.WriteLine();
                                        Console.Write("Digite o numero da casa: ");
                                        int houseNumber = int.Parse(Console.ReadLine());

                                        Console.Write("Digite o nome da cidade: ");
                                        string city = Console.ReadLine();

                                        Console.Write("Digite o nome da rua: ");
                                        string street = Console.ReadLine();

                                        adress = $"{street}, {houseNumber} - {city}";
                                        continue;
                                    }


                                    responses[cont] = Console.ReadLine();
                                    cont++;
                                }
                            }

                        }
                        catch (IOException e) {
                            Console.WriteLine(e.Message);
                        }

                        petDataBase.AddPet(
                            new Pet(
                                responses[0],
                                Enum.Parse<EPetType>(responses[1]),
                                Enum.Parse<EPetSex>(responses[2]),
                                adress,
                                int.Parse(responses[3], CultureInfo.InvariantCulture),
                                double.Parse(responses[4], CultureInfo.InvariantCulture),
                                responses[5]
                            )
                        );

                        Console.WriteLine();
                        Console.WriteLine("PET cadastrado com sucesso!!!");
                        Console.WriteLine();
                        Console.WriteLine();

                        break;

                    case 2:

                        List<Pet> filteredList = petDataBase.FindPets();
                        string adressUpdate = null;

                        if (filteredList == null) {
                            break;
                        }

                        if (filteredList.Count == 0) {
                            Console.WriteLine();
                            Console.WriteLine("Nenhum Pet foi encontrado com essa pesquisa!");
                            Console.WriteLine();
                            break;
                        }

                        Console.Write("Qual dos PETs acima gostaria de alterar? ");
                        int iDSelectedPet = int.Parse(Console.ReadLine());

                        Pet selectedPet = petDataBase.FindOnePet(iDSelectedPet);

                        Console.WriteLine();
                        Console.WriteLine($"Alterando dados de ({selectedPet.Name}):");
                        Console.WriteLine();

                        string[] responses2 = new string[4];

                        try {
                            using (StreamReader sr = File.OpenText(pathForm)) {

                                int cont = 0;

                                while (!sr.EndOfStream) {
                                    string? question = sr.ReadLine();

                                    if (question[0] == '4') {
                                        Console.WriteLine("Digite os dados do endereço: ");
                                        Console.Write("Digite o numero da casa: ");
                                        int houseNumber = int.Parse(Console.ReadLine());

                                        Console.Write("Digite o nome da cidade: ");
                                        string city = Console.ReadLine();

                                        Console.Write("Digite o nome da rua: ");
                                        string street = Console.ReadLine();

                                        adressUpdate = $"{street}, {houseNumber} - {city}";
                                        continue;
                                    }

                                    if (!question.Contains("2 -") && !question.Contains("3 -")) {
                                        Console.Write(question.Substring(4) + " ");
                                        responses2[cont] = Console.ReadLine();
                                        cont++;
                                    }
                                }
                            }

                        } catch (IOException e) {
                            Console.WriteLine(e.Message);
                        }

                        selectedPet.AlterPet(
                            responses2[0],
                            adressUpdate,
                            int.Parse(responses2[1]),
                            double.Parse(responses2[2], CultureInfo.InvariantCulture),
                            responses2[3]
                        );

                        petDataBase.UpdatePet(selectedPet);
                        Console.WriteLine();
                        Console.WriteLine("Segue lista atualizada: ");

                        petDataBase.ListPets();

                        break;

                    case 3:

                        List<Pet> filteredListForDelete = petDataBase.FindPets();

                        if (filteredListForDelete == null) {
                            break;
                        }

                        if (filteredListForDelete.Count == 0) {
                            Console.WriteLine();
                            Console.WriteLine("Nenhum Pet foi encontrado com essa pesquisa!");
                            Console.WriteLine();
                            break;
                        }

                        Console.WriteLine();
                        Console.Write("Qual dos PETs acima gostaria de Excluir? ");
                        int idSelectedPetForDelete = int.Parse(Console.ReadLine());

                        Pet foudPet = petDataBase.FindOnePet(idSelectedPetForDelete);

                        Console.WriteLine();
                        Console.Write($"Tem certeza que quer excluir {foudPet.Name}? (SIM/NAO): ");
                        string yesOrNot = Console.ReadLine();

                        if (yesOrNot == "SIM") {
                            petDataBase.RemovePet(foudPet.Id);
                            Console.WriteLine();
                            Console.WriteLine("Pet Excluido com sucesso!");
                            Console.WriteLine();
                            break;
                        }

                        Console.WriteLine();
                        Console.WriteLine("Operação de exclusão cancelada!!");
                        Console.WriteLine();

                        break;

                    case 4:

                        Console.WriteLine();
                        Console.WriteLine("Lista de todos os Pets cadastrados: ");
                        petDataBase.ListPets();
                        Console.WriteLine();
                        Console.WriteLine();

                        break;

                    case 5: petDataBase.FindPets(); break;

                    default: Console.WriteLine("Numero invalido, digite um número de 1 a 6 conforme menu: "); break;
                }
            }
        }
    }
}

