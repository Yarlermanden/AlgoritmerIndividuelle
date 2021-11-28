from queue import PriorityQueue

n = int(input())
list = []

for i in range(n):
    arr = input().split(' ')
    list.append((int(arr[0]), int(arr[1])))

queue = PriorityQueue()
slots = [0]*n

list.sort(key=lambda x: x[0])
i1 = 0
for x in list:
    if(x[0]-1 < i1): #can't just schedule this
        if(queue.queue[0][0] < x[1]):
            removed = queue.get()
            slots[removed[1]] = x[1]
            queue.put((x[1], removed[1]))
    else:
        slots[i1] = x[1]
        queue.put((x[1], i1))
        i1 = i1+1

sum = 0
for x in queue.queue:
    sum += x[0]
print(sum)