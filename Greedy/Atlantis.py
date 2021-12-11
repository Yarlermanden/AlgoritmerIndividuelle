from queue import PriorityQueue

n = int(input())
stores = []
for i in range(n):
    arr = input().split(' ')
    t = int(arr[0])
    h = int(arr[1])
    stores.append((t, h))

stores.sort(key=lambda tup: tup[1])
queue = PriorityQueue()
count = 0
time = 0
for store in stores:
    if (store[1] - store[0]) < time:
        if count == 0:
            continue
        worstStore = queue.queue[0][1]
        diff = worstStore[0] - store[0]
        if diff > 0:
            queue.get()
            queue.put((-store[0], store))
            time -= diff
    else:
        count += 1
        time += store[0]
        queue.put((-store[0], store))
print(count)
