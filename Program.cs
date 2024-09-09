using System;
using System.Collections.Generic;

namespace ProduceDept {

    class Program {

        static void Main(string[] args) {
            BackStock bs = new BackStock();
            FrontEnd fe = new FrontEnd(bs);
            Player Player = new Player(bs, fe);
            Console.WriteLine(fe.Displays["Apples"]["Current"] + " Apples in FrontEnd");
            Console.WriteLine((bs.Stock["Apples"]["Cases"] * bs.ApplesPerCase) + "Apples in BS");

            Console.WriteLine("Stocking Apples");
            Player.Stock("Apples");
            Console.WriteLine(fe.Displays["Apples"]["Current"] + " Apples in FrontEnd");
            Console.WriteLine((bs.Stock["Apples"]["Cases"] * bs.ApplesPerCase) + "Apples in BS");

            Console.WriteLine("Sold 30 Apples");
            fe.Displays["Apples"]["Current"] -= 30;
            Console.WriteLine(fe.Displays["Apples"]["Current"] + " Apples in FrontEnd");

            Console.WriteLine("Stocking Apples");
            Player.Stock("Apples");
            Console.WriteLine(fe.Displays["Apples"]["Current"] + " Apples in FrontEnd");
            Console.WriteLine((bs.Stock["Apples"]["Cases"] * bs.ApplesPerCase) + "Apples in BS");

            Console.WriteLine("Sold 60 Apples");
            fe.Displays["Apples"]["Current"] -= 60;
            Console.WriteLine(fe.Displays["Apples"]["Current"] + " Apples in FrontEnd");

            Console.WriteLine("Stocking Apples");
            Player.Stock("Apples");
            Console.WriteLine(fe.Displays["Apples"]["Current"] + " Apples in FrontEnd");
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
        public int ApplesPerCase = 52;
        public int BananasPerCase = 60;
        public int OrangesPerCase = 46;

        public int Boxes = 0;
    }

    class FrontEnd {
        public Dictionary<string, Dictionary<string, int>> Displays = new Dictionary<string, Dictionary<string, int>>() {
            { "Apples", new Dictionary<string, int>() {
                { "Max", 60 },
                { "Current", 32 },
                }
            },
            {
                "Bananas", new Dictionary<string, int>() {
                    { "Max", 46 },
                    { "Current", 23 },
                }
            },
            {
                "Oranges", new Dictionary<string, int>() {
                    { "Max", 30 },
                    { "Current", 20 },
                }
            }
        };
        public int MaxApples = 60;
        public int Apples = 32;

        public int MaxBananas = 46;
        public int Bananas = 23;

        public int MaxOranges = 30;
        public int Oranges = 20;

        public BackStock BackStock;

        public FrontEnd(BackStock bs) {
            BackStock = bs;
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
            Dictionary<string, float> BackStockItem = BackStock.Stock[Item];
            Dictionary<string, int> FrontEndDisplay = FrontEnd.Displays[Item];
            if (BackStockItem["Cases"] > 0) {
                if (BackStockItem["PerCase"] * BackStockItem["Cases"] > FrontEndDisplay["Max"] - FrontEndDisplay["Current"] ) {
                    int Stocked = FrontEndDisplay["Max"] - FrontEndDisplay["Current"];
                    BackStockItem["Cases"] -= (float) ((float)Stocked / (float)BackStockItem["PerCase"]);
                    FrontEndDisplay["Current"] = FrontEndDisplay["Max"];
                    BackStock.Stock[Item] = BackStockItem;
                    FrontEnd.Displays[Item] = FrontEndDisplay;
                } else {
                    FrontEndDisplay["Current"] += (int)(BackStockItem["PerCase"] * BackStockItem["Cases"]);
                    BackStock.Stock[Item]["Cases"] = 0;
                    FrontEnd.Displays[Item] = FrontEndDisplay;

                }
                
            }
        }
    }
}