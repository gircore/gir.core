using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace Gio
{
    public partial class Application
    {
        //private Dictionary<string, CommandAction> actions = new Dictionary<string, CommandAction>();
        
        //public void AddAction(GBaseAction action) => ActionMap.add_action(this, action);
        public void RemoveAction(string name)
        {
            /*if(!actions.TryGetValue(name, out var action))
                throw new Exception($"Action {name} not found.");

            action.Dispose();
            actions.Remove(name);

            Sys.ActionMap.remove_action(Handle, name);*/
        }

        public void AddAction(string name, ICommand command)
        {
            /*if(actions.ContainsKey(name))
                throw new Exception($"Action {name} is already used");

            var action = new CommandAction(name, command);
            Sys.ActionMap.add_action(Handle, GetHandle(action));
            actions[name] = action;*/
        }
    }
}