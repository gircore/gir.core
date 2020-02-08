using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace Gio.Core
{
    public partial class GApplication : GObject.Core.GObject
    {
        private Dictionary<string, GCommandAction> actions;
        
        //public void AddAction(GBaseAction action) => ActionMap.add_action(this, action);
        public void RemoveAction(string name)
        {
            if(!actions.TryGetValue(name, out var action))
                throw new Exception($"Action {name} not found.");

            action.Dispose();
            actions.Remove(name);

            ActionMap.remove_action(this, name);
        }

        public void AddAction(string name, ICommand command)
        {
            if(actions.ContainsKey(name))
                throw new Exception($"Action {name} is already used");

           var action = new GCommandAction(name, command);
           ActionMap.add_action(this, action);
           actions[name] = action;
        }
    }
}