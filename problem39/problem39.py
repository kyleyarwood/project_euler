from collections import defaultdict

def is_perfect_sqr(n):
	x = int(n**0.5)
	for i in range(x, x+2):
		if i**2 == n:
			return True, i
	return False, None

def most_solutions_less_than_n(n=1000):
	solutions = defaultdict(int)
	for i in range(n//2+1):
		for j in range(n//2+1):
			b,k = is_perfect_sqr(i**2+j**2)
			if b:
				solutions[i+j+k] += 1
	return max(solutions, key=lambda x: solutions[x])

result = most_solutions_less_than_n()
print(result)
