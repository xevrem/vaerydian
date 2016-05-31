/*
 Author:
      Erika V. Jonell <@xevrem>
 
 Copyright (c) 2013, 2014, 2015, 2016 Erika V. Jonell

 This program is free software: you can redistribute it and/or modify
 it under the terms of the GNU Lesser General Public License as published by
 the Free Software Foundation, either version 3 of the License, or
 (at your option) any later version.

 This program is distributed in the hope that it will be useful,
 but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 GNU Lesser General Public License for more details.

 You should have received a copy of the GNU Lesser General Public License
 along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using ECSFramework;


using Vaerydian.Components;
using Vaerydian.Utils;
using Vaerydian.Factories;
using Vaerydian.Components.Characters;
using Vaerydian.Components.Spatials;
using Vaerydian.Components.Actions;
using Vaerydian.Components.Utils;


namespace Vaerydian.Systems.Update
{
    class ProjectileSystem : EntityProcessingSystem
    {
        private ComponentMapper p_ProjectileMapper;
        private ComponentMapper p_PositionMapper;
        private ComponentMapper p_VelocityMapper;
        private ComponentMapper p_HeadingMapper;
        private ComponentMapper p_MapCollidableMapper;
        private ComponentMapper p_TransformMapper;
        private ComponentMapper p_SpatialMapper;
        private ComponentMapper p_InteractionMapper;
        private ComponentMapper p_HealthMapper;
        private ComponentMapper p_FactionMapper;
        private ComponentMapper p_LifeMapper;

        private Entity p_Spatial;


        public ProjectileSystem() { }

		protected override void initialize()
        {
            p_ProjectileMapper = new ComponentMapper(new Projectile(), ecs_instance);
            p_PositionMapper = new ComponentMapper(new Position(), ecs_instance);
            p_VelocityMapper = new ComponentMapper(new Velocity(), ecs_instance);
            p_HeadingMapper = new ComponentMapper(new Heading(), ecs_instance);
            p_MapCollidableMapper = new ComponentMapper(new MapCollidable(), ecs_instance);
            p_TransformMapper = new ComponentMapper(new Transform(), ecs_instance);
            p_SpatialMapper = new ComponentMapper(new SpatialPartition(), ecs_instance);
            p_InteractionMapper = new ComponentMapper(new Interactable(), ecs_instance);
            p_HealthMapper = new ComponentMapper(new Health(), ecs_instance);
            p_FactionMapper = new ComponentMapper(new Factions(), ecs_instance);
            p_LifeMapper = new ComponentMapper(new Life(), ecs_instance);


        }

		protected override void pre_load_content(Bag<Entity> entities)
        {
            p_Spatial = ecs_instance.tag_manager.get_entity_by_tag("SPATIAL");

        }

        protected override void process(Entity entity)
        {
                       
            Projectile projectile = (Projectile)p_ProjectileMapper.get(entity);

            projectile.ElapsedTime += ecs_instance.ElapsedTime;

            //is it time for the projectile to die?
            if (projectile.ElapsedTime >= projectile.LifeTime)
            {
                ecs_instance.delete_entity(entity);
                return;
            }
                
            Position position = (Position)p_PositionMapper.get(entity);
            Velocity velocity = (Velocity)p_VelocityMapper.get(entity);
            Heading heading = (Heading)p_HeadingMapper.get(entity);
            SpatialPartition spatial = (SpatialPartition)p_SpatialMapper.get(p_Spatial);

            Vector2 pos = position.Pos;

            //List<Entity> locals = spatial.QuadTree.retrieveContentsAtLocation(pos);
            List<Entity> locals = spatial.QuadTree.findAllWithinRange(pos, 32);

            //anything retrieved?
            if (locals != null)
            {   //anyone aruond?
                if (locals.Count > 0)
                {
                    //for all the locals see if we should do anything
                    for (int i = 0; i < locals.Count; i++)
                    {
                        //dont interact with whom fired you
                        if (locals[i] == projectile.Originator)
                            continue;

                        Life life = (Life)p_LifeMapper.get(locals[i]);

                        //if no life, uh, don't check for it...
                        if (life == null)
                            continue;

                        //if target is dead, dont worry
                        if (!life.IsAlive)
                            continue;


                        //is there an interaction available?
                        Interactable interaction = (Interactable)p_InteractionMapper.get(locals[i]);
                        if (interaction != null)
                        {
                            //get this local's position
                            Position localPosition = (Position)p_PositionMapper.get(locals[i]);

                            //are we close to it?
                            //23 - minimal radial distance for collision to occur
                            if (Vector2.Distance(pos + position.Offset, localPosition.Pos + localPosition.Offset) < 23)
                            {
                                //can we do anything to it?
                                if(interaction.SupportedInteractions.PROJECTILE_COLLIDABLE &&
                                    interaction.SupportedInteractions.ATTACKABLE)
                                {

                                    Factions lfactions = (Factions)p_FactionMapper.get(locals[i]);
                                    Factions pfactions = (Factions)p_FactionMapper.get(projectile.Originator);

                                    if (lfactions.OwnerFaction.FactionType == pfactions.OwnerFaction.FactionType)
                                        continue;

                                    //UtilFactory.createAttack(projectile.Originator, locals[i], AttackType.Projectile);
									ActionDef def = GameConfig.ActionDefs["RANGED_DMG"];
									ActionFactory.createAction(def,projectile.Originator,locals[i]);


                                    //destory yourself
                                    ecs_instance.delete_entity(entity);
                                    return;
                                }
                            }
                        }

                    }
                }
            }

            MapCollidable mapCollide = (MapCollidable)p_MapCollidableMapper.get(entity);

            if (mapCollide != null)
            {
                if (mapCollide.Collided)
                {
                    /*
                    Vector2 norm = mapCollide.ResponseVector;
                    norm.Normalize();
                    Vector2 reflect = Vector2.Reflect(heading.getHeading(), norm);
                    reflect.Normalize();

                    //Transform trans = (Transform)p_TransformMapper.get(entity);

                    //trans.Rotation = -VectorHelper.getAngle2(new Vector2(1,0), reflect);

                    heading.setHeading(reflect);
                     */
                    
					UtilFactory.createSound("audio\\effects\\hitwall", true, 0.5f);

                    ecs_instance.delete_entity(entity);
                }
            }

            pos += heading.getHeading() * velocity.Vel;

            position.Pos = pos;
        }
    }
}
