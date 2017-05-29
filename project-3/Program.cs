using System;
using System.Collections.Generic;
using System.Linq;

namespace project3
{
    public class Product
    {
        public string Name { get; set; }
        public uint Price { get; set; }
        public uint Stock { get; set; }
    }

    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("========== Program Pembelian Produk Salon Alvion ==========");

            // initial value
            List<Product> products = new List<Product>() {
                new Product{Name = "SABUN MUKA", Price = 45000, Stock = 10},
                new Product{Name = "TONER", Price = 85000, Stock = 10},
                new Product{Name = "SCP", Price = 70000, Stock = 10},
                new Product{Name = "CREAM SIANG", Price = 80000, Stock=10},
                new Product{Name = "CREAM MALAM", Price = 80000, Stock=10},
                new Product{Name = "CREAM BEBAS", Price = 70000, Stock=10},
                new Product{Name = "CREAM ACNE", Price = 110000, Stock=10}
            };

            //variable declaration
            string input;
			uint n; //total item
			string item; //name of product
			uint perItem; //total each product
			bool availability; //checking if name exist and stock is enough
			uint totalPrice;
            uint counter = 0;
            uint choose;

            while (true) 
            {
                try 
                {
					//update jumlah stock terakhir
					Console.WriteLine("------------------ Update Data Terakhir ------------------");
					foreach (var obat in products)
						Console.WriteLine("Nama = " + obat.Name + ", harga = " + obat.Price + ", stock = " + obat.Stock);
					Console.WriteLine();

					//Tampilkan produk yang habis pake LINQ syntax method
					var empty = products.Where(obat => obat.Stock <= 0).OrderBy(obat => obat.Name);
					Console.WriteLine("----------------- Data Produk yang Habis -----------------");
					foreach (var obat in empty)
						Console.WriteLine("Nama = " + obat.Name + ", stock = " + obat.Stock);
					Console.WriteLine("----------------------------------------------------------");						

					//user choose menu
					Console.WriteLine();
                    Console.WriteLine("----------------------- MENU UTAMA ---------------------");
                    Console.WriteLine("-------------------- 1. PEMBELIAN ----------------------");
                    Console.WriteLine("-------------------- 2. PENAMBAHAN STOCK ---------------");
                    Console.WriteLine("-------------------- 3. QUIT ---------------------------");
                    Console.WriteLine("----------------------- Pilih Menu ---------------------");
                    choose = UInt32.Parse(Console.ReadLine());
                    if (choose == 1)
                    {
                        // initial value
                        totalPrice = 0;
                        counter++;

                        Console.WriteLine("---------- Pembelian item ----------");
						Console.WriteLine("Pembelian ke-" + counter);
                        Console.WriteLine("Berapa jenis item?");
						input = Console.ReadLine();

						// keluar jika user masukin "quit"
						if (input.ToLower() == "quit") break;
						
						n = UInt32.Parse(input);
						//Processing
						for (var i = 1; i <= n; i++)
						{
							Console.WriteLine("Nama item ke-" + i + " ?");
							item = Console.ReadLine();
							Console.WriteLine("Jumlah " + item + " yang ingin dibeli?");
							perItem = UInt32.Parse(Console.ReadLine());
							if (perItem < 0)
							{
								Console.WriteLine("Jumlah yang diminta tidak valid");
								continue;
							}

							//Checking in a list
							//availability = products.Any(obat => obat.Name == item.ToUpper()) && products.Any(obat => obat.Stock >= perItem); //bugs found, always com[are with initial value of products
							var singleProduct = products.Single(Item => Item.Name == item.ToUpper());
							availability = products.Any(obat => obat.Name == item.ToUpper()) && products.Any(obat => singleProduct.Stock >= perItem);
							if (availability)
							{
								Console.WriteLine("Ada");
								//var singleProduct = products.Single(Item => Item.Name == item.ToUpper()); //fixing bugs
								totalPrice += perItem * singleProduct.Price;
								singleProduct.Stock -= perItem;
							}
							else
							{
								Console.WriteLine("Maaf, produknya tidak tersedia");
							}
						}

						// Output harga
						Console.WriteLine("Total harga = Rp. " + totalPrice);
					}
                    else if(choose == 2)
                    {                    
						Console.WriteLine("---------- Penambahan item ----------");
						Console.WriteLine("Nama item");
						item = Console.ReadLine().ToUpper();

						// keluar jika user masukin "quit"
						if (item.ToLower() == "quit") break;

						if (products.Any(obat => obat.Name == item))
						{
                            var singleProduct = products.Single(Item => Item.Name == item.ToUpper());
							Console.WriteLine("Produk " + item + " , harga Rp " + singleProduct.Price + " tersedia sebanyak " + singleProduct.Stock + " buah. Tambahkan product?");
							perItem = UInt32.Parse(Console.ReadLine());
							singleProduct.Stock += perItem;
						}
						else
						{
							Console.WriteLine("Harga item");
							uint price = UInt32.Parse(Console.ReadLine());
							Console.WriteLine("Jumlah item");
							perItem = UInt32.Parse(Console.ReadLine());
							products.Add(new Product { Name = item, Price = price, Stock = perItem });
						}						
                    }
                    else 
                    {
                        Console.WriteLine("Keluar Program");
                        break;
                    }

				}

                //Error action from user input
                catch(FormatException) 
                {
					Console.WriteLine("Input not valid, please try again");
					continue;
                }
            }
        }
    }
}