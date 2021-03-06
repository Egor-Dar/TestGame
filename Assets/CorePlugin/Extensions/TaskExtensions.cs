#region license

// Copyright 2021 - 2021 Arcueid Elizabeth D'athemon
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//     http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#endregion

using System;
using System.Threading.Tasks;

namespace CorePlugin.Extensions
{
    public static class TaskExtensions
    {
        /// <summary>
        ///     Blocks while condition is true or timeout occurs.
        /// </summary>
        /// <param name="condition">The condition that will perpetuate the block.</param>
        /// <param name="frequency">The frequency at which the condition will be check, in milliseconds.</param>
        /// <param name="timeout">Timeout in milliseconds.</param>
        /// <exception cref="TimeoutException"></exception>
        /// <returns></returns>
        public static async Task WaitWhile(Func<bool> condition, int frequency = 25, int timeout = -1)
        {
            var waitTask = Task.Run(async () =>
                                    {
                                        while (condition()) await Task.Delay(frequency);
                                    });
            if (waitTask != await Task.WhenAny(waitTask, Task.Delay(timeout))) throw new TimeoutException();
        }

        public static async Task AwaitRequestAsync<T>(T task) where T : Task
        {
            while (task.IsCompleted) await Task.Yield();
        }

        /// <summary>
        ///     Blocks until condition is true or timeout occurs.
        /// </summary>
        /// <param name="condition">The break condition.</param>
        /// <param name="frequency">The frequency at which the condition will be checked.</param>
        /// <param name="timeout">The timeout in milliseconds.</param>
        /// <returns></returns>
        public static async Task WaitUntil(Func<bool> condition, int frequency = 25, int timeout = -1)
        {
            var waitTask = Task.Run(async () =>
                                    {
                                        while (!condition()) await Task.Delay(frequency);
                                    });

            if (waitTask != await Task.WhenAny(waitTask,
                                               Task.Delay(timeout)))
                throw new TimeoutException();
        }

        /// <summary>
        ///     Creates task with factory method
        /// </summary>
        /// <param name="action"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Task<T> CreateTask<T>(this Func<T> action)
        {
            return Task<T>.Factory.StartNew(action);
        }
    }
}
