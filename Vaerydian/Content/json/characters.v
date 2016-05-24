{
	"character_defs":[
		{
			"name":"BAT",
			"avatar_def":"BAT",
			"behavior_def":"DEFAULT_ENEMY",
			"acb_def":"DEFAULT_ENEMY",
			"skill_level":0,
			"information" : {
				"name" : "BAT",
				"general_group" : "BAT",
				"variation_group" : "NONE",
				"unique_group" : "NONE"
			},
			"life" : {
				"is_alive" : true,
				"time_since_death" : 0,
				"death_longevity" : 1000
			},
			"supported_interactions" : {
					"PROJECTILE_COLLIDABLE" : true,
					"DESTROYABLE" : true,
					"ATTACKABLE" : true,
					"MELEE_ACTIONABLE" : true,
					"AWARDS_VICTORY" : true,
					"MAY_RECEIVE_VICTORY" : false,
					"CAUSES_ADVANCEMENT" : true,
					"MAY_ADVANCE" : false
			},
			"knowledges" : {
				"general" : [
					{
						"name" : "HUMAN",
						"value" : 5.0,
						"knowledge_type" : "General"
					},
					{
						"name" : "BAT",
						"value" : 5.0,
						"knowledge_type" : "General"
					}
				],
				"variation" : [
					{
						"name" : "NONE",
						"value" : 0.0,
						"knowledge_type" : "General"
					}
				],
				"unique" : [
					{
						"name" : "NONE",
						"value" : 0.0,
						"knowledge_type" : "General"
					}
				]

			},
			"statistics" : {
				"muscle" : {
					"name" : "MUSCLE",
					"value" : 10,
					"stat_type" : "MUSCLE"
				},
				"endurance" : {
					"name" : "ENDURANCE",
					"value" : 10,
					"stat_type" : "ENDURANCE"
				},
				"mind" : {
					"name" : "MIND",
					"value" : 10,
					"stat_type" : "MIND"
				},
				"personality" : {
					"name" : "PERSONALITY",
					"value" : 10,
					"stat_type" : "PERSONALITY"
				},
				"quickness" : {
					"name" : "QUICKNESS",
					"value" : 10,
					"stat_type" : "QUICKNESS"
				},
				"perception" : {
					"name" : "PERCEPTION",
					"value" : 10,
					"stat_type" : "PERCEPTION"
				},
				"focus" : {
					"name" : "FOCUS",
					"value" : 10,
					"stat_type" : "FOCUS"
				}
			},
			"health" : {
				"max_health" : 50,
				"current_health" : 50,
				"recovery_rate" : 1000,
				"recovery_ammount" : 2,
				"time_since_last_recover" : 0
			},
			"skills" : {
				"melee" : {
					"name" : "MELEE",
					"value" : 10,
					"skill_type" : "Offensive"
				},
				"ranged" : {
					"name" : "RANGED",
					"value" : 10,
					"skill_type" : "Offensive"
				},
				"avoidance" : {
					"name" : "AVOIDANCE",
					"value" : 10,
					"skill_type" : "Defensive"
				}
			},
			"factions" : {
				"owner_faction" : {
					"name" : "WILDERNESS",
					"value" : 100,
					"faction_type" : "Wilderness"
				},
				"known_factions" : [
					{
						"name" : "WILDERNESS",
						"value" : 100,
						"faction_type" : "Wilderness"
					},
					{
						"name" : "ALLY",
						"value" : -100,
						"faction_type" : "Ally"
					},
					{
						"name" : "PLAYER",
						"value" : -100,
						"faction_type" : "Player"
					}
				]
			}			
		}
	]
}
