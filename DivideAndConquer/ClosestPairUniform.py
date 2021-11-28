
def divideAndConquer(points):
    points.sort(key=lambda p: p[0])

    return ((points[0], points[1]))

n = int(input())
while(n != 0):
    #do stuff
    points = []
    for i in range(n):
        point = input().split(' ')
        points.append((float(point[0]), float(point[1])))

    (p1, p2) = divideAndConquer(points)
    print(str(p1[0]) + " " + str(p1[1]) + " " + str(p2[0]) + " " + str(p2[1]))

    n = int(input())
