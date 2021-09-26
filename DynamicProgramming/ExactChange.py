import sys

def cost(amount):
    if amount < min(bills):
        index_min = min(range(len(bills)), key=bills.__getitem__)
        cache[amount] = (bills[index_min], 1, {index_min})
        return


    lowestCost = sys.maxsize
    lowestTurns = sys.maxsize
    billsUsed = {-1}
    for i in range(len(bills)):
        bill = bills[i]
        extra = 0
        if(amount-bill > 0):
            while(bill > extra):
                (c, t, u) = cache[amount-bill+extra]
                if i in u:
                    extra += 1
                    continue
                break
            if(bill <= extra):
                continue

            usedBills = u.copy()
            usedBills.add(i)
            cost = bill + c
            turns = 1 + t
        else:
            cost = bill
            turns = 1
            usedBills = {i}
        if lowestCost > cost:
            lowestCost = cost
            lowestTurns = turns
            billsUsed = usedBills
        elif lowestCost == cost and lowestTurns > turns:
            lowestCost = cost
            lowestTurns = turns
            billsUsed = usedBills
    cache[amount] = (lowestCost, lowestTurns, billsUsed)

n = int(input())
results = []
for i in range(n):
    price = int(input())
    m = int(input())

    bills = []
    cache = [0]*(price+1)

    for j in range(m):
        bills.append(int(input()))

    for j in range(price+1):
        cost(j)
    results.append(cache[price])

for (c, t, u) in results:
    print(str(c) + " " + str(t))


