n = int(input())

list = []

for i in range(n):
    lineArr = input().split(" ")
    start = int(lineArr[0])
    end = int(lineArr[1])

    list.append((start, end))

sortedList = sorted(list, key=lambda tup: tup[1])

nonOverlapping = 0
latestStart = 0
latestEnd = 0

for i in range(n):
    start, end = sortedList[i]
    if(latestEnd <= start):
        nonOverlapping += 1
        latestStart = start
        latestEnd = end

print(nonOverlapping)
