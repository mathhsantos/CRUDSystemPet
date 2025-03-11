using CRUDSystemPet.Entities;
using CRUDSystemPet.Entities.Enums;
using System.Globalization;
using System.IO;
using System.Xml;

namespace CRUDSystemPet {
    internal class Program {
        static void Main(string[] args) {

            string pathForm = @"C:\Users\NOT170\Documents\projects\CRUDSystemPet\CRUDSystemPet\formulario.txt";

            int? option = null;

            while (option != 6) {
                Console.WriteLine("1. Cadastrar um novo pet");
                Console.WriteLine("2. Alterar os dados do pet cadastrado");
                Console.WriteLine("3. Deletar um pet cadastrado");
                Console.WriteLine("4. Listar todos os pets cadastrados");
                Console.WriteLine("5. Listar pets por algum critério (idade, nome, raça)");
                Console.WriteLine("6. Sair");

                Console.WriteLine();
                Console.Write("Escolha uma opção: ");
                option = int.Parse(Console.ReadLine());
                Console.WriteLine();

                switch (option) {
                    case 1:

                        using (StreamReader sr = File.OpenText(pathForm)) {

                            int cont = 0;
                            string[] responses = new string[7];
                            Adress adress = null;

                            while (!sr.EndOfStream) { 
                                Console.Write(sr.ReadLine() + " ");
                                responses[cont] = Console.ReadLine();
                                cont++;
                            }

                            PetDataBase.AddPet(
                                new Pet(
                                    responses[0],
                                    Enum.Parse<EPetType>(responses[1]),
                                    Enum.Parse<EPetSex>(responses[2]),
                                    responses[3],
                                    int.Parse(responses[4]),
                                    double.Parse(responses[5]),
                                    responses[6]
                                )
                            );
                        }

                        Console.WriteLine();
                        Console.WriteLine("PET cadastrado com sucesso!!!");
                        Console.WriteLine();
                        Console.WriteLine();

                        break;

                    case 5:

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

                        switch(criterio1) {
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

                        if(yesOrNot == 'y') {
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

                        Console.WriteLine();
                        Console.WriteLine("Pets encontrados de acordo com criterios de busca:");

                        PetDataBase.FindPet(options);

                        break;

                    case 4:
                        Console.WriteLine();
                        Console.WriteLine("Lista de todos os Pets cadastrados: ");
                        PetDataBase.ListPets();
                        Console.WriteLine();
                        Console.WriteLine();

                        break;
                }

            }
        }
    }
}

