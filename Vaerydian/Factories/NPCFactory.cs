/*
 Author:
      Thomas H. Jonell <@Net_Gnome>
 
 Copyright (c) 2013 Thomas H. Jonell

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
using Vaerydian.Characters;
using Vaerydian.Components;
using Vaerydian.Utils;

using Vaerydian.Behaviors;

using Vaerydian.Components.Characters;
using Vaerydian.Components.Spatials;
using Vaerydian.Components.Utils;
using Vaerydian.Components.Actions;
using Vaerydian.Components.Graphical;
using AgentComponentBus.Components;
using Vaerydian.ACB;
using AgentComponentBus.Core;


namespace Vaerydian.Factories
{
    class NPCFactory
    {
        private ECSInstance n_EcsInstance;
        private Random n_Rand = new Random();

        public NPCFactory(ECSInstance ecsInstance) 
        {
            n_EcsInstance = ecsInstance;
        }

        public void destroyRelatedEntities(Entity entity)
        {
            ItemFactory itFact = new ItemFactory(n_EcsInstance);

            itFact.destoryEquipment(entity);
        }


        public void createFollower(Vector2 position, Entity target, float distance)
        {
            Entity e = n_EcsInstance.create();

            n_EcsInstance.entity_manager.add_component(e, new Position(position, new Vector2(16)));
            n_EcsInstance.entity_manager.add_component(e, new Velocity(4f));
			n_EcsInstance.entity_manager.add_component(e, new Sprite("characters\\herr_von_speck_sheet", "characters\\normals\\herr_von_speck_sheet_normals", 32, 32, 0, 0));
			n_EcsInstance.entity_manager.add_component(e, new AiBehavior(new FollowerBehavior(e,target,distance,n_EcsInstance)));//new FollowPath(e, target, distance, n_EcsInstance)));
			n_EcsInstance.entity_manager.add_component(e, new MapCollidable());
			n_EcsInstance.entity_manager.add_component(e, new Heading());
			n_EcsInstance.entity_manager.add_component(e, new Transform());
			n_EcsInstance.entity_manager.add_component(e, new Aggrivation());
			n_EcsInstance.entity_manager.add_component(e, new Path());

            /*
            //setup pathing agent
            BusAgent busAgent = new BusAgent();
            busAgent.Agent = new Agent();
            busAgent.Agent.Entity = e;

            Activity activity = new Activity();
            activity.ActivityName = "activity1";
            activity.ComponentName = "PATH_FINDER";
            activity.InitialActivity = true;
            activity.NextActivity = "activity1";

            AgentProcess process = new AgentProcess();
            process.ProcessName = "path process";
            process.Activities.Add(activity.ActivityName, activity);

            busAgent.Agent.AgentProcess = process;

            n_ECSInstance.entity_manager.add_component(e, busAgent);
            */

            //create info
            Information info = new Information();
            info.Name = "TEST FOLLOWER";
			info.GeneralGroup = "BAT";
			info.VariationGroup = "NONE";
			info.UniqueGroup = "NONE";
            n_EcsInstance.entity_manager.add_component(e, info);

            //create life
            Life life = new Life();
            life.IsAlive = true;
            life.DeathLongevity = 500;
            n_EcsInstance.entity_manager.add_component(e, life);

            //create interactions
            Interactable interact = new Interactable();
            interact.SupportedInteractions.PROJECTILE_COLLIDABLE = true;
            interact.SupportedInteractions.ATTACKABLE = true;
            interact.SupportedInteractions.MELEE_ACTIONABLE = true;
            interact.SupportedInteractions.AWARDS_VICTORY = true;
            interact.SupportedInteractions.CAUSES_ADVANCEMENT = true;
            interact.SupportedInteractions.MAY_ADVANCE = false;
            n_EcsInstance.entity_manager.add_component(e, interact);

            //create test equipment
            ItemFactory iFactory = new ItemFactory(n_EcsInstance);
            n_EcsInstance.entity_manager.add_component(e, iFactory.createTestEquipment());

            int skillLevel = 25;

            //setup experiences
			Knowledges knowledges = new Knowledges();
			knowledges.GeneralKnowledge.Add ("HUMAN", new Knowledge { Name="", Value=skillLevel, KnowledgeType=KnowledgeType.General });
			knowledges.GeneralKnowledge.Add ("BAT", new Knowledge { Name="", Value=skillLevel, KnowledgeType=KnowledgeType.General });
			knowledges.VariationKnowledge.Add ("NONE", new Knowledge { Name="", Value=0f, KnowledgeType=KnowledgeType.General });
			knowledges.UniqueKnowledge.Add ("NONE", new Knowledge { Name="", Value=0f, KnowledgeType=KnowledgeType.General });
			n_EcsInstance.entity_manager.add_component(e, knowledges);

            //setup attributes
            Statistics statistics = new Statistics();

			statistics.Focus = new Statistic {Name="FOCUS",Value=skillLevel,StatType=StatType.FOCUS };
			statistics.Endurance = new Statistic {Name= "ENDURANCE",Value= skillLevel,StatType= StatType.ENDURANCE };
			statistics.Mind = new Statistic {Name= "MIND",Value= skillLevel,StatType= StatType.MIND };
			statistics.Muscle = new Statistic {Name= "MUSCLE",Value= skillLevel,StatType= StatType.MUSCLE };
			statistics.Perception = new Statistic {Name= "PERCEPTION",Value= skillLevel,StatType= StatType.PERCEPTION };
			statistics.Personality = new Statistic {Name= "PERSONALITY",Value= skillLevel,StatType= StatType.PERSONALITY };
			statistics.Quickness = new Statistic {Name= "QUICKNESS",Value= skillLevel,StatType= StatType.QUICKNESS };
            n_EcsInstance.entity_manager.add_component(e, statistics);

            //create health
			Health health = new Health(statistics.Endurance.Value * 3);
            health.RecoveryAmmount = statistics.Endurance.Value / 5;
            health.RecoveryRate = 1000;
            n_EcsInstance.entity_manager.add_component(e, health);

            //setup skills
			Skills skills = new Skills();
			skills.Ranged = new Skill{Name="RANGED",Value= skillLevel,SkillType= SkillType.Offensive};
			skills.Avoidance = new Skill{Name="AVOIDANCE",Value= skillLevel,SkillType= SkillType.Defensive};
			skills.Melee = new Skill{Name="MELEE",Value= skillLevel,SkillType= SkillType.Offensive};
			n_EcsInstance.entity_manager.add_component(e, skills);

			Factions factions = new Factions();
			factions.OwnerFaction = new Faction{Name="ALLY",Value=100,FactionType=FactionType.Ally};
			factions.KnownFactions.Add("WILDERNESS", new Faction{Name="WILDERNESS",Value=-10,FactionType= FactionType.Wilderness});
			factions.KnownFactions.Add("PLAYER", new Faction{Name="PLAYER",Value=100,FactionType= FactionType.Player});
			n_EcsInstance.entity_manager.add_component(e, factions);


			n_EcsInstance.entity_manager.add_component(e, EntityFactory.createLight(true, 3, new Vector3(position, 10), 0.5f, new Vector4(1, 1, 1, 1)));


            n_EcsInstance.refresh(e);

        }


        public void createBatEnemy(Vector2 position, int skillLevel)
        {
            Entity e = n_EcsInstance.create();

            n_EcsInstance.entity_manager.add_component(e, new Position(position, new Vector2(16)));
            n_EcsInstance.entity_manager.add_component(e, new Velocity(3f));
            n_EcsInstance.entity_manager.add_component(e, new AiBehavior(new WanderingEnemyBehavior(e, n_EcsInstance)));
            n_EcsInstance.entity_manager.add_component(e, new MapCollidable());
            n_EcsInstance.entity_manager.add_component(e, new Heading());
            n_EcsInstance.entity_manager.add_component(e, new Transform());
            n_EcsInstance.entity_manager.add_component(e, new Aggrivation());

            //create state machine
            StateMachine<EnemyState,EnemyState> stateMachine = new StateMachine<EnemyState, EnemyState>(EnemyState.Idle, EnemyAI.whenIdle, EnemyState.Idle);

            //define states
            stateMachine.addState(EnemyState.Wandering, EnemyAI.whenWandering);
            stateMachine.addState(EnemyState.Following, EnemyAI.whenFollowing);
            
            //define transitions
            stateMachine.addStateChange(EnemyState.Idle, EnemyState.Wandering, EnemyState.Wandering);
            stateMachine.addStateChange(EnemyState.Wandering, EnemyState.Following, EnemyState.Following);
            stateMachine.addStateChange(EnemyState.Following, EnemyState.Wandering, EnemyState.Wandering);

            StateContainer<EnemyState, EnemyState> stateContainer = new StateContainer<EnemyState, EnemyState>();
            stateContainer.StateMachine = stateMachine;

            n_EcsInstance.entity_manager.add_component(e, stateContainer);
            

            //create ACB component
            BusAgent busAgent = new BusAgent();
			busAgent.Agent = ResourcePool.createAgent ();
			busAgent.Agent.Entity = e;
			busAgent.Agent.Init = EnemyAI.init;
			busAgent.Agent.Run = EnemyAI.run;

            n_EcsInstance.entity_manager.add_component(e, busAgent);

			n_EcsInstance.entity_manager.add_component(e, AnimationFactory.createAvatar ("BAT"));

            //create info
            Information info = new Information();
            info.Name = "TEST WANDERER";
			info.GeneralGroup = "BAT";
			info.VariationGroup = "NONE";
			info.UniqueGroup = "NONE";
            n_EcsInstance.entity_manager.add_component(e, info);

            //create life
            Life life = new Life();
            life.IsAlive = true;
            life.DeathLongevity = 500;
            n_EcsInstance.entity_manager.add_component(e, life);

            //create interactions
            Interactable interact = new Interactable();
            interact.SupportedInteractions.PROJECTILE_COLLIDABLE = true;
            interact.SupportedInteractions.ATTACKABLE = true;
            interact.SupportedInteractions.MELEE_ACTIONABLE = true;
            interact.SupportedInteractions.AWARDS_VICTORY = true;
            interact.SupportedInteractions.CAUSES_ADVANCEMENT = true;
            interact.SupportedInteractions.MAY_ADVANCE = false;
            n_EcsInstance.entity_manager.add_component(e, interact);

            //create test equipment
            ItemFactory iFactory = new ItemFactory(n_EcsInstance);
            n_EcsInstance.entity_manager.add_component(e, iFactory.createTestEquipment());

            //setup experiences
			Knowledges knowledges = new Knowledges();
			knowledges.GeneralKnowledge.Add ("HUMAN", new Knowledge { Name="", Value=skillLevel, KnowledgeType=KnowledgeType.General });
			knowledges.GeneralKnowledge.Add ("BAT", new Knowledge { Name="", Value=skillLevel, KnowledgeType=KnowledgeType.General });
			knowledges.VariationKnowledge.Add ("NONE", new Knowledge { Name="", Value=0f, KnowledgeType=KnowledgeType.General });
			knowledges.UniqueKnowledge.Add ("NONE", new Knowledge { Name="", Value=0f, KnowledgeType=KnowledgeType.General });
			n_EcsInstance.entity_manager.add_component(e, knowledges);

            //setup attributes
            Statistics statistics = new Statistics();

			statistics.Focus = new Statistic {Name="FOCUS",Value=skillLevel,StatType=StatType.FOCUS };
			statistics.Endurance = new Statistic {Name= "ENDURANCE",Value= skillLevel,StatType= StatType.ENDURANCE };
			statistics.Mind = new Statistic {Name= "MIND",Value= skillLevel,StatType= StatType.MIND };
			statistics.Muscle = new Statistic {Name= "MUSCLE",Value= skillLevel,StatType= StatType.MUSCLE };
			statistics.Perception = new Statistic {Name= "PERCEPTION",Value= skillLevel,StatType= StatType.PERCEPTION };
			statistics.Personality = new Statistic {Name= "PERSONALITY",Value= skillLevel,StatType= StatType.PERSONALITY };
			statistics.Quickness = new Statistic {Name= "QUICKNESS",Value= skillLevel,StatType= StatType.QUICKNESS };
            n_EcsInstance.entity_manager.add_component(e, statistics);

            //create health
            Health health = new Health(statistics.Endurance.Value * 3);
            health.RecoveryAmmount = statistics.Endurance.Value / 5;
            health.RecoveryRate = 1000;
            n_EcsInstance.entity_manager.add_component(e, health);

            //setup skills
			Skills skills = new Skills();
			skills.Ranged = new Skill{Name="RANGED",Value= skillLevel,SkillType= SkillType.Offensive};
			skills.Avoidance = new Skill{Name="AVOIDANCE",Value= skillLevel,SkillType= SkillType.Defensive};
			skills.Melee = new Skill{Name="MELEE",Value= skillLevel,SkillType= SkillType.Offensive};
			n_EcsInstance.entity_manager.add_component(e, skills);

            //setup factions
            Factions factions = new Factions();
			factions.OwnerFaction = new Faction{Name="WILDERNESS",Value=100,FactionType= FactionType.Wilderness};
			factions.KnownFactions.Add ("PLAYER", new Faction { Name="PLAYER", Value=-10, FactionType= FactionType.Player });
			factions.KnownFactions.Add ("ALLY", new Faction { Name="ALLY", Value=-10, FactionType=FactionType.Ally });
            n_EcsInstance.entity_manager.add_component(e, factions);

            Aggrivation aggro = new Aggrivation();
            n_EcsInstance.entity_manager.add_component(e, aggro);

			n_EcsInstance.entity_manager.add_component(e, EntityFactory.createLight(true, 3, new Vector3(position, 10), 0.5f, new Vector4(1,1,.6f, 1)));

            n_EcsInstance.group_manager.add_entity_to_group("WANDERERS", e);

            n_EcsInstance.refresh(e);
        }

		public Entity createCharacter (CharacterDef characterDef, Vector2 position){
			Entity e = n_EcsInstance.create();

			n_EcsInstance.entity_manager.add_component(e, new Position(position, new Vector2(16)));
			n_EcsInstance.entity_manager.add_component(e, new Velocity(3f));
			n_EcsInstance.entity_manager.add_component(e, new AiBehavior(new WanderingEnemyBehavior(e, n_EcsInstance)));
			n_EcsInstance.entity_manager.add_component(e, new MapCollidable());
			n_EcsInstance.entity_manager.add_component(e, new Heading());
			n_EcsInstance.entity_manager.add_component(e, new Transform());
			n_EcsInstance.entity_manager.add_component(e, new Aggrivation());

			//create state machine
			StateMachine<EnemyState,EnemyState> stateMachine = new StateMachine<EnemyState, EnemyState>(EnemyState.Idle, EnemyAI.whenIdle, EnemyState.Idle);

			//define states
			stateMachine.addState(EnemyState.Wandering, EnemyAI.whenWandering);
			stateMachine.addState(EnemyState.Following, EnemyAI.whenFollowing);

			//define transitions
			stateMachine.addStateChange(EnemyState.Idle, EnemyState.Wandering, EnemyState.Wandering);
			stateMachine.addStateChange(EnemyState.Wandering, EnemyState.Following, EnemyState.Following);
			stateMachine.addStateChange(EnemyState.Following, EnemyState.Wandering, EnemyState.Wandering);

			StateContainer<EnemyState, EnemyState> stateContainer = new StateContainer<EnemyState, EnemyState>();
			stateContainer.StateMachine = stateMachine;
			n_EcsInstance.entity_manager.add_component(e, stateContainer);

			//create ACB component
			BusAgent busAgent = new BusAgent();
			busAgent.Agent = ResourcePool.createAgent ();
			busAgent.Agent.Entity = e;
			busAgent.Agent.Init = EnemyAI.init;
			busAgent.Agent.Run = EnemyAI.run;
			n_EcsInstance.entity_manager.add_component(e, busAgent);

			//create avatar
			n_EcsInstance.entity_manager.add_component(e, AnimationFactory.createAvatar (characterDef.AvatarDef.Name));

			//create info
			Information info = new Information();
			info.Name = characterDef.InfoDef.Name;
			info.GeneralGroup = characterDef.InfoDef.GeneralGroup;
			info.VariationGroup = characterDef.InfoDef.VariationGroup;
			info.UniqueGroup = characterDef.InfoDef.UniqueGroup;
			n_EcsInstance.entity_manager.add_component(e, info);

			//create life
			Life life = new Life();
			life.IsAlive = true;
			life.DeathLongevity = characterDef.LifeDef.DeathLongevity;
			n_EcsInstance.entity_manager.add_component(e, life);

			//create interactions
			Interactable interact = new Interactable();
			interact.SupportedInteractions = characterDef.SupportedInteractions;
			n_EcsInstance.entity_manager.add_component(e, interact);

			//create test equipment
            //FIXME: 
			ItemFactory iFactory = new ItemFactory(n_EcsInstance);
			n_EcsInstance.entity_manager.add_component(e, iFactory.createTestEquipment());

			//setup knowledges
			Knowledges knowledges = new Knowledges();
			foreach (Knowledge knowledge in characterDef.KnowledgesDef.GeneralKnowledges) {
                Knowledge k = knowledge;
                k.Value = characterDef.SkillLevel;
				knowledges.GeneralKnowledge.Add(knowledge.Name,k);
			}

			foreach (Knowledge knowledge in characterDef.KnowledgesDef.VariationKnowledges) {
				knowledges.VariationKnowledge.Add(knowledge.Name,knowledge);
			}

			foreach (Knowledge knowledge in characterDef.KnowledgesDef.UniqueKnowledges) {
				knowledges.UniqueKnowledge.Add(knowledge.Name,knowledge);
			}
			n_EcsInstance.entity_manager.add_component(e, knowledges);

			//setup attributes
			Statistics statistics = new Statistics();
			statistics.Endurance = characterDef.StatisticsDef.Endurance;
            statistics.Endurance.Value = characterDef.SkillLevel;
			statistics.Focus = characterDef.StatisticsDef.Focus;
            statistics.Focus.Value = characterDef.SkillLevel;
			statistics.Mind = characterDef.StatisticsDef.Mind;
            statistics.Mind.Value = characterDef.SkillLevel;
			statistics.Muscle = characterDef.StatisticsDef.Muscle;
            statistics.Muscle.Value = characterDef.SkillLevel;
			statistics.Perception = characterDef.StatisticsDef.Perception;
            statistics.Perception.Value = characterDef.SkillLevel;
			statistics.Personality = characterDef.StatisticsDef.Personality;
            statistics.Personality.Value = characterDef.SkillLevel;
			statistics.Quickness = characterDef.StatisticsDef.Quickness;
            statistics.Quickness.Value = characterDef.SkillLevel;
			n_EcsInstance.entity_manager.add_component(e, statistics);

			//create health
			Health health = new Health(statistics.Endurance.Value * 3);
			health.RecoveryAmmount = statistics.Endurance.Value / 5;
			health.RecoveryRate = 1000;
			n_EcsInstance.entity_manager.add_component(e, health);

			//setup skills
			Skills skills = new Skills();
			skills.Avoidance = characterDef.SkillsDef.Avoidance;
			skills.Melee = characterDef.SkillsDef.Melee;
			skills.Ranged = characterDef.SkillsDef.Ranged;
			skills.Ranged.Value = characterDef.SkillLevel;
			skills.Avoidance.Value = characterDef.SkillLevel;
			skills.Melee.Value = characterDef.SkillLevel;
			n_EcsInstance.entity_manager.add_component(e, skills);

			//setup factions
			Factions factions = new Factions();
			factions.OwnerFaction = characterDef.FactionsDef.OwnerFaction;
			foreach (Faction faction in characterDef.FactionsDef.Factions) {
				factions.KnownFactions.Add (faction.Name, faction);
			}
			n_EcsInstance.entity_manager.add_component(e, factions);

			Aggrivation aggro = new Aggrivation();
			n_EcsInstance.entity_manager.add_component(e, aggro);

			n_EcsInstance.entity_manager.add_component(e, EntityFactory.createLight(true, 3, new Vector3(position, 10), 0.5f, new Vector4(1,1,.6f, 1)));

			n_EcsInstance.group_manager.add_entity_to_group("WANDERERS", e);

			n_EcsInstance.refresh(e);

			return e;
		}

        public void createWanders(int count, GameMap map, int skillLevel)
        {
            int xSize = map.Map.XSize;
            int ySize = map.Map.YSize;

            int x, y;

            bool placed = false;

            for (int i = 0; i < count; i++)
            {

                while (!placed)
                {
                    x = n_Rand.Next(1, xSize - 1);
                    y = n_Rand.Next(1, ySize - 1);

                    if (!map.Map.Terrain[x, y].IsBlocking)
                    {
                        //createBatEnemy(new Vector2(x * 32, y * 32), skillLevel);
                        CharacterDef cDef = GameConfig.CharacterDefs["BAT"];
                        cDef.SkillLevel = skillLevel;
                        createCharacter(cDef, new Vector2(x * 32, y * 32));
                        placed = true;
                    }

                }

                placed = false;
            }
        }

        /// <summary>
        /// creates a trigger that spawns wanderers
        /// </summary>
        /// <param name="count">max number of wanderers to spawn</param>
        /// <param name="map">map they are spawned in</param>
        public void createWandererTrigger(int count, GameMap map, int skillLevel)
        {
            Entity e = n_EcsInstance.create();

            Trigger trigger = new Trigger(count, map, skillLevel);

            trigger.TriggerAction += OnTriggerActionCreateWanders;
            trigger.TimeDelay = 500;
            trigger.RecurrancePeriod = 10000;
            trigger.IsRecurring = true;

            n_EcsInstance.entity_manager.add_component(e, trigger);

            n_EcsInstance.refresh(e);
        }

        /// <summary>
        /// handles the wanderer trigger actions
        /// </summary>
        /// <param name="ecsInstance"></param>
        /// <param name="parameters"></param>
        private void OnTriggerActionCreateWanders(ECSInstance ecsInstance, Object[] parameters)
        {
            int count = (int)parameters[0];
            GameMap map = (GameMap)parameters[1];
            int skillLevel = (int)parameters[2];

            Bag<Entity> wanderers = n_EcsInstance.group_manager.get_group("WANDERERS");

            int size = 0;

            if(wanderers != null)
                size = wanderers.count;

            int create = count - size;

            if (create != 0)
                createWanders(create, map, skillLevel);
        }


    }
}
