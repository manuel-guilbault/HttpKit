using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ReverseProxy.Owin
{
    public static class TaskExtensions
    {
        /// <summary>
        /// http://blogs.msdn.com/b/pfxteam/archive/2011/11/10/10235834.aspx
        /// </summary>
        /// <param name="task"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static Task<T> Timeout<T>(this Task<T> task, TimeSpan timeout)
        {
            // Short-circuit #1: task already completed
            if (task.IsCompleted)
            {
                // Either the task has already completed or timeout will never occur.
                // No proxy necessary.
                return task;
            }

            // tcs.Task will be returned as a proxy to the caller
            var tcs = new TaskCompletionSource<T>();

            // Short-circuit #2: zero timeout
            if (timeout <= TimeSpan.Zero)
            {
                // We've already timed out.
                tcs.SetException(new TimeoutException());
                return tcs.Task;
            }

            // Set up a timer to complete after the specified timeout period
            var timer = new Timer(state =>
            {
                // Recover your state information
                var myTcs = (TaskCompletionSource<T>)state;

                // Fault our proxy with a TimeoutException
                myTcs.TrySetException(new TimeoutException());
            }, tcs, (long)timeout.TotalMilliseconds, System.Threading.Timeout.Infinite);

            // Wire up the logic for what happens when source task completes
            task.ContinueWith((antecedent, state) =>
                {
                    // Recover our state data
                    var tuple = (Tuple<Timer, TaskCompletionSource<T>>)state;

                    // Cancel the Timer
                    tuple.Item1.Dispose();

                    // Marshal results to proxy
                    MarshalTaskResults(antecedent, tuple.Item2);
                },
                Tuple.Create(timer, tcs),
                CancellationToken.None,
                TaskContinuationOptions.ExecuteSynchronously,
                TaskScheduler.Default
            );

            return tcs.Task;
        }

        private static void MarshalTaskResults<TResult>(Task source, TaskCompletionSource<TResult> proxy)
        {
            switch (source.Status)
            {
                case TaskStatus.Faulted:
                    proxy.TrySetException(source.Exception);
                    break;

                case TaskStatus.Canceled:
                    proxy.TrySetCanceled();
                    break;

                case TaskStatus.RanToCompletion:
                    Task<TResult> castedSource = source as Task<TResult>;
                    proxy.TrySetResult(castedSource == null
                        ? default(TResult) // source is a Task
                        : castedSource.Result // source is a Task<TResult>
                    );
                    break;

                default:
                    throw new InvalidProgramException("Unknown TaskStatus." + source.Status);
            }
        }
    }
}
