
line = input()

try:
	while(line != None):
		line = line.split(' ')
		x = int(line[0])
		y = int(line[1])
		if(x > y):
			print(x-y)
		else:
			print(y-x)
		line = input()
except:
	pass
