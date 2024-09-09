using System;
using System.Collections.Generic;

namespace ProduceDept {

    class Program {

        static void Main(string[] args) {
            BackStock bs = new BackStock();
            FrontEnd fe = new FrontEnd(bs);
            Player Player = new Player(bs, fe);
            Console.WriteLine(fe.Apples + " Apples in FrontEnd");
            Console.WriteLine((bs.Stock["Apples"]["Cases"] * bs.ApplesPerCase) + "Apples in BS");

            Console.WriteLine("Stocking Apples");
            Player.Stock("Apples");
            Console.WriteLine(fe.Apples + " Apples in FrontEnd");
            Console.WriteLine((bs.Stock["Apples"]["Cases"] * bs.ApplesPerCase) + "Apples in BS");

            Console.WriteLine("Sold 30 Apples");
            fe.Apples -= 30;
            Console.WriteLine(fe.Apples + " Apples in FrontEnd");

            Console.WriteLine("Stocking Apples");
            Player.Stock("Apples");
            Console.WriteLine(fe.Apples + " Apples in FrontEnd");
            Console.WriteLine((bs.Stock["Apples"]["Cases"] * bs.ApplesPerCase) + "Apples in BS");

            Console.WriteLine("Sold 60 Apples");
            fe.Apples -= 60;
            Console.WriteLine(fe.Apples + " Apples in FrontEnd");

            Console.WriteLine("Stocking Apples");
            Player.Stock("Apples");
            Console.WriteLine(fe.Apples + " Apples in FrontEnd");
            Console.WriteLine((bs.Stock["Apples"]["Cases"] * bs.ApplesPerCase) + "Apples in BS");
        }
    }

    class BackStock {
        public Dictionary<string, Dictionary<string, float>> Stock = new Dictionary<string, Dictionary<string, float>>() {
            { "Apples", new Dictionary<string, float>() {
                { "Cases", 2f },
                { "PerCase", 52f },
                }
            },
            {
                "Bananas", new Dictionary<string, float>() {
                    { "Cases", 3f },
                    { "PerCase", 60f },
                }
            },
            {
                "Oranges", new Dictionary<string, float>() {
                    { "Cases", 1f },
                    { "PerCase", 46f },
                }
            } 
        };
        public float AppleCases = 2;
        public int ApplesPerCase = 52;
        public float BananaCases = 3;
        public int BananasPerCase = 60;
        public float OrangeCases = 1;
        public int OrangesPerCase = 46;

        public int Boxes = 0;
    }

    class FrontEnd {
        public  int MaxApples = 60;
        public  int Apples = 32;

        public  int MaxBananas = 46;
        public  int Bananas = 23;

        public  int MaxOranges = 30;
        public int Oranges = 20;

        public BackStock BackStock;

        public FrontEnd(BackStock bs) {
            BackStock = bs;
        }

        public void Stock(string Item) {
            if (Item == "Apples" & BackStock.AppleCases > 0) {
                if (BackStock.ApplesPerCase * BackStock.AppleCases > MaxApples - Apples ) {
                    int UsedApples = MaxApples - Apples;
                    BackStock.AppleCases -= (float) ((float)UsedApples / (float)BackStock.ApplesPerCase);
                    Console.WriteLine(BackStock.AppleCases);
                    Apples = MaxApples;
                } else {
                    Apples += (int)(BackStock.ApplesPerCase * BackStock.AppleCases);
                    BackStock.AppleCases = 0;
                }
                
            } else if (Item == "Bananas") {
            
            } else if (Item == "Oranges") {
            }
        }
    }

    class Player {

        public BackStock BackStock;
        public FrontEnd FrontEnd;

        public Player(BackStock bs, FrontEnd fe) {
            BackStock = bs;
            FrontEnd = fe;
        }

        public void Stock(string Item) {
            Dictionary<string, float> CurrentItem = BackStock.Stock[Item];
            if (CurrentItem["Cases"] > 0) {
                if (CurrentItem["PerCase"] * CurrentItem["Cases"] > FrontEnd.MaxApples - FrontEnd.Apples ) {
                    int UsedApples = FrontEnd.MaxApples - FrontEnd.Apples;
                    CurrentItem["Cases"] -= (float) ((float)UsedApples / (float)CurrentItem["PerCase"]);
                    Console.WriteLine(CurrentItem["Cases"]);
                    Console.WriteLine(BackStock.AppleCases);
                    FrontEnd.Apples = FrontEnd.MaxApples;
                    BackStock.Stock[Item] = CurrentItem;
                } else {
                    FrontEnd.Apples += (int)(CurrentItem["PerCase"] * CurrentItem["Cases"]);
                    BackStock.Stock[Item]["Cases"] = 0;
                }
                
            }
        }
    }
}