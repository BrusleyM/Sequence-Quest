using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework.LinearSequence
{
    public class SequentialTaskRunner : MonoBehaviour
    {
        private Queue<Func<IEnumerator>> _taskQueue = new Queue<Func<IEnumerator>>();
        private Coroutine _currentCoroutine;

        public void AddTask(Func<IEnumerator> task)
        {
            _taskQueue.Enqueue(task);
        }

        public void RunTasks()
        {
            if (_currentCoroutine == null)
            {
                _currentCoroutine = StartCoroutine(RunTaskQueue());
            }
        }

        private IEnumerator RunTaskQueue()
        {
            while (_taskQueue.Count > 0)
            {
                var task = _taskQueue.Dequeue();
                if (task != null)
                {
                    yield return task();
                }
                else
                {
                    Debug.LogWarning("Null task found in the task queue.");
                }
            }

            _currentCoroutine = null;
        }

        public void StopExecution()
        {
            if (_currentCoroutine != null)
            {
                StopCoroutine(_currentCoroutine);
                _currentCoroutine = null;
            }

            _taskQueue.Clear();
        }
    }
}
 