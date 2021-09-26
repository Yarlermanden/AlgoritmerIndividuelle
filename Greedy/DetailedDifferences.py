n = int(input())

for i in range(n):
	s1 = input()
	s2 = input()

	print(s1)
	print(s2)
	for i in range(len(s1)):
		if(s1[i] == s2[i]):
			print('.', end='')
		else:
			print('*', end='')
	print()
	print()
