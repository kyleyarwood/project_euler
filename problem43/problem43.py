def k_digit_pandigital_multiples_of_n(k, n):
	result = []
	for x in range(0, 10**k, n):
		s = str(x).zfill(k)
		if len(set(s)) == len(s):
			result.append(s)
	return result

def sum_of_pandigital_property_numbers():
	endings = k_digit_pandigital_multiples_of_n(3, 17)
	for n in [13, 11, 7, 5, 3, 2]:
		new_endings = []
		for ending in endings:
			for x in map(str, range(10)):
				if x not in ending and int(x + ending[:2])%n==0:
					new_endings.append(x + ending)
		endings = new_endings
	res = 0
	for ending in endings:
		missing = 45-sum(map(int, list(ending)))
		res += int(str(missing)+ending)
	return res

result = sum_of_pandigital_property_numbers()
print(result)
