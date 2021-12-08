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

def findCompatibleSchedule(i):
    high = i-1
    low = -1
    start = intervals[i][0]
    while(high >= low):
        mid = int((high+low)/2)
        if(intervals[mid][1] <= start):
            if(intervals[mid+1][1] <= start):
                low = mid+1
            else:
                return mid
        else:
            high = mid-1
    return -1


def optimal(i):
    if i == 0:
        cache[i] = intervals[i][2]
        return

    j = findCompatibleSchedule(i)

    if j == -1:
        takeCurrent = intervals[i][2]
    else:
        takeCurrent = intervals[i][2] + cache[j]
    skipCurrent = cache[i-1]
    cache[i] = max(takeCurrent, skipCurrent)

for i in range(n):
    optimal(i)

print(cache[n-1])

