n = int(input())
for i in range(n):
    numberOfNumbers = int(input())

    list1 = map(int,input().split(" "))
    list2 = map(int,input().split(" "))

    sortedList1 = sorted(list1)
    sortedList2 = sorted(list2, reverse=True)

    amount = 0
    for i in range(len(sortedList1)):
        amount+=sortedList1[i]*sortedList2[i]
    print("Case #1: " + str(amount))
