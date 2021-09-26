n = int(input())
intervals = []
cache = [0]*n

for i in range(n):
    line = input()
    arr = line.split(" ")
    start = int(arr[0])
    finish = int(arr[1])
    weight = int(arr[2])
    intervals.append((start, finish, weight))

intervals = sorted(intervals, key=lambda tup: tup[1])
    
def recursion(i):
    if i == 0:
        cache[i] = intervals[i][2]
        return

    j = i-1
    start = intervals[i][0]
    while(j > -1):
        if intervals[j][1] <= start:
            break
        j -= 1

    if j == -1:
        takeCurrent = intervals[i][2]
    else:
        takeCurrent = intervals[i][2] + cache[j]
    skipCurrent = cache[i-1]
    cache[i] = max(takeCurrent, skipCurrent)

for i in range(n):
    recursion(i)

print(cache[n-1])

