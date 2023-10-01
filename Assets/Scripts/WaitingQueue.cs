using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class WaitingQueue<T>
{
    public Action<T> ActiveQueueDequeueCallback;
    public Action<T> ActiveQueueEnqueueCallback;
    public Action<T> WaitingQueueEnqueueCallback;

    public int ActiveCapacity;
    public int ActiveLength => _activeQueue.Count;
    public int WaitingLength => _waitingQueue.Count;
    public bool IsPaused;

    private readonly Queue<T> _activeQueue;
    private readonly Queue<T> _waitingQueue;

    public WaitingQueue(int activeCapacity)
    {
        ActiveCapacity = activeCapacity;

        _activeQueue = new Queue<T>(ActiveCapacity);
        _waitingQueue = new Queue<T>();

        IsPaused = false;
    }

    public bool Enqueue(T newElement)
    {
        // If active queue is not full, add element to the active queue
        if (ActiveLength < ActiveCapacity)
        {
            _activeQueue.Enqueue(newElement);
            ActiveQueueEnqueueCallback?.Invoke(newElement);
            return true;
        }

        // Otherwise, add element to the waiting queue
        _waitingQueue.Enqueue(newElement);
        Debug.Log("Enqueue to wait");
        WaitingQueueEnqueueCallback?.Invoke(newElement);
        return false;
    }

    public T Dequeue()
    {
        if (ActiveLength == 0)
        {
            return default;
        }

        T dequeuedElement = _activeQueue.Dequeue();

        // If the WaitingQueue is not paused,
        // then, dequeue element(s) from _waitingQueue and enqueue the element(s) to the _activeQueue 
        if (!IsPaused)
        {
            int availableSlotCount = ActiveCapacity - ActiveLength;

            for (int i = 0; i < availableSlotCount; i++)
            {
                if (_waitingQueue.TryDequeue(out T dequeuedWaitingElement))
                {
                    _activeQueue.Enqueue(dequeuedWaitingElement);
                    ActiveQueueEnqueueCallback?.Invoke(dequeuedWaitingElement);
                }
                else
                {
                    break;
                }
            }
        }

        ActiveQueueDequeueCallback?.Invoke(dequeuedElement);
        return dequeuedElement;
    }

    public T Peek()
    {
        return _activeQueue.Peek();
    }

    public override string ToString()
    {
        StringBuilder stringBuilder = new StringBuilder();

        stringBuilder.AppendLine("Active:");
        foreach (var element in _activeQueue)
        {
            stringBuilder.AppendLine(element.ToString());
        }

        stringBuilder.AppendLine("\nWaiting:");
        foreach (var element in _waitingQueue)
        {
            stringBuilder.AppendLine(element.ToString());
        }

        return stringBuilder.ToString();
    }
}