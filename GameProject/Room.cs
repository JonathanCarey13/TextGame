using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GameProject.ProgramUI;

namespace GameProject
{
    public class Room
    {
        public string Splash { get; }
        public List<Item> Items { get; }
        public List<string> Exits { get; }
        public List<Event> Events { get; }

        public void RemoveItem(Item item)
        {
            if (Items.Contains(item))
            {
                Items.Remove(item);
            };
        }

        public void ResolveEvent(Event resolvedEvent)
        {
            if (Events.Contains(resolvedEvent))
            {
                Events.Remove(resolvedEvent);
            }
        }

        public Room(string splash, List<string> exits, List<Item> items, List<Event> events)
        {
            Splash = splash;
            Exits = exits;
            Items = items;
            Events = events;
        }
    }
}
