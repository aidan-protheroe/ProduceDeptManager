using System;
using System.Collections.Generic;

namespace ProduceDept {

    class Program {

        static void Main(string[] args) {
            BackStock bs = new BackStock();
            FrontEnd fe = new FrontEnd();
            Player Player = new Player(bs, fe);
            Console.WriteLine(fe.Displays["Apples"]["Current"] + " Apples in FrontEnd");
            Console.WriteLine((bs.Stock["Apples"]["Cases"] * bs.Stock["Apples"]["PerCase"]) + "Apples in BS");

            Console.WriteLine("Stocking Apples");
            Console.WriteLine(Player.Stock("Apples"));
            Console.WriteLine(fe.Displays["Apples"]["Current"] + " Apples in FrontEnd");
            Console.WriteLine((bs.Stock["Apples"]["Cases"] * bs.Stock["Apples"]["PerCase"]) + "Apples in BS");

            Console.WriteLine("Sold 30 Apples");
            fe.Displays["Apples"]["Current"] -= 30;
            Console.WriteLine(fe.Displays["Apples"]["Current"] + " Apples in FrontEnd");

            Console.WriteLine("Stocking Apples");
            Console.WriteLine(Player.Stock("Apples"));
            Console.WriteLine(fe.Displays["Apples"]["Current"] + " Apples in FrontEnd");
            Console.WriteLine((bs.Stock["Apples"]["Cases"] * bs.Stock["Apples"]["PerCase"]) + "Apples in BS");

            Console.WriteLine("Sold 60 Apples");
            fe.Displays["Apples"]["Current"] -= 60;
            Console.WriteLine(fe.Displays["Apples"]["Current"] + " Apples in FrontEnd");

            Console.WriteLine("Stocking Apples");
            Console.WriteLine(Player.Stock("Apples"));
            Console.WriteLine(fe.Displays["Apples"]["Current"] + " Apples in FrontEnd");
            Console.WriteLine((bs.Stock["Apples"]["Cases"] * bs.Stock["Bananas"]["PerCase"]) + "Apples in BS");

            Console.WriteLine("Stocking Bananas");
            Console.WriteLine(Player.Stock("Bananas"));
            Console.WriteLine(fe.Displays["Bananas"]["Current"] + " Bananas in FrontEnd");
            Console.WriteLine((bs.Stock["Bananas"]["Cases"] * bs.Stock["Bananas"]["PerCase"]) + "Bananas in BS");

            Console.WriteLine("Sold 40 Bananas");
            fe.Displays["Bananas"]["Current"] -= 40;
            Console.WriteLine(fe.Displays["Bananas"]["Current"] + " Bananas in FrontEnd");

            Console.WriteLine("Stocking Bananas");
            Console.WriteLine(Player.Stock("Bananas"));
            Console.WriteLine(fe.Displays["Bananas"]["Current"] + " Bananas in FrontEnd");
            Console.WriteLine((bs.Stock["Bananas"]["Cases"] * bs.Stock["Bananas"]["PerCase"]) + "Bananas in BS");
        }
    }

    class Department {
        public FrontEnd FrontEnd = new FrontEnd();
        public BackStock BackStock = new BackStock();
        public Player Player;

        public Dictionary<string, Item> Items = new Dictionary<string, Item>() {
            { "Apples", new Item("Apples", 2f, 52f, 60, 35, 0.5f) },
            { "Bananas", new Item("Bananas", 3f, 60f, 46, 30, 0.15f) },
            { "Oranges", new Item("Oranges", 1f, 46f, 30, 28, 0.2f) },
            { "Lemons", new Item("Lemons", 2f, 56f, 50, 20, 0.25f) },
            { "Lettuce", new Item("Lettuce", 1f, 30f, 16, 13, 0.1f) }
        };

        public int EmptyBoxes = 0;

        public Department() {
            Player = new Player(BackStock, FrontEnd);
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
            },
            {
                "Lemons", new Dictionary<string, float>() {
                    { "Cases", 2f },
                    { "PerCase", 56f },
                }
            },
            {
                "Lettuce", new Dictionary<string, float>() {
                    { "Cases", 1f },
                    { "PerCase", 30f },
                }
            }
        };
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
            },
            {
                "Lemons", new Dictionary<string, int>() {
                    { "Max", 50 },
                    { "Current", 28 },
                }
            },
            {
                "Lettuce", new Dictionary<string, int>() {
                    { "Max", 16 },
                    { "Current", 15 },
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
                    { "Time", .2f }
                }
            },
            {
                "Lemons", new Dictionary<string, float>() {
                    { "Time", .3f }
                }
            },
            {
                "Lettuce", new Dictionary<string, float>() {
                    { "Time", .15f }
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

    class Item {
        public string Name;
        public float Cases;
        public float PerCase;
        public int MaxStocked;
        public int CurrentStocked;
        public float TimeToStock;

        public Item(string Name, float Cases, float PerCase, int MaxStocked, int CurrentStocked, float TimeToStock) {
            this.Name = Name;
            this.Cases = Cases;
            this.PerCase = PerCase;
            this.MaxStocked = MaxStocked;
            this.CurrentStocked = CurrentStocked;
            this.TimeToStock = TimeToStock;
        }
    }
}

//add a customer class that randomly shops items. (Right now) they wont shop from the display your're currently stocking.

//make a class called item, store ALL the item stats there, and instance it out to the backstock and frontend and player?

//When a case is emptied, add a cardboard box to the backstock 
