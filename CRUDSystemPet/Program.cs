using CRUDSystemPet.Entities;
using CRUDSystemPet.Entities.Enums;
using System.Globalization;
using System.IO;
using System.Threading.Channels;
using System.Xml;
using static System.Net.Mime.MediaTypeNames;

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
                        string[] responses = new string[7];

                        try {
                            using (StreamReader sr = File.OpenText(pathForm)) {

                                int cont = 0;

                                Adress adress = null;

                                while (!sr.EndOfStream) {
                                    Console.Write(sr.ReadLine() + " ");
                                    responses[cont] = Console.ReadLine();
                                    cont++;
                                }
                            }

                        } catch(IOException e ) {
                            Console.WriteLine(e.Message);
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
                        
                        Console.WriteLine();
                        Console.WriteLine("PET cadastrado com sucesso!!!");
                        Console.WriteLine();
                        Console.WriteLine();

                        break;

                    case 2: 
                        
                        List<Pet> filteredList = PetDataBase.FindPet();

                        if(filteredList == null) {
                            break;
                        }

                        if(filteredList.Count == 0) {
                            Console.WriteLine();
                            Console.WriteLine("Nenhum Pet foi encontrado com essa pesquisa!");
                            Console.WriteLine();
                            break;
                        }

                        Console.WriteLine("Qual dos PETs acima gostaria de alterar? ");
                        int indexSelectedPet = int.Parse(Console.ReadLine());

                        Pet selectedPet = filteredList[indexSelectedPet - 1];

                        Console.WriteLine($"Alterando dados de ({selectedPet.Name}):");
                        Console.WriteLine();

                        string[] responses2 = new string[5];

                        try {
                            using (StreamReader sr = File.OpenText(pathForm)) {

                                int cont = 0;

                                while (!sr.EndOfStream) {
                                    string? question = sr.ReadLine();

                                    if(!question.Contains("2 -") && !question.Contains("3 -")) {
                                        Console.Write(question.Substring(4) + " ");
                                        responses2[cont] = Console.ReadLine();
                                        cont++;
                                    }    
                                }
                            }

                        } catch(IOException e) {
                            Console.WriteLine(e.Message);
                        }

                        PetDataBase.UpdatePet(selectedPet, responses2);
                        Console.WriteLine();
                        Console.WriteLine("Segue lista atualizada: ");

                        PetDataBase.ListPets();

                        break;

                    case 3:

                        List<Pet> filteredListForDelete = PetDataBase.FindPet();

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
                        int indexSelectedPetForDelete = int.Parse(Console.ReadLine());

                        Pet selectedPetForDelete = filteredListForDelete[indexSelectedPetForDelete - 1];

                        Console.WriteLine();
                        Console.Write($"Tem certeza que quer excluir {selectedPetForDelete.Name}? (SIM/NAO): ");
                        string yesOrNot = Console.ReadLine();

                        if(yesOrNot == "SIM") {
                            PetDataBase.RemovePet(selectedPetForDelete); 
                            
                            break;
                        }

                        Console.WriteLine("Operação de exclusão cancelada!!");
                        Console.WriteLine();

                        break;

                    case 4:

                        Console.WriteLine();
                        Console.WriteLine("Lista de todos os Pets cadastrados: ");
                        PetDataBase.ListPets();
                        Console.WriteLine();
                        Console.WriteLine();

                        break;

                    case 5: PetDataBase.FindPet(); break;
                }

            }
        }
    }
}

