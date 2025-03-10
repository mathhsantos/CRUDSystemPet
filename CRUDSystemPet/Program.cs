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

