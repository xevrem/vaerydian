//
//  ComponentManager.cs
//
//  Author:
//       erika <>
//
//  Copyright (c) 2016 erika
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
// using System;

namespace ECSFramework
{
    public class ComponentManager
    {
        public Bag<Bag<IComponent>> components;
        private static int _next_type_id = 1;
        private ECSInstance _ecs_instance;

        public ComponentManager(ECSInstance instance)
        {
            this._ecs_instance = instance;
            this.components = new Bag<Bag<IComponent>>();
        }

        public void register_component_type(IComponent component)
        {
            if (component.type_id == 0)
            {
                component.type_id = _next_type_id++;
            }
            if (component.type_id < components.capacity)
            {
                if (this.components[component.type_id] == null)
                {
                    this.components[component.type_id] = new Bag<IComponent>();
                }
            }
            else
            {
                //already no null test required, add the new bag
                this.components[component.type_id] = new Bag<IComponent>();
            }
        }

        public IComponent get_component(Entity e, int component_type)
        {
            //TODO
            return this.components[component_type][e.id];
        }

        public void add_component(Entity e, IComponent c)
        {
            c.owner_id = e.id;
            this.components[c.type_id].set(e.id, c);
        }

        public void remove_components(Entity e)
        {
            for (int i = 1; i < this.components.count; i++)
            {
                this.components[i].set(e.id, null);
            }
        }

        public void remove_component(IComponent c)
        {
            this.components[c.type_id].set(c.owner_id, null);
        }

        public void delete_entity(Entity e)
        {
            remove_components(e);
        }

        public bool has_component(Entity e, int type_id)
        {
            if (type_id < this.components.capacity)
            {
                if (e.id < this.components[type_id].capacity)
                {
                    if (this.components[type_id][e.id] != null)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void clean_up()
        {
            for (int i = 0; i < this.components.count; i++)
            {
                if (this.components[i] != null)
                {
                    this.components[i].clear();
                }
            }

        }
    }
}

