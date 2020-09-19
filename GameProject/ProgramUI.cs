using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static GameProject.Event;

namespace GameProject
{
    public class ProgramUI
    {

        public enum Item { bread };
        public List<Item> inventory = new List<Item>();

        Dictionary<string, Room> Rooms = new Dictionary<string, Room>
        {
            {"under bridge", underBridge },
            {"top bridge", topBridge},
            {"park entrance", parkEntrance },
            {"alleyway", alleyWay },
            {"bar", bar },
        };

        public static Room underBridge = new Room(
            "You're under the bridge and you see a small patch of soft dirt on one side with Ms. Harper's empty basket.\n" +
            "You also see a small rope hanging over the basket that leads onto the top of the bridge.\n" +
            "Obvious exits are TOP BRIDGE. ",
            new List<string> { "top bridge", "bar"},
            new List<Item> { },
            new List<Event> { }
            );
        public static Room topBridge = new Room(
            "You're on top of the bridge. It's got a great view of the pond.\n" +
            "You notice the rope ending in a spool on the ground. There are muddy footprints leading away from the rope pile towards the park entrance.\n" +
            "Obvious exits are UNDER BRIDGE and PARK ENTRANCE. ",
            new List<string> { "park entrance", "under bridge" },
            new List<Item> { },
            new List<Event> { }
            );
        public static Room parkEntrance = new Room(
            "You're at the parkEntrance, its got a large decorative cast iron gate that is open.\n" +
            "There is a street alive with the hussle and bussle of occasional old people walking slowly by.\n" +
            "The dirty footprints lead across the street to a dark and dirty alleyway.\n" +
            "Obvious exits are TOP BRIDGE and ALLEYWAY",
            new List<string> { "alleyway", "top bridge" },
            new List<Item> { },
            new List<Event> { }
            );
        public static Room alleyWay = new Room(
            "You're at the alleyway. Is smells like a week old hamburger got sick after eating chinese food.\n" +
            "The smell is surprisingly bearable.\n" +
            "You see a hole in the side of an old brick wall. 'Bar' is scratched above with occasional shifty patrons leaving or entering.\n" +
            "Obvious exits are PARK ENTRANCE and BAR.",
            new List<string> { "bar", "park entrance"},
            new List<Item> { },
            new List<Event> { }
            );
        public static Room bar = new Room(
            "The familiar smell of stale beer and cigar smoke engulfs you.\n" +
            "Amongst the crowd of bar patrons, you see your old Mr. Rat with a loaf of bread.\n" +
            "You both lock eyes for a moment, both remembering your previous encounters with each other.\n" +
            "Mr. Rat promptly slinks away remembering the broken arm you gave him last time, leaving the bread.\n",     //make a difference splash/room after you obtain the item so the description isn't the same?
            new List<string> { "alleyway", "under bridge"},
            new List<Item> { Item.bread },
            new List<Event> {
                new Event(
                    "bread",
                    EventType.Get,
                    new Result(Item.bread, "You got a loaf of BREAD.")
                    )
                }
            );
        public void Run()
        {
            Console.WriteLine("The sun shines bright on a warm summer day over Sunshine Park, a pleasant park in the middle of town.\n" +
                "You are Ducktective, the crime solving duck of Sunshine Park.\n" +
                "You find yourself at your office in a drain pipe on the ugly side of the park, chowing down on some stale bread.\n" +
                "Garbage and litter float motionlessly in the stagnant puddles around the muddy drainage pipe.\n" +
                "The digs may be rough but hey, the rent's cheap.\n" +
                "\n\n" +
                "Press any key to continue...");
            Console.ReadKey();
            Console.Clear();

            Console.WriteLine("As you spit out a hunk of moldy bread, you see your next paycheck come waddling up.\n" +
                "A white swan looking distressed waddles infront of your makeshift milk carton desk.\n" +
                "After a short exasperated exchange, Mrs. Swan, details how her loaf of bread has been stolen and needs help.\n" +
                "She says she sat down to eat the bread and looked away for a minute and when she looked back it was gone.\n" +
                "After working out the payment details, you agree to help.\n" +
                "You head to the scene of the crime, under The Bridge at The Ol' Pond to look for clues.\n" +
                "\n\n" +
                "Press any key to continue...");
            Console.ReadKey();
            Console.Clear();


            Room currentRoom = underBridge;

            bool runGame = true;

            // for some reason neither this while loop or the current while loop below will close the app?
            //while (inventory.Contains(Item.bread))
            //{
            //    if (currentRoom == underBridge)
            //    {
            //        Console.WriteLine("You return the stolen loaf of bread to Ms. Swan.\n" +
            //        "Another crime solved and another paycheck earned.\n" +
            //        "\n\n\n\nTHE END");
            //        Console.ReadKey();
            //        runGame = false;
            //    }
            //    else
            //    {
            //        continue;
            //    }
            //}

            while (inventory.Contains(Item.bread))
            {
                if (currentRoom == underBridge)
                {
                    EndGame();
                }
                else
                {
                    continue;
                }
            }
            
            while (runGame)
            {
                Console.Clear();
                Console.WriteLine(currentRoom.Splash);
                string command = Console.ReadLine().ToLower();
                Console.Clear();


                if (command.StartsWith("go") || command.StartsWith("exit"))
                {
                    foreach (string exit in currentRoom.Exits)
                    {
                        if (command.Contains(exit) && Rooms.ContainsKey(exit))
                        {
                            currentRoom = Rooms[exit];
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Go where?");
                        }
                    }
                }
                else if (command.StartsWith("get") || command.StartsWith("take ") || command.StartsWith("grab"))
                {
                    bool foundItem = false;
                    foreach (Item item in currentRoom.Items)
                    {
                        if (!foundItem && command.Contains(item.ToString()))
                        {
                            Console.WriteLine($"You found the {item}! Lets return to Mrs. Swan.\n\n" +
                                "Press any key to continue...");
                            currentRoom.RemoveItem(item);
                            inventory.Add(item);
                            foundItem = true;
                            Console.ReadKey();
                            break;
                        }
                    }
                    if (!foundItem)
                    {
                        Console.WriteLine("Invalid input.");
                        Console.ReadKey();
                    }
                }
                else if (command.StartsWith("use") || command.StartsWith("give"))
                {
                    string eventMessage = "Invalid response.";
                    foreach (Event roomEvent in currentRoom.Events)
                    {
                        if (!command.Contains(roomEvent.TriggerPhrase) || roomEvent.Type != EventType.Use)
                        {
                            continue;
                        }
                        else if (roomEvent.EventResult.Type == Result.ResultType.GetItem)
                        {
                            inventory.Add(roomEvent.EventResult.ResultItem);
                            eventMessage = roomEvent.EventResult.ResultMessage;
                        }
                        else if (roomEvent.EventResult.Type == Result.ResultType.MessageOnly)
                        {
                            eventMessage = roomEvent.EventResult.ResultMessage;
                        }
                    }
                    Console.WriteLine(eventMessage);
                }
                else
                {
                    Console.WriteLine("Invalid response. Try using words like GO, EXIT, and TAKE.");
                    Console.ReadKey();
                }
            }
        }
        public void EndGame()
        {
            Console.WriteLine("You return the stolen loaf of bread to Ms. Swan.\n" +
                    "Another crime solved and another paycheck earned.\n" +
                    "\n\n\n\nTHE END");
            Console.ReadKey();
        }
    }
}
