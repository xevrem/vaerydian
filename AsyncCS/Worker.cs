//
//  Worker.cs
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
using System;
using System.Threading;

namespace AsyncCS
{
	public class Worker{

		public Worker(){
			RUNNING = true;
		}

		private static bool RUNNING = true;

		public int max_count = 10;

		public int tasks_complete = 0;

		public int ID = 0;

		public long sleep_time = 10000L;

		public void run(){

			Console.WriteLine ("Worker {0} Starting...",ID);

			while (RUNNING) {
				if (ResourcePool.coroutine_queue.Count > 0) {
					for (int i = 0; i < (ResourcePool.coroutine_queue.Count > max_count ? max_count : ResourcePool.coroutine_queue.Count); i++) {

						Coroutine coroutine;

						if (ResourcePool.coroutine_queue.TryDequeue (out coroutine)) {
							coroutine.next ();

							if (coroutine.can_move_next)
								ResourcePool.enqueue_coroutine (coroutine);
							else
								this.tasks_complete++;
						} else
							continue;
					}
				}

				Thread.Sleep (new TimeSpan(sleep_time));
			}
			Console.WriteLine ("Worker {0} Stopping... {1} Coroutines Completed...",ID, this.tasks_complete);
		}

		public void shutdown(){
			RUNNING = false;
		}
	}
}

