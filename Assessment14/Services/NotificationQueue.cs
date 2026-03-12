using System.Collections.Concurrent;
using Assessment14.Models;

namespace Assessment14.Services;

public class NotificationQueue
{
    private readonly ConcurrentQueue<NotificationMessage> _queue = new();

    public void Enqueue(NotificationMessage msg) => _queue.Enqueue(msg);

    public bool TryDequeue(out NotificationMessage? msg) => _queue.TryDequeue(out msg);
}