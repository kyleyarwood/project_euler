def palindromes_of_length_n(n):
	middle = list(map(str, range(10))) if n%2==1 else ['']
	outer = ['']
	if n != 1:
		outer = list(map(str, range(10**(n//2-1), 10**(n//2))))
	result = []
	for x in outer:
		for y in middle:	
			result.append(x + y + x[::-1])
	return result

def palindromes_of_length_under_n(n=7):
	result = []
	for i in range(1,n):
		result += palindromes_of_length_n(i)
	return result

def is_bin_of_n_palindrome(n):
	b = bin(n)[2:]
	return b==b[::-1]

result = 0
print(palindromes_of_length_under_n())
for x in map(int, palindromes_of_length_under_n()):
	if is_bin_of_n_palindrome(x):
		print(x, bin(x))
		result += x
print(result)
