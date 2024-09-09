using System;
using System.Collections.Generic;

namespace ProduceDept {

    class Program {

        static void Main(string[] args) {
            BackStock bs = new BackStock();
            FrontEnd fe = new FrontEnd();
            Player Player = new Player(bs, fe);
            Console.WriteLine(fe.Displays["Apples"]["Current"] + " Apples in FrontEnd");
            Console.WriteLine((bs.Stock["Apples"]["Cases"] * bs.ApplesPerCase) + "Apples in BS");

            Console.WriteLine("Stocking Apples");
            Console.WriteLine(Player.Stock("Apples"));
            Console.WriteLine(fe.Displays["Apples"]["Current"] + " Apples in FrontEnd");
            Console.WriteLine((bs.Stock["Apples"]["Cases"] * bs.ApplesPerCase) + "Apples in BS");

            Console.WriteLine("Sold 30 Apples");
            fe.Displays["Apples"]["Current"] -= 30;
            Console.WriteLine(fe.Displays["Apples"]["Current"] + " Apples in FrontEnd");

            Console.WriteLine("Stocking Apples");
            Console.WriteLine(Player.Stock("Apples"));
            Console.WriteLine(fe.Displays["Apples"]["Current"] + " Apples in FrontEnd");
            Console.WriteLine((bs.Stock["Apples"]["Cases"] * bs.ApplesPerCase) + "Apples in BS");

            Console.WriteLine("Sold 60 Apples");
            fe.Displays["Apples"]["Current"] -= 60;
            Console.WriteLine(fe.Displays["Apples"]["Current"] + " Apples in FrontEnd");

            Console.WriteLine("Stocking Apples");
            Console.WriteLine(Player.Stock("Apples"));
            Console.WriteLine(fe.Displays["Apples"]["Current"] + " Apples in FrontEnd");
            Console.WriteLine((bs.Stock["Apples"]["Cases"] * bs.ApplesPerCase) + "Apples in BS");

            Console.WriteLine("Stocking Bananas");
            Console.WriteLine(Player.Stock("Bananas"));
            Console.WriteLine(fe.Displays["Bananas"]["Current"] + " Bananas in FrontEnd");
            Console.WriteLine((bs.Stock["Bananas"]["Cases"] * bs.BananasPerCase) + "Bananas in BS");

            Console.WriteLine("Sold 40 Bananas");
            fe.Displays["Bananas"]["Current"] -= 40;
            Console.WriteLine(fe.Displays["Bananas"]["Current"] + " Bananas in FrontEnd");

            Console.WriteLine("Stocking Bananas");
            Console.WriteLine(Player.Stock("Bananas"));
            Console.WriteLine(fe.Displays["Bananas"]["Current"] + " Bananas in FrontEnd");
            Console.WriteLine((bs.Stock["Bananas"]["Cases"] * bs.BananasPerCase) + "Bananas in BS");
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
    }

    class Player {
        public Dictionary<string, Dictionary<string, float>> Items = new Dictionary<string, Dictionary<string, float>>() {
            { "Apples", new Dictionary<string, float>() {
                { "Time", .5f }
                }
            },
            {
                "Bananas", new Dictionary<string, float>() {
                    { "Time", .1f }
                }
            },
            {
                "Oranges", new Dictionary<string, float>() {
                    { "Time", .25f }
                }
            }
        };


        public BackStock BackStock;
        public FrontEnd FrontEnd;

        public float Time = 8f;

        public Player(BackStock bs, FrontEnd fe) {
            BackStock = bs;
            FrontEnd = fe;
        }

        public string Stock(string Item) {
            Dictionary<string, float> BackStockItem = BackStock.Stock[Item];
            Dictionary<string, int> FrontEndDisplay = FrontEnd.Displays[Item];
            int Stocked;
            if (BackStockItem["Cases"] > 0) {
                if (BackStockItem["PerCase"] * BackStockItem["Cases"] > FrontEndDisplay["Max"] - FrontEndDisplay["Current"] ) {
                    Stocked = FrontEndDisplay["Max"] - FrontEndDisplay["Current"];
                    BackStockItem["Cases"] -= (float) ((float)Stocked / (float)BackStockItem["PerCase"]);
                    FrontEndDisplay["Current"] = FrontEndDisplay["Max"];
                } else {
                    Stocked = (int)(BackStockItem["PerCase"] * BackStockItem["Cases"]);
                    FrontEndDisplay["Current"] += Stocked;
                    BackStockItem["Cases"] = 0;
                }
                BackStock.Stock[Item] = BackStockItem;
                FrontEnd.Displays[Item] = FrontEndDisplay;

                float TimeSpent = (float) ((float)Stocked / (float)BackStockItem["PerCase"]) * Items[Item]["Time"];
                Time -= TimeSpent;
                Console.WriteLine("Time left: " + Time);

                return "Stocked " + Stocked + " " + Item;;
            } else {
                return "No " + Item + " in BackStock"; 
            }
        }
    }
}

//Add an algoerith mfor time used per case(which is the players item["time"] value = per case so use caseamount/amoutn stocked * time and subtract it from the players total time)